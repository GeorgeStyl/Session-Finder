using System.Windows.Forms;

namespace NodeJSClient.Forms
{
    partial class Session
    {
        private System.ComponentModel.IContainer components = null;
        private FlowLayoutPanel dayContainer;

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
            this.TopFlayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.customSwitch1 = new NodeJSClient.Forms.CustomSwitch();
            this.TopFlayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dayContainer
            // 
            this.dayContainer.Location = new System.Drawing.Point(53, 222);
            this.dayContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dayContainer.Name = "dayContainer";
            this.dayContainer.Size = new System.Drawing.Size(1200, 704);
            this.dayContainer.TabIndex = 0;
            this.dayContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // PreviousBtn
            // 
            this.PreviousBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PreviousBtn.Location = new System.Drawing.Point(131, 36);
            this.PreviousBtn.Name = "PreviousBtn";
            this.PreviousBtn.Size = new System.Drawing.Size(69, 25);
            this.PreviousBtn.TabIndex = 4;
            this.PreviousBtn.Text = "<-";
            this.PreviousBtn.UseVisualStyleBackColor = true;
            this.PreviousBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // Date
            // 
            this.Date.Location = new System.Drawing.Point(216, 0);
            this.Date.Margin = new System.Windows.Forms.Padding(13, 0, 13, 0);
            this.Date.Name = "Date";
            this.Date.Size = new System.Drawing.Size(240, 97);
            this.Date.TabIndex = 5;
            this.Date.Text = "label1";
            this.Date.Click += new System.EventHandler(this.Date_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(472, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(78, 25);
            this.button2.TabIndex = 5;
            this.button2.Text = "->";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // WeekDaysFlowLayout
            // 
            this.WeekDaysFlowLayout.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.WeekDaysFlowLayout.Location = new System.Drawing.Point(53, 127);
            this.WeekDaysFlowLayout.Margin = new System.Windows.Forms.Padding(4);
            this.WeekDaysFlowLayout.Name = "WeekDaysFlowLayout";
            this.WeekDaysFlowLayout.Size = new System.Drawing.Size(1200, 89);
            this.WeekDaysFlowLayout.TabIndex = 1;
            this.WeekDaysFlowLayout.Paint += new System.Windows.Forms.PaintEventHandler(this.WeekDaysFlowLayout_Paint);
            // 
            // TopFlayoutPanel
            // 
            this.TopFlayoutPanel.Controls.Add(this.customSwitch1);
            this.TopFlayoutPanel.Controls.Add(this.PreviousBtn);
            this.TopFlayoutPanel.Controls.Add(this.Date);
            this.TopFlayoutPanel.Controls.Add(this.button2);
            this.TopFlayoutPanel.Location = new System.Drawing.Point(56, 20);
            this.TopFlayoutPanel.Name = "TopFlayoutPanel";
            this.TopFlayoutPanel.Size = new System.Drawing.Size(1197, 100);
            this.TopFlayoutPanel.TabIndex = 2;
            this.TopFlayoutPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.TopFlayoutPanel_Paint);
            // 
            // customSwitch1
            // 
            this.customSwitch1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.customSwitch1.Location = new System.Drawing.Point(3, 17);
            this.customSwitch1.Name = "customSwitch1";
            this.customSwitch1.Size = new System.Drawing.Size(122, 63);
            this.customSwitch1.TabIndex = 3;
            this.customSwitch1.Load += new System.EventHandler(this.customSwitch1_Load);
            // 
            // Session
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1377, 1024);
            this.Controls.Add(this.TopFlayoutPanel);
            this.Controls.Add(this.WeekDaysFlowLayout);
            this.Controls.Add(this.dayContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Session";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Session";
            this.Load += new System.EventHandler(this.Session_Load);
            this.TopFlayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private FlowLayoutPanel WeekDaysFlowLayout;
        private CustomSwitch customSwitch1;
        private Button PreviousBtn;
        private Button button2;
        private Label Date;
        private FlowLayoutPanel TopFlayoutPanel;
    }
}
