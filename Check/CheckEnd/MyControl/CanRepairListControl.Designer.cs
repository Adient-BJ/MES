namespace CheckEnd.MyControl
{
    partial class CanRepairListControl
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.part = new System.Windows.Forms.Label();
            this.description = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(80, 21);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // part
            // 
            this.part.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.part.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.part.Location = new System.Drawing.Point(110, 0);
            this.part.Name = "part";
            this.part.Size = new System.Drawing.Size(221, 53);
            this.part.TabIndex = 1;
            this.part.Text = "零件1";
            this.part.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // description
            // 
            this.description.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.description.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.description.Location = new System.Drawing.Point(337, 1);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(418, 53);
            this.description.TabIndex = 2;
            this.description.Text = "描述1";
            this.description.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CanRepairListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.description);
            this.Controls.Add(this.part);
            this.Controls.Add(this.checkBox1);
            this.Name = "CanRepairListControl";
            this.Size = new System.Drawing.Size(755, 53);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label part;
        private System.Windows.Forms.Label description;
    }
}
