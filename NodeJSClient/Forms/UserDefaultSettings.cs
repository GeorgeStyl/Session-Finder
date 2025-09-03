using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NodeJSClient.Forms
{
    public partial class UserDefaultSettings : Session
    {

        public UserDefaultSettings()
        {
            InitializeComponent();

            // Override child size with parent’s initial size
            this.Size = InitialFormSize;
            AdjustFormSizeAndPosition();

            Session_InitializeLayout();
        }

        protected override void AdjustFormSizeAndPosition()
        {
            base.AdjustFormSizeAndPosition(); // use parent logic
        }

        protected override void Session_InitializeLayout()
        {
            base.Session_InitializeLayout(); // keep parent defaults
        }

        //protected override void displayDays()
        //{
        //    dayContainer.Controls.Clear();

        //    dayContainer.WrapContents = true;
        //    dayContainer.FlowDirection = FlowDirection.LeftToRight;

        //    int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        //    int controlCountPerRow = 7;
        //    int margin = 5;
        //    int totalSpacing = controlCountPerRow * margin * 2;
        //    int controlWidth = (dayContainer.Width - totalSpacing) / controlCountPerRow;
        //    int controlHeight = 100;


        //    //~~Create userControlDays~~
        //    DateTime dayDate;
        //    int _ID = 1;
        //    if (daysInMonth == 30) // For 30 days in month it will need 5 additional days to fill the last row
        //    {
        //        for (int day = 1; day <= daysInMonth; day++)
        //        {
        //            dayDate = DateTime.Now.AddDays(day - 1);
        //            string formatted = dayDate.ToString("dddd, dd MMMM yyyy");  // Format: Weekday, Day Month Year
        //            var dayControl = new userControlDays(day, _ID, DateTime.Now.AddDays(day - 1), formatted); // Pass the date for each next days control 


        //            Padding padding = new Padding(margin);
        //            dayControl.Margin = padding;
        //            dayControl.Size = new Size(controlWidth, controlHeight);

        //            dayContainer.Controls.Add(dayControl);


        //            _ID++;
        //        }
        //    }
        //    else // For 31 days in month it will need 4 additional days to fill the last row
        //        for (int day = 1; day <= daysInMonth; day++)
        //        {
        //            dayDate = DateTime.Now.AddDays(day - 1);
        //            string formatted = dayDate.ToString("dddd, dd MMMM yyyy");  // Format: Weekday, Day Month Year
        //            var dayControl = new userControlDays(day, _ID, DateTime.Now.AddDays(day - 1), formatted); // Pass the date for each next days control 


        //            Padding padding = new Padding(margin);
        //            dayControl.Margin = padding;
        //            dayControl.Size = new Size(controlWidth, controlHeight);

        //            dayContainer.Controls.Add(dayControl);


        //            _ID++;
        //        }
        ////}

        //protected override void InitilizeDateLabel()
        //{
        //    // Ignore Click and hover event
        //    Date.Enabled = true;
        //    Date.Cursor = Cursors.Default; // remove hand cursor if set

        //    //Date.Size = new Size(200, 40);    
        //    Date.Font = new Font("Segoe UI", 19, FontStyle.Bold);
        //    Date.TextAlign = ContentAlignment.MiddleLeft;

        //    Date.Text = DateTime.Now.ToString("MMMM / yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //}
    }
}
