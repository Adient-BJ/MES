using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Picking.Frm
{
    public partial class UserLogin : Form
    {
        public UserLogin(string msg)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.labMsg.Text = msg;
        }

        /// <summary>
        /// 发运的生产号，右可能是两两发运过来就是两个，右可能单两份发运，就是一个。
        /// </summary>
        public List<string> ProductionNumber { get; set; }
        /// <summary>
        /// 掉用赵工存储过程需要的jisa序列,和上边也是顺序关系
        /// </summary>
        public List<string> JISANo { get; set; }

        private void UserLogin_Load(object sender, EventArgs e)
        {
            Model.EntruckModel.FaYunUser = "";
            this.labMsg.Focus();
        }

        private string userID = "";
        private void UserLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            userID += e.KeyChar.ToString().Replace("\r", "");
            if (e.KeyChar == 13)
            {
                BLL.T_JISA t_JISA = new BLL.T_JISA();
                string user = t_JISA.GetUserNamebyID(userID);
                if (!string.IsNullOrEmpty(user))
                {
                    Model.EntruckModel.FaYunUser = user;
                    this.labUser.Text = user;
                }
                else
                {
                    this.labUser.Text = "未找到员工信息";
                }
                userID = "";
            }
        }

        BLL.T_JISA t_JISA = new BLL.T_JISA();
        private void button1_Click(object sender, EventArgs e)
        {
            string txt = this.labUser.Text;
            if (txt.Length > 4)
            {
                MessageBox.Show("员工未扫码或者扫码错误");
            }
            else
            {
                string GroupCode = Guid.NewGuid().ToString().Replace("-", "");
                for (int i = 0; i < ProductionNumber.Count; i++)
                {
                    //保存发运状态
                    t_JISA.SetFaYun(ProductionNumber[i], txt, GroupCode);
                    //调用赵工存储过程
                    t_JISA.ExcuteJisA(ProductionNumber[i], JISANo[i]);
                }
                this.Close();
            }

            this.labMsg.Focus();
        }

        private void UserLogin_Activated(object sender, EventArgs e)
        {
            this.labMsg.Focus();
        }

        private void UserLogin_Shown(object sender, EventArgs e)
        {
            this.labMsg.Focus();
        }
    }
}
