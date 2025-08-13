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
    public partial class LoginSignupHandler : Form
    {
        public LoginSignupHandler()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void logInBtn_Click(object sender, EventArgs e)
        {
            string usrname = usrNameTxtBox.Text;
            string pwd = pwdTxtBox.Text;
            MessageBox.Show(usrname, pwd);
        }

      

        private void usrNameTxtBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void pwdTxtBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void usrNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void singInButt_Click(object sender, EventArgs e)
        {
            SingInForm signInForm = new SingInForm();
            signInForm.ShowDialog();
        }
    }
}
