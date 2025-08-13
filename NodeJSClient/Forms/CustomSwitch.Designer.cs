namespace NodeJSClient.Forms
{
    partial class CustomSwitch
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChangeOnHoverMode = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ChangeOnHoverMode
            // 
            this.ChangeOnHoverMode.AutoSize = true;
            this.ChangeOnHoverMode.Location = new System.Drawing.Point(14, 30);
            this.ChangeOnHoverMode.Name = "ChangeOnHoverMode";
            this.ChangeOnHoverMode.Size = new System.Drawing.Size(104, 20);
            this.ChangeOnHoverMode.TabIndex = 0;
            this.ChangeOnHoverMode.Text = "Single target";
            this.ChangeOnHoverMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChangeOnHoverMode.UseVisualStyleBackColor = true;
            this.ChangeOnHoverMode.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // CustomSwitch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChangeOnHoverMode);
            this.Name = "CustomSwitch";
            this.Size = new System.Drawing.Size(164, 79);
            this.Load += new System.EventHandler(this.CustomSwitch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ChangeOnHoverMode;
    }
}
