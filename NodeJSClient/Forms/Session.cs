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
        private readonly DayInfoService _dayInfoService;


        protected readonly Size InitialFormSize;


        protected const int MinFormWidth = 400;
        protected const int VerticalOffset = 5;  // Move window a bit up to avoid taskbar overlap


        protected DateTime changeDateLbl;


        private ComboBox DateComboBox;




        protected DateTime currentDate = new DateTime(
             DateTime.Now.Year,   // current year
             DateTime.Now.Month,  // current month
             DateTime.Now.Day     // current day
        );

        private userControlDays _activeDayControl = null;

        public event EventHandler previousMonthBtnEvent;
        private bool _previousMonthBtnFired = false;
        public event EventHandler nextMonthBtnEvent;
        private bool _nextMonthBtnEvent = false;

        public Session()
        {
            InitializeComponent();

            // Capture initial size
            InitialFormSize = this.Size;

            AdjustFormSizeAndPosition();
            this.Load += (s, e) => Session_InitializeLayout();


            // ~~Events~~

            // Subscribe to the Click event
            this.previousMonthBtnEvent += (s, e) => _previousMonthBtnFired = true;
            this.nextMonthBtnEvent += (s, e) => _nextMonthBtnEvent = true;

            // Subscribe the methods to corresponding events
            previousMonthBtnEvent += OnPreviousMonthBtnEvent;
            nextMonthBtnEvent += OnNextMonthBtnEvent;


        



            // Debug: list all controls
            foreach (Control c in this.Controls)
            {
                Console.WriteLine(c.Name + " - Visible: " + c.Visible);
            }

        }

        public Session(DayInfoService service) : this()
        {
            _dayInfoService = service;
        }


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

        //protected void DateComboBox_Initialize()
        //{
        //    // Initialize the DateComboBox with all 12 months of the current year
        //    DateComboBox.Items.Clear();
        //    int currentYear = DateTime.Now.Year;

        //    for (int i = 1; i <= 12; i++)
        //    {
        //        // Use i for month
        //        DateTime monthDate = new DateTime(currentYear, i, 1);
        //        DateComboBox.Items.Add(monthDate.ToString("MMMM yyyy"));
        //    }

        //    // Set to current month by default
        //    DateComboBox.SelectedIndex = DateTime.Now.Month - 1;
        //}


        private void Session_Load(object sender, EventArgs e)
        {
            // Steps to initialize the session layout and controls

            // 1) TopFlayoutPanel is already set up in the designer

            // 2) Create the day controls
            displayDays();

            // 3) Now that dayContainer has controls, align the weekday labels
            InitializeWeekDaysLabels();

            // 4) Date ComboBox
            //DateComboBox_Initialize();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        // displayDays can be called for different months / years too
        protected virtual void displayDays()
        {
            dayContainer.Controls.Clear();

            int year = currentDate.Year;
            int month = currentDate.Month;

            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime firstDayOfMonth = new DateTime(year, month, 1);

            // Monday = 0 … Sunday = 6
            int startIndex = (firstDayOfMonth.DayOfWeek == DayOfWeek.Sunday)
                                ? 6
                                : (int)firstDayOfMonth.DayOfWeek - 1;

            int totalCells = daysInMonth + startIndex;
            if (totalCells % 7 != 0)
                totalCells += 7 - (totalCells % 7);

            int margin = 5;
            int controlCountPerRow = 7;
            int controlWidth = (dayContainer.Width - (controlCountPerRow * margin * 2)) / controlCountPerRow;
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
                    // Current month
                    cellDate = new DateTime(year, month, dayNumber);
                    isCurrentMonth = true;
                    displayDay = dayNumber;
                    dayNumber++;
                }
                else if (i < startIndex)
                {
                    // Previous month filler
                    DateTime prevMonth = firstDayOfMonth.AddMonths(-1);
                    int prevMonthDays = DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
                    int day = prevMonthDays - (startIndex - i - 1);
                    cellDate = new DateTime(prevMonth.Year, prevMonth.Month, day);
                    displayDay = day;
                }
                else
                {
                    // Next month filler
                    int day = i - startIndex - daysInMonth + 1;
                    DateTime nextMonth = firstDayOfMonth.AddMonths(1);
                    cellDate = new DateTime(nextMonth.Year, nextMonth.Month, day);
                    displayDay = day;
                }

                var dayControl = new userControlDays(
                    displayDay,   // This must be the number shown in the top-right label
                    _seqIndex,    // Sequential index in the container
                    cellDate,
                    isCurrentMonth
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

            // Get the first userControlDays instance to grab its size and margin
            var firstDayControl = dayContainer.Controls.OfType<userControlDays>().FirstOrDefault();

            // If none found, fallback to defaults
            int labelWidth = 110;
            int labelHeight = 31;
            Padding labelMargin = new Padding(5);

            if (firstDayControl != null)
            {
                labelWidth = firstDayControl.Width;
                labelMargin = firstDayControl.Margin;
            }

            WeekDaysFlowLayout.Controls.Clear();

            for (int i = 0; i < weekdays.Length; i++)
            {
                System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
                lbl.Text = weekdays[i];
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Size = new Size(labelWidth, labelHeight);

                // Set margin similarly but avoid extra margin on last label's right side
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


        private void WeekDaysFlowLayout_Paint(object sender, PaintEventArgs e)
        {

        }

        //protected virtual void InitilizeDateLabel()
        //{
        //    // Ignore Click and hover event
        //    DateLbl.Enabled = true;
        //    DateLbl.Cursor = Cursors.Default; // remove hand cursor if set

        //    //Date.Size = new Size(200, 40);    
        //    DateLbl.Font = new Font("Segoe UI", 19, FontStyle.Bold);
        //    DateLbl.TextAlign = ContentAlignment.MiddleLeft;

        //    //DateLbl.Text = currentDate.ToString("MMMM / yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //}

        private void customSwitch1_Load(object sender, EventArgs e)
        {
            // Do nothing
        }

        private void TopFlayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TopPanel_Paint(object sender, PaintEventArgs e)
        {
            //myPanel_Resize(sender, e);
        }

        private void PreviousMonthBtn_Click(object sender, EventArgs e)
        {
            // Fire the event when button is clicked
            previousMonthBtnEvent?.Invoke(this, EventArgs.Empty);
        }

        private void NextMonthBtn_Click(object sender, EventArgs e)
        {
            // Fire the event when button is clicked
            nextMonthBtnEvent?.Invoke(this, EventArgs.Empty);
        }

        private void OnPreviousMonthBtnEvent(object sender, EventArgs e)
        {
            // Subtract one month
            currentDate = currentDate.AddMonths(-1);

            // Update the label
            //DateLbl.Text = currentDate.ToString("MMMM / yyyy", System.Globalization.CultureInfo.InvariantCulture);

            // Optional: update your day display
            displayDays();

            Console.WriteLine($"Current Month: {currentDate.Month}, Year: {currentDate.Year}");
        }


        private void OnNextMonthBtnEvent(object sender, EventArgs e)
        {
            // Add one month
            currentDate = currentDate.AddMonths(1);

            // Update the label
            //DateLbl.Text = currentDate.ToString("MMMM / yyyy", System.Globalization.CultureInfo.InvariantCulture);

            // Optional: update your day display
            displayDays();

            Console.WriteLine($"Current Month: {currentDate.Month}, Year: {currentDate.Year}");


        }

        private void DateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}