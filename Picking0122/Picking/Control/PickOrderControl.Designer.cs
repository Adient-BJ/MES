namespace Picking.Control
{
    partial class PickOrderControl
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
            this.serialNo = new System.Windows.Forms.Label();
            this.carModel = new System.Windows.Forms.Label();
            this.carType = new System.Windows.Forms.Label();
            this.barCodes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // serialNo
            // 
            this.serialNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serialNo.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.serialNo.Location = new System.Drawing.Point(0, 20);
            this.serialNo.Name = "serialNo";
            this.serialNo.Size = new System.Drawing.Size(241, 52);
            this.serialNo.TabIndex = 0;
            this.serialNo.Text = "生产号";
            this.serialNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // carModel
            // 
            this.carModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.carModel.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.carModel.Location = new System.Drawing.Point(557, 20);
            this.carModel.Name = "carModel";
            this.carModel.Size = new System.Drawing.Size(257, 52);
            this.carModel.TabIndex = 1;
            this.carModel.Text = "车型";
            this.carModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // carType
            // 
            this.carType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.carType.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.carType.Location = new System.Drawing.Point(834, 20);
            this.carType.Name = "carType";
            this.carType.Size = new System.Drawing.Size(241, 52);
            this.carType.TabIndex = 2;
            this.carType.Text = "类型";
            this.carType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // barCodes
            // 
            this.barCodes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barCodes.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.barCodes.Location = new System.Drawing.Point(260, 20);
            this.barCodes.Name = "barCodes";
            this.barCodes.Size = new System.Drawing.Size(281, 52);
            this.barCodes.TabIndex = 3;
            this.barCodes.Text = "主条码";
            this.barCodes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PickOrderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barCodes);
            this.Controls.Add(this.carType);
            this.Controls.Add(this.carModel);
            this.Controls.Add(this.serialNo);
            this.Name = "PickOrderControl";
            this.Size = new System.Drawing.Size(1089, 110);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label serialNo;
        private System.Windows.Forms.Label carModel;
        private System.Windows.Forms.Label carType;
        private System.Windows.Forms.Label barCodes;
    }
}
