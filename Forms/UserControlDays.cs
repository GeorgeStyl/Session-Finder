using NodeJSClient.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NodeJSClient
{
    public partial class userControlDays : UserControl
    {
        // Static list that stores a reference to every userControlDays instance created
        public static List<userControlDays> AllInstances { get; } = new List<userControlDays>(); public int InstanceNumber { get; private set; }
        static int UCDCount = 0; // Static counter to keep track of the number of instances
        public Color originalBackColor { get; private set; }

        private Label topRightLabel;
        private int dayNum;


        public int UCDID { get; private set; }
        public bool highlightDay { get; set; } = false;

        public static CustomSwitch GlobalCustomSwitchInstance;

        public userControlDays(int dayNum, int id)
        {
            InitializeComponent();

            this.dayNum = dayNum;
            this.UCDID = id; // assign properly here
            this.originalBackColor = this.BackColor;

            AllInstances.Add(this);

            this.MouseEnter += DayControl_MouseEnter;
            this.MouseLeave += DayControl_MouseLeave;

            InitializeTopRightLabel();
            highlightControl();
        }




        private void UserControlDays_Load(object sender, EventArgs e)
        {

        }

        private void UserControlDays_Click(object sender, EventArgs e)
        {
            // This code runs when userControlDays is clicked
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

            // Use Anchor to stick to top-right corner
            topRightLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // Use Padding for internal spacing if needed
            topRightLabel.Padding = new Padding(3, 0, 3, 0);

            // Position relative to the UserControl size — offset a bit from right edge and top
            // Instead of fixed Location that may break if resized, handle layout dynamically:
            topRightLabel.Location = new Point(this.ClientSize.Width - topRightLabel.PreferredWidth - 5, 5);

            // Adjust label position when the UserControl resizes
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
            {
                this.BackColor = Color.Orange; // Highlight color
            }
            else
            {
                this.BackColor = Color.Teal;
            }
            originalBackColor = this.BackColor; // store original color
        }

        public int getCurrentInstanceNum()
        {
            return UCDCount;
        }

        //~~Events Handling~~
        public void HighlightRow()
        {
            int rowIndex = (this.UCDID - 1) / 7;
            int rowStart = rowIndex * 7 + 1;
            int rowEnd = rowStart + 6; // 7 blocks per row

            foreach (var instance in AllInstances)
            {
                if (instance.UCDID >= rowStart && instance.UCDID <= rowEnd)
                {
                    instance.BackColor = Color.LightBlue;
                }
                else
                {
                    instance.BackColor = instance.originalBackColor; // reset others
                }
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
                    control.HighlightRow();
                }
                else
                {
                    control.BackColor = Color.LightBlue;
                }

                Console.WriteLine($"Mouse entered day control with ID: {control.UCDID}");
            }
        }
        private void DayControl_MouseLeave(object sender, EventArgs e)
        {
            var control = sender as userControlDays;

            if (control != null)
            {
                if (NodeJSClient.Forms.CustomSwitch.GlobalCustomSwitchInstance != null &&
                    NodeJSClient.Forms.CustomSwitch.GlobalCustomSwitchInstance.changeOnHoverMode)
                {
                    // Reset the entire row's color to original
                    int rowIndex = (control.UCDID - 1) / 7;
                    int rowStart = rowIndex * 7 + 1;
                    int rowEnd = rowStart + 6;

                    foreach (var instance in userControlDays.AllInstances)
                    {
                        if (instance.UCDID >= rowStart && instance.UCDID <= rowEnd)
                        {
                            instance.BackColor = instance.originalBackColor;
                        }
                    }
                }
                else
                {
                    // Just reset this control's color as usual
                    control.BackColor = control.originalBackColor;
                }
            }
        }
    }
}
