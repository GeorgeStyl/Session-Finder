using System.Windows.Forms;

namespace NodeJSClient.Forms
{
    partial class Session
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dayContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.PreviousBtn = new System.Windows.Forms.Button();
            this.Date = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.WeekDaysFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.customSwitch2 = new NodeJSClient.Forms.CustomSwitch();
            this.customSwitch1 = new NodeJSClient.Forms.CustomSwitch();
            this.TopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dayContainer
            // 
            this.dayContainer.Location = new System.Drawing.Point(13, 222);
            this.dayContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dayContainer.Name = "dayContainer";
            this.dayContainer.Size = new System.Drawing.Size(1200, 694);
            this.dayContainer.TabIndex = 0;
            this.dayContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // PreviousBtn
            // 
            this.PreviousBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PreviousBtn.Location = new System.Drawing.Point(408, 30);
            this.PreviousBtn.Name = "PreviousBtn";
            this.PreviousBtn.Size = new System.Drawing.Size(69, 39);
            this.PreviousBtn.TabIndex = 4;
            this.PreviousBtn.Text = "<-";
            this.PreviousBtn.UseVisualStyleBackColor = true;
            this.PreviousBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // Date
            // 
            this.Date.BackColor = System.Drawing.Color.Beige;
            this.Date.Location = new System.Drawing.Point(493, 6);
            this.Date.Margin = new System.Windows.Forms.Padding(13, 0, 13, 0);
            this.Date.Name = "Date";
            this.Date.Size = new System.Drawing.Size(295, 76);
            this.Date.TabIndex = 5;
            this.Date.Text = "label1";
            this.Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Date.Click += new System.EventHandler(this.Date_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(804, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(78, 39);
            this.button2.TabIndex = 5;
            this.button2.Text = "->";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // WeekDaysFlowLayout
            // 
            this.WeekDaysFlowLayout.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.WeekDaysFlowLayout.Location = new System.Drawing.Point(13, 127);
            this.WeekDaysFlowLayout.Margin = new System.Windows.Forms.Padding(4);
            this.WeekDaysFlowLayout.Name = "WeekDaysFlowLayout";
            this.WeekDaysFlowLayout.Size = new System.Drawing.Size(1200, 89);
            this.WeekDaysFlowLayout.TabIndex = 1;
            this.WeekDaysFlowLayout.Paint += new System.Windows.Forms.PaintEventHandler(this.WeekDaysFlowLayout_Paint);
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.customSwitch2);
            this.TopPanel.Controls.Add(this.button2);
            this.TopPanel.Controls.Add(this.Date);
            this.TopPanel.Controls.Add(this.PreviousBtn);
            this.TopPanel.Location = new System.Drawing.Point(16, 20);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(1197, 100);
            this.TopPanel.TabIndex = 4;
            this.TopPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.TopPanel_Paint);
            // 
            // customSwitch2
            // 
            this.customSwitch2.Location = new System.Drawing.Point(91, 16);
            this.customSwitch2.Name = "customSwitch2";
            this.customSwitch2.Size = new System.Drawing.Size(162, 66);
            this.customSwitch2.TabIndex = 0;
            this.customSwitch2.Load += new System.EventHandler(this.customSwitch2_Load);
            // 
            // customSwitch1
            // 
            this.customSwitch1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.customSwitch1.Location = new System.Drawing.Point(53, -7);
            this.customSwitch1.Name = "customSwitch1";
            this.customSwitch1.Size = new System.Drawing.Size(0, 61);
            this.customSwitch1.TabIndex = 3;
            this.customSwitch1.Load += new System.EventHandler(this.customSwitch1_Load);
            // 
            // Session
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 941);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.customSwitch1);
            this.Controls.Add(this.WeekDaysFlowLayout);
            this.Controls.Add(this.dayContainer);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Session";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Session";
            this.Load += new System.EventHandler(this.Session_Load);
            this.TopPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private FlowLayoutPanel WeekDaysFlowLayout;
        private CustomSwitch customSwitch1;
        private Button PreviousBtn;
        private Button button2;
        private CustomSwitch customSwitch2;
        protected FlowLayoutPanel dayContainer;
        protected Panel TopPanel;
        protected Label Date;
    }
}
