using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckEnd.Frms
{
    public partial class PwdLogin : Form
    {
        public PwdLogin()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = this.userName.Text;

            string userPwd = this.userPwd.Text;

            Bll.T_User t_User = new Bll.T_User();

            string a = t_User.UserLogin(userName, userPwd);

            if(a!="")
            {
                MangJianFrm f = new MangJianFrm();
                f.Owner = this;
                this.Hide();
                f.ShowDialog();
                Application.ExitThread();

            }
            else
            {
                MessageBox.Show("用户名密码错误！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Frm_Login f = new Frm_Login();
            f.Owner = this;
            this.Hide();
            f.ShowDialog();
            Application.ExitThread();
        }
    }
}
