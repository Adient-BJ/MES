namespace CheckEnd
{
    partial class ByPassErrorInfo
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
            this.workByName = new System.Windows.Forms.Label();
            this.productionNumber = new System.Windows.Forms.Label();
            this.carType = new System.Windows.Forms.Label();
            this.createTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // workByName
            // 
            this.workByName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.workByName.Location = new System.Drawing.Point(52, 40);
            this.workByName.Name = "workByName";
            this.workByName.Size = new System.Drawing.Size(136, 23);
            this.workByName.TabIndex = 0;
            this.workByName.Text = "label1";
            this.workByName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // productionNumber
            // 
            this.productionNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.productionNumber.Location = new System.Drawing.Point(221, 40);
            this.productionNumber.Name = "productionNumber";
            this.productionNumber.Size = new System.Drawing.Size(200, 23);
            this.productionNumber.TabIndex = 1;
            this.productionNumber.Text = "label2";
            this.productionNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // carType
            // 
            this.carType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.carType.Location = new System.Drawing.Point(455, 40);
            this.carType.Name = "carType";
            this.carType.Size = new System.Drawing.Size(136, 23);
            this.carType.TabIndex = 2;
            this.carType.Text = "label2";
            this.carType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // createTime
            // 
            this.createTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.createTime.Location = new System.Drawing.Point(631, 40);
            this.createTime.Name = "createTime";
            this.createTime.Size = new System.Drawing.Size(200, 23);
            this.createTime.TabIndex = 3;
            this.createTime.Text = "label2";
            this.createTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ByPassErrorInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Controls.Add(this.createTime);
            this.Controls.Add(this.carType);
            this.Controls.Add(this.productionNumber);
            this.Controls.Add(this.workByName);
            this.Name = "ByPassErrorInfo";
            this.Size = new System.Drawing.Size(867, 320);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label workByName;
        private System.Windows.Forms.Label productionNumber;
        private System.Windows.Forms.Label carType;
        private System.Windows.Forms.Label createTime;
    }
}
