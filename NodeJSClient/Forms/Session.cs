using NodeJSClient;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace NodeJSClient.Forms
{
    public partial class Session : Form
    {
        // =========================
        // Constants
        // =========================
        protected const int MinFormWidth = 400;
        protected const int VerticalOffset = 5;  // Move window slightly up to avoid taskbar overlap

        // =========================
        // Readonly / Services
        // =========================
        private readonly DayInfoService _dayInfoService;
        protected readonly Size InitialFormSize;

        // =========================
        // State / Fields
        // =========================
        protected DateTime changeDateLbl;

        protected DateTime currentDate = new DateTime(
                                             DateTime.Now.Year,   // current year
                                             DateTime.Now.Month,  // current month
                                             DateTime.Now.Day     // current day
                                           );

        private ComboBox DateComboBox;
        private userControlDays _activeDayControl = null;

        public event EventHandler previousMonthBtnEvent;
        private bool _previousMonthBtnFired = false;

        public event EventHandler nextMonthBtnEvent;
        private bool _nextMonthBtnEvent = false;

        // =========================
        // Properties
        // =========================
        public string CurrentSelection
        {
            get
            {
                if (SingleSelection.Checked) return "SINGLE";
                if (RowSelection.Checked) return "ROW";
                return "MULTIPLE";  // fallback, guaranteed at least one checked
            }
        }

        // =========================
        // Constructor
        // =========================
        public Session()
        {
            InitializeComponent();

            // Capture initial size
            InitialFormSize = this.Size;

            // Adjust form size and layout
            AdjustFormSizeAndPosition();
            this.Load += (s, e) => Session_InitializeLayout();

            // Load events for checkboxes
            this.Load += SingleSelection_Load;
            this.Load += RowSelection_Load;
            this.Load += MultipleSelection_Load;

            SingleSelection.CheckedChanged += Selection_CheckedChanged;
            RowSelection.CheckedChanged += Selection_CheckedChanged;
            MultipleSelection.CheckedChanged += Selection_CheckedChanged;
            MultipleSelection.CheckedChanged += MultipleSelection_CheckedChanged;

            // Subscribe to month navigation events
            this.previousMonthBtnEvent += (s, e) => _previousMonthBtnFired = true;
            this.nextMonthBtnEvent += (s, e) => _nextMonthBtnEvent = true;

            previousMonthBtnEvent += OnPreviousMonthBtnEvent;
            nextMonthBtnEvent += OnNextMonthBtnEvent;

            // Debug: list all controls
            foreach (Control c in this.Controls)
            {
                Console.WriteLine(c.Name + " - Visible: " + c.Visible);
            }
        }


        // =========================
        // Constructors
        // =========================
        public Session(DayInfoService service) : this()
        {
            _dayInfoService = service;
        }

        // =========================
        // Form Layout / Initialization
        // =========================
        protected virtual void AdjustFormSizeAndPosition()
        {
            var screen = Screen.FromControl(this);

            // Lock size using captured initial size
            this.MinimumSize = InitialFormSize;
            this.MaximumSize = InitialFormSize;
            this.Size = InitialFormSize;

            int newX = screen.WorkingArea.X + (screen.WorkingArea.Width - this.Width) / 2;
            int newY = screen.WorkingArea.Y + (screen.WorkingArea.Height - this.Height) / 2 - VerticalOffset;
            this.Location = new Point(newX, newY);
        }

        protected virtual void Session_InitializeLayout()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            foreach (Control ctrl in TopPanel.Controls)
            {
                ctrl.Margin = new Padding();
                ctrl.Padding = new Padding();
            }

            dayContainer.Dock = DockStyle.None;
            dayContainer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dayContainer.FlowDirection = FlowDirection.LeftToRight;
            dayContainer.WrapContents = true;
            dayContainer.AutoScroll = true;
        }

        // =========================
        // Load / Paint Events
        // =========================
        private void Session_Load(object sender, EventArgs e)
        {
            // Initialize day controls and labels
            displayDays();
            InitializeWeekDaysLabels();
            //DateComboBox_Initialize();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e) { }

        private void WeekDaysFlowLayout_Paint(object sender, PaintEventArgs e) { }

        private void TopPanel_Paint(object sender, PaintEventArgs e) { }

        // =========================
        // Day Display Methods
        // =========================
        protected virtual void displayDays()
        {
            dayContainer.Controls.Clear();

            int year = currentDate.Year;
            int month = currentDate.Month;
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime firstDay = new DateTime(year, month, 1);

            // Monday = 0 … Sunday = 6
            int startIndex = (firstDay.DayOfWeek == DayOfWeek.Sunday) ? 6 : (int)firstDay.DayOfWeek - 1;
            int totalCells = daysInMonth + startIndex;
            if (totalCells % 7 != 0)
                totalCells += 7 - (totalCells % 7);

            int margin = 5;
            int controlCount = 7;
            int controlWidth = (dayContainer.Width - (controlCount * margin * 2)) / controlCount;
            int controlHeight = 100;

            int dayNumber = 1;   // For current month
            int _seqIndex = 1;   // Sequential counter for all cells (including filler)

            for (int i = 0; i < totalCells; i++)
            {
                DateTime cellDate;
                bool isCurrentMonth = false;
                int displayDay;

                if (i >= startIndex && dayNumber <= daysInMonth)
                {
                    cellDate = new DateTime(year, month, dayNumber);
                    isCurrentMonth = true;
                    displayDay = dayNumber;
                    dayNumber++;
                }
                else if (i < startIndex)
                {
                    DateTime prevMonth = firstDay.AddMonths(-1);
                    int prevMonthDays = DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
                    int day = prevMonthDays - (startIndex - i - 1);
                    cellDate = new DateTime(prevMonth.Year, prevMonth.Month, day);
                    displayDay = day;
                }
                else
                {
                    int day = i - startIndex - daysInMonth + 1;
                    DateTime nextMonth = firstDay.AddMonths(1);
                    cellDate = new DateTime(nextMonth.Year, nextMonth.Month, day);
                    displayDay = day;
                }

                var dayControl = new userControlDays(
                    displayDay,
                    _seqIndex,
                    cellDate,
                    isCurrentMonth,
                    this
                );

                dayControl.Margin = new Padding(margin);
                dayControl.Size = new Size(controlWidth, controlHeight);

                dayContainer.Controls.Add(dayControl);
                _seqIndex++;
            }
        }

        protected void InitializeWeekDaysLabels()
        {
            string[] weekdays = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            var firstDay = dayContainer.Controls.OfType<userControlDays>().FirstOrDefault();

            int labelWidth = firstDay != null ? firstDay.Width : 110;
            int labelHeight = 31;
            Padding labelMargin = firstDay != null ? firstDay.Margin : new Padding(5);

            WeekDaysFlowLayout.Controls.Clear();

            for (int i = 0; i < weekdays.Length; i++)
            {
                System.Windows.Forms.Label lbl = new System.Windows.Forms.Label(); // fully qualified
                lbl.Text = weekdays[i];
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Size = new Size(labelWidth, labelHeight);
                lbl.Margin = new Padding(
                                      labelMargin.Left,
                                      labelMargin.Top,
                                      i < weekdays.Length - 1 ? labelMargin.Right : 0,
                                      labelMargin.Bottom);
                lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lbl.BackColor = Color.LightGray;
                lbl.ForeColor = Color.Black;

                WeekDaysFlowLayout.Controls.Add(lbl);
            }
        }

        // =========================
        // Month Navigation
        // =========================
        private void PreviousMonthBtn_Click(object sender, EventArgs e) => previousMonthBtnEvent?.Invoke(this, EventArgs.Empty);
        private void NextMonthBtn_Click(object sender, EventArgs e) => nextMonthBtnEvent?.Invoke(this, EventArgs.Empty);

        private void OnPreviousMonthBtnEvent(object sender, EventArgs e)
        {
            currentDate = currentDate.AddMonths(-1);
            //DateLbl.Text = currentDate.ToString("MMMM / yyyy", CultureInfo.InvariantCulture);
            displayDays();
            Console.WriteLine($"Current Month: {currentDate.Month}, Year: {currentDate.Year}");
        }

        private void OnNextMonthBtnEvent(object sender, EventArgs e)
        {
            currentDate = currentDate.AddMonths(1);
            //DateLbl.Text = currentDate.ToString("MMMM / yyyy", CultureInfo.InvariantCulture);
            displayDays();
            Console.WriteLine($"Current Month: {currentDate.Month}, Year: {currentDate.Year}");
        }


        /****************************************************************************************************
         *                                                                                                  *
         *                     ████████ CHECKBOX SELECTION & BUTTON HANDLING ████████                       *
         *                                                                                                  *
         * This set of methods manages the selection mode checkboxes and the "UpdateChanges" button.        *
         *                                                                                                  *
         * Key behaviors:                                                                                   *
         *                                                                                                  *
         * 1. SingleSelection_Load / RowSelection_Load / MultipleSelection_Load                             *
         *    - Initializes the checkbox text and default checked state.                                    *
         *    - Only SingleSelection is checked by default.                                                 *
         *                                                                                                  *
         * 2. Selection_CheckedChanged                                                                      *
         *    - Single handler for all three checkboxes (Single, Row, Multiple).                            *
         *    - Ensures exactly one checkbox is always checked:                                             *
         *        • Checking one automatically unchecks the others.                                         *
         *        • Attempting to uncheck the last remaining checked box restores it immediately.           *
         *    - Updates the "UpdateChanges" button state:                                                   *
         *        • Enabled only if MultipleSelection is checked.                                           *
         *                                                                                                  *
         * 3. MultipleSelection_CheckedChanged                                                              *
         *    - Optional separate handler (if used) to enable/disable the button based on MultipleSelection.*
         *                                                                                                  *
         * 4. UpdateChanges_Click                                                                           *
         *    - Handles click event for the button.                                                         *
         *    - Ensures the button state reflects MultipleSelection checkbox.                               *
         *                                                                                                  *
         * Notes:                                                                                           *
         *  - All checkbox events can share the Selection_CheckedChanged handler to simplify logic.         *
         *  - UpdateChanges button is dynamically enabled only for Multiple selection mode.                 *
         *  - This setup avoids dynamic unsubscribing errors and keeps UI state consistent.                 *
         *                                                                                                  *
         ****************************************************************************************************/


        private void SingleSelection_Load(object sender, EventArgs e)
        {
            SingleSelection.Text = "Single selection";
            SingleSelection.Checked = true;   
        }

        private void RowSelection_Load(object sender, EventArgs e)
        {
            RowSelection.Text = "All row selection";
            RowSelection.Checked = false;
        }


        private void MultipleSelection_Load(object sender, EventArgs e)
        {
            MultipleSelection.Text = "Multiple selection";
            MultipleSelection.Checked = false;
        }


        // Attach this single handler to all three checkboxes 
        private void Selection_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox changed = sender as CheckBox;

            if (changed.Checked)
            {
                foreach (CheckBox cb in new[] { SingleSelection, RowSelection, MultipleSelection })
                {
                    if (cb != changed)
                        cb.Checked = false;
                }
            }
            else
            {
                if (!SingleSelection.Checked && !RowSelection.Checked && !MultipleSelection.Checked)
                    changed.Checked = true; // restore
            }

            // Update button state
            UpdateChanges.Enabled = MultipleSelection.Checked;
        }


        private void MultipleSelection_CheckedChanged(object sender, EventArgs e)
        {
            // Enable or disable the UpdateChanges button based on the checkbox state
            UpdateChanges.Enabled = MultipleSelection.Checked;
        }


        private void UpdateChanges_Click(object sender, EventArgs e)
        {
            if (MultipleSelection.Checked == true)
            {
                UpdateChanges.Enabled = true;
            }
            else UpdateChanges.Enabled = false;
        }
    }
}