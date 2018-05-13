using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Picking.Frm
{
    public partial class ImportKuWei : Form
    {
        public ImportKuWei()
        {
            InitializeComponent();
        }
        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr1"].ConnectionString;
        #endregion
        DB.SqlHelper sqlHelper = new DB.SqlHelper();

        private void button1_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < int.Parse(textBox1.Text); i++)
            {
                
                string ss = textBox2.Text.Trim() + (i+1).ToString("00");
                sqlHelper.ExecuteNonQuery(connstr, $"insert into T_KuWei (LocationNo,state,SerialNo) values ('{ss}',0,{textBox3.Text.Trim()})");
                //MessageBox.Show(ss);
            }
        }
    }
}
