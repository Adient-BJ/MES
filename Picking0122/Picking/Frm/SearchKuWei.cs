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
    public partial class SearchKuWei : Form
    {
        public SearchKuWei()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string productNo = this.textBox1.Text;
                if (string.IsNullOrEmpty(productNo))
                {
                    this.info.Text = "请输入生产号";
                }
                else
                {
                    BLL.T_KuWei t_KuWei = new BLL.T_KuWei(); 
                    string locationNo = t_KuWei.SearchKuWei(productNo); 
                    if(!string.IsNullOrEmpty(locationNo))
                    {
                        this.info.Text = "该生产号所对应库位信息为：" + locationNo;
                    }
                    else
                    {
                        this.info.Text = "该生产号无对应信息";
                    }
       
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }




    }
}
