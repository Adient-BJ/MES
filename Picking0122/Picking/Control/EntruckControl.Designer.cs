namespace Picking.Control
{
    partial class EntruckControl
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
            this.scanTitle = new System.Windows.Forms.Label();
            this.scanBox = new System.Windows.Forms.Label();
            this.d1 = new System.Windows.Forms.Label();
            this.z1 = new System.Windows.Forms.Label();
            this.m1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // scanTitle
            // 
            this.scanTitle.BackColor = System.Drawing.SystemColors.ControlDark;
            this.scanTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scanTitle.Font = new System.Drawing.Font("微软雅黑", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.scanTitle.Location = new System.Drawing.Point(30, 3);
            this.scanTitle.Name = "scanTitle";
            this.scanTitle.Size = new System.Drawing.Size(189, 79);
            this.scanTitle.TabIndex = 0;
            this.scanTitle.Text = "扫描左前座椅条码";
            this.scanTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scanBox
            // 
            this.scanBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scanBox.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.scanBox.Location = new System.Drawing.Point(242, 3);
            this.scanBox.Name = "scanBox";
            this.scanBox.Size = new System.Drawing.Size(465, 79);
            this.scanBox.TabIndex = 1;
            this.scanBox.Text = "label1";
            this.scanBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // d1
            // 
            this.d1.BackColor = System.Drawing.Color.GreenYellow;
            this.d1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.d1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold);
            this.d1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.d1.Location = new System.Drawing.Point(277, 108);
            this.d1.Name = "d1";
            this.d1.Size = new System.Drawing.Size(100, 48);
            this.d1.TabIndex = 18;
            this.d1.Text = "电检结果";
            this.d1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // z1
            // 
            this.z1.BackColor = System.Drawing.Color.GreenYellow;
            this.z1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.z1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold);
            this.z1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.z1.Location = new System.Drawing.Point(509, 108);
            this.z1.Name = "z1";
            this.z1.Size = new System.Drawing.Size(100, 48);
            this.z1.TabIndex = 17;
            this.z1.Text = "终检结果";
            this.z1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m1
            // 
            this.m1.BackColor = System.Drawing.Color.GreenYellow;
            this.m1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.m1.Location = new System.Drawing.Point(383, 108);
            this.m1.Name = "m1";
            this.m1.Size = new System.Drawing.Size(100, 48);
            this.m1.TabIndex = 16;
            this.m1.Text = "盲检结果";
            this.m1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EntruckControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.d1);
            this.Controls.Add(this.z1);
            this.Controls.Add(this.m1);
            this.Controls.Add(this.scanBox);
            this.Controls.Add(this.scanTitle);
            this.Name = "EntruckControl";
            this.Size = new System.Drawing.Size(725, 200);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label scanTitle;
        private System.Windows.Forms.Label scanBox;
        private System.Windows.Forms.Label d1;
        private System.Windows.Forms.Label z1;
        private System.Windows.Forms.Label m1;
    }
}
