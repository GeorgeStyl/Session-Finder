namespace NodeJSClient
{
    partial class LoginSignupHandler
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.usrNameLabel = new System.Windows.Forms.Label();
            this.logInBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.usrNameTxtBox = new System.Windows.Forms.TextBox();
            this.pwdTxtBox = new System.Windows.Forms.TextBox();
            this.singInButt = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // usrNameLabel
            // 
            this.usrNameLabel.AutoSize = true;
            this.usrNameLabel.Location = new System.Drawing.Point(122, 118);
            this.usrNameLabel.Name = "usrNameLabel";
            this.usrNameLabel.Size = new System.Drawing.Size(76, 16);
            this.usrNameLabel.TabIndex = 0;
            this.usrNameLabel.Text = "User Name:";
            this.usrNameLabel.Click += new System.EventHandler(this.usrNameLabel_Click);
            // 
            // logInBtn
            // 
            this.logInBtn.Location = new System.Drawing.Point(131, 227);
            this.logInBtn.Name = "logInBtn";
            this.logInBtn.Size = new System.Drawing.Size(124, 23);
            this.logInBtn.TabIndex = 2;
            this.logInBtn.Text = "Login";
            this.logInBtn.UseVisualStyleBackColor = true;
            this.logInBtn.Click += new System.EventHandler(this.logInBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Password:";
            // 
            // usrNameTxtBox
            // 
            this.usrNameTxtBox.Location = new System.Drawing.Point(193, 118);
            this.usrNameTxtBox.Name = "usrNameTxtBox";
            this.usrNameTxtBox.Size = new System.Drawing.Size(123, 24);
            this.usrNameTxtBox.TabIndex = 5;
            this.usrNameTxtBox.TextChanged += new System.EventHandler(this.usrNameTxtBox_TextChanged);
            // 
            // pwdTxtBox
            // 
            this.pwdTxtBox.Location = new System.Drawing.Point(193, 169);
            this.pwdTxtBox.Name = "pwdTxtBox";
            this.pwdTxtBox.Size = new System.Drawing.Size(123, 24);
            this.pwdTxtBox.TabIndex = 6;
            this.pwdTxtBox.TextChanged += new System.EventHandler(this.pwdTxtBox_TextChanged);
            // 
            // singInButt
            // 
            this.singInButt.Location = new System.Drawing.Point(384, 148);
            this.singInButt.Name = "singInButt";
            this.singInButt.Size = new System.Drawing.Size(75, 23);
            this.singInButt.TabIndex = 7;
            this.singInButt.Text = "Sign-in";
            this.singInButt.UseVisualStyleBackColor = true;
            this.singInButt.Click += new System.EventHandler(this.singInButt_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(364, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Don\'t have account?";
            // 
            // LoginSignupHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumAquamarine;
            this.ClientSize = new System.Drawing.Size(500, 336);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.singInButt);
            this.Controls.Add(this.pwdTxtBox);
            this.Controls.Add(this.usrNameTxtBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logInBtn);
            this.Controls.Add(this.usrNameLabel);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "LoginSignupHandler";
            this.Text = "Sessions Finder";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label usrNameLabel;
        private System.Windows.Forms.Button logInBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox usrNameTxtBox;
        private System.Windows.Forms.TextBox pwdTxtBox;
        private System.Windows.Forms.Button singInButt;
        private System.Windows.Forms.Label label2;
    }
}