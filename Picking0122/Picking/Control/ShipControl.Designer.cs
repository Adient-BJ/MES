namespace Picking.Control
{
    partial class ShipControl
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
            this.name = new System.Windows.Forms.Label();
            this.barCode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.BackColor = System.Drawing.SystemColors.ControlDark;
            this.name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.name.Font = new System.Drawing.Font("微软雅黑", 26.25F);
            this.name.Location = new System.Drawing.Point(32, 40);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(185, 54);
            this.name.TabIndex = 0;
            this.name.Text = "label1";
            this.name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // barCode
            // 
            this.barCode.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.barCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barCode.Font = new System.Drawing.Font("微软雅黑", 21.75F);
            this.barCode.Location = new System.Drawing.Point(250, 40);
            this.barCode.Name = "barCode";
            this.barCode.Size = new System.Drawing.Size(324, 54);
            this.barCode.TabIndex = 1;
            this.barCode.Text = "label2";
            this.barCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ShipControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barCode);
            this.Controls.Add(this.name);
            this.Name = "ShipControl";
            this.Size = new System.Drawing.Size(669, 139);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label barCode;
    }
}
