namespace CheckEnd.Frms
{
    partial class ConfirmFrm
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
            this.pic = new System.Windows.Forms.PictureBox();
            this.lb_msg = new System.Windows.Forms.Label();
            this.logoPic = new System.Windows.Forms.PictureBox();
            this.remark = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoPic)).BeginInit();
            this.SuspendLayout();
            // 
            // pic
            // 
            this.pic.Image = global::CheckEnd.Properties.Resources.zw;
            this.pic.Location = new System.Drawing.Point(371, 222);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(189, 166);
            this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic.TabIndex = 1;
            this.pic.TabStop = false;
            // 
            // lb_msg
            // 
            this.lb_msg.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_msg.Location = new System.Drawing.Point(57, 449);
            this.lb_msg.Name = "lb_msg";
            this.lb_msg.Size = new System.Drawing.Size(825, 43);
            this.lb_msg.TabIndex = 2;
            this.lb_msg.Text = "请输入班长的指纹...";
            this.lb_msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // logoPic
            // 
            this.logoPic.Image = global::CheckEnd.Properties.Resources.logo;
            this.logoPic.Location = new System.Drawing.Point(302, 89);
            this.logoPic.Name = "logoPic";
            this.logoPic.Padding = new System.Windows.Forms.Padding(1);
            this.logoPic.Size = new System.Drawing.Size(348, 73);
            this.logoPic.TabIndex = 3;
            this.logoPic.TabStop = false;
            // 
            // remark
            // 
            this.remark.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.remark.Location = new System.Drawing.Point(176, 519);
            this.remark.Name = "remark";
            this.remark.Size = new System.Drawing.Size(512, 131);
            this.remark.TabIndex = 4;
            this.remark.Text = "";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(714, 532);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(168, 109);
            this.button1.TabIndex = 5;
            this.button1.Text = "提交备注";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(78, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(825, 61);
            this.label1.TabIndex = 6;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConfirmFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 758);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.remark);
            this.Controls.Add(this.logoPic);
            this.Controls.Add(this.lb_msg);
            this.Controls.Add(this.pic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConfirmFrm";
            this.Text = "请输入班长指纹";
            this.Load += new System.EventHandler(this.Frm_Login_Load);
            this.Shown += new System.EventHandler(this.ConfirmFrm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Label lb_msg;
        private System.Windows.Forms.PictureBox logoPic;
        private System.Windows.Forms.RichTextBox remark;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}