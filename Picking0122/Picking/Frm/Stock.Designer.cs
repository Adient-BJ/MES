namespace Picking.Frm
{
    partial class Stock
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
            this.scanTitle = new System.Windows.Forms.Label();
            this.scanBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(179, 169);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(742, 273);
            this.panel1.TabIndex = 0;
            // 
            // scanTitle
            // 
            this.scanTitle.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold);
            this.scanTitle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.scanTitle.Location = new System.Drawing.Point(89, 64);
            this.scanTitle.Name = "scanTitle";
            this.scanTitle.Size = new System.Drawing.Size(337, 59);
            this.scanTitle.TabIndex = 1;
            this.scanTitle.Text = "请扫描条形码";
            this.scanTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scanBox
            // 
            this.scanBox.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold);
            this.scanBox.Location = new System.Drawing.Point(432, 66);
            this.scanBox.Name = "scanBox";
            this.scanBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.scanBox.Size = new System.Drawing.Size(542, 59);
            this.scanBox.TabIndex = 2;
            this.scanBox.Text = "";
            // 
            // Stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 643);
            this.Controls.Add(this.scanBox);
            this.Controls.Add(this.scanTitle);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Stock";
            this.Text = "Stock";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Stock_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label scanTitle;
        private System.Windows.Forms.RichTextBox scanBox;
    }
}