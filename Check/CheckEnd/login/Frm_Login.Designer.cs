namespace Mes.login
{
    partial class Frm_Login
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lb_ZhiWen_Msg = new System.Windows.Forms.Label();
            this.pic = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lb_user_msg = new System.Windows.Forms.Label();
            this.but_Login = new System.Windows.Forms.Button();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_user = new System.Windows.Forms.TextBox();
            this.lb_password = new System.Windows.Forms.Label();
            this.lb_user = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(142, 47);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(665, 533);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lb_ZhiWen_Msg);
            this.tabPage1.Controls.Add(this.pic);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(657, 499);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "指纹登入";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lb_ZhiWen_Msg
            // 
            this.lb_ZhiWen_Msg.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_ZhiWen_Msg.Location = new System.Drawing.Point(6, 407);
            this.lb_ZhiWen_Msg.Name = "lb_ZhiWen_Msg";
            this.lb_ZhiWen_Msg.Size = new System.Drawing.Size(645, 45);
            this.lb_ZhiWen_Msg.TabIndex = 1;
            this.lb_ZhiWen_Msg.Text = "请输入您的指纹...";
            this.lb_ZhiWen_Msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(204, 146);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(220, 175);
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lb_user_msg);
            this.tabPage2.Controls.Add(this.but_Login);
            this.tabPage2.Controls.Add(this.txt_password);
            this.tabPage2.Controls.Add(this.txt_user);
            this.tabPage2.Controls.Add(this.lb_password);
            this.tabPage2.Controls.Add(this.lb_user);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(657, 499);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "用户名登入";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lb_user_msg
            // 
            this.lb_user_msg.Location = new System.Drawing.Point(6, 438);
            this.lb_user_msg.Name = "lb_user_msg";
            this.lb_user_msg.Size = new System.Drawing.Size(645, 34);
            this.lb_user_msg.TabIndex = 3;
            this.lb_user_msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // but_Login
            // 
            this.but_Login.Location = new System.Drawing.Point(252, 339);
            this.but_Login.Name = "but_Login";
            this.but_Login.Size = new System.Drawing.Size(182, 42);
            this.but_Login.TabIndex = 2;
            this.but_Login.Text = "登  入";
            this.but_Login.UseVisualStyleBackColor = true;
            this.but_Login.Click += new System.EventHandler(this.but_Login_Click);
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(252, 220);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(301, 29);
            this.txt_password.TabIndex = 1;
            // 
            // txt_user
            // 
            this.txt_user.Location = new System.Drawing.Point(252, 133);
            this.txt_user.Name = "txt_user";
            this.txt_user.Size = new System.Drawing.Size(301, 29);
            this.txt_user.TabIndex = 1;
            // 
            // lb_password
            // 
            this.lb_password.AutoSize = true;
            this.lb_password.Location = new System.Drawing.Point(187, 224);
            this.lb_password.Name = "lb_password";
            this.lb_password.Size = new System.Drawing.Size(46, 21);
            this.lb_password.TabIndex = 0;
            this.lb_password.Text = "密码:";
            // 
            // lb_user
            // 
            this.lb_user.AutoSize = true;
            this.lb_user.Location = new System.Drawing.Point(171, 136);
            this.lb_user.Name = "lb_user";
            this.lb_user.Size = new System.Drawing.Size(62, 21);
            this.lb_user.TabIndex = 0;
            this.lb_user.Text = "用户名:";
            // 
            // Frm_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 626);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_Login";
            this.Load += new System.EventHandler(this.Frm_Login_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lb_ZhiWen_Msg;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lb_user_msg;
        private System.Windows.Forms.Button but_Login;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txt_user;
        private System.Windows.Forms.Label lb_password;
        private System.Windows.Forms.Label lb_user;
    }
}