using NodeJSClient.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NodeJSClient
{
    public partial class UserControlDays : UserControl
    {
        /****************************************************************************************************
        *                                      STATIC MEMBERS                                              *
        ****************************************************************************************************/

        // Static list to keep track of all generated instances of this control
        public static List<UserControlDays> AllInstances { get; } = new List<UserControlDays>();


        /****************************************************************************************************
         *                                      EVENTS                                                      *
         ****************************************************************************************************/

        // Custom event that parent form can subscribe to in order to detect clicks
        public event EventHandler DayClicked;


        /****************************************************************************************************
         *                                      PRIVATE FIELDS                                              *
         ****************************************************************************************************/
        private bool _isSelected = false;                  // Tracks persistent selection state
       

        private int dayNum;                                // Internal storage for day number
        private Label topRightLabel;                       // Label to show day number (top-right corner)
        private UserControlDays _activeDayControl = null;  // Reference to currently active day control
        private Session _parentForm;                       // Reference to parent form (Session)


        /****************************************************************************************************
         *                                      PUBLIC PROPERTIES                                           *
         ****************************************************************************************************/

        public int SeqIndex { get; private set; }         // Sequential index in container (for row calculations)
        public Color originalBackColor { get; private set; } = Color.Teal; // Default background color
        public bool isInCurrentMonth { get; set; } = true; // False if this control belongs to filler days


        /****************************************************************************************************
         *                                      PROTECTED PROPERTIES                                        *
         ****************************************************************************************************/

        public DateTime dateTime { get; private set; }   // Full DateTime value for this day
        public DateTime Date => dateTime;  // Read-only public property

        protected string toStringDay { get; private set; }  // String representation of the day
        protected string toStringMonth { get; private set; } // String representation of the month
        protected string toStringYear { get; private set; } // String representation of the year


        public UserControlDays(int dayNum, int seqIndex, DateTime dateTime, bool isInCurrentMonth, Session parent)
        {
            InitializeComponent();

            // Attach events
            this.MouseEnter += DayControl_MouseEnter;
            this.MouseLeave += DayControl_MouseLeave;
            this.Click += UserControlDays_Click; // attach internal click

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

            

            // Initialize the label
            InitializeTopRightLabel();

            this._parentForm = parent;
        }





        private void UserControlDays_Load(object sender, EventArgs e)
        {
            // Nothing needed here
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
         *                           ████████ DAY CONTROL HIGHLIGHTING & CLICK ████████                     *
         *                                                                                                  *
         * This region manages visual feedback and selection state for day cells in the calendar.           *
         *                                                                                                  *
         * Methods included:                                                                                *
         *                                                                                                  *
         * 1. highlightControl()                                                                            *
         *    - Highlights the current day in orange.                                                       *
         *    - All other days default to teal.                                                             *
         *    - Stores the original back color for reset logic.                                             *
         *                                                                                                  *
         * 2. HighlightRow()                                                                                *
         *    - Highlights an entire week row (7 consecutive days) in light blue.                           *
         *    - Only applies to days in the current month.                                                  *
         *    - Filler days (outside current month) remain gray.                                            *
         *                                                                                                  *
         * 3. DayControl_MouseEnter()                                                                       *
         *    - Handles temporary hover highlighting based on CurrentSelection mode:                        *
         *        • ROW       → highlights the entire week row.                                             *
         *        • MULTIPLE  → highlights only the hovered cell if not already selected.                   *
         *        • SINGLE    → highlights only the hovered cell (if in current month).                     *
         *                                                                                                  *
         * 4. DayControl_MouseLeave()                                                                       *
         *    - Resets highlighting when the mouse leaves:                                                  *
         *        • ROW       → resets the entire week row back to original colors.                         *
         *        • MULTIPLE  → restores violet for selected cells, original color for non-selected.        *
         *        • SINGLE    → resets only the hovered cell.                                               *
         *                                                                                                  *
         * 5. UserControlDays_Click()                                                                       *
         *    - Toggles **persistent selection** in MULTIPLE mode.                                          *
         *    - Selected cells are marked violet, unselected revert to originalBackColor.                   *
         *    - Raises the DayClicked event so the parent form can respond to clicks.                       *
         *                                                                                                  *
         * Notes:                                                                                           *
         *  - _parentForm.CurrentSelection decides which mode is active (SINGLE / ROW / MULTIPLE).          *
         *  - _isSelected tracks persistent state in MULTIPLE mode.                                         *
         *  - originalBackColor keeps each cell’s original color for restoring after hover.                 *
         *  - AllInstances holds references to every generated day cell for row-based operations.           *
         *  - This system ensures consistent, predictable visual feedback in all selection modes.           *
         *                                                                                                  *
         ****************************************************************************************************/

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
            var control = sender as UserControlDays;

            if (control == null) return;



            if (_parentForm.CurrentSelection == "ROW")
            {
                // Highlight the entire row using the helper method
                control.HighlightRow();
            }
            else if (_parentForm.CurrentSelection == "MULTIPLE")
            {
                if (!_isSelected) // only highlight temporarily if not selected
                    this.BackColor = Color.LightBlue;
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
            var control = sender as UserControlDays;

            if (control == null) return;


            if (_parentForm.CurrentSelection == "ROW")
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
            else if (_parentForm.CurrentSelection == "MULTIPLE")
            {
                // If it was selected, keep violet
                if (_isSelected)
                    this.BackColor = Color.Violet;
                else
                    this.BackColor = originalBackColor;
            }
            else
            {
                //Reset only this cell if in current month
                if (control.isInCurrentMonth)
                    control.BackColor = control.originalBackColor;
            }
        }

        protected void UserControlDays_Click(object sender, EventArgs e)
        {
            // Toggle selection
            _isSelected = !_isSelected;

            if (_parentForm.CurrentSelection == "MULTIPLE")
            {
                // Change background color based on selection
                this.BackColor = _isSelected ? Color.Violet : originalBackColor;

                // Update the multiple selection list
                if (_isSelected)
                {
                    // Add this item to the parent form's selection list if not already added
                    if (!_parentForm.SelectedItems.Contains(this))
                    {
                        _parentForm.SelectedItems.Add(this);
                    }
                }
                else
                {
                    // Remove this item from the parent form's selection list
                    if (_parentForm.SelectedItems.Contains(this))
                    {
                        _parentForm.SelectedItems.Remove(this);
                    }
                }
            }
        }

        public void Deselect()
        {
            _isSelected = false;
            this.BackColor = originalBackColor;

            if (_parentForm.SelectedItems.Contains(this))
                _parentForm.SelectedItems.Remove(this);
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
