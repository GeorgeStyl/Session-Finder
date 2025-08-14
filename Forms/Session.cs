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

        private const int MinFormWidth = 400;
        private const int VerticalOffset = 5;  // Move window a bit up to avoid taskbar overlap

        private userControlDays _activeDayControl = null;


        public Session()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual; // manual control over position


            // Adjust size and initial position on load
            this.Load += (s, e) => AdjustFormSizeAndPosition();


           

            Session_InitializeLayout(); // run layout code ONCE here
        }



        private void AdjustFormSizeAndPosition()
        {
            // Get the screen where the form is currently displayed
            var screen = Screen.FromControl(this);

            // Set minimum width
            this.MinimumSize = new Size(MinFormWidth, this.MinimumSize.Height);

            // Lock height to the screen's working area
            int maxHeight = screen.WorkingArea.Height;
            this.Height = maxHeight;

            // Keep horizontal position centered, vertical slightly above center
            int newX = screen.WorkingArea.X + (screen.WorkingArea.Width - this.Width) / 2;
            int newY = screen.WorkingArea.Y + (screen.WorkingArea.Height - this.Height) / 2 - VerticalOffset;

            this.Location = new Point(newX, newY);
        }

        public Session(DayInfoService service) : this()
        {
            _dayInfoService = service;
        }

        private void Session_InitializeLayout()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            int totalHeight = this.ClientSize.Height;
            int topSpacing = totalHeight / 4;
            int containerHeight = (totalHeight * 3) / 4;

            dayContainer.Dock = DockStyle.None;
            dayContainer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dayContainer.FlowDirection = FlowDirection.LeftToRight;
            dayContainer.WrapContents = true;
            dayContainer.AutoScroll = true;
        }

        private void Session_Load(object sender, EventArgs e)
        {
            // Steps to initialize the session layout and controls

            // 1️) Create the day controls
            displayDays();

            // 2️) Now that dayContainer has controls, align the weekday labels
            InitializeWeekDaysLabels();

            // 3️) Date label can be set any time
            InitilizeDateLabel();

            // 4️) Initialize the top flow layout panel
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void displayDays()
        {
            dayContainer.Controls.Clear();

            dayContainer.WrapContents = true;
            dayContainer.FlowDirection = FlowDirection.LeftToRight;

            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            int controlCountPerRow = 7;
            int margin = 5;
            int totalSpacing = controlCountPerRow * margin * 2;
            int controlWidth = (dayContainer.Width - totalSpacing) / controlCountPerRow;
            int controlHeight = 100;


            //~~Create userControlDays~~
            int _ID = 1;
            if (daysInMonth == 30) // For 30 days in month it will need 5 additional days to fill the last row
            {
                for (int day = 1; day <= daysInMonth + 5; day++)
                {
                    var dayControl = new userControlDays(day, _ID);
                    Padding padding = new Padding(margin);
                    dayControl.Margin = padding;
                    dayControl.Size = new Size(controlWidth, controlHeight);

                    dayContainer.Controls.Add(dayControl);
                    _ID++;
                }
            }
            else // For 31 days in month it will need 4 additional days to fill the last row
                for (int day = 1; day <= daysInMonth + 4; day++)
                {
                    var dayControl = new userControlDays(day, _ID);
                    Padding padding = new Padding(margin);
                    dayControl.Margin = padding;
                    dayControl.Size = new Size(controlWidth, controlHeight);

                    dayContainer.Controls.Add(dayControl);
                    _ID++;
                }
        }

        private void InitializeWeekDaysLabels()
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

        private void InitilizeDateLabel()
        {
            // Ignore Click and hover event
            Date.Enabled = true;
            Date.Cursor = Cursors.Default; // remove hand cursor if set

            //Date.Size = new Size(200, 40);    
            Date.Font = new Font("Segoe UI", 19, FontStyle.Bold);
            Date.TextAlign = ContentAlignment.MiddleLeft;

            Date.Text = DateTime.Now.ToString("MMMM / yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void customSwitch1_Load(object sender, EventArgs e)
        {
            // Do nothing
        }

        private void Date_Click_1(object sender, EventArgs e)
        {

        }

        private void TopFlayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void TopPanel_Paint(object sender, PaintEventArgs e)
        {
            myPanel_Resize(sender, e);
        }

        private void myPanel_Resize(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;

            // Vertically center multiple children
            foreach (Control child in panel.Controls)
            {
                if (child.Tag != null && child.Tag.ToString() == "centerVertically")
                {
                    child.Top = (panel.Height - child.Height) / 2;
                }
            }
        }

        private void customSwitch2_Load(object sender, EventArgs e)
        {

        }
    }
}