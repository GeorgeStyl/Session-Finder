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
        // Static list to keep track of all instances
        public static List<userControlDays> AllInstances { get; } = new List<userControlDays>();
        public int InstanceNumber { get; private set; }
        public int DayNum { get; private set; }
        public Color originalBackColor { get; private set; } = Color.Teal;
        public bool isInCurrentMonth { get; set; } = true; // The children that dont belong to current month will be set to false by parent form
        private int dayNum;


        private Label topRightLabel;

        private userControlDays _activeDayControl = null;

        private Session _parentForm;


        public int SeqIndex { get; private set; }           // Sequential index in container (for row calculations)

        public bool highlightDay { get; set; } = false;
        

        protected DateTime dateTime { get; private set; }
        protected string toStringDay { get; private set; }
        protected string toStringMonth { get; private set; }
        protected string toStringYear { get; private set; }


        public userControlDays(int dayNum, int seqIndex, DateTime dateTime, bool isInCurrentMonth, Session parent)
        {
            InitializeComponent();

            // Store the parameters properly
            this.dayNum = dayNum;         // The day number to display
            this.SeqIndex = seqIndex;     // Sequential index for row calculations
            this.dateTime = dateTime;
            this.isInCurrentMonth = isInCurrentMonth;

            // Set the original back color
            if (!isInCurrentMonth)
                this.originalBackColor = Color.Gray;      // Filler day
            else if (dateTime.Date == DateTime.Now.Date)
                this.originalBackColor = Color.Orange;    // Today
            else
                this.originalBackColor = Color.Teal;      // Current month day

            this.BackColor = this.originalBackColor;

            // Add to static list
            AllInstances.Add(this);

            // Attach events
            this.MouseClick += UserControlDays_Click;
            this.MouseEnter += DayControl_MouseEnter;
            this.MouseLeave += DayControl_MouseLeave;

            // Initialize the label
            InitializeTopRightLabel();

            this._parentForm = parent;
        }





        private void UserControlDays_Load(object sender, EventArgs e)
        {
            // Nothing needed here
        }

        protected void UserControlDays_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"Clicked day: {this.toStringDay}/{this.toStringMonth}/{this.toStringYear}");
        }

        private void InitializeTopRightLabel()
        {
            topRightLabel = new Label();
            topRightLabel.Text = this.dayNum.ToString();      
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


        /****************************************************************************************************
         *                                                                                                  *
         *                         ████████ DAY CONTROL HIGHLIGHTING ████████                               *
         *                                                                                                  *
         * This set of methods handles visual feedback for day cells in the calendar user control.          *
         *                                                                                                  *
         * Methods included:                                                                                *
         *                                                                                                  *
         * 1. highlightControl()                                                                            *
         *    - Highlights the current day in orange.                                                       *
         *    - All other days default to teal.                                                             *
         *    - Stores the original back color for reset.                                                   *
         *                                                                                                  *
         * 2. HighlightRow()                                                                                *
         *    - Highlights an entire week row (7 days) in light blue.                                       *
         *    - Only affects days in the current month.                                                     *
         *    - Filler days (previous/next month) remain gray.                                              *
         *                                                                                                  *
         * 3. DayControl_MouseEnter()                                                                       *
         *    - On hover, highlights either:                                                                *
         *        • A single cell (if CurrentSelection != "MULTIPLE")                                       *
         *        • The entire row (if CurrentSelection == "MULTIPLE")                                      *
         *                                                                                                  *
         * 4. DayControl_MouseLeave()                                                                       *
         *    - Resets the cell(s) back to original color:                                                  *
         *        • Single cell if CurrentSelection != "MULTIPLE"                                           *
         *        • Entire row if CurrentSelection == "MULTIPLE"                                            *
         *                                                                                                  *
         * Notes:                                                                                           *
         *  - _parentForm.CurrentSelection determines single/multiple selection mode.                       *
         *  - AllInstances is the collection of all day controls.                                           *
         *  - originalBackColor preserves the original state to restore after hover.                        *
         *  - This logic ensures consistent highlighting behavior for both single and multiple selection.   *
         *                                                                                                  *
         ****************************************************************************************************/



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
            int rowIndex = (this.SeqIndex - 1) / 7; // SeqIndex = sequential index in container
            int rowStart = rowIndex * 7 + 1;
            int rowEnd = rowStart + 6;

            foreach (var instance in AllInstances)
            {
                if (instance == null)
                {
                    continue;
                }

                else if (instance.isInCurrentMonth) // Only highlight if in current month
                {
                    if (instance.SeqIndex >= rowStart && instance.SeqIndex <= rowEnd)
                    {
                        // Temporarily highlight visually
                        instance.BackColor = Color.LightBlue;
                    }
                }
                else
                {
                    // Filler days remain gray
                    instance.BackColor = Color.Gray;
                }

            }
        }

        private void DayControl_MouseEnter(object sender, EventArgs e)
        {
            var control = sender as userControlDays;

            if (control == null) return;



            if (_parentForm.CurrentSelection == "MULTIPLE")
            {
                // Highlight the entire row using the helper method
                control.HighlightRow();
            }
            else
            {
                // Highlight only the hovered cell if it is in the current month
                if (control.isInCurrentMonth)
                    control.BackColor = Color.LightBlue;
            }
        }


        private void DayControl_MouseLeave(object sender, EventArgs e)
        {
            var control = sender as userControlDays;
            if (control == null) return;


            if (_parentForm.CurrentSelection == "MULTIPLE")
            {
                // Reset the entire row
                int rowIndex = (control.SeqIndex - 1) / 7;
                int rowStart = rowIndex * 7 + 1;
                int rowEnd = rowStart + 6;

                foreach (var instance in AllInstances)
                {
                    if (instance.SeqIndex >= rowStart && instance.SeqIndex <= rowEnd)
                        instance.BackColor = instance.originalBackColor; // gray for filler, teal/orange for month/today

                }
            }
            else
            {
                // Reset only this cell if in current month
                if (control.isInCurrentMonth)
                    control.BackColor = control.originalBackColor;
            }
        }












        //~~~~~~~~~~~~~~~~~~~~Data~~~~~~~~~~~~~~~~~~~~  

        private void SetFormattedDateTime()
        {
            this.dateTime = DateTime.Now.AddDays(dayNum - 1);
            this.toStringDay = dateTime.ToString("dddd");
            this.toStringMonth = dateTime.ToString("MMMM");
            this.toStringYear = dateTime.ToString("yyyy");
        }
    }
}
