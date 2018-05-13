using Picking.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OPCAutomation;
using Picking.Frm;

namespace Picking
{
    public partial class Picking : Form
    {
        public static string barCode;

        public static string AlCCode;

        public bool Isone = false;

        public bool Istwo = false;

        public int Index_1 = 0;
        public int Index_2 = 0;
        //一区所有亮灯的OPC点
        public List<string> list_1 { get; set; }

        //二区
        public List<string> list_2 { get; set; }



        //OPC连接初始化
        public OPCAutomation.OPCServer MyOPCServer;
        OPCGroup OpcIn;

        public Picking()
        {
            InitializeComponent();

        }

        private void Picking_Load(object sender, EventArgs e)
        {
            Frm_Initialize();
            //OPC连接
            OPC_Ini();
            SetOPC();
            this.orderInfo.Focus();
            ShowOrderInfo();

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

            Label lb_top = new Label();
            lb_top.Width = width / 2;
            lb_top.Height = p_top.Height;
            lb_top.Text = "北京安道拓亮灯拾取系统";
            lb_top.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lb_top.Font = new Font("微软雅黑", 22, FontStyle.Regular);
            lb_top.Location = new Point(width / 2 - lb_top.Width / 2, 0);
            lb_top.AutoSize = false;

            Panel p_mid = new Panel();
            p_mid.Width = width;
            p_mid.Height = height - p_top.Height;
            p_mid.Location = new Point(0, p_top.Height);
            // p_mid.BackColor = Color.Orange;


            this.orderInfo.Size = new Size(Convert.ToInt32(this.Width * 0.942), Convert.ToInt32(this.Height * 0.6));
            this.orderInfo.Location = new Point(Convert.ToInt32(this.Width * 0.028), Convert.ToInt32(this.Height * 0.2));


            this.label1.Size = new Size(this.orderInfo.Width, this.orderInfo.Height / 6);
            this.label1.Location = new Point(Convert.ToInt32(this.Width * 0.028), Convert.ToInt32(this.Height * 0.2) + this.orderInfo.Height);
            this.label1.Hide();

            //this.timer1.Enabled = true;
            //this.timer1.Start();
            //this.timer1.Interval = 5000;

            p_top.Controls.Add(lb_top);
            p_top.Controls.Add(logo);

            this.Controls.Add(p_top);
            this.Controls.Add(p_mid);

            timer1.Enabled = true;
            timer1.Start();
            timer1.Interval = 60000;
        }
        #endregion

        #region 定时器刷新列表数据

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dictionary.Count <= 0)
            {
                ShowOrderInfo();
            }
        }

        #endregion



        #region 展示主序列信息
        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    ShowOrderInfo();
        //}
        Dictionary<string, PickOrderControl> dictionary = new Dictionary<string, PickOrderControl>();


        private void ShowOrderInfo()
        {
            dictionary.Clear();
            int x = 0;
            int y = 0;
            BLL.T_PickOrder t_PickOrder = new BLL.T_PickOrder();
            DataTable dt = t_PickOrder.GetOrder();
            this.orderInfo.Controls.Clear();
            foreach (DataRow item in dt.Rows)
            {
                PickOrderControl pickOrderControl = new PickOrderControl();
                pickOrderControl.serial = item["ProductionNumber"].ToString();
                pickOrderControl.barCode = item["MasterBarCode"].ToString();
                pickOrderControl.carModelNo = item["CarModelName"].ToString();
                pickOrderControl.carTypeNo = item["CarType"].ToString();

                pickOrderControl.SetInfo(this.orderInfo.Width, this.orderInfo.Height / 10);
                pickOrderControl.Location = new Point(x, y);
                this.orderInfo.Controls.Add(pickOrderControl);
                y += pickOrderControl.Height;
                dictionary.Add(item["MasterBarCode"].ToString(), pickOrderControl);
                if (SaveCode.ContainsKey(item["MasterBarCode"].ToString()))
                {
                    pickOrderControl.SetChoice(Color.Blue);
                }

            }

        }
        #endregion


        #region 扫描条码

        private void Picking_KeyPress(object sender, KeyPressEventArgs e)
        {

            barCode += e.KeyChar.ToString().Replace("\r", "");
            if (e.KeyChar == 13)
            {
                Scan(barCode);

            }

            this.orderInfo.Focus();
        }

        private void Scan(string ScanNO)
        {
            try
            {
                string Scan = ScanNO;
                barCode = "";

                BLL.T_PickOrder t_PickOrder = new BLL.T_PickOrder();
                string masterBar = t_PickOrder.GetScanOrder();

                if (Scan == masterBar)
                {

                    t_PickOrder.SaveScanedOrder(masterBar, 1);
                    ShowOrderInfo();
                    //扫描条码与主序列匹配时关闭蜂鸣器
                    if (WriteOpc(OpcIn, "KittingLine1.KittingLine1-1.Lamp", "0"))
                    {
                        // MessageBox.Show("蜂鸣器发送失败");
                    }

                    t_PickOrder.ExcuteScanBar(masterBar);


                }
                else
                {
                    //扫描条码与主序列不匹配时触发蜂鸣器
                    if (WriteOpc(OpcIn, "KittingLine1.KittingLine1-1.Lamp", "1"))
                    {

                    }
                    HZ hZ = new HZ("扫描错误！当前应扫描序列号为：" + masterBar);
                    hZ.ShowDialog();

                }

                this.orderInfo.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Picking_Activated(object sender, EventArgs e)
        {
            this.orderInfo.Focus();
        }
        #endregion

        DataTable configDt = new DataTable();
        #region OPC操作

        private void OPC_Ini()
        {
            try
            {
                MyOPCServer = new OPCServer();
                MyOPCServer.Connect("KEPware.KEPServerEx.V6", "193.100.101.221");
                OpcIn = MyOPCServer.OPCGroups.Add("KtingByLight.MKtingByLight.001");
                OpcIn = MyOPCServer.OPCGroups.Add("KtingByLight.MKtingByLight.002");
                OpcIn = MyOPCServer.OPCGroups.Add("KtingByLight.MKtingByLight.003");
                OpcIn = MyOPCServer.OPCGroups.Add("KtingByLight.MKtingByLight.004");
                OpcIn = MyOPCServer.OPCGroups.Add("KittingLine1.KittingLine1-1");
                BLL.GetPickData getPickData = new BLL.GetPickData();

                configDt = getPickData.GetData();


                int index = 1;
                foreach (DataRow item in configDt.Rows)
                {
                    // MessageBox.Show(item["OPCSite"].ToString());
                    OpcIn.OPCItems.AddItem(item["OPCSite"].ToString().Trim(), index);
                    index++;

                }
                OpcIn.OPCItems.AddItem("KtingByLight.MKtingByLight.004.001", index);
                index++;
                for (int i = 1; i < 6; i++)
                {
                    OpcIn.OPCItems.AddItem("KittingLine1.KittingLine1-1." + i + "#放行", index);
                    index++;
                    OpcIn.OPCItems.AddItem("KittingLine1.KittingLine1-1." + i + "#阻挡", index);
                    index++;
                    OpcIn.OPCItems.AddItem("KittingLine1.KittingLine1-1." + i + "#到位", index);
                    index++;
                }
                //蜂鸣器
                OpcIn.OPCItems.AddItem("KittingLine1.KittingLine1-1.Lamp", index);

                OpcIn.UpdateRate = 50;
                OpcIn.IsActive = true;
                OpcIn.IsSubscribed = true;
                OpcIn.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(OpcInTri_DataChange);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        Dictionary<string, string> SaveCode = new Dictionary<string, string>();
        bool Area1IsDW = false;
        bool Area2IsDW = false;
        private void OpcInTri_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            try
            {
                for (int i = 1; i <= NumItems; i++)
                {

                    string value = ItemValues.GetValue(i).ToString().ToLower().Trim();//value值
                    int clientHandles = int.Parse(ClientHandles.GetValue(i).ToString());//唯一建
                    string tag = OpcIn.OPCItems.Item(clientHandles).ItemID;   //TAG点

                    BLL.T_PickOrder t_PickOrder = new BLL.T_PickOrder();
                    BLL.T_PickConfig t_PickConfig = new BLL.T_PickConfig();

                    #region 一区逻辑处理
                    //一区到位信号
                    if (tag == "KittingLine1.KittingLine1-1.2#到位")
                    {
                        if (value == "true")
                        {
                            if (Area1IsDW)
                            {
                                return;
                            }
                            //   MessageBox.Show("接收到一区到位信号");
                            DataTable dt = t_PickOrder.GetArea1Order();
                            if (dt.Rows.Count > 0)
                            {
                                string masterBarCode = dt.Rows[0]["MasterBarCode"].ToString();
                                string AlCCode = dt.Rows[0]["ALCCode"].ToString();
                                //string AssyNo1 = t_PickOrder.GetAssyNo(AlCCode);
                                //MessageBox.Show(masterBarCode);
                                //MessageBox.Show(dictionary.Count.ToString());

                                if (dictionary.Count > 0)
                                {
                                    dictionary[masterBarCode].SetChoice(Color.Blue);
                                    if (!SaveCode.ContainsKey(masterBarCode))
                                    {
                                        SaveCode.Add(masterBarCode, "");
                                    }
                                }
                                list_1 = t_PickConfig.GetPickItem(masterBarCode, AlCCode, 1);
                                if (list_1.Count == 0)
                                {
                                    WriteOpc(OpcIn, "KittingLine1.KittingLine1-1.2#放行", "1"); 
                                    //一区没有配置灯
                                }
                                else
                                {
                                    foreach (var item in list_1)
                                    {
                                        Isone = true;
                                        WriteOpc(OpcIn, item, "3");
                                        //   MessageBox.Show("正在亮灯。。");
                                    }
                                    Area1IsDW = true;
                                }

                            }
                        }
                    }

                    if (Isone)
                    {
                        if (value == "0")
                        {
                            if (list_1.Contains(tag))
                            {
                                Index_1++;
                                if (Index_1 == list_1.Count)
                                {
                                    WriteOpc(OpcIn, "KittingLine1.KittingLine1-1.2#放行", "1");
                                    Index_1 = 0;
                                    list_1.Clear();
                                    Isone = false;
                                  //一区全部灭灯
                                }
                            }
                        }
                    }

                    if (tag == "KittingLine1.KittingLine1-1.2#阻挡")
                    {
                        if (value == "true")
                        {
                            DataTable dt = t_PickOrder.GetArea1Order();
                            if (dt.Rows.Count > 0)
                            {
                                string masterBarCode = dt.Rows[0]["MasterBarCode"].ToString();
                                t_PickOrder.SaveArea1Order(masterBarCode, 1);
                                Area1IsDW = false;
                            }
                        }
                    }
                    #endregion


                    #region 二区逻辑处理
                    if (tag == "KittingLine1.KittingLine1-1.4#到位")
                    {
                        if (value == "true")
                        {
                            if (Area2IsDW)
                            {
                                return;
                            }
                            //   MessageBox.Show("接收到一区到位信号");
                            DataTable dt = t_PickOrder.GetArea2Order();
                            if (dt.Rows.Count > 0)
                            {
                                string masterBarCode = dt.Rows[0]["MasterBarCode"].ToString();
                                string AlCCode = dt.Rows[0]["ALCCode"].ToString();
                                //string AssyNo2 = t_PickOrder.GetAssyNo(AlCCode);
                                list_2 = t_PickConfig.GetPickItem(masterBarCode, AlCCode, 2);
                                //MessageBox.Show(masterBarCode);
                                //MessageBox.Show(dictionary.Count.ToString());
                                if (dictionary.Count > 0)
                                {
                                    dictionary[masterBarCode].SetChoice(Color.Green);
                                    if (!SaveCode.ContainsKey(masterBarCode))
                                    {
                                        SaveCode.Add(masterBarCode, "");
                                    }
                                }
                                if (list_2.Count == 0)
                                {
                                    WriteOpc(OpcIn, "KittingLine1.KittingLine1-1.4#放行", "1");
                                    //二区没有配置灯
                                }
                                else
                                {
                                    foreach (var item in list_2)
                                    {
                                        Istwo = true;
                                        WriteOpc(OpcIn, item, "3");
                                        //   MessageBox.Show("正在亮灯。。");
                                    }
                                    Area2IsDW = true;
                                }
                            }
                        }
                    }

                    if (Istwo)
                    {
                        if (value == "0")
                        {
                            if (list_2.Contains(tag))
                            {
                                Index_2++;
                                if (Index_2 == list_2.Count)
                                {
                                    WriteOpc(OpcIn, "KittingLine1.KittingLine1-1.4#放行", "1");
                                    Index_2 = 0;
                                    list_2.Clear();
                                    Istwo = false;
                                    //二区全部灭灯
                                }
                            }
                        }
                    }

                    if (tag == "KittingLine1.KittingLine1-1.4#阻挡")
                    {
                        if (value == "true")
                        {
                            DataTable dt = t_PickOrder.GetArea2Order();
                            if (dt.Rows.Count > 0)
                            {
                                string masterBarCode = dt.Rows[0]["MasterBarCode"].ToString(); 
                                t_PickOrder.SaveArea2Order(masterBarCode, 1);
                                t_PickOrder.SaveScanedOrder(masterBarCode, 2);
                                Area2IsDW = false;
                                ShowOrderInfo();
                            }
                        }
                    }
                    #endregion

                    if (tag == "KtingByLight.MKtingByLight.004.001")
                    {
                        if (value == "false")
                        {
                            WriteOpc(OpcIn, "KtingByLight.MKtingByLight.004.001", "1");
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

        }

        /// <summary>
        /// OPC写方法
        /// </summary>
        /// <param name="opcGroup">OPC组名</param>
        /// <param name="opcItem">OPC点名</param>
        /// <param name="value">待写入的值</param>
        private bool WriteOpc(OPCGroup opcGroup, string opcItem, string value)
        {
            try
            {
                opcGroup.OPCItems.Item(opcItem).Write(value);

                //    opcGroup.AsyncWrite()
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception("写入OPC失败(WriteOpc):opcItem=" + opcItem + ",value=" + value + "  :" + ex.Message);
            }
        }

        /// <summary>
        /// 读取OPC的值
        /// </summary>
        /// <param name="opcGroup">OPC组名</param>
        /// <param name="opcItem">OPC点名</param>
        /// <returns>OPC的值</returns>
        private string ReadOpc(OPCGroup opcGroup, string opcItem)
        {
            string result = "";
            try
            {
                object value, quality, timestamp;
                opcGroup.OPCItems.Item(opcItem).Read(2, out value, out quality, out timestamp);

                if (value != null)
                {
                    result = value.ToString();
                }
                else
                {
                    result = "";
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("读取OPC失败(ReadOpc):opcItem=" + opcItem + "  :" + ex.Message);
            }
            return result;

        }


        #endregion


        private void SetOPC()
        {
            WriteOpc(OpcIn, "KtingByLight.MKtingByLight.004.001", "0");
            WriteOpc(OpcIn, "KtingByLight.MKtingByLight.004.001", "1");

        }

    }
}
