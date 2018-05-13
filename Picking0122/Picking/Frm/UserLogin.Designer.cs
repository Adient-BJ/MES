namespace Picking.Frm
{
    partial class UserLogin
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
            this.button1 = new System.Windows.Forms.Button();
            this.labUser = new System.Windows.Forms.Label();
            this.labMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(288, 455);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(232, 87);
            this.button1.TabIndex = 3;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labUser
            // 
            this.labUser.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labUser.ForeColor = System.Drawing.Color.GreenYellow;
            this.labUser.Location = new System.Drawing.Point(11, 241);
            this.labUser.Name = "labUser";
            this.labUser.Size = new System.Drawing.Size(860, 64);
            this.labUser.TabIndex = 4;
            this.labUser.Text = "等待员工扫码";
            this.labUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labMsg
            // 
            this.labMsg.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labMsg.Location = new System.Drawing.Point(0, 54);
            this.labMsg.Name = "labMsg";
            this.labMsg.Size = new System.Drawing.Size(871, 64);
            this.labMsg.TabIndex = 1;
            this.labMsg.Text = "请员工扫码确认发运";
            this.labMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 642);
            this.Controls.Add(this.labUser);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "UserLogin";
            this.Text = "员工登录";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.UserLogin_Activated);
            this.Load += new System.EventHandler(this.UserLogin_Load);
            this.Shown += new System.EventHandler(this.UserLogin_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UserLogin_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labUser;
        private System.Windows.Forms.Label labMsg;
    }
}