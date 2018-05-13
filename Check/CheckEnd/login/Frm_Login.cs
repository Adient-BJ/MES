using libzkfpcsharp;
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

namespace Mes.login
{
    public partial class Frm_Login : Form
    {
        Bll.T_ZhiWen ZhiWen_Bll = new Bll.T_ZhiWen();
        Bll.Base bll = new Bll.Base();
        Bll.T_Staff T_Staff_Bll = new Bll.T_Staff();

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

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        public Frm_Login()
        {
            InitializeComponent();
        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {

        }

        private void ControlSetting()
        {
            this.txt_password.UseSystemPasswordChar = true;
            this.txt_password.Multiline = false;
            this.txt_password.PasswordChar = '*';

            pic.Paint += pic_Paint;



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
                        lb_ZhiWen_Msg.Text = "没有连接的设备";
                        //  MessageBox.Show("没有连接的设备");
                    }
                }
                else
                {
                    lb_ZhiWen_Msg.Text = "没有安装驱动,或未连接";
                    //  MessageBox.Show("没有安装驱动,或未连接");
                }
            }
            catch (Exception ex)
            {
                lb_ZhiWen_Msg.Text = "没有安装驱动,或未连接";
                // MessageBox.Show("没有安装驱动,或未连接");
            }


            if (IntPtr.Zero == (mDevHandle = zkfp2.OpenDevice(0)))
            {
                lb_ZhiWen_Msg.Text = "打开设备失败";
                //MessageBox.Show("打开设备失败");
                return;
            }
            if (IntPtr.Zero == (mDBHandle = zkfp2.DBInit()))
            {
                lb_ZhiWen_Msg.Text = "初始化数据失败";
                //MessageBox.Show("初始化数据失败");
                zkfp2.CloseDevice(mDevHandle);
                mDevHandle = IntPtr.Zero;
                return;
            }

            RegisterCount = 0;//注册(录入次数)
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

            Thread captureThread = new Thread(new ThreadStart(DoCapture));
            captureThread.IsBackground = true;
            captureThread.Start();
            bIsTimeToDie = false;
            lb_ZhiWen_Msg.Text = "设备连接成功,正在加载指纹数据...";
            Thread.Sleep(500);
        }

        private void DataInitialize()
        {
            lb_ZhiWen_Msg.Text = "正在加载指纹...";
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
            lb_ZhiWen_Msg.Text = "指纹数据已加载完毕,请按指纹...";

        }
        private void DoCapture()
        {
            while (!bIsTimeToDie)
            {
                cbCapTmp = 2048;
                int ret = zkfp2.AcquireFingerprint(mDevHandle, FPBuffer, CapTmp, ref cbCapTmp);
                if (ret == zkfp.ZKFP_ERR_OK)
                {
                    SendMessage(FormHandle, MESSAGE_CAPTURED_OK, IntPtr.Zero, IntPtr.Zero);
                }
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
                    this.pic.Image = bmp;
                    {
                        int ret = zkfp.ZKFP_ERR_OK;
                        int fid = 0, score = 0;
                        ret = zkfp2.DBIdentify(mDBHandle, CapTmp, ref fid, ref score);
                        if (zkfp.ZKFP_ERR_OK == ret)
                        {
                            lb_ZhiWen_Msg.Text = "指纹识别成功,匹配度=" + score + "%";
                            MyShowDialog();
                        }
                        else
                        {
                            lb_ZhiWen_Msg.Text = "指纹识别失败,状态码=" + ret;
                            return;
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
            Pen pp = new Pen(Color.Silver);
            e.Graphics.DrawRectangle(pp, e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.X + e.ClipRectangle.Width - 1, e.ClipRectangle.Y + e.ClipRectangle.Height - 1);
        }

        private void but_Login_Click(object sender, EventArgs e)
        {
            string UserName = this.txt_user.Text;
            string PassWord = this.txt_password.Text;
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(PassWord))
            {
                lb_user_msg.Text = "用户名或密码不能为空...";
                lb_user_msg.ForeColor = Color.Red;
                return;
            }
            List<Model.T_Staff> list = T_Staff_Bll.GetAll();
            if (list.Where(m => m.LoginName == UserName && m.LoginPwd == PassWord).Count() > 0)
            {
                Bll.Session.LoginUser = list.Where(m => m.LoginName == UserName && m.LoginPwd == PassWord).Select(k => k.LoginName).FirstOrDefault();
                Bll.Session.LoginUser = list.Where(m => m.LoginName == UserName && m.LoginPwd == PassWord).Select(k => k.LoginPwd).FirstOrDefault();
                MyShowDialog();
            }
            else
            {
                lb_user_msg.Text = "用户名或密码错误...";
                lb_user_msg.ForeColor = Color.Red;
            }

        }

        private void MyShowDialog()
        {
            List<Model.T_WorkbayIPConfig> list = bll.FindT_WorkbayIPConfig().Where(m => m.IPAddress == bll.IPAddress).ToList();
            if (list.Count > 0)
            {
                switch (list[0].WorkbaySchemeID)
                {

                    case "1":
                        Main m = new Main();
                        m.Owner = this;
                        this.Hide();
                        m.ShowDialog();
                        break;
                    case "2":
                        MainGroup mg = new MainGroup();
                        mg.Owner = this;
                        this.Hide();
                        mg.ShowDialog();
                        break;
                    case "3":
                        MainFCSKTV mfcsktv = new MainFCSKTV();
                        mfcsktv.Owner = this;
                        this.Hide();
                        mfcsktv.ShowDialog();
                        break;
                    case "4":
                        MainGroupFC mfc = new MainGroupFC();
                        mfc.Owner = this;
                        this.Hide();
                        mfc.ShowDialog();
                        break;
                    case "5":
                        MainRCRB mrcrb = new MainRCRB();
                        mrcrb.Owner = this;
                        this.Hide();
                        mrcrb.ShowDialog();
                        break;
                }
            }
            else
            {
                MessageBox.Show(bll.IPAddress + " 该IP地址未配置");
            }
        }
    }
}
