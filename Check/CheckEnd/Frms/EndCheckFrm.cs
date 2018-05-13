using CheckEnd.Bll;
using CheckEnd.Command;
using CheckEnd.Model;
using CheckEnd.MyControl;
using CheckEnd.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OPCAutomation;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WindowsFormsApplication1;

namespace CheckEnd
{
    public partial class EndCheckFrm : Form
    {
        public string bigPics;

        public static Dictionary<string, 终检> dic = new Dictionary<string, 终检>();

        public EndCheckFrm()
        {
            InitializeComponent();

        }
        private void EndCheck_Load(object sender, EventArgs e)
        {
            Frm_Initialize();
            this.border.Visible = false;
            GetAnPic();
            OPC_Ini();
        }


        #region 初始化窗体
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
            lb_top.Text = "北京安道拓质量检查系统";
            lb_top.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lb_top.Font = new Font("微软雅黑", 22, FontStyle.Regular);
            lb_top.Location = new Point(width / 2 - lb_top.Width / 2, 0);
            lb_top.AutoSize = false;

            Panel p_mid = new Panel();
            p_mid.Width = width;
            p_mid.Height = height - p_top.Height;
            p_mid.Location = new Point(0, p_top.Height);
            // p_mid.BackColor = Color.Orange;

            this.record.Hide();

            this.bigPic.Size = new Size(this.Width, Convert.ToInt32(height * 0.65));
            this.bigPic.Location = new Point(Convert.ToInt32(width * 0.01), height / 12);

            this.record.Size = new Size(this.Width - 80, Convert.ToInt32(height * 0.3));
            this.record.Location = new Point(Convert.ToInt32(width * 0.02), this.bigPic.Height + 110);

            this.part1.Location = new Point(14, 44);
            this.part2.Location = new Point(this.part1.Right + 25, 44);
            this.part3.Location = new Point(this.part2.Right + 25, 44);

            this.remark.Size = new Size(Convert.ToInt32(this.bigPic.Width * 0.7), this.bigPic.Height / 3);
            this.remark.Location = new Point(14, 66 + this.part1.Height);

            this.save.Size = new Size(249, 98);
            this.save.Location = new Point(this.remark.Right + 60, 144);


            this.button1.Width = Convert.ToInt32(width * 0.104);
            this.button1.Height = Convert.ToInt32(height * 0.11);
            this.button1.Location = new Point(Convert.ToInt32(this.Width * 0.744), Convert.ToInt32(height * 0.851));
            this.button1.BackgroundImage = Resources.正常;

            this.label1.Hide();

            p_top.Controls.Add(lb_top);
            p_top.Controls.Add(logo);


            this.Controls.Add(p_top);
            this.Controls.Add(p_mid);


        }

        #endregion


        #region 删除安卓图片
        public void DeleteAnPic(string PicPath)
        {
            this.bigPic.Controls.RemoveByKey(PicPath);

        }

        #endregion

        #region 定时获取安卓照相图片并且点击展示大图

        private void GetAnPic()
        {
            this.timing.Enabled = true;
            this.timing.Start();
            this.timing.Interval = 5000;

        }
        int anPicX = 0;
        int anPicY = 0;
        int picCount = 0;
        WebClient wc = new WebClient();
        private void timing_Tick(object sender, EventArgs e)
        {
            try
            {
                Bll.T_ZJInfo t_ZJInfo = new T_ZJInfo();
                DataTable anDt = t_ZJInfo.GetAnPicPath();

            

                if (anDt.Rows.Count > 0)
                {

                    foreach (DataRow item in anDt.Rows)
                    {
                        string PicPath = item["AnPicPath"].ToString();
                        wc.DownloadFile(PicPath, @"安卓图片\" + item["AnPicID"].ToString().Trim());

                        bigPicListControl bigPicListControl = new bigPicListControl();
                        bigPicListControl.AndoridServerPicPath = item["AnPicPath"].ToString();
                        bigPicListControl.listPic = new Bitmap(@"安卓图片\" + item["AnPicID"].ToString());
                        //这里保存图片路径
                        bigPicListControl.PicPath = @"安卓图片\" + item["AnPicID"].ToString();
                        //bigPicListControl.PicPath =PicPath;

                        bigPicListControl.Name = @"安卓图片\" + item["AnPicID"].ToString();
                        bigPicListControl.SetInfo(this.bigPic.Width / 6, this.bigPic.Height);
                        bigPicListControl.Location = new Point(anPicX, anPicY);
                        bigPicListControl.deletePic += DeleteAnPic;
                        anPicX += bigPicListControl.Width + 30;


                        this.bigPic.Controls.Add(bigPicListControl);
                        //picCount++;

                        //if (this.bigPic.Controls.Count == picCount)
                        //{
                            t_ZJInfo.UpdateAnPic(PicPath, Model.UserAnswerQuestions.BarCode);
                        //}

                    }

                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }



        #endregion



        #region 记录选择的终检缺陷
        /// <summary>
        /// 根据图片地址展示保存的缺陷内容
        /// </summary>
        /// <param name="picPath"></param>

        private void ShowZJSavedFlaw(string path)
        {
            this.part1.Items.Clear();
            this.part1.DataSource = null;
            //   MessageBox.Show(this.part1.SelectedText);
            this.part1.Text = "";


            this.part2.Items.Clear();
            this.part2.DataSource = null;
            //   MessageBox.Show(this.part1.SelectedText);
            this.part2.Text = "";


            this.part3.Items.Clear();
            this.part3.DataSource = null;
            //   MessageBox.Show(this.part1.SelectedText);
            this.part3.Text = "";

            this.remark.Clear();

            Bll.T_CheckResult t_CheckResult = new T_CheckResult();
            DataTable dt = t_CheckResult.GetZJFlawSaved(path);

            DataTable Part1 = t_CheckResult.GetZJPartName();

            this.label1.Text = path;
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    DataTable option = t_CheckResult.GetZJOptionName(dt.Rows[0]["ZJFlawDetail"].ToString());
                    this.part1.Items.Add(dt.Rows[0]["ZJPartName"]);
                    this.part1.SelectedItem = dt.Rows[0]["ZJPartName"];
                    this.part2.Items.Add(option.Rows[0]["ZJOptionConfigName"]);
                    this.part2.SelectedItem = option.Rows[0]["ZJOptionConfigName"];
                    this.part3.Items.Add(dt.Rows[0]["ZJFlawDetail"]);
                    this.part3.SelectedItem = dt.Rows[0]["ZJFlawDetail"];
                    this.remark.Text = dt.Rows[0]["Remark"].ToString();

                }
                else
                {
                    foreach (DataRow item in Part1.Rows)
                    {
                        this.part1.Items.Add(item["ZJPartConfigName"]);
                    }

                }
            }
            else
            {
                foreach (DataRow item in Part1.Rows)
                {
                    this.part1.Items.Add(item["ZJPartConfigName"]);
                }
            }

        }
        private void part1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.part1.Items.Count < 2)
            {
                Bll.T_CheckResult t_CheckResult = new T_CheckResult();
                DataTable Part1 = t_CheckResult.GetZJPartName();
                foreach (DataRow item in Part1.Rows)
                {
                    this.part1.Items.Add(item["ZJPartConfigName"]);
                }
            }

            if (this.part2.Items.Count > 0)
            {

            }
            else
            {
                Bll.T_CheckResult t_CheckResult = new T_CheckResult();
                DataTable part2 = t_CheckResult.GetZJOptionName();

                foreach (DataRow item in part2.Rows)
                {
                    this.part2.Items.Add(item["ZJOptionConfigName"]);
                }
            }

        }

        private void part2_SelectedValueChanged(object sender, EventArgs e)
        {
            this.part3.Items.Clear();
            this.part3.DataSource = null;
            Bll.T_CheckResult t_CheckResult = new T_CheckResult();
            DataTable part3 = t_CheckResult.GetZJFlawDetail(this.part2.SelectedItem.ToString());
            foreach (DataRow item in part3.Rows)
            {
                this.part3.Items.Add(item["ZJFlawName"]);
            }


        }

        /// <summary>
        /// 保存每个终检的缺陷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_Click(object sender, EventArgs e)
        {

            try
            {
                string barCode = Model.UserAnswerQuestions.BarCode;
                string picPath = this.label1.Text;
                string part1 = this.part1.SelectedItem.ToString();
                string part3 = this.part3.SelectedItem.ToString();
                string remark = this.remark.Text;

                Bll.T_CheckResult t_CheckResult = new T_CheckResult();
                DataTable dt = t_CheckResult.GetZJFlawSaved(picPath);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        t_CheckResult.UpdateZJFlawSaved(barCode, picPath, part1, part3, remark);
                        MessageBox.Show("修改成功");
                    }
                    else
                    {
                        if (part3 == "" || this.remark.Text == "")
                        {
                            MessageBox.Show("请选择缺陷或输入备注");
                        }
                        else
                        {
                            t_CheckResult.SaveZJFlawSaved(barCode, picPath, part1, part3, remark);
                            MessageBox.Show("保存成功");
                        }

                    }

                }
                else
                {
                    t_CheckResult.SaveZJFlawSaved(barCode, picPath, part1, part3, remark);
                    MessageBox.Show("保存成功");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败" + ex.Message);
            }


        }



        #endregion


        #region 确认保存终检缺陷
        /// <summary>
        /// 确认保存终检缺陷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                Bll.T_CheckResult t_CheckResult = new T_CheckResult();
                string masterBarCode = Model.UserAnswerQuestions.BarCode;


                if (this.bigPic.Controls.Count == 0)
                {
                    SendRelease();
                }
                else if (this.bigPic.Controls.Count <= dic.Keys.Count)
                {
                    SendRelease();

                    foreach (var item in dic)
                    {
                        t_CheckResult.SaveZJFlawSaved(masterBarCode, item.Value.AndoridServerPicPath, item.Value.ZJPartConfigName, item.Value.ZJFlawName, item.Value.Remark);
                    }
                }
                else
                {
                    MessageBox.Show("请添加缺陷或备注");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        #region 按钮
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            this.button1.BackgroundImage = Resources.按下;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            this.button1.BackgroundImage = Resources.选中;
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            this.button1.BackgroundImage = Resources.正常;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            this.button1.BackgroundImage = Resources.正常;
        }

        #endregion

        #endregion



        #region OPC操作

        //OPC连接初始化
        public OPCAutomation.OPCServer MyOPCServer;
        OPCGroup OpcIn;
        OPCServer OpcSvr;


        private string WorkbayName;

        private string workBayTag;

        private string LineName;
        private void OPC_Ini()
        {
            try
            {

                MyOPCServer = new OPCServer();
                MyOPCServer.Connect("KEPware.KEPServerEx.V6", "193.100.101.221");
                //OpcIn = MyOPCServer.OPCGroups.Add("FR.FR");

                XML.XmlConfig xmlConfig = new XML.XmlConfig();
                xmlConfig.GetIPXML();

                //string IP = XML.XmlConfig.GetIPXML();
                Bll.T_MJAnswer t_MJAnswer = new T_MJAnswer();
                //string workBay = t_MJAnswer.GetWorkBay(IP);
                //workBayTag = t_MJAnswer.GetWorkBayTag(IP);
                workBayTag = xmlConfig.staionName;
                T_OPCTag t_OPCTag = new T_OPCTag();
                //string line = t_OPCTag.GetLine(workBay);
                string line = xmlConfig.line;
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
                    if (tag == LineName + "." + LineName + "." + workBayTag + "_RELEASE_READ")
                    {
                        if (value == "true")
                        {
                            bigPic.Controls.Clear();
                            bigPic.Dispose();
                            this.timing.Dispose();
                            //MangJianFrm mangJianFrm = new MangJianFrm();
                            //mangJianFrm.Owner = this;
                            //this.Hide();
                            //mangJianFrm.ShowDialog();
                            this.Dispose();
                        }
                    }
                    #region 到位


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
            this.timing.Stop();
            this.timing.Dispose();
            string item = LineName + "." + LineName + "." + workBayTag + "_RELEASE";
            if (WriteOpc(OpcIn, item, "1"))
            {
                this.bigPic.Hide();
                this.record.Hide();
                this.button1.Hide();

                this.border.Visible = true;
                this.border.Width = this.Width / 2;
                this.border.Height = this.Height / 2;
                border.Location = new Point(this.Width / 2 - this.border.Width / 2, this.Height / 2 - this.border.Height / 2);
                errorInfo.Width = this.border.Width;
                errorInfo.Height = this.border.Height;
                errorInfo.Location = new Point(0, 0);
                errorInfo.BackColor = Color.White;
                errorInfo.ForeColor = Color.GreenYellow;

                //this.border.Paint += Panel_Paint;
                this.bigPic.Dispose();
            }
            else
            {
                MessageBox.Show("发送放行信号失败");


            }


        }


        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            Panel la = sender as Panel;
            ControlPaint.DrawBorder(e.Graphics,
                        la.ClientRectangle,
                        System.Drawing.Color.FromArgb(64, 64, 64),
                        3,
                        ButtonBorderStyle.Solid,
                        System.Drawing.Color.FromArgb(64, 64, 64),
                        3,
                        ButtonBorderStyle.Solid,
                       System.Drawing.Color.FromArgb(64, 64, 64),
                       3,
                       ButtonBorderStyle.Solid,
                       System.Drawing.Color.FromArgb(64, 64, 64),
                       3,
                       ButtonBorderStyle.Solid);
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


        #endregion

        private void remark_Enter(object sender, EventArgs e)
        {
            ShowKeyBorad();
        }


        public static void ShowKeyBorad()
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序
            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine("osk.exe&exit");
            p.StandardInput.AutoFlush = true;
            p.Close();
        }
        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="processName">进程名</param>
        private void KillProcess(string processName)
        {
            Process[] myproc = Process.GetProcesses();
            foreach (Process item in myproc)
            {
                if (item.ProcessName == processName)
                {
                    item.Kill();
                }
            }
        }

 
    }

}
#endregion
