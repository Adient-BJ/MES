namespace CheckEnd.MyControl
{
    partial class TestResult
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
            this.checkPro = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.handle = new System.Windows.Forms.Panel();
            this.confirm = new System.Windows.Forms.Button();
            this.checkPro.SuspendLayout();
            this.handle.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkPro
            // 
            this.checkPro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkPro.Controls.Add(this.label1);
            this.checkPro.Location = new System.Drawing.Point(127, 94);
            this.checkPro.Margin = new System.Windows.Forms.Padding(2);
            this.checkPro.Name = "checkPro";
            this.checkPro.Size = new System.Drawing.Size(164, 34);
            this.checkPro.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(-1, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // handle
            // 
            this.handle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.handle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.handle.Controls.Add(this.confirm);
            this.handle.Location = new System.Drawing.Point(297, 94);
            this.handle.Margin = new System.Windows.Forms.Padding(2);
            this.handle.Name = "handle";
            this.handle.Size = new System.Drawing.Size(158, 34);
            this.handle.TabIndex = 1;
            // 
            // confirm
            // 
            this.confirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(191)))), ((int)(((byte)(122)))));
            this.confirm.FlatAppearance.BorderSize = 0;
            this.confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.confirm.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.confirm.ForeColor = System.Drawing.Color.Snow;
            this.confirm.Location = new System.Drawing.Point(47, 8);
            this.confirm.Margin = new System.Windows.Forms.Padding(2);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(60, 17);
            this.confirm.TabIndex = 0;
            this.confirm.Text = "button1";
            this.confirm.UseVisualStyleBackColor = false;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // TestResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 9F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.handle);
            this.Controls.Add(this.checkPro);
            this.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TestResult";
            this.Size = new System.Drawing.Size(631, 232);
            this.checkPro.ResumeLayout(false);
            this.handle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel checkPro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel handle;
        private System.Windows.Forms.Button confirm;
    }
}
