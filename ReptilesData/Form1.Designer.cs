namespace ReptilesData
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.MouseMoveTimer = new System.Windows.Forms.Timer(this.components);
            this.ScrollTimer = new System.Windows.Forms.Timer(this.components);
            this.CommonTimer = new System.Windows.Forms.Timer(this.components);
            this.InputTimer = new System.Windows.Forms.Timer(this.components);
            this.DelTimer = new System.Windows.Forms.Timer(this.components);
            this.tabPage_webView = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPage_webView.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MouseMoveTimer
            // 
            this.MouseMoveTimer.Interval = 8;
            this.MouseMoveTimer.Tick += new System.EventHandler(this.MouseMoveTimer_Tick);
            // 
            // ScrollTimer
            // 
            this.ScrollTimer.Interval = 6;
            this.ScrollTimer.Tick += new System.EventHandler(this.ScrollTimer_Tick);
            // 
            // CommonTimer
            // 
            this.CommonTimer.Interval = 5000;
            this.CommonTimer.Tick += new System.EventHandler(this.CommonTimer_Tick);
            // 
            // InputTimer
            // 
            this.InputTimer.Tick += new System.EventHandler(this.InputTimer_Tick);
            // 
            // DelTimer
            // 
            this.DelTimer.Tick += new System.EventHandler(this.DelTimer_Tick);
            // 
            // tabPage_webView
            // 
            this.tabPage_webView.Controls.Add(this.label1);
            this.tabPage_webView.Location = new System.Drawing.Point(4, 22);
            this.tabPage_webView.Name = "tabPage_webView";
            this.tabPage_webView.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_webView.Size = new System.Drawing.Size(1276, 635);
            this.tabPage_webView.TabIndex = 0;
            this.tabPage_webView.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(658, 360);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "↖";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_webView);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1284, 661);
            this.tabControl1.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1284, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 661);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1300, 700);
            this.MinimumSize = new System.Drawing.Size(1300, 700);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "演示模拟执行及爬虫Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPage_webView.ResumeLayout(false);
            this.tabPage_webView.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Timer MouseMoveTimer;
        private System.Windows.Forms.Timer ScrollTimer;
        private System.Windows.Forms.Timer CommonTimer;
        private System.Windows.Forms.Timer InputTimer;
        private System.Windows.Forms.Timer DelTimer;
        private System.Windows.Forms.TabPage tabPage_webView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label1;
    }
}

