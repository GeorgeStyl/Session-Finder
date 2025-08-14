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
       
        public UserDefaultSettings() : base()
        {
            InitializeComponent();
        }

        protected override void displayDays()
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
                for (int day = 1; day <= daysInMonth; day++)
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
                for (int day = 1; day <= daysInMonth; day++)
                {
                    var dayControl = new userControlDays(day, _ID);
                    Padding padding = new Padding(margin);
                    dayControl.Margin = padding;
                    dayControl.Size = new Size(controlWidth, controlHeight);

                    dayContainer.Controls.Add(dayControl);
                    _ID++;
                }
        }
    }
}
