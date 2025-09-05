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
            this.WeekDaysFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.UpdateChangesButt = new System.Windows.Forms.Button();
            this.MultipleSelection = new System.Windows.Forms.CheckBox();
            this.RowSelection = new System.Windows.Forms.CheckBox();
            this.SingleSelection = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NextMonthBtn = new System.Windows.Forms.Button();
            this.PreviousMonthBtn = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.TopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dayContainer
            // 
            this.dayContainer.Location = new System.Drawing.Point(13, 222);
            this.dayContainer.Margin = new System.Windows.Forms.Padding(0);
            this.dayContainer.Name = "dayContainer";
            this.dayContainer.Size = new System.Drawing.Size(1200, 694);
            this.dayContainer.TabIndex = 0;
            this.dayContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // WeekDaysFlowLayout
            // 
            this.WeekDaysFlowLayout.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.WeekDaysFlowLayout.Location = new System.Drawing.Point(13, 127);
            this.WeekDaysFlowLayout.Margin = new System.Windows.Forms.Padding(0);
            this.WeekDaysFlowLayout.Name = "WeekDaysFlowLayout";
            this.WeekDaysFlowLayout.Size = new System.Drawing.Size(1200, 95);
            this.WeekDaysFlowLayout.TabIndex = 1;
            this.WeekDaysFlowLayout.Paint += new System.Windows.Forms.PaintEventHandler(this.WeekDaysFlowLayout_Paint);
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.UpdateChangesButt);
            this.TopPanel.Controls.Add(this.MultipleSelection);
            this.TopPanel.Controls.Add(this.RowSelection);
            this.TopPanel.Controls.Add(this.SingleSelection);
            this.TopPanel.Controls.Add(this.label1);
            this.TopPanel.Controls.Add(this.NextMonthBtn);
            this.TopPanel.Controls.Add(this.PreviousMonthBtn);
            this.TopPanel.Location = new System.Drawing.Point(15, 5);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(1201, 119);
            this.TopPanel.TabIndex = 4;
            this.TopPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.TopPanel_Paint);
            // 
            // UpdateChangesButt
            // 
            this.UpdateChangesButt.Enabled = false;
            this.UpdateChangesButt.Location = new System.Drawing.Point(176, 75);
            this.UpdateChangesButt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.UpdateChangesButt.Name = "UpdateChangesButt";
            this.UpdateChangesButt.Size = new System.Drawing.Size(143, 28);
            this.UpdateChangesButt.TabIndex = 0;
            this.UpdateChangesButt.Text = "Update changes";
            this.UpdateChangesButt.UseVisualStyleBackColor = true;
            this.UpdateChangesButt.Click += new System.EventHandler(this.UpdateChanges_Click);
            // 
            // MultipleSelection
            // 
            this.MultipleSelection.AutoSize = true;
            this.MultipleSelection.Location = new System.Drawing.Point(176, 31);
            this.MultipleSelection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MultipleSelection.Name = "MultipleSelection";
            this.MultipleSelection.Size = new System.Drawing.Size(95, 20);
            this.MultipleSelection.TabIndex = 0;
            this.MultipleSelection.Text = "checkBox1";
            this.MultipleSelection.UseVisualStyleBackColor = true;
            this.MultipleSelection.CheckedChanged += new System.EventHandler(this.Selection_CheckedChanged);
            // 
            // RowSelection
            // 
            this.RowSelection.AutoSize = true;
            this.RowSelection.Location = new System.Drawing.Point(4, 48);
            this.RowSelection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RowSelection.Name = "RowSelection";
            this.RowSelection.Size = new System.Drawing.Size(95, 20);
            this.RowSelection.TabIndex = 0;
            this.RowSelection.Text = "checkBox1";
            this.RowSelection.UseVisualStyleBackColor = true;
            this.RowSelection.CheckedChanged += new System.EventHandler(this.Selection_CheckedChanged);
            // 
            // SingleSelection
            // 
            this.SingleSelection.AutoSize = true;
            this.SingleSelection.Location = new System.Drawing.Point(4, 4);
            this.SingleSelection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SingleSelection.Name = "SingleSelection";
            this.SingleSelection.Size = new System.Drawing.Size(95, 20);
            this.SingleSelection.TabIndex = 0;
            this.SingleSelection.Text = "checkBox1";
            this.SingleSelection.UseVisualStyleBackColor = true;
            this.SingleSelection.CheckedChanged += new System.EventHandler(this.Selection_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(577, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            // 
            // NextMonthBtn
            // 
            this.NextMonthBtn.Location = new System.Drawing.Point(805, 31);
            this.NextMonthBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.NextMonthBtn.Name = "NextMonthBtn";
            this.NextMonthBtn.Size = new System.Drawing.Size(100, 38);
            this.NextMonthBtn.TabIndex = 7;
            this.NextMonthBtn.Text = "->";
            this.NextMonthBtn.UseVisualStyleBackColor = true;
            this.NextMonthBtn.Click += new System.EventHandler(this.NextMonthBtn_Click);
            // 
            // PreviousMonthBtn
            // 
            this.PreviousMonthBtn.Location = new System.Drawing.Point(363, 31);
            this.PreviousMonthBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PreviousMonthBtn.Name = "PreviousMonthBtn";
            this.PreviousMonthBtn.Size = new System.Drawing.Size(100, 38);
            this.PreviousMonthBtn.TabIndex = 6;
            this.PreviousMonthBtn.Text = "<-";
            this.PreviousMonthBtn.UseVisualStyleBackColor = true;
            this.PreviousMonthBtn.Click += new System.EventHandler(this.PreviousMonthBtn_Click);
            // 
            // Session
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 942);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.WeekDaysFlowLayout);
            this.Controls.Add(this.dayContainer);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Session";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Session";
            this.Load += new System.EventHandler(this.Session_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        private FlowLayoutPanel WeekDaysFlowLayout;
        protected FlowLayoutPanel dayContainer;
        protected Panel TopPanel;
        private Button NextMonthBtn;
        private Button PreviousMonthBtn;
        private Label label1;
        private HelpProvider helpProvider1;
        private CheckBox RowSelection;
        private CheckBox SingleSelection;
        private CheckBox MultipleSelection;
        private Button button1;
        private Button UpdateChangesButt;
    }
}
