namespace CheckEnd.MyControl
{
    partial class ZJOption
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
            this.detail = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // detail
            // 
            this.detail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(158)))), ((int)(((byte)(235)))));
            this.detail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.detail.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.detail.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.detail.Location = new System.Drawing.Point(69, 0);
            this.detail.Name = "detail";
            this.detail.Size = new System.Drawing.Size(492, 62);
            this.detail.TabIndex = 0;
            this.detail.Text = "label1";
            this.detail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.detail.Click += new System.EventHandler(this.detail_Click);
            // 
            // ZJOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.detail);
            this.Name = "ZJOption";
            this.Size = new System.Drawing.Size(607, 62);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label detail;
    }
}
