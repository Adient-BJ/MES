namespace Picking.Frm
{
    partial class Forwarding
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
            this.scanTitle = new System.Windows.Forms.Label();
            this.EDIQueue = new System.Windows.Forms.Panel();
            this.print = new System.Windows.Forms.Button();
            this.errorInfo = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.Label();
            this.scanBox = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // scanTitle
            // 
            this.scanTitle.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold);
            this.scanTitle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.scanTitle.Location = new System.Drawing.Point(101, 51);
            this.scanTitle.Name = "scanTitle";
            this.scanTitle.Size = new System.Drawing.Size(268, 59);
            this.scanTitle.TabIndex = 0;
            this.scanTitle.Text = "请扫描条形码";
            this.scanTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EDIQueue
            // 
            this.EDIQueue.AutoScroll = true;
            this.EDIQueue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EDIQueue.Location = new System.Drawing.Point(22, 211);
            this.EDIQueue.Name = "EDIQueue";
            this.EDIQueue.Size = new System.Drawing.Size(487, 504);
            this.EDIQueue.TabIndex = 2;
            // 
            // print
            // 
            this.print.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.print.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.print.Location = new System.Drawing.Point(612, 585);
            this.print.Name = "print";
            this.print.Size = new System.Drawing.Size(352, 61);
            this.print.TabIndex = 5;
            this.print.Text = "打印装车单";
            this.print.UseVisualStyleBackColor = true;
            this.print.Click += new System.EventHandler(this.print_Click);
            // 
            // errorInfo
            // 
            this.errorInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.errorInfo.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.errorInfo.ForeColor = System.Drawing.Color.Red;
            this.errorInfo.Location = new System.Drawing.Point(542, 158);
            this.errorInfo.Name = "errorInfo";
            this.errorInfo.Size = new System.Drawing.Size(451, 321);
            this.errorInfo.TabIndex = 1;
            this.errorInfo.Text = "提示信息";
            this.errorInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Title
            // 
            this.Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Title.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Title.ForeColor = System.Drawing.Color.Red;
            this.Title.Location = new System.Drawing.Point(22, 158);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(487, 50);
            this.Title.TabIndex = 7;
            this.Title.Text = "查询EDI信息";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Title.Click += new System.EventHandler(this.Title_Click);
            // 
            // scanBox
            // 
            this.scanBox.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Bold);
            this.scanBox.Location = new System.Drawing.Point(497, 46);
            this.scanBox.Name = "scanBox";
            this.scanBox.Size = new System.Drawing.Size(100, 23);
            this.scanBox.TabIndex = 8;
            this.scanBox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Forwarding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 838);
            this.Controls.Add(this.scanBox);
            this.Controls.Add(this.errorInfo);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.print);
            this.Controls.Add(this.EDIQueue);
            this.Controls.Add(this.scanTitle);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Forwarding";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.Forwarding_Activated);
            this.Load += new System.EventHandler(this.Forwarding_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Forwarding_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label scanTitle;
        private System.Windows.Forms.Panel EDIQueue;
        private System.Windows.Forms.Button print;
        private System.Windows.Forms.Label errorInfo;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label scanBox;
    }
}