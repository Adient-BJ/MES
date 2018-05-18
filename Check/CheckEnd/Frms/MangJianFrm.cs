using CheckEnd.Bll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using OPCAutomation;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Net;
using CheckEnd.Frms;
using CheckEnd.Command;

namespace CheckEnd
{
    public partial class MangJianFrm : Form
    {

        public string BarCode { get; set; }
        public string WorkOpcTag { get; set; }
        public string ProductNum;
        public string WorkBayName { get; set; }

        public string Code { get; set; }


        Bll.T_WorkbayIPConfig Bll_T_WorkbayIPConfig = new Bll.T_WorkbayIPConfig();
        Bll.T_WorkbayFuntionTag Bll_T_WorkbayFuntionTag = new Bll.T_WorkbayFuntionTag();

        //OPC连接初始化
        public OPCAutomation.OPCServer MyOPCServer;
        OPCGroup OpcIn;
        OPCServer OpcSvr;

        private string WorkbayName;

        private string workBayTag;
        public MangJianFrm()
        {
            InitializeComponent();
        }

        private void MangJianFrm_Load(object sender, EventArgs e)
        {
            Frm_Initialize();
            //loadGunRslt();

            this.barCode.Text = "";
            ShowErrorMessageInfo("请等待托盘到位");
            OPC_Ini();
            //AnswerQuestions();
        }

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
            logo.Click += button2_Click;

            Label lb_top = new Label();
            lb_top.Width = width / 2;
            lb_top.Height = p_top.Height;
            lb_top.Text = "北京安道拓盲检系统 V2.0";
            lb_top.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lb_top.Font = new Font("微软雅黑", 22, FontStyle.Regular);
            lb_top.Location = new Point(width / 2 - lb_top.Width / 2, 0);
            lb_top.AutoSize = false;

            Panel p_mid = new Panel();
            p_mid.Width = width;
            p_mid.Height = height - p_top.Height;
            p_mid.Location = new Point(0, p_top.Height);
            // p_mid.BackColor = Color.Orange;

            this.panel1.Width = width;
            this.panel1.Height = height / 8;
            int p1w = this.panel1.Width;
            int p1h = this.panel1.Height;
            this.panel1.Location = new Point(0, p1h);

            this.panel2.Width = Convert.ToInt32(p1w*0.8) - Convert.ToInt32(width * 0.06);
            this.panel2.Height = (height / 2) / 5 * 4;
            this.panel2.Location = new Point(Convert.ToInt32(width * 0.03), Convert.ToInt32(p1h * 2.5));
            this.panel2.BorderStyle = BorderStyle.FixedSingle;

            this.flowLayoutPanel1.Width = width - panel2.Width - Convert.ToInt32(width * 0.06);
            this.flowLayoutPanel1.Height = panel2.Height;
            this.flowLayoutPanel1.Location = new Point(panel2.Right+ Convert.ToInt32(width * 0.01), Convert.ToInt32(p1h * 2.5));

            this.label5.Location = new Point(panel2.Right + Convert.ToInt32(width * 0.01), Convert.ToInt32(p1h * 2.5)-label5.Height);

            this.mes.Size = new Size();

            this.label1.Width = p1w / 10;
            this.label1.Height = Convert.ToInt32(p1h * 0.384);
            this.label1.Location = new Point(Convert.ToInt32(p1w * 0.01), Convert.ToInt32(p1h * 0.33));

            this.barCode.Width = Convert.ToInt32(p1w * 0.3);
            this.barCode.Height = Convert.ToInt32(p1h * 0.4);
            this.barCode.Location = new Point(this.label1.Right, Convert.ToInt32(p1h * 0.36));


            this.label2.Width = p1w / 17;
            this.label2.Height = Convert.ToInt32(p1h * 0.32);
            this.label2.Location = new Point(this.barCode.Right + 20, Convert.ToInt32(p1h * 0.33));


            this.carModelName.Width = p1w / 6;
            this.carModelName.Height = Convert.ToInt32(p1h * 0.32);
            this.carModelName.Location = new Point(this.label2.Right, Convert.ToInt32(p1h * 0.36));
            this.carModelName.Text = "";

            this.label4.Width = p1w / 15;
            this.label4.Height = Convert.ToInt32(p1h * 0.32);
            this.label4.Location = new Point(this.carModelName.Right - 20, Convert.ToInt32(p1h * 0.33));

            this.carType.Width = p1w / 13;
            this.carType.Height = Convert.ToInt32(p1h * 0.32);
            this.carType.Location = new Point(this.label4.Right, Convert.ToInt32(p1h * 0.36));
            this.carType.Text = "";

            this.label3.Size = this.label2.Size;
            this.label3.Location = new Point(this.carType.Right, Convert.ToInt32(p1h * 0.33));

            this.workBay.Size = this.carType.Size;
            this.workBay.Location = new Point(this.label3.Right, Convert.ToInt32(p1h * 0.36));
            this.workBay.Text = "";

            this.exit.Width = Convert.ToInt32(p1w * 0.08);
            this.exit.Height = Convert.ToInt32(p1h * 0.384);
            this.exit.Location = new Point(20, 20);
            this.exit.Hide();

            this.bypass.Width = Convert.ToInt32(width * 0.158);
            this.bypass.Height = Convert.ToInt32(height * 0.05);
            this.bypass.Location = new Point(Convert.ToInt32(width * 0.03), Convert.ToInt32(height * 0.258));

            this.submit.Width = Convert.ToInt32(width * 0.23);
            this.submit.Height = Convert.ToInt32(height * 0.099);
            this.submit.Location = new Point(this.submit.Right + this.Width / 13, Convert.ToInt32(height * 0.846));

            this.pass.Size = this.submit.Size;
            this.pass.Location = new Point(Convert.ToInt32(width * 0.15), Convert.ToInt32(height * 0.846));

            this.retry.Size = this.submit.Size;
            this.retry.Location = new Point(Convert.ToInt32(width * 0.645), Convert.ToInt32(height * 0.846));
            this.retry.Hide();

            this.pass.Hide();
            this.submit.Hide();

            //string IP = XML.XmlConfig.GetIPXML();
            XML.XmlConfig xmlconfig = new XML.XmlConfig();
            xmlconfig.GetIPXML();
            //Bll.T_MJAnswer t_MJAnswer = new T_MJAnswer();
            string workBay = xmlconfig.staionName;
            WorkbayName = workBay;
            this.workBay.Text = workBay;

            p_top.Controls.Add(lb_top);
            p_top.Controls.Add(logo);

            this.Controls.Add(p_top);
            this.Controls.Add(p_mid);

        }

        //private void GetTag()
        //{
        //    List<Model.T_WorkbayIPConfig> list = Bll_T_WorkbayIPConfig.GetAll();
        //    WorkOpcTag = list.Where(m => m.IPAddress == Bll.Base.IPAddress).Select(k => k.GetWorkOpcTag).FirstOrDefault();
        //    WorkBayName = list.Where(m => m.IPAddress == Bll.Base.IPAddress).Select(k => k.WorkbayName).FirstOrDefault();
        //}



        private string LineName;

        #region OPC
        /// <summary>
        /// OPC初始化操作：连接、注册变量
        /// </summary>
        private void OPC_Ini()
        {
            try
            {

                MyOPCServer = new OPCServer();
                MyOPCServer.Connect("KEPware.KEPServerEx.V6", "193.100.101.221");
                //OpcIn = MyOPCServer.OPCGroups.Add("FR.FR");

                XML.XmlConfig xmlconfig = new XML.XmlConfig();
                xmlconfig.GetIPXML();

                //string IP = XML.XmlConfig.GetIPXML();
                Bll.T_MJAnswer t_MJAnswer = new T_MJAnswer();
                //string workBay = t_MJAnswer.GetWorkBay(IP);
                //workBayTag = t_MJAnswer.GetWorkBayTag(IP);
                workBayTag = xmlconfig.staionName;
                T_OPCTag t_OPCTag = new T_OPCTag();
                string line = xmlconfig.line;
                //string line = t_OPCTag.GetLine(workBay);
                OpcIn = MyOPCServer.OPCGroups.Add(line + "." + line);
                LineName = line;
                DataTable allOPCTag = t_OPCTag.GetAllOPCTag(workBayTag);

                int index = 1;
                foreach (DataRow item in allOPCTag.Rows)
                {
                    OpcIn.OPCItems.AddItem(line + "." + line + "." + item["kepserverAllName"].ToString().Trim(), index);
                    index++;
                }

                //OpcIn.OPCItems.AddItem("FR.FR.FR25_CODE", 1);
                //OpcIn.OPCItems.AddItem("FR.FR.FR25_IsCode", 2);

                OpcIn.UpdateRate = 50;
                OpcIn.IsActive = true;
                OpcIn.IsSubscribed = true;
                OpcIn.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(OpcInTri_DataChange);
                // blState = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                // blState = false;
                // log.writeTxt(Application.StartupPath, ErrorLog.logType.ERRORLOG, "OPC连接异常:" + ex.Message);
            }
        }

        private void OpcInTri_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            try
            {
                for (int i = 1; i <= NumItems; i++)   //FL24_REACH
                {
                    string value = ItemValues.GetValue(i).ToString().ToLower().Trim();//value值
                    int clientHandles = int.Parse(ClientHandles.GetValue(i).ToString());//唯一建
                    string tag = OpcIn.OPCItems.Item(clientHandles).ItemID;   //TAG点

                    int a = 0;
                    #region 到位
                    if (tag == LineName + "." + LineName + "." + workBayTag + "_RFID_READ_DATE")
                    {
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            //Code = ReadOpc(OpcIn, LineName + "." + LineName + "." + workBayTag + "_RFID_READ_DATE");
                            Code = value.ToUpper();
                            this.barCode.Text = Code;
                            if (!string.IsNullOrWhiteSpace(Code))
                            {
                                BarCode = this.barCode.Text;

                                Model.UserAnswerQuestions.BarCode = barCode.Text;

                                Bll.T_MJCarInfo carType = new Bll.T_MJCarInfo();
                                DataTable dt = carType.GetCarType(BarCode);
                                if (dt.Rows.Count > 0)
                                {
                                    this.carModelName.Text = dt.Rows[0]["CarModelName"].ToString();
                                    this.carType.Text = dt.Rows[0]["CarType"].ToString();
                                    ProductNum = dt.Rows[0]["ProductionNumber"].ToString();

                                }
                                else
                                {
                                    this.carModelName.Text = "无数据";
                                    this.carType.Text = "无数据";
                                }

                                if (!loadGunRslt(BarCode))
                                {
                                    MessageBox.Show("大枪扭矩值有不合格记录");
                                    BarCode = "";
                                    ProductNum = "";
                                    return;
                                }

                                this.submit.Show();
                                this.pass.Show();
                                this.bypass.Text = "请回答下列问题：";
                                if (Model.UserAnswerQuestions.UserAnswer != null)
                                {
                                    Model.UserAnswerQuestions.UserAnswer.Clear();
                                }
                                AnswerQuestions();
                                BarCode = "";
                                ProductNum = "";

                            }
                            else
                            {
                                ShowErrorMessageInfo("RFID读取验证失败");
                            }
                        }
                    }
                    #endregion

                    #region 放行
                    if (tag == LineName + "." + LineName + "." + workBayTag + "_RELEASE_READ")
                    { 
                        if (value == "true")
                        {

                            ShowErrorMessageInfo("等待托盘到位");
                            this.submit.Hide();
                            this.pass.Hide();
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        } 
        /// <summary>
        /// 发送放行信号
        /// </summary>
        public void SendRelease()
        {
            string item = LineName + "." + LineName + "." + workBayTag + "_RELEASE";
            WriteOpc(OpcIn, item, "1"); 
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
                //   log.writeTxt(Application.StartupPath, ErrorLog.logType.ERRORLOG, "写入OPC失败(WriteOpc):opcItem=" + opcItem + ",value=" + value + ":" + ex.Message);
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
                // log.writeTxt(Application.StartupPath, ErrorLog.logType.ERRORLOG, "读取OPC失败(ReadOpc):opcItem=" + opcItem + ":" + ex.Message);
                //throw new Exception("读取OPC失败(ReadOpc):opcItem=" + opcItem + "  :" + ex.Message);
            }
            return result;

        }
        #endregion


        List<AnswerControl> AnswerControlList = new List<AnswerControl>();
        #region 展示盲检问题
        private void AnswerQuestions()
        {
            try
            {
                ShowErrorMessageInfo("请回答盲检问题");
                AnswerControlList.Clear();
                panel2.Controls.Clear();

                GC.Collect();

                Bll.T_MJAnswer bt = new Bll.T_MJAnswer();
                DataTable dt = bt.GetAnswer();

                Bll.T_AnswerPic t_AnswerPic = new T_AnswerPic();

                int locationX = 0;
                int locationY = 0;
                foreach (DataRow item in dt.Rows)
                {
                    AnswerControl ac = new AnswerControl();
                    ac.QuestionNumber = item["MJProblemCode"].ToString();
                    ac.Questions = item["Problem"].ToString();
                    #region 
                    string picPath = t_AnswerPic.GetPicPath(item["Answers"].ToString());
                    if (picPath != "")
                    {
                        string localPath = GlobalPath.ApplicationPath + "\\盲检图片\\" + Path.GetFileName(picPath);
                        DownFile(picPath, localPath);
                    
                        ac.pic1 = new Bitmap(localPath);
                        ac.Answers = "";
                    }
                    else
                    {
                        ac.Answers = item["Answers"].ToString();
                    }

                    string picPath2 = t_AnswerPic.GetPicPath(item["Answers2"].ToString());
                    if (picPath2 != "")
                    {
                        string localPath = GlobalPath.ApplicationPath + "\\盲检图片\\" + Path.GetFileName(picPath2);
                        DownFile(picPath, localPath);
                 
                        ac.pic2 = new Bitmap(localPath);
                        ac.Answers2 = "";
                    }
                    else
                    {
                        ac.Answers2 = item["Answers2"].ToString();
                    }

                    string picPath3 = t_AnswerPic.GetPicPath(item["Answers3"].ToString());
                    if (picPath3 != "")
                    {
                        string localPath = GlobalPath.ApplicationPath + "\\盲检图片\\" + Path.GetFileName(picPath3);
                        DownFile(picPath, localPath);
             
                        ac.pic3 = new Bitmap(localPath);
                        ac.Answers3 = "";
                    }
                    else
                    {
                        ac.Answers3 = item["Answers3"].ToString();
                    }

                    string picPath4 = t_AnswerPic.GetPicPath(item["Answers4"].ToString());
                    if (picPath4 != "")
                    {
                        string localPath = GlobalPath.ApplicationPath + "\\盲检图片\\" + Path.GetFileName(picPath4);
                        DownFile(picPath, localPath);
            
                        ac.pic4 = new Bitmap(localPath);
                        ac.Answers4 = "";
                    }
                    else
                    {
                        ac.Answers4 = item["Answers4"].ToString();
                    }
                    #endregion

                    ac.Answers = item["Answers"].ToString();
                    ac.Answers2 = item["Answers2"].ToString();
                    ac.Answers3 = item["Answers3"].ToString();
                    ac.Answers4 = item["Answers4"].ToString();
                    ac.ZhengQue = item["ZhengQue"].ToString();
                    ac.SetInfo(panel2.Width, panel2.Height);
                    ac.Location = new Point(locationX, locationY);
                    panel2.Controls.Add(ac);
                    locationY += (ac.Height + 20);
                    AnswerControlList.Add(ac);
                }
            }
            catch (Exception ex)
            {
                this.mesInfo.Text = "未找到盲检信息";
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        WebClient webClient = new WebClient();
        private void DownFile(string httpPath, string localPath)
        {
            if (File.Exists(localPath))
            {
                return;
            }
            webClient.DownloadFile(httpPath, localPath);
        }

        #region 展示Bypass错误信息
        private void ShowBypassInfo()
        {
            panel2.Controls.Clear();

            Bll.T_Bypass t_Bypass = new Bll.T_Bypass();
            DataTable dt = t_Bypass.bypassErrorMes(BarCode);

            int locationX = 0;
            int locationY = 0;

            foreach (DataRow item in dt.Rows)
            {
                ByPassErrorInfo ef = new ByPassErrorInfo();
                ef.WorkByName = item[0].ToString();
                ef.ProductionNumber = item[1].ToString();
                ef.CarType = item[2].ToString();
                ef.CreateTime = item[3].ToString();

                ef.SetInfo(panel2.Width, panel2.Height);
                ef.Location = new Point(locationX, locationY);

                panel2.Controls.Add(ef);


                locationY += (ef.Height + 20);
            }


        }

        #endregion

        string[] GunListRobot = new string[5] {"101","201","301","401","501" };
        string[] GunlistFB = new string[2] { "1", "2" };
        Dictionary<string, Label> DicRobot = new Dictionary<string, Label>();
        Dictionary<string, Label> DicFB = new Dictionary<string, Label>();


        #region 扫描条形码
        /// <summary>
        /// 获取扫描条形码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void MangJianFrm_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                BarCode += e.KeyChar.ToString().Replace("\r", "");
                if (e.KeyChar == 13)
                {
                    //BarCode = "AXABA4000000003115179P1805170037";
                    this.barCode.Text = BarCode;
                    Model.UserAnswerQuestions.BarCode = barCode.Text;
                    Bll.T_MJCarInfo carType = new Bll.T_MJCarInfo();
                    DataTable dt = carType.GetCarType(BarCode);
                    if (dt.Rows.Count > 0)
                    {
                        this.carModelName.Text = dt.Rows[0]["CarModelName"].ToString();
                        this.carType.Text = dt.Rows[0]["CarType"].ToString();
                        ProductNum = dt.Rows[0]["ProductionNumber"].ToString();
                    }
                    else
                    {
                        this.carModelName.Text = "无数据";
                        this.carType.Text = "无数据";
                    }

                    if (!loadGunRslt(BarCode))
                    {
                        MessageBox.Show("大枪扭矩值有不合格记录");
                        BarCode = "";
                        ProductNum = "";
                        return;
                    }


                    this.bypass.Text = "请回答下列问题：";
                    AnswerQuestions();
                    this.pass.Show();
                    this.submit.Show();
                    ProductNum = "";
                    BarCode = "";
                    //Model.UserAnswerQuestions.BarCode = "";
                }
                this.label1.Focus();

            }
            catch (Exception ex)
            {
                ShowErrorMessageInfo("等待托盘到位");
                this.pass.Show();
                this.submit.Show();
            }
        }

        private bool loadGunRslt(string barcodeS)
        {
            Dictionary<string, Label> btnBagRobot = new Dictionary<string, Label>();
            Dictionary<string, Label> btnBagFB = new Dictionary<string, Label>();
            foreach (var item in GunListRobot)
            {
                string objectName = "R" + item;
                btnBagRobot[objectName] = new Label();
                btnBagRobot[objectName].Text = objectName.Substring(0, 2);
                btnBagRobot[objectName].AutoSize = false;
                btnBagRobot[objectName].BorderStyle = BorderStyle.FixedSingle;
                btnBagRobot[objectName].Size = new Size(Convert.ToInt32(flowLayoutPanel1.Width / 2 - 1), flowLayoutPanel1.Height / 4);
                btnBagRobot[objectName].Margin = new Padding(0, 0, 0, 0);
                btnBagRobot[objectName].Font = new Font("微软雅黑", 22, FontStyle.Bold);
                DicRobot.Add(item, btnBagRobot[objectName]);
                flowLayoutPanel1.Controls.Add(btnBagRobot[objectName]);
            }
            
            foreach (var item in GunlistFB)
            {
                string objName = "FB" + item;
                btnBagFB[objName] = new Label();
                if (barcodeS.Substring(0, 1) == "A")
                {
                    btnBagFB[objName].Text = "FLB3-" + item;

                }
                else if (barcodeS.Substring(0, 1) == "E")
                {
                    btnBagFB[objName].Text = "FRB3-" + item;

                }

                btnBagFB[objName].AutoSize = false;
                btnBagFB[objName].BorderStyle = BorderStyle.FixedSingle;
                btnBagFB[objName].Size = new Size(Convert.ToInt32(flowLayoutPanel1.Width / 2 - 1), flowLayoutPanel1.Height / 4);
                btnBagFB[objName].Margin = new Padding(0, 0, 0, 0);
                btnBagFB[objName].Font = new Font("微软雅黑", 22, FontStyle.Bold);
                DicFB.Add(item, btnBagFB[objName]);
                flowLayoutPanel1.Controls.Add(btnBagFB[objName]);

            }
            T_Robot_PFRecord robotPF = new T_Robot_PFRecord();
            DataTable dt = robotPF.GetPFResult(BarCode);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (GunListRobot.Contains(dt.Rows[i]["IsOK"].ToString()))
                {
                    DicRobot[dt.Rows[i]["IsOK"].ToString()].BackColor = Color.Green;
                }
            }
            DataTable dtFB = robotPF.GetFBResult(barcodeS, ProductNum);
            for (int i = 0; i < dtFB.Rows.Count; i++)
            {
                if (GunlistFB.Contains(dtFB.Rows[i]["PFIndex"].ToString()))
                {
                    DicFB[dtFB.Rows[i]["PFIndex"].ToString()].BackColor = Color.Green;
                }
            }

            //foreach (var item in DicRobot)
            //{
            //    if (item.Value.BackColor!=Color.Green)
            //    {
            //        return false;
            //    }
            //}
            //foreach (var item in DicFB)
            //{
            //    if (item.Value.BackColor != Color.Green)
            //    {
            //        return false;
            //    }

            //}
            foreach (Label item in flowLayoutPanel1.Controls)
            {
                if (item.BackColor!=Color.Green)
                {
                    return false;
                }
            }
                           
            return true;

        }

        private void MangJianFrm_Activated(object sender, EventArgs e)
        {
            this.label1.Focus();
        }

        #endregion


        #region 退出按钮
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
        }
        #endregion

        int ErrorCount = 0;
        #region 提交盲检答案
        /// <summary>
        /// 提交盲检答案，记录数据库，不正确重答
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submit_Click(object sender, EventArgs e)
        {
            try
            {
                Bll.T_MJAnswer t_MJAnswer = new Bll.T_MJAnswer();
                bool b = true;
                foreach (AnswerControl item in AnswerControlList)
                {
                    if (!item.IsSuccess)
                    {
                        b = false;
                    }
                }
                if (b)
                {
                
                    //MessageBox.Show("提交成功");
                    //保存盲检结果 1：成功 2：失败
                    if(string.IsNullOrEmpty(Model.UserAnswerQuestions.BarCode))
                    {
                        Model.UserAnswerQuestions.BarCode = this.barCode.Text;
                    }
                    int result = t_MJAnswer.SaveMJRecode(Model.UserAnswerQuestions.BarCode, 1, Bll.User.UserID, DateTime.Now);
                    if (result>0)
                    {
                        mangjianConfirm mangjianConfirm = new mangjianConfirm(Model.UserAnswerQuestions.BarCode);
                        mangjianConfirm.ShowDialog();
                        SendRelease();
                        ShowErrorMessageInfo("盲检合格，请放行");
                        this.panel2.Controls.Clear();
                        this.flowLayoutPanel1.Controls.Clear();
                        DicFB.Clear();
                        DicRobot.Clear();
                        barCode.Text = "";
                        carModelName.Text = "";
                        carType.Text = "";
                        this.pass.Hide();
                        this.submit.Hide();
                    }
                    else
                    {
                        string confrimCode = this.barCode.Text;
                        if(confrimCode == Model.UserAnswerQuestions.BarCode)
                        {
                          int a =  t_MJAnswer.SaveMJRecode(Model.UserAnswerQuestions.BarCode, 1, Bll.User.UserID, DateTime.Now);
                            if(a>0)
                            {
                                SendRelease();
                                ShowErrorMessageInfo("盲检合格，请放行");
                                this.panel2.Controls.Clear();
                                this.flowLayoutPanel1.Controls.Clear();
                                DicFB.Clear();
                                DicRobot.Clear();
                                barCode.Text = "";
                                carModelName.Text = "";
                                carType.Text = "";

                                this.pass.Hide();
                                this.submit.Hide();
                            }
                        }
                        else
                        {
                           int a2 =  t_MJAnswer.SaveMJRecode(confrimCode, 1, Bll.User.UserID, DateTime.Now);
                            if(a2>0)
                            {
                                SendRelease();
                                ShowErrorMessageInfo("盲检合格，请放行");
                                this.panel2.Controls.Clear();
                                this.pass.Hide();
                                this.submit.Hide();
                            }
                        }
                        
                    }
               

                    //EndCheckFrm ce = new EndCheckFrm();
                    //this.Visible = false;
                    //ce.ShowDialog();
                    //this.Dispose();
                }
                else
                {
                    ErrorCount++;
                    if (ErrorCount == 3)
                    {
                        ErrorCount = 0;
                        //保存盲检结果 1：成功 2：失败
                        t_MJAnswer.SaveMJRecode(Model.UserAnswerQuestions.BarCode, 2, Bll.User.UserID, DateTime.Now);
                        pass_Click(null, null);
                    }
                    else
                    {
                        ShowErrorMessageInfo("答题错误或未答" + ErrorCount + "次,请重新回答");
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //MessageBox.Show("操作错误！");
                ShowErrorMessageInfo(ex.ToString());

            }
        }
        #endregion


        #region 保存盲检错误答案
        /// <summary>
        /// 保存盲检错误答案
        /// </summary>
        private void SaveErrorAnswer()
        {
            try
            {

                Bll.T_MJAnswer ta = new Bll.T_MJAnswer();
                foreach (var item in Model.UserAnswerQuestions.UserAnswer)
                {
                    string trueAnswer = ta.GetTrueAnswer(BarCode, item.Key);

                    string value = string.Join("", item.Value);

                    if (trueAnswer != value)
                    {
                        Bll.T_MJAnswer bt = new Bll.T_MJAnswer();
                        string userId = Bll.User.UserID;
                        bt.SaveErrorAnswer(item.Key, BarCode, value, userId);
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        #endregion


        #region 重新回答盲检问题
        /// <summary>
        /// 重新回答按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void retry_Click(object sender, EventArgs e)
        {
            this.mesInfo.Text = "";

            if (this.barCode.Text == Model.UserAnswerQuestions.BarCode)
            {
                if (Model.UserAnswerQuestions.UserAnswer != null)
                {
                    Model.UserAnswerQuestions.UserAnswer.Clear();
                }

                AnswerQuestions();
            }
            else
            {
                //MessageBox.Show("请先扫描条形码");
                ShowErrorMessageInfo("等待托盘到位");
            }

        }
        #endregion


        #region 提示错误信息
        /// <summary>
        /// 提示错误信息
        /// </summary>
        /// <param name="errorText"></param>
        private void ShowErrorMessageInfo(string errorText)
        {
            this.mes.Size = new Size(this.panel2.Width, this.panel2.Height / 5);
            this.mes.Location = new Point(Convert.ToInt32(this.Width * 0.03), Convert.ToInt32(this.panel1.Height * 2.5) + this.panel2.Height);
            this.mes.BorderStyle = BorderStyle.None;

            this.mesInfo.Size = this.mes.Size;
            this.mesInfo.Text = errorText;
        }
        #endregion


        #region 强制放行
        /// <summary>
        /// 强制放行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pass_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = "强行放行.请班长输入指纹";
                if (sender == null)
                {
                    msg = "答题错误3次,请班长输入指纹放行.";
                }
                ConfirmFrm confirmFrm = new ConfirmFrm(msg);
                //MessageBox.Show("123");
                DialogResult r = confirmFrm.ShowDialog();

                if (r == DialogResult.OK)
                {

                    //MessageBox.Show(r.ToString());
                    Bll.T_MJAnswer t = new T_MJAnswer();
                    //保存盲检结果 1：成功 2：失败
                    t.SaveErrorPassLog(Model.UserAnswerQuestions.BarCode, 2, Bll.User.UserID, DateTime.Now);

                    this.panel2.Controls.Clear();
                    this.barCode.Text = "";
                    Model.UserAnswerQuestions.BarCode = "";

                    SendRelease();
                    ShowErrorMessageInfo("已强制放行,请确认放行...");
                    this.pass.Hide();
                    this.submit.Hide();
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.panel2.Controls.Clear();
                this.barCode.Text = "";
                Model.UserAnswerQuestions.BarCode = "";
                SendRelease();
            }

        }



        #endregion


        #region 删除终检照相图片

        public void DeleteZJPic()
        {
            try
            {
                //删除本地保存的安卓照片里所有图片
                DirectoryInfo dir = new DirectoryInfo(@"安卓图片");

                FileInfo[] file = dir.GetFiles();
                foreach (var item in file)
                {
                    File.Delete(item.FullName);
                }
                if (dir.GetDirectories().Length != 0)
                {
                    foreach (var item in dir.GetDirectories())
                    {
                        if (!item.ToString().Contains("$") && (!item.ToString().Contains("Boot")))
                        {
                            // Console.WriteLine(item);

                            //DeleteFiles(dir.ToString() + "\\" + item.ToString());
                        }
                    }
                }
                //Directory.Delete(@"安卓图片");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion


    }


}

