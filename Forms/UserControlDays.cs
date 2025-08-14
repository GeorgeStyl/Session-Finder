using NodeJSClient.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NodeJSClient
{
    public partial class userControlDays : UserControl
    {
        public static List<userControlDays> AllInstances { get; } = new List<userControlDays>();
        public int InstanceNumber { get; private set; }
        static int UCDCount = 0;
        public Color originalBackColor { get; private set; }

        private Label topRightLabel;
        private int dayNum;

        private userControlDays _activeDayControl = null;
        private CustomSwitch _customSwitch;

        public int UCDID { get; private set; }
        public bool highlightDay { get; set; } = false;
        public static CustomSwitch GlobalCustomSwitchInstance;

        public userControlDays(int dayNum, int id)
        {
            InitializeComponent();
            this.dayNum = dayNum;
            this.UCDID = id;
            this.originalBackColor = this.BackColor;

            AllInstances.Add(this);

            this.MouseEnter += DayControl_MouseEnter;
            this.MouseLeave += DayControl_MouseLeave;

            InitializeTopRightLabel();
            highlightControl();
        }

        private void UserControlDays_Load(object sender, EventArgs e) { }

        private void UserControlDays_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Day clicked! for id:{this.UCDID}");
        }

        private void InitializeTopRightLabel()
        {
            topRightLabel = new Label();
            topRightLabel.Text = getlblNnum();
            topRightLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            topRightLabel.ForeColor = Color.Red;
            topRightLabel.BackColor = Color.Transparent;
            topRightLabel.AutoSize = true;
            topRightLabel.TextAlign = ContentAlignment.TopRight;

            topRightLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            topRightLabel.Padding = new Padding(3, 0, 3, 0);
            topRightLabel.Location = new Point(this.ClientSize.Width - topRightLabel.PreferredWidth - 5, 5);

            this.Resize += (s, e) =>
            {
                topRightLabel.Location = new Point(this.ClientSize.Width - topRightLabel.PreferredWidth - 5, 5);
            };

            this.Controls.Add(topRightLabel);
            topRightLabel.BringToFront();
        }

        private string getlblNnum()
        {
            string result = $"{(dayNum <= 31 ? dayNum : dayNum - 31)}";
            return result;
        }

        private void highlightControl()
        {
            if (this.dayNum == DateTime.Now.Day)
                this.BackColor = Color.Orange;
            else
                this.BackColor = Color.Teal;

            originalBackColor = this.BackColor;
        }

        public void HighlightRow()
        {
            int rowIndex = (this.UCDID - 1) / 7;
            int rowStart = rowIndex * 7 + 1;
            int rowEnd = rowStart + 6;

            foreach (var instance in AllInstances)
            {
                if (instance.UCDID >= rowStart && instance.UCDID <= rowEnd)
                    instance.BackColor = Color.LightBlue;
                else
                    instance.BackColor = instance.originalBackColor;
            }
        }

        private void DayControl_MouseEnter(object sender, EventArgs e)
        {
            var control = sender as userControlDays;

           

            if (control != null)
            {
                if (NodeJSClient.Forms.CustomSwitch.GlobalCustomSwitchInstance != null &&
                    NodeJSClient.Forms.CustomSwitch.GlobalCustomSwitchInstance.changeOnHoverMode)
                {
                    Console.WriteLine($"Mouse entered control with ID: {control.UCDID}");
                    control.HighlightRow();
                }
                else
                {
                    control.BackColor = Color.LightBlue;
                }
            }
        }

        private void DayControl_MouseLeave(object sender, EventArgs e)
        {
            var control = sender as userControlDays;

            Console.WriteLine($"Global instance is null? {GlobalCustomSwitchInstance == null}, changeOnHoverMode: {GlobalCustomSwitchInstance?.changeOnHoverMode}");
            if (control != null)
            {
                if (NodeJSClient.Forms.CustomSwitch.GlobalCustomSwitchInstance != null &&
                    NodeJSClient.Forms.CustomSwitch.GlobalCustomSwitchInstance.changeOnHoverMode)
                {
                    int rowIndex = (control.UCDID - 1) / 7;
                    int rowStart = rowIndex * 7 + 1;
                    int rowEnd = rowStart + 6;

                    foreach (var instance in userControlDays.AllInstances)
                    {
                        if (instance.UCDID >= rowStart && instance.UCDID <= rowEnd)
                            instance.BackColor = instance.originalBackColor;
                    }
                }
                else
                {
                    control.BackColor = control.originalBackColor;
                }
            }
        }
    }
}
