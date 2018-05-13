namespace Picking.Frm
{
    partial class AllEntruck
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
            this.scanTitle = new System.Windows.Forms.Label();
            this.scanDes = new System.Windows.Forms.Panel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.scanBox = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // scanTitle
            // 
            this.scanTitle.Font = new System.Drawing.Font("微软雅黑", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.scanTitle.Location = new System.Drawing.Point(103, 58);
            this.scanTitle.Name = "scanTitle";
            this.scanTitle.Size = new System.Drawing.Size(323, 47);
            this.scanTitle.TabIndex = 0;
            this.scanTitle.Text = "请扫描条形码";
            // 
            // scanDes
            // 
            this.scanDes.BackColor = System.Drawing.SystemColors.Control;
            this.scanDes.Location = new System.Drawing.Point(105, 161);
            this.scanDes.Name = "scanDes";
            this.scanDes.Size = new System.Drawing.Size(216, 456);
            this.scanDes.TabIndex = 2;
            // 
            // scanBox
            // 
            this.scanBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scanBox.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.scanBox.Location = new System.Drawing.Point(418, 72);
            this.scanBox.Name = "scanBox";
            this.scanBox.Size = new System.Drawing.Size(460, 57);
            this.scanBox.TabIndex = 4;
            this.scanBox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(685, 650);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(288, 67);
            this.button1.TabIndex = 20;
            this.button1.Text = "查询库位信息";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AllEntruck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 773);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.scanBox);
            this.Controls.Add(this.scanDes);
            this.Controls.Add(this.scanTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "AllEntruck";
            this.Text = "HeZhuang";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.AllEntruck_Activated);
            this.Load += new System.EventHandler(this.HeZhuang_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AllEntruck_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label scanTitle;
        private System.Windows.Forms.Panel scanDes;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Label scanBox;
        private System.Windows.Forms.Button button1;
    }
}