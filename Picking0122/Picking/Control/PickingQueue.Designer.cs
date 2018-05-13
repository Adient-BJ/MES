namespace Picking.Control
{
    partial class PickingQueue
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
            this.partNo = new System.Windows.Forms.Label();
            this.seatNo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // partNo
            // 
            this.partNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.partNo.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.partNo.Location = new System.Drawing.Point(44, 19);
            this.partNo.Name = "partNo";
            this.partNo.Size = new System.Drawing.Size(152, 23);
            this.partNo.TabIndex = 0;
            this.partNo.Text = "零件号";
            this.partNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // seatNo
            // 
            this.seatNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.seatNo.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.seatNo.Location = new System.Drawing.Point(221, 19);
            this.seatNo.Name = "seatNo";
            this.seatNo.Size = new System.Drawing.Size(152, 23);
            this.seatNo.TabIndex = 1;
            this.seatNo.Text = "货位号";
            this.seatNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PickingQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.seatNo);
            this.Controls.Add(this.partNo);
            this.Name = "PickingQueue";
            this.Size = new System.Drawing.Size(596, 62);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label partNo;
        private System.Windows.Forms.Label seatNo;
    }
}
