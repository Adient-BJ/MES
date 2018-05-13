namespace CheckEnd
{
    partial class BackRepairFrm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.confirm = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.canRepairList = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.canRepairAllCheck = new System.Windows.Forms.CheckBox();
            this.canRepairListTitle = new System.Windows.Forms.Label();
            this.confirmedRework = new System.Windows.Forms.Panel();
            this.changePartLog = new System.Windows.Forms.Panel();
            this.confirmedRepairTitle = new System.Windows.Forms.Label();
            this.confirmedCheckAll = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.changePartLogTitle = new System.Windows.Forms.Label();
            this.submit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.confirm);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.canRepairList);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.canRepairAllCheck);
            this.panel1.Controls.Add(this.canRepairListTitle);
            this.panel1.Location = new System.Drawing.Point(31, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 200);
            this.panel1.TabIndex = 0;
            // 
            // confirm
            // 
            this.confirm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.confirm.FlatAppearance.BorderSize = 0;
            this.confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.confirm.Location = new System.Drawing.Point(30, 162);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(108, 35);
            this.confirm.TabIndex = 3;
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            this.confirm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.confirm_MouseDown);
            this.confirm.MouseEnter += new System.EventHandler(this.confirm_MouseEnter);
            this.confirm.MouseLeave += new System.EventHandler(this.confirm_MouseLeave);
            this.confirm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.confirm_MouseUp);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(170, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "描述";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // canRepairList
            // 
            this.canRepairList.AutoScroll = true;
            this.canRepairList.BackColor = System.Drawing.Color.White;
            this.canRepairList.Location = new System.Drawing.Point(3, 48);
            this.canRepairList.Name = "canRepairList";
            this.canRepairList.Size = new System.Drawing.Size(371, 108);
            this.canRepairList.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(68, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "可返修的零件";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // canRepairAllCheck
            // 
            this.canRepairAllCheck.AutoSize = true;
            this.canRepairAllCheck.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.canRepairAllCheck.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.canRepairAllCheck.Location = new System.Drawing.Point(14, 26);
            this.canRepairAllCheck.Name = "canRepairAllCheck";
            this.canRepairAllCheck.Size = new System.Drawing.Size(68, 23);
            this.canRepairAllCheck.TabIndex = 0;
            this.canRepairAllCheck.Text = "全选";
            this.canRepairAllCheck.UseVisualStyleBackColor = true;
            this.canRepairAllCheck.CheckedChanged += new System.EventHandler(this.canRepairAllCheck_CheckedChanged);
            // 
            // canRepairListTitle
            // 
            this.canRepairListTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(243)))), ((int)(((byte)(245)))));
            this.canRepairListTitle.Font = new System.Drawing.Font("黑体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.canRepairListTitle.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.canRepairListTitle.Location = new System.Drawing.Point(3, 0);
            this.canRepairListTitle.Name = "canRepairListTitle";
            this.canRepairListTitle.Size = new System.Drawing.Size(374, 23);
            this.canRepairListTitle.TabIndex = 0;
            this.canRepairListTitle.Text = "   可返修零件列表显示";
            this.canRepairListTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // confirmedRework
            // 
            this.confirmedRework.AutoScroll = true;
            this.confirmedRework.BackColor = System.Drawing.Color.White;
            this.confirmedRework.Location = new System.Drawing.Point(3, 55);
            this.confirmedRework.Name = "confirmedRework";
            this.confirmedRework.Size = new System.Drawing.Size(377, 129);
            this.confirmedRework.TabIndex = 1;
            // 
            // changePartLog
            // 
            this.changePartLog.BackColor = System.Drawing.Color.White;
            this.changePartLog.Location = new System.Drawing.Point(23, 312);
            this.changePartLog.Name = "changePartLog";
            this.changePartLog.Size = new System.Drawing.Size(771, 155);
            this.changePartLog.TabIndex = 2;
            // 
            // confirmedRepairTitle
            // 
            this.confirmedRepairTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(243)))), ((int)(((byte)(245)))));
            this.confirmedRepairTitle.Font = new System.Drawing.Font("黑体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.confirmedRepairTitle.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.confirmedRepairTitle.Location = new System.Drawing.Point(6, 3);
            this.confirmedRepairTitle.Name = "confirmedRepairTitle";
            this.confirmedRepairTitle.Size = new System.Drawing.Size(374, 23);
            this.confirmedRepairTitle.TabIndex = 3;
            this.confirmedRepairTitle.Text = "   已确认返修零件列表";
            this.confirmedRepairTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // confirmedCheckAll
            // 
            this.confirmedCheckAll.AutoSize = true;
            this.confirmedCheckAll.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.confirmedCheckAll.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.confirmedCheckAll.Location = new System.Drawing.Point(10, 29);
            this.confirmedCheckAll.Name = "confirmedCheckAll";
            this.confirmedCheckAll.Size = new System.Drawing.Size(68, 23);
            this.confirmedCheckAll.TabIndex = 4;
            this.confirmedCheckAll.Text = "全选";
            this.confirmedCheckAll.UseVisualStyleBackColor = true;
            this.confirmedCheckAll.CheckedChanged += new System.EventHandler(this.confirmedCheckAll_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(84, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "可返修的零件";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(178, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(207, 25);
            this.label5.TabIndex = 6;
            this.label5.Text = "描述";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.confirmedRepairTitle);
            this.panel2.Controls.Add(this.confirmedRework);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.confirmedCheckAll);
            this.panel2.Location = new System.Drawing.Point(414, 66);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(388, 197);
            this.panel2.TabIndex = 3;
            // 
            // changePartLogTitle
            // 
            this.changePartLogTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(243)))), ((int)(((byte)(245)))));
            this.changePartLogTitle.Font = new System.Drawing.Font("黑体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.changePartLogTitle.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.changePartLogTitle.Location = new System.Drawing.Point(28, 286);
            this.changePartLogTitle.Name = "changePartLogTitle";
            this.changePartLogTitle.Size = new System.Drawing.Size(771, 23);
            this.changePartLogTitle.TabIndex = 4;
            this.changePartLogTitle.Text = "   更换零件记录";
            this.changePartLogTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // submit
            // 
            this.submit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.submit.FlatAppearance.BorderSize = 0;
            this.submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submit.Location = new System.Drawing.Point(325, 473);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(130, 45);
            this.submit.TabIndex = 5;
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            this.submit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.submit_MouseDown);
            this.submit.MouseEnter += new System.EventHandler(this.submit_MouseEnter);
            this.submit.MouseLeave += new System.EventHandler(this.submit_MouseLeave);
            // 
            // BackRepairFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(829, 549);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.changePartLogTitle);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.changePartLog);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BackRepairFrm";
            this.Text = "BackRepair";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BackRepair_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel confirmedRework;
        private System.Windows.Forms.Panel changePartLog;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Label canRepairListTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel canRepairList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox canRepairAllCheck;
        private System.Windows.Forms.Label confirmedRepairTitle;
        private System.Windows.Forms.CheckBox confirmedCheckAll;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label changePartLogTitle;
        private System.Windows.Forms.Button submit;
    }
}