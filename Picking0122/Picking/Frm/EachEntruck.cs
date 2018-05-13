
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
    public partial class EachEntruck : Form
    {

        private static string productionNo { get; set; }


        public EachEntruck()
        {
            InitializeComponent();
        }

        private void EachEntruck_Load(object sender, EventArgs e)
        {
            Frm_Initialize();

            ShowJISANo();
            //searchfayun.S_BTime = this.beginDate.Value.ToString();
            //S_ETime = this.endDate.Value.ToString();
            SearchFayun.S_BTime = DateTime.Now.AddDays(-1).ToString();
            SearchFayun.S_ETime = DateTime.Now.ToString();
            ShowJISAed();
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
            lb_top.Text = "北京安道拓发运系统 V2.0";
            lb_top.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lb_top.Font = new Font("微软雅黑", 22, FontStyle.Regular);

            lb_top.Location = new Point(width / 2 - lb_top.Width / 2, 0);
            lb_top.AutoSize = false;

            Panel p_mid = new Panel();
            p_mid.Width = width;
            p_mid.Height = height - p_top.Height;
            p_mid.Location = new Point(0, p_top.Height);
            // p_mid.BackColor = Color.Orange;

            this.TitleNo.Size = new Size(Convert.ToInt32(this.Width * 0.22), Convert.ToInt32(this.Height * 0.09));
            this.TitleNo.Location = new Point(Convert.ToInt32(this.Width * 0.22), Convert.ToInt32(this.Height * 0.1));

            this.scanBox.Size = new Size(Convert.ToInt32(this.Width * 0.23), Convert.ToInt32(this.Height * 0.09));
            this.scanBox.Location = new Point(this.TitleNo.Right, 107);

            this.scanBox1.Size = this.scanBox.Size;
            this.scanBox1.Location = new Point(this.scanBox.Right + 30, 107);

            this.tabControl1.Size = new Size(720, 670);
            this.tabControl1.Location = new Point(35, 220);

            this.dataGridView1.Size = this.tabControl1.Size;
            this.dataGridView1.Location = new Point(0, 0);

            this.dataGridView2.Size = this.tabControl1.Size;
            this.dataGridView2.Location = new Point(0, 0);

            this.scanInfo.Size = new Size(1095, 650);
            this.scanInfo.Location = new Point(789, 242);

            this.errorInfo.Size = new Size(Convert.ToInt32(this.Width * 0.5), Convert.ToInt32(this.Height * 0.17));
            this.errorInfo.Location = new Point(Convert.ToInt32(this.Width * 0.35), Convert.ToInt32(this.Height * 0.83));

            //this.panel1.Size = new Size(this.scanInfo.Width, this.scanInfo.Height / 5);
            //this.panel1.Location = new Point(0, 0);
            //this.l.Size = new Size(Convert.ToInt32(this.panel1.Width * 3 / 7), this.panel1.Height);
            //this.l.Location = new Point(0, 0);
            //this.s1.Size = new Size(Convert.ToInt32(this.panel1.Width * 4 / 7), this.panel1.Height);
            //this.s1.Location = new Point(this.l.Right, 0);

            //this.panel2.Size = this.panel1.Size;
            //this.panel2.Location = new Point(0, this.panel1.Height);
            //this.r.Size = this.l.Size;
            //this.r.Location = new Point(0, 0);
            //this.s2.Size = this.s1.Size;
            //this.s2.Location = new Point(this.r.Right, 0);

            //this.panel3.Size = this.panel1.Size;
            //this.panel3.Location = new Point(0, this.panel1.Height * 2);
            //this.c.Size = this.r.Size;
            //this.c.Location = new Point(0, 0);
            //this.s3.Size = this.s1.Size;
            //this.s3.Location = new Point(this.c.Right, 0);

            //this.panel4.Size = this.panel1.Size;
            //this.panel4.Location = new Point(0, this.panel1.Height * 3);
            //this.b40.Size = this.l.Size;
            //this.b40.Location = new Point(0, 0);
            //this.s4.Size = this.s1.Size;
            //this.s4.Location = new Point(this.b40.Right, 0);

            //this.panel5.Size = this.panel1.Size;
            //this.panel5.Location = new Point(0, this.panel1.Height * 4);
            //this.b60.Size = this.l.Size;
            //this.b60.Location = new Point(0, 0);
            //this.s5.Size = this.s1.Size;
            //this.s5.Location = new Point(this.b60.Right, 0);


            this.search.Location = new Point(100, 936);

            this.pass.Location = new Point(this.search.Right + 60, 936);
            this.forceShip.Location = new Point(this.pass.Right + 60, 936);

            p_top.Controls.Add(lb_top);
            p_top.Controls.Add(logo);

            this.Controls.Add(p_top);
            this.Controls.Add(p_mid);
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
        }

        #endregion

        #region 展示JISA序
        DataTable dt1 = new DataTable();
        private void ShowJISANo()
        {
            BLL.T_JISA t_JISA = new BLL.T_JISA();
            dt1 = t_JISA.GetJISAWait();

            DataTable dt = new DataTable();
            dt.Columns.Add("生产号");
            dt.Columns.Add("JISA");
            dt.Columns.Add("状态");

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                DataRow item = dt1.Rows[i];
                string State = item["State"].ToString();
                string LocationNo = item["LocationNo"].ToString();
                //if (string.IsNullOrEmpty(LocationNo)) { LocationNo = "无"; }
                //if (State == "0") { State = "未入库"; }
                //if (State == "1") { if (LocationNo != "无") { State = LocationNo; } else { State = "未入库"; } }
                //if (State == "2") { State = "已出库"; }
                //if (State == "3") { State = "已验证"; }
                if (!string.IsNullOrEmpty(LocationNo)) { State = LocationNo; }
                if (State == "0") { State = "未入库"; }
                if (State == "1") { State = "已打印"; }
                if (State == "2") { State = "已出库"; }
                if (State == "3") { State = "已验证"; }

                DataRow dr = dt.NewRow();
                dr[0] = item[0];
                dr[1] = item[1];
                dr[2] = State;
                dt.Rows.Add(dr);
            }

            this.dataGridView1.DataSource = dt;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (this.dataGridView1.Rows[i].Cells[2].Value.ToString() != "无" && this.dataGridView1.Rows[i].Cells[2].Value.ToString() != "未入库" && this.dataGridView1.Rows[i].Cells[2].Value.ToString() != "已出库" && this.dataGridView1.Rows[i].Cells[2].Value.ToString() != "已验证")
                {
                    this.dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Yellow;
                }
                else if (this.dataGridView1.Rows[i].Cells[2].Value.ToString() == "已出库")
                {
                    this.dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.GreenYellow;
                }
                else if (this.dataGridView1.Rows[i].Cells[2].Value.ToString() == "未入库")
                {
                    this.dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Red;
                    errorInfo.Text = "注意:有JISA生产号没有入库!";
                }

                //判断调序
                if (i < dt.Rows.Count - 1)
                {
                    string JIAS = this.dataGridView1.Rows[i].Cells[1].Value.ToString();
                    string NextJIAS = this.dataGridView1.Rows[i + 1].Cells[1].Value.ToString();
                    if (NextJIAS.Substring(NextJIAS.Length - 3, 3) != "001")
                    {
                        if ((int.Parse(JIAS.Substring(JIAS.Length - 3, 3)) + 1).ToString().PadLeft(3, '0') != NextJIAS.Substring(NextJIAS.Length - 3, 3))
                        {
                            this.dataGridView1.Rows[i + 1].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                    }
                }
            }

        }
        DataTable dt2 = new DataTable();
        private void ShowJISAed()
        {
            BLL.T_JISA t_JISA = new BLL.T_JISA();
            dt2 = t_JISA.GetYiFaYun(SearchFayun.S_SHC, SearchFayun.S_BTime, SearchFayun.S_ETime, SearchFayun.S_User);
            this.dataGridView2.DataSource = dt2;
        }
        #endregion

        private Color fyColor = Color.Aquamarine;
        private Color GetFYColor()
        {
            Color result = fyColor;
            if (fyColor == Color.Aquamarine)
            {
                fyColor = Color.AntiqueWhite;//切换下一个颜色
            }
            else if (fyColor == Color.AntiqueWhite)
            {
                fyColor = Color.Aquamarine;//切换下一个颜色
            }
            return result;
        }

        public List<string> LOrder = new List<string>();
        public List<string> ROrder = new List<string>();
        Dictionary<string, ShipControl> DicL = new Dictionary<string, ShipControl>();
        Dictionary<string, ShipControl> DicR = new Dictionary<string, ShipControl>();
        public List<ShipControl> ItemsL = new List<ShipControl>();
        public List<ShipControl> ItemsR = new List<ShipControl>();


        static string 左前 = "左前";
        static string 右前 = "右前";
        static string 背40 = "背40";
        static string 背60 = "背60";
        static string 整垫 = "整垫";
        static string 整背 = "整背";
        int locationY = 0;
        string CarType;
        string MasterBarCodeL;
        string MasterBarCodeR ;
        string MasterBarCodeC;
        string MasterBarCode40;
        string MasterBarCode60;
        string MasterBarCodeB;




        #region 扫描生产号
        private void EachEntruck_KeyPress(object sender, KeyPressEventArgs e)
        {
            productionNo += e.KeyChar.ToString().Replace("\r", "");

            //BLL.T_FZOrderConfirm t_FZOrderConfirm = new BLL.T_FZOrderConfirm();
            //DataTable dt0 = t_FZOrderConfirm.Get0FZOrder();
            //DataTable dt1 = t_FZOrderConfirm.Get1FZOrder();

            //BLL.T_Verifying t_Verifying = new BLL.T_Verifying();
            //DataTable dt = t_Verifying.GetHZOrder();
            try
            {
                if (e.KeyChar == 13)
                {
                    string LorR = productionNo.Substring(0, 1);
                    string p = productionNo.Substring(1);

                    if (p.Length == 7)
                    {
                        BLL.T_JISA t_JISA = new BLL.T_JISA();
                        DataTable dttop2 = t_JISA.GetTop2JISA();
                        //如果有数据
                        if (dttop2.Rows.Count > 0)
                        {
                            //string serialP = t_JISA.GetTop1JISA();
                            string serialP = dttop2.Rows[0][0].ToString();
                            //判断调序
                            string thisJISA = dttop2.Rows[0][1].ToString();
                            //如果大于1行，说明右两行，继续验证，
                            if (dttop2.Rows.Count > 1)
                            {
                                string nextJISA = dttop2.Rows[1][1].ToString();
                                if (nextJISA.Substring(nextJISA.Length - 3, 3) != "001")
                                {
                                    if ((int.Parse(thisJISA.Substring(thisJISA.Length - 3, 3)) + 1).ToString().PadLeft(3, '0') != nextJISA.Substring(nextJISA.Length - 3, 3))
                                    {
                                        HZ hZ = new HZ("当前第一个JISA和下一个已乱序，无法操作。。。");
                                        hZ.ShowDialog();
                                        ClearAllItems();
                                        return;
                                    }
                                }
                            }

                            if (p != serialP)
                            {
                                HZ hZ = new HZ("条码与JISA序列不匹配");
                                hZ.ShowDialog();
                                ClearAllItems();
                                return;
                            }
                            else
                            {
                                if (LorR == "0")
                                {
                                    ClearAllItems();
                                    BLL.T_Verifying t_Verifying = new BLL.T_Verifying();
                                    DataTable dt = t_Verifying.GetHZOrderP(p);

                                    MasterBarCodeL = dt.Rows[0]["MasterBarCodeL"].ToString();
                                    MasterBarCodeR = dt.Rows[0]["MasterBarCodeR"].ToString();
                                    MasterBarCodeC = dt.Rows[0]["MasterBarCodeC"].ToString();
                                    MasterBarCode40 = dt.Rows[0]["MasterBarCode40"].ToString();
                                    MasterBarCode60 = dt.Rows[0]["MasterBarCode60"].ToString();
                                    MasterBarCodeB = dt.Rows[0]["MasterBarCodeB"].ToString();
                                    CarType = dt.Rows[0]["CarType"].ToString();


                                    this.scanBox.Text = p;

                                    ShipControl FL = new ShipControl();
                                    FL.SetInfo(scanInfo.Width, scanInfo.Height / 5, "左前条码");
                                    FL.Location = new Point(0, locationY);
                                    locationY += FL.Height;
                                    scanInfo.Controls.Add(FL);
                                    FL.TrueCode = MasterBarCodeL;
                                    FL.ThisCode = "";
                                    DicL.Add(左前, FL);

                                    ShipControl FR = new ShipControl();
                                    FR.SetInfo(scanInfo.Width, scanInfo.Height / 5, "右前条码");
                                    //FL.Location = new Point(0, locationY);
                                    //locationY += FR.Height;
                                    //scanInfo.Controls.Add(FL);
                                    FR.TrueCode = MasterBarCodeR;
                                    FR.ThisCode = "";
                                    DicR.Add(右前, FR);

                                    ShipControl C = new ShipControl();
                                    C.SetInfo(scanInfo.Width, scanInfo.Height / 5, "整垫条码");
                                    //FL.Location = new Point(0, locationY);
                                    //locationY += FR.Height;
                                    //scanInfo.Controls.Add(FL);
                                    C.TrueCode = MasterBarCodeC;
                                    C.ThisCode = "";
                                    DicR.Add(整垫, C);


                                    if (CarType == "X156")
                                    {
                                        ShipControl B60 = new ShipControl();
                                        B60.SetInfo(scanInfo.Width, scanInfo.Height / 5, "60背条码");
                                        B60.Location = new Point(0, locationY);
                                        locationY += B60.Height;
                                        scanInfo.Controls.Add(B60);
                                        B60.TrueCode = MasterBarCode60;
                                        B60.ThisCode = "";
                                        DicL.Add(背60, B60);

                                        ShipControl B40 = new ShipControl();
                                        B40.SetInfo(scanInfo.Width, scanInfo.Height / 5, "40背条码");
                                        //FL.Location = new Point(0, locationY);
                                        //locationY += FR.Height;
                                        //scanInfo.Controls.Add(FL);
                                        B40.TrueCode = MasterBarCode40;
                                        B40.ThisCode = "";
                                        DicR.Add(背40, B40);

                                    }
                                    else if (CarType=="Z177")
                                    {
                                        ShipControl B = new ShipControl();
                                        B.SetInfo(scanInfo.Width, scanInfo.Height / 5, "整背条码");
                                        B.Location = new Point(0, locationY);
                                        locationY += B.Height;
                                        scanInfo.Controls.Add(B);
                                        B.TrueCode = MasterBarCodeB;
                                        B.ThisCode = "";
                                        DicL.Add(整背, B);

                                    }
                                    productionNo = "";
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(scanBox.Text))
                                    {
                                        HZ hZ = new HZ("请先扫描左装箱单！");
                                        hZ.ShowDialog();
                                        productionNo = "";
                                        return;
                                    }
                                    if (scanBox.BackColor != Color.Green)
                                    {
                                        HZ hZ = new HZ("左分装单未验证！");
                                        hZ.ShowDialog();
                                        productionNo = "";

                                        return;

                                    }
                                    this.scanBox1.Text = p;

                                    foreach (var item in DicR)
                                    {
                                        item.Value.Location = new Point(0, locationY);
                                        locationY += item.Value.Height;
                                        scanInfo.Controls.Add(item.Value);
                                    }
                                    productionNo = "";

                                }
                            }

                        }

                        //LoadList(productionNo);
                        //if (LOrder.Count >= 2)
                        //{
                        //    if (LOrder.Contains(this.s1.Text) && LOrder.Contains(this.s5.Text))
                        //    {
                        //        this.errorInfo.Text = "左分装单验证完毕";
                        //        this.scanBox1.Text = this.scanBox.Text;
                        //    }
                        //}

                        //if (ROrder.Count >= 3)
                        //{
                        //    if (ROrder.Contains(this.s2.Text) && ROrder.Contains(this.s3.Text) && ROrder.Contains(this.s4.Text))
                        //    {
                        //        this.errorInfo.Text = "右分装单验证完毕";
                        //        this.scanBox1.Text = this.scanBox.Text;
                        //    }
                        //}

                        //Confirm();


                    }
                    else
                    {
                        if (string.IsNullOrEmpty(scanBox.Text))
                        {
                            HZ hZ = new HZ("请扫描左分装单！");
                            hZ.ShowDialog();
                            productionNo = "";
                            return;
                        }
                        string part = productionNo.Substring(0, 1);
                        if (scanBox.BackColor!=Color.Green)
                        {
                            if (DicL.ContainsKey(PartStr(part)))
                            {
                                DicL[PartStr(part)].ThisCode = productionNo;
                                foreach (var item in DicL.Values)
                                {
                                    if (!item.IsPass)
                                    {
                                        productionNo = "";

                                        return;
                                    }
                                }
                                scanBox.BackColor = Color.Green;

                            }
                            else
                            {
                                HZ hZ = new HZ("非左装箱单条码！");
                                hZ.ShowDialog();
                                productionNo = "";
                                return;
                            }
                        }
                        else
                        {
                            if (scanBox1.Text=="")
                            {
                                HZ hZ = new HZ("请扫描右分装单！");
                                hZ.ShowDialog();
                                productionNo = "";
                                return;
                            }
                            else
                            {
                                if (DicR.ContainsKey(PartStr(part)))
                                {
                                    DicR[PartStr(part)].ThisCode = productionNo;
                                    foreach (var item in DicR.Values)
                                    {
                                        if (!item.IsPass)
                                        {
                                            productionNo = "";

                                            return;
                                        }
                                    }
                                    scanBox1.BackColor = Color.Green;
                                    BLL.T_JISA t_JISA = new BLL.T_JISA();
                                    if (scanBox.BackColor == Color.Green && scanBox1.BackColor == Color.Green)
                                    {
                                        HZ hZ = new HZ("分装单验证成功！");
                                        hZ.ShowDialog();

                                        string pNumber = this.scanBox.Text;
                                        t_JISA.SaveJISA(pNumber, 3);
                                        Send();
                                        ClearAllItems();
                                    }

                                }
                                else
                                {
                                    HZ hZ = new HZ("非右装箱单条码！");
                                    hZ.ShowDialog();
                                    productionNo = "";
                                    return;

                                }
                            }
                        }

                        productionNo = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("单号无效");
                productionNo = "";
            }
        }

        public string PartStr(string part)
        {
            if (productionNo.Length > 10 && productionNo.Length < 20)
            {
                switch (part)
                {
                    case "B"://整垫
                        return "整垫";

                    case "C"://40
                        return "背40";

                    case "A"://背60
                        return "背60";

                    default:
                        return "";

                }
                //if (part == "B")//整点
                //{
                //    DicR[整垫].ThisCode = productionNo;
                //}
                //else if (part == "C")//40
                //{
                //    DicR[背40].ThisCode = productionNo;
                //}
                //if (part == "A")//60
                //{
                //    DicL[背60].ThisCode = productionNo;
                //}
            }
            else if (productionNo.Length > 20)
            {
                switch (part)
                {
                    case "A"://左前
                        return "左前";
                    case "E"://右前
                        return "右前";
                    case "L"://整垫
                        return "整垫";
                    case "P"://整背
                        return "整背";
                    default:
                        return "";
                }
            }
            else
            {
                return "";
            }
        }

        public void ClearAllItems()
        {
            scanInfo.Controls.Clear();
            DicL.Clear();
            DicR.Clear();
            scanBox.Text = "";
            scanBox1.Text = "";
            scanBox.BackColor = Color.Empty;
            scanBox1.BackColor = Color.Empty;
            locationY = 0;
            //ClearPanel();
            productionNo = "";

        }

        private void EachEntruck_Activated(object sender, EventArgs e)
        {
            this.TitleNo.Focus();
        }

        #endregion

        public static string uID = "";
        BLL.T_JISA t_JISA = new BLL.T_JISA();
        #region 两两发运
        private void Send()
        {
            DataTable dt = t_JISA.GetJISAWait();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != 0)
                {
                    DataRow item = dt.Rows[i];
                    string State = item["State"].ToString();
                    DataRow prevItem = dt.Rows[i - 1];
                    string prevState = prevItem["State"].ToString();
                    if (State == "3" && prevState == "3")
                    {
                        //HZ hZ = new HZ("请确定两两发运！");
                        //hZ.ShowDialog(); 
                        UserLogin userLogin = new UserLogin("两量份已验证成功，请扫描员工条码登录发运。");
                        userLogin.ProductionNumber = new List<string>();
                        userLogin.ProductionNumber.Add(item["ProductionNumber"].ToString());
                        userLogin.ProductionNumber.Add(prevItem["ProductionNumber"].ToString());

                        userLogin.JISANo = new List<string>();
                        userLogin.JISANo.Add(item["JISANumber"].ToString());
                        userLogin.JISANo.Add(prevItem["JISANumber"].ToString());
                        userLogin.ShowDialog();
                        //string user = Model.EntruckModel.FaYunUser;
                        //if (!string.IsNullOrEmpty(user))
                        //{
                        //    string twoFayunID = Guid.NewGuid().ToString();
                        //    t_JISA.SaveFaYunState(item["ProductionNumber"].ToString(), 1, user, twoFayunID);
                        //    t_JISA.SaveFaYunState(prevItem["ProductionNumber"].ToString(), 1, user, twoFayunID);

                        //    string a = t_JISA.GetJISASer(item["ProductionNumber"].ToString());
                        //    string b = t_JISA.GetJISASer(prevItem["ProductionNumber"].ToString());
                        //    //调用赵工存储过程
                        //    t_JISA.ExcuteJisA(item["ProductionNumber"].ToString(), a);
                        //    t_JISA.ExcuteJisA(prevItem["ProductionNumber"].ToString(), b);
                        //}
                        //else
                        //{
                        //    MessageBox.Show("员工请先扫码登录");
                        //}


                        //ShippingConfirmFrm SC = new ShippingConfirmFrm("两量份已验证成功，请扫描员工条码");
                        //SC.ShowDialog();
                        //if (EachEntruck.uID != "")
                        //{
                        //    //string uID = SC.userID;
                        //    t_JISA.SaveShippingPerson(item["ProductionNumber"].ToString(), EachEntruck.uID);
                        //    t_JISA.SaveShippingPerson(prevItem["ProductionNumber"].ToString(), EachEntruck.uID);
                        //}
                    }
                }
            }
            ShowJISANo();
            //ShowJISAed();
        }
        #endregion


        #region 验证左右分装单
        private void Confirm()
        {
            BLL.T_JISA t_JISA = new BLL.T_JISA();
            BLL.T_FZOrderConfirm t_FZOrderConfirm = new BLL.T_FZOrderConfirm();
            if (!string.IsNullOrEmpty(this.s1.Text) && !string.IsNullOrEmpty(this.s2.Text) && !string.IsNullOrEmpty(this.s3.Text) && !string.IsNullOrEmpty(this.s4.Text) && !string.IsNullOrEmpty(this.s5.Text))
            {
                if (this.s1.BackColor != Color.Red && this.s2.BackColor != Color.Red && this.s3.BackColor != Color.Red && this.s4.BackColor != Color.Red && this.s5.BackColor != Color.Red)
                {
                    HZ hZ = new HZ("分装单验证成功！");
                    hZ.ShowDialog();

                    string pNumber = this.scanBox.Text;
                    t_JISA.SaveJISA(pNumber, 3);
                    Send();
                }
                else
                {
                    HZ hZ = new HZ("分装单验证失败！");
                    hZ.ShowDialog();
                }
                ClearPanel();
            }


        }
        #endregion


        #region 刷新列表
        private void ClearPanel()
        {
            this.s1.Text = "";
            this.s2.Text = "";
            this.s3.Text = "";
            this.s4.Text = "";
            this.s5.Text = "";

            this.s1.BackColor = Color.White;
            this.s2.BackColor = Color.White;
            this.s3.BackColor = Color.White;
            this.s4.BackColor = Color.White;
            this.s5.BackColor = Color.White;

            this.l.BackColor = Color.Gray;
            this.r.BackColor = Color.Gray;
            this.c.BackColor = Color.Gray;
            this.b40.BackColor = Color.Gray;
            this.b60.BackColor = Color.Gray;

            this.errorInfo.Text = "";
            LOrder.Clear();
            ROrder.Clear();

            this.scanBox.Text = "";
            this.scanBox1.Text = "";
        }
        #endregion


        #region 确认左右分装单
        private void LoadList(string pNo)
        {
            if (pNo.Substring(0, 1) == "0")
            {
                if (r.BackColor == Color.Green)
                {
                    if (!string.IsNullOrEmpty(this.s2.Text) && !string.IsNullOrEmpty(this.s3.Text) && !string.IsNullOrEmpty(this.s4.Text))
                    {
                        ConfirmLorder(pNo);
                    }
                    else
                    {
                        HZ hZ = new HZ("右分装单未验证！");
                        hZ.ShowDialog();
                        ClearPanel();
                    }
                }
                else
                {
                    ConfirmLorder(pNo);
                }
            }
            else if (pNo.Substring(0, 1) == "1")
            {
                if (l.BackColor == Color.Green)
                {
                    if (!string.IsNullOrEmpty(this.s1.Text) && !string.IsNullOrEmpty(this.s5.Text))
                    {
                        ConfrimRorder(pNo);
                    }
                    else
                    {
                        HZ hZ = new HZ("左分装单未验证！");
                        hZ.ShowDialog();
                        ClearPanel();
                    }
                }
                else
                {
                    ConfrimRorder(pNo);
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(pNo))
                {

                    if (pNo.Substring(0, 1) == "A" && pNo.Length > 20)
                    {
                        if (LOrder.Contains(pNo))
                        {
                            if (this.l.BackColor == Color.Green)
                            {
                                this.s1.Text = pNo;
                                this.s1.BackColor = Color.GreenYellow;
                            }
                            else
                            {
                                HZ hZ = new HZ("验证错误，该条码为左分装单条码！");
                                hZ.ShowDialog();
                                ClearPanel();
                            }
                        }
                        else
                        {
                            this.s1.Text = pNo;
                            this.s1.BackColor = Color.Red;

                            HZ hZ = new HZ("验证错误，该条码为左分装单条码！");
                            hZ.ShowDialog();
                            ClearPanel();
                        }
                    }

                    if (pNo.Substring(0, 1) == "A" && pNo.Length < 20)
                    {
                        if (LOrder.Contains(pNo))
                        {
                            if (this.b60.BackColor == Color.Green)
                            {
                                this.s5.Text = pNo;
                                this.s5.BackColor = Color.GreenYellow;
                            }
                            else
                            {
                                HZ hZ = new HZ("验证错误，该条码为左分装单条码！");
                                hZ.ShowDialog();
                                ClearPanel();
                            }
                        }
                        else
                        {
                            this.s5.Text = pNo;
                            this.s5.BackColor = Color.Red;
                            HZ hZ = new HZ("验证错误，该条码为左分装单条码！");
                            hZ.ShowDialog();
                            ClearPanel();
                        }
                    }

                    if (pNo.Substring(0, 1) == "E")
                    {
                        if (this.r.BackColor == Color.Green)
                        {
                            if (ROrder.Contains(pNo))
                            {
                                this.s2.Text = pNo;
                                this.s2.BackColor = Color.GreenYellow;
                            }
                            else
                            {
                                HZ hZ = new HZ("验证错误，该条码为右分装单条码！");
                                hZ.ShowDialog();
                                ClearPanel();
                            }
                        }
                        else
                        {
                            this.s2.Text = pNo;
                            this.s2.BackColor = Color.Red;
                            HZ hZ = new HZ("验证错误，该条码为右分装单条码！");
                            hZ.ShowDialog();
                            ClearPanel();
                        }
                    }

                    if (pNo.Substring(0, 1) == "B")
                    {
                        if (ROrder.Contains(pNo))
                        {
                            if (this.c.BackColor == Color.Green)
                            {
                                this.s3.Text = pNo;
                                this.s3.BackColor = Color.GreenYellow;
                            }
                            else
                            {
                                HZ hZ = new HZ("验证错误，该条码为右分装单条码！");
                                hZ.ShowDialog();
                                ClearPanel();
                            }
                        }
                        else
                        {
                            this.s3.Text = pNo;
                            this.s3.BackColor = Color.Red;
                            HZ hZ = new HZ("验证错误，该条码为右分装单条码！");
                            hZ.ShowDialog();
                            ClearPanel();
                        }

                    }

                    if (pNo.Substring(0, 1) == "C")
                    {
                        if (ROrder.Contains(pNo))
                        {
                            if (this.b40.BackColor == Color.Green)
                            {
                                this.s4.Text = pNo;
                                this.s4.BackColor = Color.GreenYellow;
                            }
                            else
                            {
                                HZ hZ = new HZ("验证错误，该条码为右分装单条码！");
                                hZ.ShowDialog();
                                ClearPanel();
                            }
                        }
                        else
                        {
                            this.s4.Text = pNo;
                            this.s4.BackColor = Color.Red;
                            HZ hZ = new HZ("验证错误，该条码为右分装单条码！");
                            hZ.ShowDialog();
                            ClearPanel();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("验证错误");
                    ClearPanel();
                }

            }
        }

        private void ConfirmLorder(string pNo)
        {
            LOrder.Clear();
            BLL.T_FZOrder t_FZOrder = new BLL.T_FZOrder();
            DataTable dt = t_FZOrder.GetLOrder(pNo.Substring(1));
            if (dt.Rows.Count > 0)
            {
                LOrder.Add(dt.Rows[0]["MasterBarCodeL"].ToString());
                LOrder.Add(dt.Rows[0]["MasterBarCode60"].ToString());
                l.BackColor = Color.Green;
                b60.BackColor = Color.Green;
            }
            else
            {
                MessageBox.Show("未找到左分装单");
            }


        }

        private void ConfrimRorder(string pNo)
        {
            ROrder.Clear();
            BLL.T_FZOrder t_FZOrder = new BLL.T_FZOrder();
            DataTable dt = t_FZOrder.GetROrder(pNo.Substring(1));

            if (dt.Rows.Count > 0)
            {
                ROrder.Add(dt.Rows[1]["MasterBarCodeR"].ToString());
                ROrder.Add(dt.Rows[1]["MasterBarCodeC"].ToString());
                ROrder.Add(dt.Rows[1]["MasterBarCode40"].ToString());
                r.BackColor = Color.Green;
                c.BackColor = Color.Green;
                b40.BackColor = Color.Green;
            }
            else
            {
                MessageBox.Show("未找到右分装单");
            }

        }
        #endregion


        #region JISA列表
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.dataGridView1.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
        }


        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(this.dataGridView2.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.dataGridView2.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
        }


        #endregion


        #region datagridView
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.dataGridView1.CurrentCell = null;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                DataRow item = dt1.Rows[i];
                string state = item["State"].ToString();
                string JisA = item["JISANumber"].ToString();

                if (state == "3")
                {
                    this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.GreenYellow;
                }


            }
        }
        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                //判断调序
                if (i < dt2.Rows.Count - 1)
                {
                    string JIAS = this.dataGridView2.Rows[i].Cells[1].Value.ToString();
                    string NextJIAS = this.dataGridView2.Rows[i + 1].Cells[1].Value.ToString();
                    if (NextJIAS.Substring(NextJIAS.Length - 3, 3) != "001")
                    {
                        if ((int.Parse(JIAS.Substring(JIAS.Length - 3, 3)) + 1).ToString().PadLeft(3, '0') != NextJIAS.Substring(NextJIAS.Length - 3, 3))
                        {
                            this.dataGridView2.Rows[i + 1].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                    }
                    //发运颜色 
                    string thisGropuCode = dt2.Rows[i]["GroupCode"].ToString();
                    string nextGropuCode = dt2.Rows[i + 1]["GroupCode"].ToString();
                    Color c = GetFYColor();
                    if (thisGropuCode == nextGropuCode)
                    {
                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = c;
                        this.dataGridView2.Rows[i + 1].DefaultCellStyle.BackColor = c;
                    }
                    else
                    {
                        string prevGropuCode = "";
                        if (i != 0)
                        {
                            prevGropuCode = dt2.Rows[i - 1]["GroupCode"].ToString();
                        }
                        if (thisGropuCode != prevGropuCode)
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = c;
                        }
                        else
                        {
                            GetFYColor();//切换一下颜色
                        }
                    }
                }

            }
            //this.dataGridView2.CurrentCell = null;
            //for (int i = 0; i < dt2.Rows.Count; i++)
            //{
            //    if (dt2.Rows.Count -1> i&&i > 0)
            //    {
            //        DataRow item0 = dt2.Rows[i - 1];
            //        DataRow item = dt2.Rows[i];
            //        DataRow item1 = dt2.Rows[i + 1];
            //        string flag0 = item0["TwoFayunID"].ToString();
            //        string flag = item["TwoFayunID"].ToString();
            //        string flag1 = item1["TwoFayunID"].ToString();

            //        if(flag0 == flag)
            //        {
            //            this.dataGridView2.Rows[i-1].DefaultCellStyle.BackColor = Color.Blue;
            //            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Blue;
            //            this.dataGridView2.Rows[i+1].DefaultCellStyle.BackColor = Color.Yellow;
            //        }
            //        else if(flag1 ==flag)
            //        {
            //            this.dataGridView2.Rows[i - 1].DefaultCellStyle.BackColor = Color.Yellow;
            //            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Blue;
            //            this.dataGridView2.Rows[i + 1].DefaultCellStyle.BackColor = Color.Blue;
            //        }


            //    }

            //}
        }
        #endregion

        private void EachEntruck_Shown(object sender, EventArgs e)
        {
            this.JISAwait.Controls.Add(this.dataGridView1);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView1.ColumnHeadersHeight = 50;
            this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToOrderColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.AllowDrop = false;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //    MyDG["DG1"].RowHeadersVisible = false;
            this.dataGridView1.Columns[0].Width = 160;
            this.dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //         MyDG["DG1"].RowTemplate.Height = 60;
            this.dataGridView1.Columns[1].Width = 280;
            this.dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.Columns[2].Width = 220;
            this.dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;

            //this.dataGridView1.Columns[3].Width = 140;
            //this.dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView1.Columns[0].HeaderText = "生产号";
            // DGV_PLAN.Columns[1].HeaderText = "标签";
            this.dataGridView1.Columns[1].HeaderText = "JISA";
            //this.dataGridView1.Columns[2].HeaderText = "库位号";

            this.dataGridView1.Columns[2].HeaderText = "状态";


            this.JISAed.Controls.Add(this.dataGridView2);

            this.dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView2.ColumnHeadersHeight = 50;
            this.dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToOrderColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView2.AllowDrop = false;
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //    MyDG["DG2"].RowHeadersVisible = false;
            this.dataGridView2.Columns[0].Width = 150;
            this.dataGridView2.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //         MyDG["DG2"].RowTemplate.Height = 60;
            this.dataGridView2.Columns[1].Width = 217;
            this.dataGridView2.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dataGridView2.Columns[2].Width = 176;
            this.dataGridView2.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dataGridView2.Columns[3].Width = 120;
            this.dataGridView2.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //this.dataGridView2.Columns[2].Width = 180;
            //this.dataGridView2.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView2.Columns[0].HeaderText = "生产号";

            // DGV_PLAN.Columns[2].HeaderText = "标签";
            this.dataGridView2.Columns[1].HeaderText = "JISA";

            this.dataGridView2.Columns[2].HeaderText = "发运时间";

            this.dataGridView2.Columns[3].HeaderText = "发运人";
            this.dataGridView1.Font = new Font("微软雅黑", 14f, FontStyle.Bold);
            this.dataGridView2.Font = new Font("微软雅黑", 14f, FontStyle.Bold);
            //this.dataGridView2.Columns[2].HeaderText = "库位号";
        }


        #region 强制发运
        private void pass_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.T_JISA t_JISA = new BLL.T_JISA();
                int c = this.dataGridView1.SelectedRows.Count;
                if (c != 1)
                {
                    MessageBox.Show("请选中一行");
                }
                else
                {

                    //DataGridViewRow item = this.dataGridView1.SelectedRows[0];
                    DataGridViewRow item = this.dataGridView1.Rows[0];
                    string pNo = item.Cells[0].Value.ToString();
                    string JISA = item.Cells[1].Value.ToString();
                    string state = item.Cells[2].Value.ToString();

                    if (state == "已验证")
                    {
                        UserLogin userLogin = new UserLogin("请扫描员工条码登录发运单辆份。");
                        userLogin.ProductionNumber = new List<string>();
                        userLogin.ProductionNumber.Add(pNo);


                        userLogin.JISANo = new List<string>();
                        userLogin.JISANo.Add(JISA);

                        userLogin.ShowDialog();

                        ShowJISANo();
                        //ShowJISAed();
                        ////ConfirmFrm confirmFrm = new ConfirmFrm("单批量发运需要班长验证");
                        //UserLogin userLogin = new UserLogin();
                        //userLogin.ShowDialog();
                        ////if (confirmFrm.ShowDialog() == DialogResult.OK)
                        ////{
                        //string userName = Model.EntruckModel.FaYunUser;
                        //if (!string.IsNullOrEmpty(userName))
                        //{
                        //    string oneFayunID = Guid.NewGuid().ToString();
                        //    t_JISA.SaveFaYunState(pNo, 1, userName, oneFayunID);

                        //    HZ hZ = new HZ("已单批量发运");
                        //    hZ.ShowDialog();

                        //    string a = t_JISA.GetJISASer(pNo);
                        //    //调用赵工存储过程
                        //    t_JISA.ExcuteJisA(pNo, a);

                        //    ShowJISANo();
                        //    ShowJISAed();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("请员工先扫描条码");
                        //}

                        //ShippingConfirmFrm SC = new ShippingConfirmFrm("验证完成，请扫描员工条码。");
                        //SC.ShowDialog();
                        //if (EachEntruck.uID != "")
                        //{
                        //    t_JISA.SaveShippingPerson(pNo, EachEntruck.uID);
                        //} 
                        //} 
                    }
                    else
                    {
                        HZ hZ = new HZ("只有验证过的分装单才能强制发运");
                        hZ.ShowDialog();
                        this.TitleNo.Focus();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool JudgeTX(string thisNo, string NextNo)
        {
            string NextJIAS = NextNo;
            if (NextJIAS.Substring(NextJIAS.Length - 3, 3) != "001")
            {
                if ((int.Parse(thisNo.Substring(thisNo.Length - 3, 3)) + 1).ToString().PadLeft(3, '0') != NextJIAS.Substring(NextJIAS.Length - 3, 3))
                {
                    return true;
                }

                return false;
            }
            return false;
        }


        #endregion

        #region 查询已发运信息
        private void search_Click(object sender, EventArgs e)
        {
            SearchFayun searchFayun = new SearchFayun();
            searchFayun.ShowDialog();

            ShowJISAed();

            this.TitleNo.Focus();
        }


        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name== "JISAed")
            {
                SearchFayun.S_BTime = DateTime.Now.AddDays(-1).ToString();
                SearchFayun.S_ETime = DateTime.Now.ToString();
                ShowJISAed();
            }
        }

        private void ForceShip_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.T_JISA t_JISA = new BLL.T_JISA();
                int c = this.dataGridView1.SelectedRows.Count;
                if (c != 1)
                {
                    MessageBox.Show("请选中一行");
                }
                else
                {
                    ShippingConfirmFrm SC = new ShippingConfirmFrm();
                    DialogResult dr = SC.ShowDialog();

                    if (dr==DialogResult.OK)
                    {
                        DataGridViewRow item = this.dataGridView1.SelectedRows[0];
                        string pNo = item.Cells[0].Value.ToString();
                        string pJISA = item.Cells[1].Value.ToString();

                        t_JISA.ForceShip(pNo);
                        t_JISA.ExcuteJisA(pNo,pJISA);

                    }

                    ShowJISANo();

                }
                this.TitleNo.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
    }
}