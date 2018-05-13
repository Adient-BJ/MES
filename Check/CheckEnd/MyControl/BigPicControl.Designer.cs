namespace CheckEnd.MyControl
{
    partial class BigPicControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.bigPicShow = new System.Windows.Forms.PictureBox();
            this.Delete = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.bigPicShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Delete)).BeginInit();
            this.SuspendLayout();
            // 
            // bigPicShow
            // 
            this.bigPicShow.Location = new System.Drawing.Point(-6, 2);
            this.bigPicShow.Name = "bigPicShow";
            this.bigPicShow.Size = new System.Drawing.Size(817, 437);
            this.bigPicShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.bigPicShow.TabIndex = 0;
            this.bigPicShow.TabStop = false;
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(711, 2);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(82, 50);
            this.Delete.TabIndex = 1;
            this.Delete.TabStop = false;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // BigPicControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 437);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.bigPicShow);
            this.Name = "BigPicControl";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.bigPicShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Delete)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox bigPicShow;
        private System.Windows.Forms.PictureBox Delete;
    }
}
