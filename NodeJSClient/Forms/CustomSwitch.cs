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
    public partial class CustomSwitch : UserControl
    {
        public bool changeOnHoverMode = false;

        public static CustomSwitch GlobalCustomSwitchInstance { get; set; }
        public CustomSwitch()
        {
            InitializeComponent();

            GlobalCustomSwitchInstance = this;
        }

        private void CustomSwitch_Load(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ChangeOnHoverMode.Checked)
            {
                ChangeOnHoverMode.Text = "Multiple targets";
                this.changeOnHoverMode = true;
            }
            else 
            {
                ChangeOnHoverMode.Text = "Single target";
                this.changeOnHoverMode = false;
            }
        }
    }
}
