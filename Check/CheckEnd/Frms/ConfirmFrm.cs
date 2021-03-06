﻿using CheckEnd.Bll;
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

namespace CheckEnd.Frms
{
    public partial class ConfirmFrm : Form
    {
        public ConfirmFrm(string msg)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.label1.Text = msg;
        }

        Bll.T_ZhiWen ZhiWen_Bll = new Bll.T_ZhiWen();
        Bll.User bll_user = new User();
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

        //  [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        // public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);


        private void Frm_Login_Load(object sender, EventArgs e)
        {
            pic.Paint += pic_Paint;
            FormHandle = this.Handle;

            //logoPic.Size = new Size(this.Width / 5, this.Height / 10);
            //logoPic.Location = new Point(this.Width / 3 + 100, this.Height / 10);
            logoPic.SizeMode = PictureBoxSizeMode.StretchImage;

            //this.pic.Size = new Size(this.Width / 4, this.Width / 4);
            //this.pic.Location = new Point(this.Width / 3 + 70, this.Height / 3 - 40);
            this.pic.SizeMode = PictureBoxSizeMode.StretchImage;


            DeviceInitialize();
            DataInitialize();


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
                        lb_msg.Text = "没有连接的设备";
                        //  MessageBox.Show("没有连接的设备");
                    }
                }
                else
                {
                    lb_msg.Text = "没有安装驱动,或未连接";
                    //  MessageBox.Show("没有安装驱动,或未连接");
                }
            }
            catch (Exception ex)
            {
                lb_msg.Text = "没有安装驱动,或未连接";
                // MessageBox.Show("没有安装驱动,或未连接");
            }


            if (IntPtr.Zero == (mDevHandle = zkfp2.OpenDevice(0)))
            {
                lb_msg.Text = "打开设备失败";
                //MessageBox.Show("打开设备失败");
                return;
            }
            if (IntPtr.Zero == (mDBHandle = zkfp2.DBInit()))
            {
                lb_msg.Text = "初始化数据失败";
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

            captureThread = new Thread(new ThreadStart(DoCapture));
            captureThread.IsBackground = true;
            captureThread.Start();
            bIsTimeToDie = false;
            lb_msg.Text = "设备连接成功,正在加载指纹数据...";
            Thread.Sleep(500);
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
                iFid = Convert.ToInt32(item.ZhiWenID);
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
                            try
                            {
                                lb_msg.Text = "指纹识别成功,匹配度=" + score + "%";

                                Bll.T_Staff t_Staff = new T_Staff();
                                DataTable user = t_Staff.GetUserID(fid.ToString());
                                string userId = user.Rows[0]["StaffID"].ToString();
                                Bll.User.UserID = userId;
                                bool result = t_Staff.GetRoleID(userId);
                                //MessageBox.Show(result.ToString());
                                if (result)
                                {
                                    this.DialogResult = DialogResult.OK;
                                    zkfp2.Terminate();
                                    cbRegTmp = 0;
                                    if (captureThread != null)
                                    {
                                        captureThread.Abort();
                                    }
                                    this.Close();
                                }
                                else
                                {
                                    //MessageBox.Show("请班长输入指纹！");

                                    lb_msg.BackColor = Color.Red;
                                }
                       
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                        else
                        {
                            lb_msg.Text = "指纹识别失败,状态码=" + ret;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.remark.Text !=null)
            {
                Bll.T_MJAnswer t_MJAnswer = new T_MJAnswer();
                t_MJAnswer.SaveErrorPassRecord(Model.UserAnswerQuestions.BarCode, this.remark.Text);

            }
        }



        private void ConfirmFrm_Shown(object sender, EventArgs e)
        {
            this.logoPic.Focus();
        }
    }
}
