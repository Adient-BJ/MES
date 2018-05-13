namespace CheckEnd
{
    partial class EndCheckFrm
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
            this.button1 = new System.Windows.Forms.Button();
            this.bigPic = new System.Windows.Forms.Panel();
            this.timing = new System.Windows.Forms.Timer(this.components);
            this.border = new System.Windows.Forms.Panel();
            this.errorInfo = new System.Windows.Forms.Label();
            this.record = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.remark = new System.Windows.Forms.RichTextBox();
            this.save = new System.Windows.Forms.Button();
            this.part3 = new System.Windows.Forms.ComboBox();
            this.part2 = new System.Windows.Forms.ComboBox();
            this.part1 = new System.Windows.Forms.ComboBox();
            this.border.SuspendLayout();
            this.record.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(901, 592);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(190, 74);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            this.button1.MouseEnter += new System.EventHandler(this.button1_MouseEnter);
            this.button1.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            this.button1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button1_MouseUp);
            // 
            // bigPic
            // 
            this.bigPic.AutoScroll = true;
            this.bigPic.BackColor = System.Drawing.SystemColors.Control;
            this.bigPic.Location = new System.Drawing.Point(21, 51);
            this.bigPic.Name = "bigPic";
            this.bigPic.Size = new System.Drawing.Size(1131, 241);
            this.bigPic.TabIndex = 0;
            // 
            // timing
            // 
            this.timing.Interval = 1000;
            this.timing.Tick += new System.EventHandler(this.timing_Tick);
            // 
            // border
            // 
            this.border.Controls.Add(this.errorInfo);
            this.border.Location = new System.Drawing.Point(30, 576);
            this.border.Name = "border";
            this.border.Size = new System.Drawing.Size(821, 105);
            this.border.TabIndex = 8;
            this.border.Visible = false;
            // 
            // errorInfo
            // 
            this.errorInfo.Font = new System.Drawing.Font("微软雅黑", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.errorInfo.Location = new System.Drawing.Point(18, 16);
            this.errorInfo.Name = "errorInfo";
            this.errorInfo.Size = new System.Drawing.Size(781, 75);
            this.errorInfo.TabIndex = 0;
            this.errorInfo.Text = "已完成，等待确认放行...";
            this.errorInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // record
            // 
            this.record.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.record.Controls.Add(this.label1);
            this.record.Controls.Add(this.remark);
            this.record.Controls.Add(this.save);
            this.record.Controls.Add(this.part3);
            this.record.Controls.Add(this.part2);
            this.record.Controls.Add(this.part1);
            this.record.Location = new System.Drawing.Point(30, 310);
            this.record.Name = "record";
            this.record.Size = new System.Drawing.Size(1122, 247);
            this.record.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "label1";
            // 
            // remark
            // 
            this.remark.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.remark.Location = new System.Drawing.Point(14, 141);
            this.remark.Name = "remark";
            this.remark.Size = new System.Drawing.Size(714, 96);
            this.remark.TabIndex = 10;
            this.remark.Text = "";
            this.remark.Enter += new System.EventHandler(this.remark_Enter);
            // 
            // save
            // 
            this.save.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.save.Location = new System.Drawing.Point(853, 124);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(249, 98);
            this.save.TabIndex = 3;
            this.save.Text = "保存此缺陷";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // part3
            // 
            this.part3.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.part3.FormattingEnabled = true;
            this.part3.Location = new System.Drawing.Point(14, 3);
            this.part3.Name = "part3";
            this.part3.Size = new System.Drawing.Size(554, 37);
            this.part3.TabIndex = 2;
            // 
            // part2
            // 
            this.part2.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.part2.FormattingEnabled = true;
            this.part2.Location = new System.Drawing.Point(14, 114);
            this.part2.Name = "part2";
            this.part2.Size = new System.Drawing.Size(554, 37);
            this.part2.TabIndex = 1;
            this.part2.SelectedValueChanged += new System.EventHandler(this.part2_SelectedValueChanged);
            // 
            // part1
            // 
            this.part1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.part1.FormattingEnabled = true;
            this.part1.Location = new System.Drawing.Point(14, 44);
            this.part1.Name = "part1";
            this.part1.Size = new System.Drawing.Size(554, 37);
            this.part1.TabIndex = 0;
            this.part1.SelectedValueChanged += new System.EventHandler(this.part1_SelectedValueChanged);
            // 
            // EndCheckFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 695);
            this.Controls.Add(this.record);
            this.Controls.Add(this.border);
            this.Controls.Add(this.bigPic);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EndCheckFrm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.EndCheck_Load);
            this.border.ResumeLayout(false);
            this.record.ResumeLayout(false);
            this.record.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timing;
        private System.Windows.Forms.Panel bigPic;
        private System.Windows.Forms.Panel border;
        private System.Windows.Forms.Label errorInfo;
        private System.Windows.Forms.Panel record;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.ComboBox part3;
        private System.Windows.Forms.ComboBox part2;
        private System.Windows.Forms.ComboBox part1;
        private System.Windows.Forms.RichTextBox remark;
        private System.Windows.Forms.Label label1;
    }
}