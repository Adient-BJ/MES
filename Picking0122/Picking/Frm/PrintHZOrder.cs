using Picking.XML;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Picking.Frm
{
    public partial class PrintHZOrder : Form
    {
        private string PrintName { get; set; }
        public PrintHZOrder()
        {
            InitializeComponent();

            //自动打印程序
            //timer1.Interval = 5000;
            //timer1.Start();
            //timer1.Enabled = true;
        }

        private bool IsOpenPrint = false;

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsOpenPrint)
            { 
                if (this.comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("请选择打印机");
                    return;
                }

                string printNames = this.comboBox1.SelectedItem.ToString();
                if (string.IsNullOrEmpty((printNames)))
                {
                    MessageBox.Show("请选择打印机");
                    return;
                }
                //XDocument xmlDocument = XDocument.Load("Config.xml");
                //XElement xElement = new XElement("appSettings");
                //xElement.SetElementValue("Print",printNames);
                //xElement.Save("Config.xml");
                XML.XmlConfig.PrintName = printNames;
                MessageBox.Show("启动成功");
                timer1.Interval = 10000;
                timer1.Start(); 

                IsOpenPrint = true;

                button1.Text = "关闭自动打印程序";

            }
            else
            {
                timer1.Enabled = false;
                IsOpenPrint = false;
                MessageBox.Show("关闭成功");
                button1.Text = "开启自动打印程序";
            }
        }

        private void PrintHZOrder_Load(object sender, EventArgs e)
        {

            try
            {
                foreach (string sPrint in PrinterSettings.InstalledPrinters)//获取所有打印机名称
                {
                    this.comboBox1.Items.Add(sPrint);
                }
                PrintName = XML.XmlConfig.PrintName; //XmlConfig.GetIPXML();
                this.comboBox1.SelectedItem = PrintName;
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString());
            }
        }


        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        BLL.T_Verifying t_Verifying = new BLL.T_Verifying();
        PrintHzOrders printHzOrders = new PrintHzOrders();
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedItem == null)
            {
                MessageBox.Show("请选择打印机");
                return;
            }

            string printNames = this.comboBox1.SelectedItem.ToString();
            if (string.IsNullOrEmpty((printNames)))
            {
                MessageBox.Show("请选择打印机");
                return;
            }
            string produtNo = textBox1.Text;
            if (!string.IsNullOrEmpty(produtNo))
            {
                string result = printHzOrders.PrintHZOrders( produtNo, printNames);
                if (!string.IsNullOrEmpty(result))
                {
                    t_Verifying.MarkHZOrder(result, 1);
                    MessageBox.Show("打印成功");
                }
                else
                {
                    MessageBox.Show("补打失败,可能没有找到合装信息");
                }
            }
            else
            {
                MessageBox.Show("请输入生产号");
            }
            GC.Collect();//强行销毁
            //string printer = printNames;// XML.XmlConfig.PrintName; //XML.XmlConfig.GetIPXML();
            //BLL.T_Verifying t_Verifying = new BLL.T_Verifying();
            //if (!string.IsNullOrEmpty(printer))
            //{
            //    PrintHzOrders printHzOrders = new PrintHzOrders(); 
            //    string path = @"D:\合装单打印模板.xlsx";

            //    string produtNo = textBox1.Text;
            //    if (!string.IsNullOrEmpty(produtNo))
            //    {
            //        string result = printHzOrders.PrintHZOrders(path, produtNo, printNames);
            //        if (!string.IsNullOrEmpty(result))
            //        {
            //            t_Verifying.MarkHZOrder(result, 1);
            //            MessageBox.Show("打印成功");
            //        }
            //        else
            //        {
            //            MessageBox.Show(result);
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("请输入生产号");
            //    }
            //    GC.Collect();//强行销毁
            //}
            //else
            //{
            //    MessageBox.Show("请先选择一个有效的打印机");
            //}

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            try
            {
                BLL.T_Verifying t_Verifying = new BLL.T_Verifying();
                DataTable dt = t_Verifying.GetPrintHZOrderList();
                if (dt.Rows.Count > 0)
                { 
                    foreach (DataRow item in dt.Rows)
                    {
                        TimingPrintOrder timingPrintOrder = new TimingPrintOrder();
                        timingPrintOrder.PrintEntruckOrderNew(item); 
                            t_Verifying.MarkHZOrder(item["HZOrderGID"].ToString(), 1);  
                    }
                    GC.Collect();//强行销毁
                }

                //if (dt != null)
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //        //string path = @"D:\1.xlsx";
                //        //if (!System.IO.File.Exists(path))
                //        //{
                //        //    MessageBox.Show("未找到模板地址");

                //        //}

                //        TimingPrintOrder timingPrintOrder = new TimingPrintOrder();
                //        string result = timingPrintOrder.PrintEntruckOrderNew(); 
                //        //MessageBox.Show(result);
                //        if (!string.IsNullOrEmpty((result)))
                //        {
                //            t_Verifying.MarkHZOrder(result, 1);
                //        }
                //        GC.Collect();//强行销毁
                //    }
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        
    }
}
