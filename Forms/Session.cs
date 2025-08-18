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

        private userControlDays _activeDayControl = null;


        public Session()
        {
            InitializeComponent();

            // Capture initial size
            InitialFormSize = this.Size;

            AdjustFormSizeAndPosition();
            this.Load += (s, e) => Session_InitializeLayout();


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

            dayContainer.Dock = DockStyle.None;
            dayContainer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dayContainer.FlowDirection = FlowDirection.LeftToRight;
            dayContainer.WrapContents = true;
            dayContainer.AutoScroll = true;
        }

        private void Session_Load(object sender, EventArgs e)
        {
            // Steps to initialize the session layout and controls

            // 1) TopFlayoutPanel is already set up in the designer

            // 2) Create the day controls
            displayDays();

            // 3) Now that dayContainer has controls, align the weekday labels
            InitializeWeekDaysLabels();

            // 4) Date label can be set any time
            InitilizeDateLabel();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        protected virtual void displayDays()
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
            DateTime dayDate;
            int _ID = 1;
            if (daysInMonth == 30) // For 30 days in month it will need 5 additional days to fill the last row
            {
                for (int day = 1; day <= daysInMonth + 5; day++)
                {
                    dayDate = DateTime.Now.AddDays(day - 1);
                    string formatted = dayDate.ToString("dddd, dd MMMM yyyy");  // Format: Weekday, Day Month Year
                    var dayControl = new userControlDays(day, _ID, DateTime.Now.AddDays(day - 1), formatted); // Pass the date for each next days control    
                                                                            
                    
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
                    dayDate = DateTime.Now.AddDays(day - 1);
                    string formatted = dayDate.ToString("dddd, dd MMMM yyyy");  // Format: Weekday, Day Month Year
                    var dayControl = new userControlDays(day, _ID, DateTime.Now.AddDays(day - 1), formatted); // Pass the date for each next days control    


                    Padding padding = new Padding(margin);
                    dayControl.Margin = padding;
                    dayControl.Size = new Size(controlWidth, controlHeight);

                    dayContainer.Controls.Add(dayControl);


                    _ID++;
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

        protected virtual void InitilizeDateLabel()
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
            //myPanel_Resize(sender, e);
        }

        //private void myPanel_Resize(object sender, EventArgs e)
        //{
        //    Panel panel = sender as Panel;

        //    // Vertically center multiple children
        //    foreach (Control child in panel.Controls)
        //    {
        //        if (child.Tag != null && child.Tag.ToString() == "centerVertically")
        //        {
        //            child.Top = (panel.Height - child.Height) / 2;
        //        }
        //    }
        //}
    }
}