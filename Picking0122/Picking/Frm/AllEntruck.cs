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
    public partial class AllEntruck : Form
    {
        private static string produtionNumber;

        public AllEntruck()
        {
            InitializeComponent();
        }

        private void HeZhuang_Load(object sender, EventArgs e)
        {
            Frm_Initialize();
            this.scanTitle.Focus();
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
            logo.Click += confirm_Click;

            Label lb_top = new Label();
            lb_top.Width = width / 2;
            lb_top.Height = p_top.Height;
            lb_top.Text = "北京安道拓合装系统 V2.0";
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
            this.scanTitle.Location = new Point(Convert.ToInt32(this.Width * 0.15), Convert.ToInt32(this.Height * 0.1));

            this.scanBox.Size = new Size(Convert.ToInt32(this.Width * 0.457), Convert.ToInt32(this.Height * 0.07));
            this.scanBox.Location = new Point(this.scanTitle.Right, this.Height / 10);

            this.scanDes.Size = new Size(Convert.ToInt32(this.Width * 0.81), Convert.ToInt32(this.Height * 0.7));
            this.scanDes.Location = new Point(this.Width / 10, this.Height / 5);

            this.button1.Location = new Point(1520, 989);

            p_top.Controls.Add(lb_top);
            p_top.Controls.Add(logo);

            this.Controls.Add(p_top);
            this.Controls.Add(p_mid);

        }
        #endregion

        private void confirm_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
        }

        #region 扫描条形码
        private void AllEntruck_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                produtionNumber += e.KeyChar.ToString().Replace("\r", "");

                if (e.KeyChar == 13)
                {
                    string part = produtionNumber.Substring(0, 1);

                    if (produtionNumber.Length == 7)
                    {
                        LoadList(produtionNumber);
                    }
                    else
                    {
                        if (produtionNumber.Length > 10 && produtionNumber.Length < 20)
                        {
                            if (part == "B")//整点
                            {
                                DicEn[整垫].ThisCode = produtionNumber;
                            }
                            else if (part == "C")//40
                            {
                                DicEn[背40].ThisCode = produtionNumber;
                            }
                            if (part == "A")//60
                            {
                                DicEn[背60].ThisCode = produtionNumber;
                            }
                        }
                        else if (produtionNumber.Length > 20)
                        {

                            switch (part)
                            {
                                case "A"://左前
                                    DicEn[左前].ThisCode = produtionNumber;
                                    break;
                                case "E"://右前
                                    DicEn[右前].ThisCode = produtionNumber;
                                    break;
                                case "L"://整垫
                                    DicEn[整垫].ThisCode = produtionNumber;
                                    break;
                                case "P"://整背
                                    DicEn[整背].ThisCode = produtionNumber;
                                    break;
                            }

                            ////string part2 = produtionNumber.Substring(0, 2);
                            //if (part == "A") //左前
                            //{
                            //    DicEn[左前].ThisCode = produtionNumber;
                            //}
                            //else if (part == "E") //右前
                            //{
                            //    DicEn[右前].ThisCode = produtionNumber;
                            //}
                        }

                        if (yanZheng() == true)
                        {
                            bool isTGresult = true;
                            foreach (var item in DicEn)
                            {
                                if (!item.Value.IsTG())
                                {
                                    isTGresult = false;
                                    break;
                                }
                            }
                            //bool b1 = DicEn[左前].IsTG();
                            //bool b2 = DicEn[右前].IsTG();
                            //bool b3 = DicEn[整垫].IsTG();
                            //bool b4 = DicEn[背40].IsTG();
                            //bool b5 = DicEn[背60].IsTG();
                            //bool b6 = DicEn[整背].IsTG();

                            //if (b1 && b2 && b3 && b4 && b5)
                            if (isTGresult)
                            {
                                BLL.T_Verifying t_Verifying = new BLL.T_Verifying();
                                //合装验证成功逻辑
                                string productNo = this.scanBox.Text;
                                DataTable data = t_Verifying.GetHZOrder(productNo);
                                if (data.Rows.Count > 0)
                                {
                                    string HZOrderID = data.Rows[0]["HZOrderGID"].ToString();
                                    BLL.T_KuWei t_KuWei = new BLL.T_KuWei();
                                    if (t_KuWei.SaveKuWeiInfo(productNo) == false)
                                    {
                                        HZ hZ = new HZ("库位已占满");
                                        hZ.ShowDialog();
                                    }
                                    else
                                    {
                                        t_Verifying.MarkHZOrder(HZOrderID, 2);

                                        DataTable kuwei = t_KuWei.GetKuWeiInfo(productNo);
                                        if (kuwei.Rows.Count == 1)
                                        {
                                            HZ hZ = new HZ("合装验证成功,请将物品送至 ：" + kuwei.Rows[0]["LocationNo"].ToString() + "区域中");
                                            hZ.ShowDialog();
                                            t_KuWei.SaveKuWeiState(productNo, 1);
                                        }
                                        else
                                        {
                                            HZ hZ = new HZ("未找到库位信息，或者该生产号已有库位");
                                            hZ.ShowDialog();
                                        }


                                        this.scanDes.Controls.Clear();

                                        produtionNumber = "";
                                        Model.EntruckModel.ScanedBarCode = null;

                                    }
                                    //清空等待扫码
                                    scanDes.Controls.Clear();
                                    this.scanBox.Text = "";

                                }
                                else
                                {
                                    MessageBox.Show("未找到合装单信息");
                                }
                            }
                        }
                    }
                    produtionNumber = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }

        }

        private void AllEntruck_Activated(object sender, EventArgs e)
        {
            this.scanTitle.Focus();
        }

        #region 查询库位信息
        private void button1_Click(object sender, EventArgs e)
        {
            SearchKuWei searchKuWei = new SearchKuWei();
            searchKuWei.ShowDialog();
            this.scanTitle.Focus();
        }

        #endregion
        Dictionary<string, EntruckControl> DicEn = new Dictionary<string, EntruckControl>();
        static string 左前 = "左前";
        static string 右前 = "右前";
        static string 背40 = "背40";
        static string 背60 = "背60";
        static string 整垫 = "整垫";
        static string 整背 = "整背";

        string CarType;

        public void LoadList(string shengchanhao)
        {
            BLL.T_Verifying t_Verifying = new BLL.T_Verifying();
            BLL.T_EOL t_EOL = new BLL.T_EOL();
            DataTable dt = t_Verifying.GetHZOrder(shengchanhao);
            DataTable dt1 = t_Verifying.GetHZOrderP(shengchanhao);
            DicEn.Clear();
            this.scanDes.Controls.Clear();
            if (dt1.Rows.Count > 0)
            {
                HZ hZ = new HZ("此合装单已合装完成");
                hZ.ShowDialog();
                return;
            }
            this.scanBox.Text = shengchanhao;

            int locationY = 0;
            string MasterBarCodeL = dt.Rows[0]["MasterBarCodeL"].ToString();
            string MasterBarCodeR = dt.Rows[0]["MasterBarCodeR"].ToString();
            string MasterBarCodeC = dt.Rows[0]["MasterBarCodeC"].ToString();
            string MasterBarCode40 = dt.Rows[0]["MasterBarCode40"].ToString();
            string MasterBarCode60 = dt.Rows[0]["MasterBarCode60"].ToString();
            string MasterBarCodeB = dt.Rows[0]["MasterBarCodeB"].ToString();
            CarType = dt.Rows[0]["CarType"].ToString();


            EntruckControl e1 = new EntruckControl();
            int mj1 = t_Verifying.GetMJResult(MasterBarCodeL);
            e1.Is40Or60 = false;
            e1.IsZ177B = false;
            e1.TrueCode = MasterBarCodeL;
            e1.ThisCode = "";
            e1.MJR = (mj1 == 1 ? true : false);
            e1.ZJR = t_Verifying.GetZJResult(MasterBarCodeL);
            e1.DJR = t_EOL.GetEOLResult(MasterBarCodeL);
            e1.SetInfo(this.scanDes.Width, this.scanDes.Height, "请扫码左前条码");
            e1.Location = new Point(0, locationY);
            this.scanDes.Controls.Add(e1);
            locationY += e1.Height;
            DicEn.Add(左前, e1);

            EntruckControl e2 = new EntruckControl();
            int mj2 = t_Verifying.GetMJResult(MasterBarCodeR);
            e2.Is40Or60 = false;
            e2.IsZ177B = false;
            e2.TrueCode = MasterBarCodeR;
            e2.ThisCode = "";
            e2.MJR = mj2 == 1 ? true : false;
            e2.ZJR = t_Verifying.GetZJResult(MasterBarCodeR);
            e2.DJR = t_EOL.GetEOLResult(MasterBarCodeR);
            e2.SetInfo(this.scanDes.Width, this.scanDes.Height, "请扫码右前条码");
            e2.Location = new Point(0, locationY);
            this.scanDes.Controls.Add(e2);
            locationY += e2.Height;
            DicEn.Add(右前, e2);

            EntruckControl e3 = new EntruckControl();
            int mj3 = t_Verifying.GetMJResult(MasterBarCodeC);
            e3.Is40Or60 = true;
            e3.IsZ177B = false; ;
            e3.TrueCode = MasterBarCodeC;
            e3.ThisCode = "";
            e3.MJR = mj3 == 1 ? true : false;
            e3.ZJR = t_Verifying.GetZJResult(MasterBarCodeC);
            e3.DJR = t_EOL.GetEOLResult(MasterBarCodeC);
            e3.SetInfo(this.scanDes.Width, this.scanDes.Height, "请扫码整垫");
            e3.Location = new Point(0, locationY);
            this.scanDes.Controls.Add(e3);
            locationY += e3.Height;
            DicEn.Add(整垫, e3);

            if (CarType == "X156")
            {
                EntruckControl e4 = new EntruckControl();
                int m4 = t_Verifying.GetMJResult(MasterBarCode40);
                e4.Is40Or60 = true;
                e4.IsZ177B = false;
                e4.TrueCode = MasterBarCode40;
                e4.ThisCode = "";
                e4.MJR = m4 == 1 ? true : false;
                e4.ZJR = t_Verifying.GetZJResult(MasterBarCode40);
                e4.DJR = t_EOL.GetEOLResult(MasterBarCode40);
                e4.SetInfo(this.scanDes.Width, this.scanDes.Height, "请扫码40");
                e4.Location = new Point(0, locationY);
                this.scanDes.Controls.Add(e4);
                locationY += e4.Height;
                DicEn.Add(背40, e4);

                EntruckControl e5 = new EntruckControl();
                e5.Is40Or60 = true;
                e5.IsZ177B = false;
                int m5 = t_Verifying.GetMJResult(MasterBarCode60);
                e5.TrueCode = MasterBarCode60;
                e5.ThisCode = "";
                e5.MJR = m5 == 1 ? true : false;
                e5.ZJR = t_Verifying.GetZJResult(MasterBarCode60);
                e5.DJR = t_EOL.GetEOLResult(MasterBarCode60);
                e5.SetInfo(this.scanDes.Width, this.scanDes.Height, "请扫码60");
                e5.Location = new Point(0, locationY);
                this.scanDes.Controls.Add(e5);
                locationY += e5.Height;
                DicEn.Add(背60, e5);
            }
            else if (CarType == "Z177")
            {
                EntruckControl e4 = new EntruckControl();
                int m4 = t_Verifying.GetMJResult(MasterBarCodeB);
                e4.Is40Or60 = false;
                e4.IsZ177B = true;
                e4.TrueCode = MasterBarCodeB;
                e4.ThisCode = "";
                e4.MJR = m4 == 1 ? true : false;
                e4.ZJR = t_Verifying.GetZJResult(MasterBarCodeB);
                e4.DJR = t_EOL.GetRearEOLResult(MasterBarCodeB);
                e4.SetInfo(this.scanDes.Width, this.scanDes.Height, "请扫码整背");
                e4.Location = new Point(0, locationY);
                this.scanDes.Controls.Add(e4);
                locationY += e4.Height;
                DicEn.Add(整背, e4);

            }
        }

        private bool yanZheng()
        {
            bool result = true;

            if (DicEn.Count <= 0)
            {
                result = false;
            }
            else
            {
                foreach (var item in DicEn)
                {
                    if (item.Value.IsText == false)
                    {
                        result = false;
                    }
                }
            }

            return result;
        }
    }
}

#endregion

