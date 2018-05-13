using System;
using System.Data;
using System.Windows.Forms;
using Picking.BLL;

namespace Picking.Frm
{
    public partial class InitialKuWei : Form
    {
        public static string produtionNumber;

        public InitialKuWei()
        {
            InitializeComponent();
            label1.Focus();
            label3.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            T_KuWei kuWei = new T_KuWei();
            kuWei.StateClearAll();
            produtionNumber = "";
            HZ Hz=new HZ("操作成功");
            Hz.ShowDialog();
            textBox1.Focus();
        }
 
        private void InitialKuWei_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                produtionNumber += e.KeyChar.ToString().Replace("\r", "");

                if (e.KeyChar == 13)
                {
                    T_KuWei t_KuWei = new BLL.T_KuWei();
                    string locationNo = t_KuWei.IsInStock(produtionNumber);

                    if (!string.IsNullOrEmpty(locationNo))
                    {
                        this.label3.Text = "该合装单已入库库位信息为：" + locationNo;
                    }
                    else
                    {
                        if (t_KuWei.SaveKuWeiInfo(produtionNumber) == false)
                        {
                            HZ hZ = new HZ("库位已占满");
                        }
                        else
                        {
                            DataTable kuwei = t_KuWei.GetKuWeiInfo(produtionNumber);
                            //if (kuwei.Rows.Count == 1)
                            //{
                            label3.Text=("扫描成功,请将物品送至 ：" + kuwei.Rows[0]["LocationNo"] + "区域中");
                                t_KuWei.SaveKuWeiState(produtionNumber, 1);
                            //}
                            //else
                            //{
                            //    HZ hZ = new HZ("未找到库位信息，或者该生产号已有库位");
                            //    hZ.ShowDialog();
                            //}
                        }
                    }
                    produtionNumber = "";
                    textBox1.Text = "";
                    textBox1.Focus();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    }
    
    
    


