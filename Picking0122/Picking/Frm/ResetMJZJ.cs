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
    public partial class ResetMJZJ : Form
    {
        public string MasterBarCode { get; set; }
        public ResetMJZJ()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void ResetMJZJ_Load(object sender, EventArgs e)
        {

        }

        private void ResetMJZJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            MasterBarCode += e.KeyChar.ToString().Replace("\r", "");
            if (e.KeyChar == 13)
            {
                this.label2.Text = MasterBarCode;
                MasterBarCode = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BLL.T_ReSetMJZJ t_ReSetMJZJ = new BLL.T_ReSetMJZJ();

            string barcode = this.label2.Text.ToString();
            if (!string.IsNullOrEmpty(barcode))
            {
                if (barcode.Length > 10)
                {
                    int result = t_ReSetMJZJ.UpdateMJZJ(this.label2.Text);

                    if (result == 1)
                    {
                        HZ hZ = new HZ("更新成功");
                        hZ.ShowDialog();
                        this.label2.Text = "";
                    }
                    else
                    {
                        HZ hZ = new HZ("更新失败。。。");
                        hZ.ShowDialog();
                    }
                }
                else
                {
                    HZ hZ = new HZ("扫描主条码有误！请重新扫");
                    hZ.ShowDialog();
                }
            }
            else
            {
                HZ hZ = new HZ("请先扫描条码");
                hZ.ShowDialog();
            }




            this.label1.Focus();
        }

        private void label2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.label2.Text = MasterBarCode;
                MasterBarCode = "";
            }
        }

        private void ResetMJZJ_Activated_1(object sender, EventArgs e)
        {
            this.label1.Focus();
        }
    }
}
