using Picking.Control;
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
    public partial class Forwarding : Form
    {
        private string productionNo;

        public Forwarding()
        {
            InitializeComponent();
        }

        private void Forwarding_Load(object sender, EventArgs e)
        {
            Frm_Initialize();
        }

        #region 初始化窗体组件
        private void Frm_Initialize()
        {
            this.BackColor = Color.White;
            int width = this.Width;
            int height = this.Height;
            Panel p_top = new Panel();
            p_top.Width = width;
            p_top.Height = height / 15;
            p_top.Location = new Point(0, 0);
            p_top.BackColor = Color.FromArgb(240, 244, 247);

            PictureBox logo = new PictureBox();
            logo.Width = width / 8;
            logo.Height = p_top.Height;
            logo.Image = Properties.Resources.logo;
            logo.Location = new Point(0, 0);
            logo.SizeMode = PictureBoxSizeMode.StretchImage;
            logo.Click += Exit;


            Label lb_top = new Label();
            lb_top.Width = width / 2;
            lb_top.Height = p_top.Height;
            lb_top.Text = "北京安道拓打印分装单系统";
            lb_top.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lb_top.Font = new Font("微软雅黑", 22, FontStyle.Regular);

            lb_top.Location = new Point(width / 2 - lb_top.Width / 2, 0);
            lb_top.AutoSize = false;

            Panel p_mid = new Panel();
            p_mid.Width = width;
            p_mid.Height = height - p_top.Height;
            p_mid.Location = new Point(0, p_top.Height);
            // p_mid.BackColor = Color.Orange;


            this.scanTitle.Size = new Size(Convert.ToInt32(this.Width * 0.268), Convert.ToInt32(this.Height * 0.07));
            this.scanTitle.Location = new Point(Convert.ToInt32(this.Width * 0.09), Convert.ToInt32(this.Height * 0.1));

            this.scanBox.Size = new Size(Convert.ToInt32(this.Width * 0.457), Convert.ToInt32(this.Height * 0.07));
            this.scanBox.Location = new Point(this.scanTitle.Right, this.Height / 10);


            this.EDIQueue.Size = new Size(Convert.ToInt32(this.Width * 0.35), Convert.ToInt32(this.Height * 0.575));
            this.EDIQueue.Location = new Point(Convert.ToInt32(this.Width * 0.07), this.Height / 5 + this.EDIQueue.Height / 8);

            this.Title.Size = new Size(Convert.ToInt32(this.Width * 0.35), this.EDIQueue.Height / 8);
            this.Title.Location = new Point(Convert.ToInt32(this.Width * 0.07), this.Height / 5);

            this.errorInfo.Size = new Size(this.Width / 3, this.EDIQueue.Height - 30);
            this.errorInfo.Location = new Point(this.EDIQueue.Width + this.Width / 6, this.Height / 5);
            this.errorInfo.Text = "提示信息";

            this.print.Size = new Size(352, 61);
            this.print.Location = new Point(Convert.ToInt32(this.Width * 0.58), Convert.ToInt32(this.Height * 0.8));

            

            p_top.Controls.Add(lb_top);
            p_top.Controls.Add(logo);

            this.Controls.Add(p_top);
            this.Controls.Add(p_mid);

        }

        #endregion


        #region 扫描条码

        private void Forwarding_KeyPress(object sender, KeyPressEventArgs e)
        {
            productionNo += e.KeyChar.ToString().Replace("\r", "");
            BLL.T_JISA t_JISA = new BLL.T_JISA();

            if (e.KeyChar == 13)
            {
                BLL.T_Verifying t_Verifying = new BLL.T_Verifying();
                DataTable dt = t_Verifying.GetHZOrder();
                List<string> product = dt.AsEnumerable().Select(d => d.Field<string>("ProductNo")).ToList();


                if (product.Contains(productionNo))
                {
                    DataTable tt = t_JISA.GetJISA(2);
                    string p = tt.Rows[0]["ProductionNumber"].ToString();
                    if(p==productionNo)
                    {
                        t_JISA.SaveJISA(p, 2);
                        ShowEDIInfo();
                        
                    }
                }
                else
                {
                    HZ hZ = new HZ("未找到生产号");
                    hZ.ShowDialog();
                }
            }
            this.scanTitle.Focus();

        }
    
        private void Forwarding_Activated(object sender, EventArgs e)
        {
            this.scanTitle.Focus();
        }


        #endregion


        #region 打印装车单
        private void print_Click(object sender, EventArgs e)
        {
            BLL.T_JISA t_JISA = new BLL.T_JISA();
            BLL.T_FZOrderConfirm t_FZOrderConfirm = new BLL.T_FZOrderConfirm();
            if (Model.FZOrderModel.ProductNo != null)
            {
                if (Model.FZOrderModel.ProductNo.Count > 0)
                {
                    foreach (string item in Model.FZOrderModel.ProductNo)
                    {

                        t_JISA.SaveFZOrderRecord(item);

                        string ExcelPath1 = @"D:\左座椅分装单模板.xlsx";
                        DataTable dtL = t_JISA.GetFZOrderL(item);
                        string ExcelPath2 = @"D:\右座椅分装单模板.xlsx";
                        DataTable dtR = t_JISA.GetFZOrderR(item);
                        BLL.PrintFZOrder printFZOrder = new BLL.PrintFZOrder();
                        if(dtL.Rows.Count>0)
                        {
                            printFZOrder.PrintOrder(ExcelPath1, dtL,0);
                            t_FZOrderConfirm.SaveFZOrderState(item, 1,0);
                        }
                        else
                        {
                            HZ hz = new HZ("未找到左分装单");
                            hz.ShowDialog();
                        }

                        if(dtR.Rows.Count>0)
                        {
                            printFZOrder.PrintOrder(ExcelPath2, dtR,1);
                            t_FZOrderConfirm.SaveFZOrderState(item, 1, 1);
                        }
                        else
                        {
                            HZ hz = new HZ("未找到右分装单");
                            hz.ShowDialog();
                        }
                       

                        t_JISA.SaveJISA(item, 1);

                    }
                }

            }

        }
        #endregion


        #region 查询EDI信息

        private void Title_Click(object sender, EventArgs e)
        {
            ShowEDIInfo();
        }

        private void ShowEDIInfo()
        {
            this.EDIQueue.Controls.Clear();

            BLL.T_JISA t_JISA = new BLL.T_JISA();
            DataTable dt = t_JISA.GetJISA(2);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    int locationX = 0;
                    int locationY = 0;

                    foreach (DataRow item in dt.Rows)
                    {

                        FZPrintControl fz = new FZPrintControl();
                        fz.Name = item["ProductionNumber"].ToString();
                        fz.productNo = item["ProductionNumber"].ToString();
                        fz.carType = item["CarType"].ToString();
                        fz.carModelName = item["CarModelName"].ToString();

                        int state = t_JISA.GetJISAState(item["ProductionNumber"].ToString());
                        if (state == 1)
                        {
                            fz.FontColor = Color.Blue;
                        }
                        else
                        {
                            fz.FontColor = Color.Black;
                        }

                        fz.SetInfo(this.EDIQueue.Width, this.EDIQueue.Height / 12);
                        fz.Location = new Point(locationX, locationY);

                        this.EDIQueue.Controls.Add(fz);
                        locationY += fz.Height;
                    }
                }

            }
            else
            {
                MessageBox.Show("未找到JISA信息");
            }

        }


        #endregion

        public void Exit(object sender, EventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
        }

     
    }
}
