using libzkfpcsharp;
using Mes.DBUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace Mes.login
{
    public partial class Login : Form
    {
   //     string updateInfoPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "UpdateInfo.xml");
        private ErrorLog log = new ErrorLog();
        Bll.V_StaffZhiWen ZhiWen_Bll = new Bll.V_StaffZhiWen();
        Bll.Base bll = new Bll.Base();
        Bll.T_Staff T_Staff_Bll = new Bll.T_Staff();
        Bll.T_LoginRecord T_LoginRecord_Bll = new Bll.T_LoginRecord();
        Global.GlobalFieid global = new Global.GlobalFieid();

        List<Model.T_Staff> list = new List<Model.T_Staff>();
        public bool IsQuailty = false;
        public bool IsCraft = false;
        public bool IsProduction = false;

        Thread captureThread;

        IntPtr mDevHandle = IntPtr.Zero;
        IntPtr mDBHandle = IntPtr.Zero;
        IntPtr FormHandle = IntPtr.Zero;
        bool bIsTimeToDie = false;
        bool IsRegister = false;
        bool bIdentify = true;
        byte[] FPBuffer;
        int RegisterCount = 0;
        const int REGISTER_FINGER_COUNT = 3;

        byte[][] RegTmps = new byte[3][];
        byte[] RegTmp = new byte[2048];
        byte[] CapTmp = new byte[2048];
        int cbCapTmp = 2048;
        int cbRegTmp = 0;
        int iFid = 1;

        private int mfpWidth = 0;
        private int mfpHeight = 0;

        const int MESSAGE_CAPTURED_OK = 0x0400 + 6;

   //     [DllImport("user32.dll", EntryPoint = "SendMessageA")]
    //    [DllImport("user32.dll", EntryPoint = "PostMessage")]
       // public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        public Login()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = "MacOS.ssk";
        }

        private void Login_Load(object sender, EventArgs e)
        {
            LoadFrm();
            this.BackColor = Color.Wheat;
            this.TransparencyKey = Color.Wheat;
            tabControl1.Location = new Point(this.Width / 2 - tabControl1.Width / 2, this.Height / 2 - tabControl1.Height/2);
            //CheckVersion();
            FormHandle = this.Handle;
            ControlSetting();

            DeviceInitialize();
            DataInitialize();

            GetStaffAll();

            Global.GlobalFieid.IsOneLogin = false;

        }

        private void LoadFrm() 
        {
            if (Global.GlobalFieid.LoginType=="1")
            {
                pan_zw_1.Visible = false;
                pan_zw_2.Visible = true;
                lb_ZhiWen_Msg_2.Text = "请员工输入指纹...";
                pan_zw_3.Visible = false;
                pan_txt_1.Visible = false;
                pan_txt_2.Visible = true;
                lb_txt_msg_2.Text = "请员工登入...";
                pan_txt_3.Visible = false;
               
            }
            if (Global.GlobalFieid.LoginType == "2")
            {
                pan_zw_1.Visible = false;
                pan_zw_2.Visible = true;
                lb_ZhiWen_Msg_2.Text = "请班长输入指纹...";
                pan_zw_3.Visible = false;
                pan_txt_1.Visible = false;
                pan_txt_2.Visible = true;
                lb_txt_msg_2.Text = "请班长登入...";
                pan_txt_3.Visible = false;

                bnt_Close.Visible = true;
            }
            if (Global.GlobalFieid.LoginType == "3" || Global.GlobalFieid.LoginType == "4" || Global.GlobalFieid.LoginType == "5")
            {
                pan_zw_1.Visible = true;
                lb_ZhiWen_Msg_1.Text = "请生产输入指纹...";
                pan_zw_2.Visible = true;
                lb_ZhiWen_Msg_2.Text = "请质量输入指纹...";
                pan_zw_3.Visible = true;
                lb_ZhiWen_Msg_3.Text = "请工艺输入指纹...";
                pan_txt_1.Visible = true;
                lb_txt_msg_1.Text = "请生产登入...";
                pan_txt_2.Visible = true;
                lb_txt_msg_2.Text = "请质量登入...";
                pan_txt_3.Visible = true;
                lb_txt_msg_3.Text = "请工艺登入...";
                bnt_Close.Visible = true;
            }
        }

        private void GetStaffAll()
        {
            list = T_Staff_Bll.GetAll();
        }
        
        private void ControlSetting()
        {
            this.txt_password_1.UseSystemPasswordChar = true;
            this.txt_password_1.Multiline = false;
            this.txt_password_1.PasswordChar = '*';

            this.txt_password_2.UseSystemPasswordChar = true;
            this.txt_password_2.Multiline = false;
            this.txt_password_2.PasswordChar = '*';

            this.txt_password_3.UseSystemPasswordChar = true;
            this.txt_password_3.Multiline = false;
            this.txt_password_3.PasswordChar = '*';

            pic_1.Paint += pic_Paint;
            pic_2.Paint += pic_Paint;
            pic_3.Paint += pic_Paint;



        }

        private void DeviceInitialize()
        {
            try
            {
                int ret = zkfperrdef.ZKFP_ERR_OK;
                if ((ret = zkfp2.Init()) == zkfperrdef.ZKFP_ERR_OK)
                {
                    int nCount = zkfp2.GetDeviceCount();//共有多少个指纹设备
                    if (nCount > 0)
                    {
                        //for (int i = 0; i < nCount; i++)
                        //{
                        //    cmbIdx.Items.Add(i.ToString());
                        //} 
                    }
                    else
                    {
                        zkfp2.Terminate();
                        lb_ZhiWen_Msg_1.Text = "没有连接的设备";
                        lb_ZhiWen_Msg_2.Text = "没有连接的设备";
                        lb_ZhiWen_Msg_3.Text = "没有连接的设备";
                        //  MessageBox.Show("没有连接的设备");
                    }
                }
                else
                {
                 //   lb_ZhiWen_Msg_1.Text = "没有安装驱动,或未连接";
                  //  lb_ZhiWen_Msg_2.Text = "没有安装驱动,或未连接";
                 // //  lb_ZhiWen_Msg_3.Text = "没有安装驱动,或未连接";
                    //  MessageBox.Show("没有安装驱动,或未连接");
                }
            }
            catch (Exception ex)
            {
              //  lb_ZhiWen_Msg_1.Text = "没有安装驱动,或未连接";
              //  lb_ZhiWen_Msg_2.Text = "没有安装驱动,或未连接";
               // lb_ZhiWen_Msg_3.Text = "没有安装驱动,或未连接";
                // MessageBox.Show("没有安装驱动,或未连接");
            }


            if (IntPtr.Zero == (mDevHandle = zkfp2.OpenDevice(0)))
            {
                lb_ZhiWen_Msg_1.Text = "打开设备失败";
                lb_ZhiWen_Msg_2.Text = "打开设备失败";
                lb_ZhiWen_Msg_3.Text = "打开设备失败";
                //MessageBox.Show("打开设备失败");
                return;
            }
            if (IntPtr.Zero == (mDBHandle = zkfp2.DBInit()))
            {
                lb_ZhiWen_Msg_1.Text = "初始化数据失败";
                lb_ZhiWen_Msg_2.Text = "初始化数据失败";
                lb_ZhiWen_Msg_3.Text = "初始化数据失败";
                //MessageBox.Show("初始化数据失败");
                zkfp2.CloseDevice(mDevHandle);
                mDevHandle = IntPtr.Zero;
                return;
            }
            RegisterCount = 0;
            cbRegTmp = 0;
            iFid = 1;
            for (int i = 0; i < 3; i++)
            {
                RegTmps[i] = new byte[2048];
            }
            byte[] paramValue = new byte[4];
            int size = 4;
            zkfp2.GetParameters(mDevHandle, 1, paramValue, ref size);
            zkfp2.ByteArray2Int(paramValue, ref mfpWidth);

            size = 4;
            zkfp2.GetParameters(mDevHandle, 2, paramValue, ref size);
            zkfp2.ByteArray2Int(paramValue, ref mfpHeight);

            FPBuffer = new byte[mfpWidth * mfpHeight];

            captureThread = new Thread(new ThreadStart(DoCapture));
            captureThread.IsBackground = true;
            captureThread.Start();
            bIsTimeToDie = false;
           
           // lb_ZhiWen_Msg_1.Text = "设备连接成功,正在加载指纹数据...";
           // Thread.Sleep(500);
        }

        private void DataInitialize()
        {
        //    lb_ZhiWen_Msg_1.Text = "正在加载指纹...";
            //从数据库获取所有指纹
            List<Model.T_ZhiWen> list = ZhiWen_Bll.GetAll();
            string[] arrs = new string[0];
            byte[] bys = new byte[0];
            foreach (Model.T_ZhiWen item in list)
            {
                arrs = item.BytesStr.ToString().Split(',');
                bys = new byte[arrs.Length];
                iFid = item.ZhiWenID;
                for (int i = 0; i < bys.Length; i++)
                {
                    bys[i] = Convert.ToByte(arrs[i]);
                }
                zkfp2.DBAdd(mDBHandle, iFid, bys);
            }
     //       lb_ZhiWen_Msg_1.Text = "指纹数据已加载完毕,请按指纹...";

        }
        private void DoCapture()
        {
            while (!bIsTimeToDie)
            {
                cbCapTmp = 2048;
                int ret = zkfp2.AcquireFingerprint(mDevHandle, FPBuffer, CapTmp, ref cbCapTmp);
                if (ret == zkfp.ZKFP_ERR_OK)
                {
                    PostMessage(FormHandle, MESSAGE_CAPTURED_OK, IntPtr.Zero, IntPtr.Zero);
                }
             //  MessageBox.Show("ret :" + ret + "\n\t zkfp.ZKFP_ERR_OK : " + zkfp.ZKFP_ERR_OK);
                Thread.Sleep(200);
            }
        }

        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case MESSAGE_CAPTURED_OK:
                    MemoryStream ms = new MemoryStream();
                    BitmapFormat.GetBitmap(FPBuffer, mfpWidth, mfpHeight, ref ms);
                    Bitmap bmp = new Bitmap(ms);
                //    this.pic.Image = bmp;
                    {
                        int ret = zkfp.ZKFP_ERR_OK;
                        int fid = 0, score = 0;
                        ret = zkfp2.DBIdentify(mDBHandle, CapTmp, ref fid, ref score);
                        if (zkfp.ZKFP_ERR_OK == ret)
                        {
                            List<Model.T_Staff> staffList = list.Where(i => i.ZhiWenID.Split(',').Contains(fid.ToString())).ToList() ;
                         //   List<Model.T_Staff> staffList = T_Staff_Bll.GetUser(fid.ToString());
                            if (Global.GlobalFieid.LoginType=="1")
                            {
                                if (staffList.Select(k => k.RoleID).FirstOrDefault().ToString().Split(',').Contains("7"))
                                {
                                    Global.GlobalFieid.LoginName = staffList.Select(k => k.LoginName).FirstOrDefault();
                                    Global.GlobalFieid.UserName = staffList.Select(k => k.StaffName).FirstOrDefault();
                                    Global.GlobalFieid.User_Pic_Url = staffList.Select(k => k.TitileImagePath).FirstOrDefault();
                                    Global.GlobalFieid.StaffID = staffList.Select(k => k.StaffID).FirstOrDefault();
                                    MyShowDialog();
                                }
                                else
                                {
                                    lb_ZhiWen_Msg_2.Text = "只能员工登入...";
                                    lb_ZhiWen_Msg_2.ForeColor = Color.Red;
                                    return;
                                }
                            }
                            else if (Global.GlobalFieid.LoginType=="2")
                            {
                                if (staffList.Select(k => k.RoleID).FirstOrDefault().ToString().Split(',').Contains("2"))
                                {
                                    Global.GlobalFieid.SquadLeaderID = staffList.Select(k => k.StaffID).FirstOrDefault();
                                    MyShowDialog();
                                   // DefWndProc_Click(null,null);
                                  //  lb_ZhiWen_Msg_2.Text = "班长审核成功..";
                                //    this.button1.PerformClick();
                                }
                                else
                                {
                                    lb_ZhiWen_Msg_2.Text = "只能班长审核...";
                                    lb_ZhiWen_Msg_2.ForeColor = Color.Red;
                                    return;
                                }
                            }
                            else if (Global.GlobalFieid.LoginType == "3" || Global.GlobalFieid.LoginType == "4" || Global.GlobalFieid.LoginType == "5")
                            {
                                if (staffList.Select(k => k.RoleID).FirstOrDefault().ToString().Split(',').Contains("5") || staffList.Select(k => k.RoleID).FirstOrDefault().ToString().Split(',').Contains("6") || staffList.Select(k => k.RoleID).FirstOrDefault().ToString().Split(',').Contains("8"))
                                {
                                    if (staffList.Select(k => k.RoleID).FirstOrDefault().ToString().Split(',').Contains("5")) 
                                    {
                                        if (!IsProduction)
                                        {
                                            IsProduction = true;
                                            Global.GlobalFieid.ProductionID = staffList.Select(k => k.StaffID).FirstOrDefault();
                                            lb_ZhiWen_Msg_1.Text = "生产验证成功..";
                                            lb_ZhiWen_Msg_1.ForeColor = Color.Green;
                                            if (IsProduction && IsQuailty && IsCraft)
                                            {
                                                MyShowDialog();
                                            }
                                        }

                                    }else  if (staffList.Select(k => k.RoleID).FirstOrDefault().ToString().Split(',').Contains("6"))
                                    {
                                        if (!IsQuailty)
                                        {
                                            IsQuailty = true;
                                            Global.GlobalFieid.QuailtyID = staffList.Select(k => k.StaffID).FirstOrDefault();
                                            lb_ZhiWen_Msg_2.Text = "质量验证成功..";
                                            lb_ZhiWen_Msg_2.ForeColor = Color.Green;
                                            if (IsProduction && IsQuailty && IsCraft)
                                            {
                                                MyShowDialog();
                                            }
                                        }

                                    }
                                    else if (staffList.Select(k => k.RoleID).FirstOrDefault().ToString().Split(',').Contains("8"))
                                    {
                                        if (!IsCraft)
                                        {
                                            IsCraft = true;
                                            Global.GlobalFieid.CraftID = staffList.Select(k => k.StaffID).FirstOrDefault();
                                            lb_ZhiWen_Msg_3.Text = "工艺验证成功..";
                                            lb_ZhiWen_Msg_3.ForeColor = Color.Green;
                                            if (IsProduction && IsQuailty && IsCraft)
                                            {
                                                MyShowDialog();
                                            }
                                        }

                                    }                               
                                }
                                else
                                {
                                    if (!IsProduction)
                                    {
                                        lb_ZhiWen_Msg_1.Text = "只能生产,质量,工艺审核";
                                        lb_ZhiWen_Msg_1.ForeColor = Color.Red;
                                    }
                                    else if (!IsQuailty)
                                    {
                                        lb_ZhiWen_Msg_2.Text = "只能生产,质量,工艺审核";
                                        lb_ZhiWen_Msg_2.ForeColor = Color.Red;

                                    }
                                    else if (!IsCraft)
                                    {
                                        lb_ZhiWen_Msg_3.Text = "只能生产,质量,工艺审核";
                                        lb_ZhiWen_Msg_3.ForeColor = Color.Red;
                                    }
                                        return;
                                }
                            }
                            //else if (Global.GlobalFieid.LoginType == "4")
                            //{

                            //}
                            //else if (Global.GlobalFieid.LoginType == "5")
                            //{

                            //}
                        }
                        else
                        {
                            if (Global.GlobalFieid.LoginType=="1")
                            {
                                lb_ZhiWen_Msg_2.Text = "指纹识别失败";
                                lb_ZhiWen_Msg_2.ForeColor = Color.Red;
                            }
                            else if (Global.GlobalFieid.LoginType=="2")
                            {
                                lb_ZhiWen_Msg_2.Text = "指纹识别失败";
                                lb_ZhiWen_Msg_2.ForeColor = Color.Red;
                            }
                            else if (Global.GlobalFieid.LoginType=="3")
                            {
                                if (!IsProduction)
                                {
                                    lb_ZhiWen_Msg_1.Text = "指纹识别失败";
                                    lb_ZhiWen_Msg_1.ForeColor = Color.Red;
                                }
                                else if (!IsQuailty)
                                {
                                    lb_ZhiWen_Msg_2.Text = "指纹识别失败";
                                    lb_ZhiWen_Msg_2.ForeColor = Color.Red;

                                }
                                else if (!IsCraft)
                                {
                                    lb_ZhiWen_Msg_3.Text = "指纹识别失败";
                                    lb_ZhiWen_Msg_3.ForeColor = Color.Red;
                                }
                            }
                        }
                    }
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }
        private void pic_Paint(object sender, PaintEventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            Pen pp = new Pen(Color.White);
            e.Graphics.DrawRectangle(pp, e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.X + e.ClipRectangle.Width - 1, e.ClipRectangle.Y + e.ClipRectangle.Height - 1);
        }

        private void but_Login_Click(object sender, EventArgs e)
        {

                string  UserName1 = this.txt_user_1.Text;
                string  PassWord1 = this.txt_password_1.Text;
               if (string.IsNullOrWhiteSpace(UserName1) || string.IsNullOrWhiteSpace(PassWord1))
               {
                   lb_txt_msg_1.Text = "用户名或密码不能为空...";
                   lb_txt_msg_1.ForeColor = Color.Red;
                   return;
               }

               List<Model.T_Staff> ModelList = list.Where(m => m.LoginName == UserName1 && m.LoginPwd == PassWord1).ToList();
            if (ModelList.Count() > 0)
            {
                if (ModelList.Select(m => m.RoleID).FirstOrDefault().ToString().Split(',').Contains("5"))
                {
                    Global.GlobalFieid.ProductionID = ModelList.Select(k => k.StaffID).FirstOrDefault();
                    IsProduction = true;
                    but_Login_1.Enabled = false;
                    lb_txt_msg_1.Text = "生产登录成功";
                    lb_txt_msg_1.ForeColor = Color.Green;
                    if (IsProduction && IsCraft && IsQuailty)
                    {
                        MyShowDialog();
                    }

                }
                else
                {
                    lb_txt_msg_1.Text = "需要生产的权限...";
                    lb_txt_msg_1.ForeColor = Color.Red;
                    return;
                }
               
            }
            else
            {
                lb_txt_msg_1.Text = "用户名或密码错误...";
                lb_txt_msg_1.ForeColor = Color.Red;
            }
               
        }
        private void MyShowDialog()
        {
            zkfp2.Terminate();
            cbRegTmp = 0;
            if (captureThread != null)
            {
                captureThread.Abort();
            }
            Global.GlobalFieid.IsLogin = true;
            if (Global.GlobalFieid.LoginType=="1")
            {
                Global.GlobalFieid.LoginRecordID = Guid.NewGuid().ToString();
                T_LoginRecord_Bll.Add(Global.GlobalFieid.LoginRecordID,bll.WorkbayName,Global.GlobalFieid.StaffID);
                Global.GlobalFieid.IsLogOut = false;
                if (Global.GlobalFieid.MyControl.Name == "MainGroup")
                {
                    Mes.MainGroup Main = new MainGroup();
                    Main = (MainGroup)this.Owner;
                    Main.SetStaff();
                    this.Dispose();
                } else if (Global.GlobalFieid.MyControl.Name == "MainGroupFC")
                {
                    Mes.MainGroupFC Main = new MainGroupFC();
                    Main = (MainGroupFC)this.Owner;
                    Main.SetStaff();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "Main")
                {
                    Mes.Main Main = new Main();
                    Main = (Main)this.Owner;
                    Main.SetStaff();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "MainFCSKTV")
                {
                    Mes.MainFCSKTV Main = new MainFCSKTV();
                    Main = (MainFCSKTV)this.Owner;
                    Main.SetStaff();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "MainRCRB")
                {
                    Mes.MainRCRB Main = new MainRCRB();
                    Main = (MainRCRB)this.Owner;
                    Main.SetStaff();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "MainGroupRCRB")
                {
                    Mes.MainGroupRCRB Main = new MainGroupRCRB();
                    Main = (MainGroupRCRB)this.Owner;
                    Main.SetStaff();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "MainGroupM")
                {
                    Mes.MainGroupM Main = new MainGroupM();
                    Main = (MainGroupM)this.Owner;
                    Main.SetStaff();
                    this.Dispose();
                }
            }
            if (Global.GlobalFieid.LoginType == "2")
            {              
                if (Global.GlobalFieid.MyControl.Name == "MainGroup")
                {
                    Mes.MainGroup Main = new MainGroup();
                    Main = (MainGroup)this.Owner;
                    Main.Rework();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "MainGroupFC")
                {
                    MainGroupFC Main;
                    Main = (MainGroupFC)this.Owner;
                    Main.Rework();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "Main")
                {
                    Main Main;
                    Main = (Main)this.Owner;
                    Main.Rework();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "MainFCSKTV")
                {
                    MainFCSKTV Main;
                    Main = (MainFCSKTV)this.Owner;
                    Main.Rework();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "MainRCRB")
                {
                    MainRCRB Main;
                    Main = (MainRCRB)this.Owner;
                    Main.Rework();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "MainGroupRCRB")
                {
                    MainGroupRCRB Main;
                    Main = (MainGroupRCRB)this.Owner;
                    Main.Rework();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "MainGroupM")
                {
                    MainGroupM Main;
                    Main = (MainGroupM)this.Owner;
                    Main.Rework();
                    this.Dispose();
                }
            }

            if (Global.GlobalFieid.LoginType == "3")
            {
                IsProduction = false;
                IsCraft = false;
                IsQuailty = false;
                if (Global.GlobalFieid.MyControl.Name == "MainGroup")
                {
                    MainGroup Main;
                    Main = (MainGroup)this.Owner;
                    Main.Bypass();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "MainGroupFC")
                {
                    MainGroupFC Main;
                    Main = (MainGroupFC)this.Owner;
                    Main.Bypass();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "Main")
                {
                    Main Main;
                    Main = (Main)this.Owner;
                    Main.Bypass();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "MainFCSKTV")
                {
                    MainFCSKTV Main;
                    Main = (MainFCSKTV)this.Owner;
                    Main.Bypass();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "MainRCRB")
                {
                    MainRCRB Main;
                    Main = (MainRCRB)this.Owner;
                    Main.Bypass();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "MainGroupRCRB")
                {
                    MainGroupRCRB Main;
                    Main = (MainGroupRCRB)this.Owner;
                    Main.Bypass();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "MainGroupM")
                {
                    MainGroupM Main;
                    Main = (MainGroupM)this.Owner;
                    Main.Bypass();
                    this.Dispose();
                }
            }

            if (Global.GlobalFieid.LoginType == "4")
            {
                IsProduction = false;
                IsCraft = false;
                IsQuailty = false;
                if (Global.GlobalFieid.MyControl.Name == "MainGroup")
                {
                    MainGroup Main;
                    Main = (MainGroup)this.Owner;
                    Main.TPS_Stop();
                    this.Dispose();
                } 
                else if (Global.GlobalFieid.MyControl.Name == "MainRCRB")
                {
                    MainRCRB Main;
                    Main = (MainRCRB)this.Owner;
                    Main.TPS_Stop();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "MainGroupRCRB")
                {
                    MainGroupRCRB Main;
                    Main = (MainGroupRCRB)this.Owner;
                    Main.TPS_Stop();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "Main")
                {
                    Main Main;
                    Main = (Main)this.Owner;
                    Main.TPS_Stop();
                    this.Dispose();
                }
                //else if (Global.GlobalFieid.MyControl.Name == "MainGroupFC")
                //{
                //    MainGroupFC Main;
                //    Main = (MainGroupFC)this.Owner;
                //    Main.Bypass();
                //    this.Dispose();
                //}
                //else if (Global.GlobalFieid.MyControl.Name == "MainGroupM")
                //{
                //    MainGroupM Main;
                //    Main = (MainGroupM)this.Owner;
                //    Main.Bypass();
                //    this.Dispose();
                //}
               
                //else if (Global.GlobalFieid.MyControl.Name == "MainFCSKTV")
                //{
                //    MainFCSKTV Main;
                //    Main = (MainFCSKTV)this.Owner;
                //    Main.Bypass();
                //    this.Dispose();
                //}
               
            }
            if (Global.GlobalFieid.LoginType == "5")
            {
                IsProduction = false;
                IsCraft = false;
                IsQuailty = false;
                if (Global.GlobalFieid.MyControl.Name == "MainGroup")
                {
                    MainGroup Main;
                    Main = (MainGroup)this.Owner;
                    Main.TPS_Start();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "MainRCRB")
                {
                    MainRCRB Main;
                    Main = (MainRCRB)this.Owner;
                    Main.TPS_Start();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "MainGroupRCRB")
                {
                    MainGroupRCRB Main;
                    Main = (MainGroupRCRB)this.Owner;
                    Main.TPS_Start();
                    this.Dispose();
                }
                else if (Global.GlobalFieid.MyControl.Name == "Main")
                {
                    Main Main;
                    Main = (Main)this.Owner;
                    Main.TPS_Start();
                    this.Dispose();
                }
                //else if (Global.GlobalFieid.MyControl.Name == "MainGroupFC")
                //{
                //    MainGroupFC Main;
                //    Main = (MainGroupFC)this.Owner;
                //    Main.Bypass();
                //    this.Dispose();
                //}
                //else if (Global.GlobalFieid.MyControl.Name == "Main")
                //{
                //    Main Main;
                //    Main = (Main)this.Owner;
                //    Main.Bypass();
                //    this.Dispose();
                //}
                //else if (Global.GlobalFieid.MyControl.Name == "MainFCSKTV")
                //{
                //    MainFCSKTV Main;
                //    Main = (MainFCSKTV)this.Owner;
                //    Main.Bypass();
                //    this.Dispose();
                //}
                
            }
        }

        //#region 自动更新
        //private void CheckVersion()
        //{
        //    if (File.Exists(updateInfoPath))
        //    {
        //        try
        //        {
        //            XmlDocument xml = new XmlDocument();
        //            xml.Load(updateInfoPath);
        //            string UpdatePath = string.Empty;
        //            string local_Version = string.Empty;
        //            foreach (XmlNode node in xml.ChildNodes)
        //            {
        //                if (node.Name == "AppInfo")
        //                {
        //                    foreach (XmlNode nodes in node.ChildNodes)
        //                    {
        //                        if (nodes.Name == "UpdatePath")
        //                        {
        //                            UpdatePath = nodes.InnerText;
        //                        }
        //                        if (nodes.Name == "Version")
        //                        {
        //                            local_Version = nodes.InnerText;
        //                        }
        //                    }
        //                }
        //            }
        //            if (string.IsNullOrWhiteSpace(UpdatePath) || string.IsNullOrWhiteSpace(local_Version))
        //            {
        //                log.writeTxt(Application.StartupPath, ErrorLog.logType.ERRORLOG, "获取版本信息错误,建议您重新安装MES系统");
        //                MessageBox.Show("获取版本信息错误,建议您重新安装MES系统");
        //                return;
        //            }
        //            XmlDocument xmln = new XmlDocument();
        //            xmln.Load(Path.Combine(UpdatePath, "UpdateInfo.xml"));
        //            string server_Version = string.Empty;
        //            foreach (XmlNode node in xmln.ChildNodes)
        //            {
        //                if (node.Name == "AppInfo")
        //                {
        //                    foreach (XmlNode nodes in node.ChildNodes)
        //                    {
        //                        if (nodes.Name == "Version")
        //                        {
        //                            server_Version = nodes.InnerText;
        //                        }
        //                    }
        //                }
        //            }
        //            if (string.IsNullOrWhiteSpace(server_Version))
        //            {
        //                log.writeTxt(Application.StartupPath, ErrorLog.logType.ERRORLOG, "无法获取服务器版本信息,请联系服务器管理员");
        //                MessageBox.Show("无法获取服务器版本信息,请联系服务器管理员");
        //                return;
        //            }
        //            if (server_Version != local_Version)
        //            {
        //                log.writeTxt(Application.StartupPath, ErrorLog.logType.ERRORLOG, "MES系统有新版本,请及时更新以免影响生产");
        //                MessageBox.Show("MES系统有新版本,请及时更新以免影响生产");

        //            }
        //        }
        //        catch
        //        {
        //            log.writeTxt(Application.StartupPath, ErrorLog.logType.ERRORLOG, "获取版本信息失败,建议您重新安装MES系统");
        //            MessageBox.Show("获取版本信息失败,建议您重新安装MES系统");

        //        }
        //    }
        //    else
        //    {
        //        log.writeTxt(Application.StartupPath, ErrorLog.logType.ERRORLOG, "版本信息配置文件不存在,建议您重新安装MES系统");
        //        MessageBox.Show("版本信息配置文件不存在,建议您重新安装MES系统");
        //    }
        //}

        //#endregion

        private void lb_close_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void but_Login_2_Click(object sender, EventArgs e)
        {
            string UserName2 = this.txt_user_2.Text;
            string PassWord2 = this.txt_password_2.Text;
            List<Model.T_Staff> ModelList = new List<Model.T_Staff>();
            if (string.IsNullOrWhiteSpace(UserName2) || string.IsNullOrWhiteSpace(PassWord2))
            {
                lb_txt_msg_2.Text = "用户名或密码不能为空...";
                lb_txt_msg_2.ForeColor = Color.Red;
                return;
            }
            ModelList = list.Where(m => m.LoginName == UserName2 && m.LoginPwd == PassWord2).ToList();
            if (Global.GlobalFieid.LoginType == "1" )
            {
                if (ModelList.Count() > 0)
                {
                    if (ModelList.Select(m => m.RoleID).FirstOrDefault().ToString().Split(',').Contains("7"))
                    {
                        Global.GlobalFieid.LoginName = ModelList.Select(k => k.LoginName).FirstOrDefault();
                        Global.GlobalFieid.UserName = ModelList.Select(k => k.StaffName).FirstOrDefault();
                        Global.GlobalFieid.User_Pic_Url = ModelList.Select(k => k.TitileImagePath).FirstOrDefault();
                        Global.GlobalFieid.StaffID = ModelList.Select(k => k.StaffID).FirstOrDefault();

                        MyShowDialog();
                    }
                    else
                    {
                        lb_txt_msg_2.Text = "只有员工可以登录...";
                        lb_txt_msg_2.ForeColor = Color.Red;
                        return;
                    }
                
                }
                else
                {
                    lb_txt_msg_2.Text = "用户名或密码错误...";
                    lb_txt_msg_2.ForeColor = Color.Red;
                    return;
                }
            }
            if (Global.GlobalFieid.LoginType == "2")
            {
                if (ModelList.Count() > 0)
                {
                    if (ModelList.Select(m => m.RoleID).FirstOrDefault().ToString().Split(',').Contains("2"))
                    {
                        Global.GlobalFieid.SquadLeaderID = ModelList.Select(k => k.StaffID).FirstOrDefault();
                        MyShowDialog();
                    }
                    else
                    {
                        lb_txt_msg_2.Text = "需要班长的权限...";
                        lb_txt_msg_2.ForeColor = Color.Red;
                        return;
                    }
                
                }
                else
                {
                    lb_txt_msg_2.Text = "用户名或密码错误...";
                    lb_txt_msg_2.ForeColor = Color.Red;
                }
            }
            if (Global.GlobalFieid.LoginType == "3" || Global.GlobalFieid.LoginType == "4" || Global.GlobalFieid.LoginType == "5")
            {
                if (ModelList.Count() > 0)
                {
                    if (ModelList.Select(m => m.RoleID).FirstOrDefault().ToString().Split(',').Contains("6"))
                    {
                        Global.GlobalFieid.QuailtyID = ModelList.Select(k => k.StaffID).FirstOrDefault();
                        IsQuailty = true;
                        but_Login_2.Enabled = false;
                        lb_txt_msg_2.Text = "质量登录成功";
                        lb_txt_msg_2.ForeColor = Color.Green;
                        if (IsProduction && IsCraft && IsQuailty)
                        {
                            MyShowDialog();
                        }
                     
                    }
                    else
                    {
                        lb_txt_msg_2.Text = "需要质量的权限...";
                        lb_txt_msg_2.ForeColor = Color.Red;
                        return;
                    }

                }
                else
                {
                    lb_txt_msg_2.Text = "用户名或密码错误...";
                    lb_txt_msg_2.ForeColor = Color.Red;
                }
            }
        }

        private void but_Login_3_Click(object sender, EventArgs e)
        {
            string UserName3 = this.txt_user_3.Text;
            string PassWord3= this.txt_password_3.Text;
            if (string.IsNullOrWhiteSpace(UserName3) || string.IsNullOrWhiteSpace(PassWord3))
            {
                lb_txt_msg_3.Text = "用户名或密码不能为空...";
                lb_txt_msg_3.ForeColor = Color.Red;
                return;
            }

            List<Model.T_Staff> ModelList = list.Where(m => m.LoginName == UserName3 && m.LoginPwd == PassWord3).ToList();
            if (ModelList.Count() > 0)
            {
                if (ModelList.Select(m => m.RoleID).FirstOrDefault().ToString().Split(',').Contains("8"))
                {
                    Global.GlobalFieid.CraftID = ModelList.Select(k => k.StaffID).FirstOrDefault();
                    IsCraft = true;
                    but_Login_3.Enabled = false;
                    lb_txt_msg_3.Text = "工艺登录成功";
                    lb_txt_msg_3.ForeColor = Color.Green;
                    if (IsProduction && IsCraft && IsQuailty)
                    {
                        MyShowDialog();
                    }

                }
                else
                {
                    lb_txt_msg_3.Text = "需要工艺的权限...";
                    lb_txt_msg_3.ForeColor = Color.Red;
                    return;
                }

            }
            else
            {
                lb_txt_msg_3.Text = "用户名或密码错误...";
                lb_txt_msg_3.ForeColor = Color.Red;
            }
        }

        private void bnt_Close_Click(object sender, EventArgs e)
        {
            zkfp2.Terminate();
            cbRegTmp = 0;
            if (captureThread != null)
            {
                captureThread.Abort();
            }
            Global.GlobalFieid.IsLogin = true;
            this.Dispose();
        }

        private void txt_user_2_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start(@"C:\WINDOWS\system32\osk.exe");//调出屏幕键盘
            //txt_user_2.Focus();
        }
    }
}
