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

        public Session()
        {
            InitializeComponent();

            NodeJSClient.Forms.CustomSwitch.GlobalCustomSwitchInstance = customSwitch1;

            Session_InitializeLayout(); // run layout code ONCE here
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
        }


        private void Session_Resize(object sender, EventArgs e)
        {

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
            Date.Font = new Font("Segoe UI", 20, FontStyle.Bold);  
            Date.TextAlign = ContentAlignment.MiddleLeft;       

            Date.Text = DateTime.Now.ToString("MMMM / yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }




        private void Date_Click(object sender, EventArgs e)
        {
            // Do nothing
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void customSwitch1_Load(object sender, EventArgs e)
        {
            // Do nothing
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void topFlowLayoutPanel4_Load(object sender, EventArgs e)
        {

        }
    }
}
