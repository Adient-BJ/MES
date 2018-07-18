using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using ISOFixCheck.BLL;

namespace ISOFixCheck
{
    public partial class ISOFixChk : Form
    {
        string _barCode = "";
        DataTable _isoData;
        DataTable _alcCode;

        public ISOFixChk()
        {
            InitializeComponent();
        }

        #region 初始化窗体组件
        private void Frm_Initialize()
        {

            this.BackColor = Color.White;
            int width = Width;
            int height = Height;
            var p_top = new Panel
            {
                Width = width,
                Height = height / 12,
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(240, 244, 247)
            };

            PictureBox logo = new PictureBox
            {
                Width = width / 8,
                Height = p_top.Height,
                Image = Properties.Resources.logo,
                Location = new Point(0, 0),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            logo.Click += confirm_Click;

            var lbTop = new Label
            {
                Width = width / 2,
                Height = p_top.Height,
                Text = "北京安道拓ISOFIX目视系统 V1.0",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new Font("微软雅黑", 22, FontStyle.Regular)
            };
            lbTop.Location = new Point(width / 2 - lbTop.Width / 2, 0);
            lbTop.AutoSize = false;

            var p_mid = new Panel
            {
                Width = width,
                Height = height - p_top.Height,
                Location = new Point(0, p_top.Height)
            };
            // p_mid.BackColor = Color.Orange;

            panel1.Width = width;
            panel1.BackColor = scanTitle.BackColor;
            panel1.Location = new Point(0, height-panel1.Height);

            List<Label> info = new List<Label>();
            info.Add(label1);
            info.Add(label2);
            info.Add(label3);
            info.Add(label4);
            info.Add(label5);
            info.Add(label6);
            //label1.BackColor = panel1.BackColor;
            //label3.BackColor = panel1.BackColor;
            //label5.BackColor = panel1.BackColor;

            int lblsHeight = height - p_top.Height - panel1.Height;
            panel2.Location = new Point(0, p_top.Height);
            panel2.Height = lblsHeight;
            panel2.Width = width / 4;
            int lblHeight = lblsHeight / 6;
            int aa = 0;
            for (int i = 0; i < info.Count; i ++)
            {
                info[i].Location = new Point(0,aa);
                info[i].Font = new Font("微软雅黑", 25F, FontStyle.Bold);
                info[i].Size = new Size(panel2.Width, lblHeight);
                info[i].AutoSize = false;
                info[i].TextAlign = ContentAlignment.MiddleCenter;
                if(i%2==0)
                {
                    info[i].BackColor = Color.Azure;
                }
                aa += lblHeight; 

            }

            pictureBox1.Location = new Point(panel2.Width, p_top.Height);
            pictureBox1.Size = new Size(width - panel2.Width, panel2.Height);
            pictureBox1.BackColor = Color.Black;

            //this.scanTitle.Size = new Size(Convert.ToInt32(this.Width * 0.268), Convert.ToInt32(this.Height * 0.07));
            //this.scanTitle.Location = new Point(Convert.ToInt32(this.Width * 0.15), Convert.ToInt32(this.Height * 0.1));

            //this.scanBox.Size = new Size(Convert.ToInt32(this.Width * 0.457), Convert.ToInt32(this.Height * 0.07));
            //this.scanBox.Location = new Point(this.scanTitle.Right, this.Height / 10);

            //this.scanDes.Size = new Size(Convert.ToInt32(this.Width * 0.81), Convert.ToInt32(this.Height * 0.7));
            //this.scanDes.Location = new Point(this.Width / 10, this.Height / 5);

            //this.button1.Location = new Point(1520, 989);



            p_top.Controls.Add(lbTop);
            p_top.Controls.Add(logo);

            Controls.Add(p_top);
            Controls.Add(p_mid);


        }
        #endregion
        private void confirm_Click(object sender, EventArgs e)
        {
            Close();
            Environment.Exit(0);
        }

        private void ISOFixChk_Load(object sender, EventArgs e)
        {
            Frm_Initialize();
            //_isoData = GetIsoData();
            //_alcCode = GetAlcData();
            ShowPic();

        }
        WebClient webClient =new WebClient();
        private void ShowPic()
        {
            ShowPic showPic=new ShowPic();
            string picPath=  showPic.GetPicPath("EXABA4000000003121939P1806260110");
            string localPath = Application.StartupPath + "\\answerPic\\";
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);//不存在就创建文件夹
            }
            webClient.DownloadFile(picPath,localPath+Path.GetFileName(picPath));

            pictureBox1.Image = Image.FromFile(localPath + Path.GetFileName(picPath));
            //throw new NotImplementedException();
        }

        public DataTable GetIsoData()
        {
            var dtConfig = new DataTable();

            string excelPath = @"D:\ISOFIX_config.xlsx";

            string excelName = "Sheet1";

            //注意：把一个excel文件看做一个数据库，一个sheet看做一张表。语法 "SELECT * FROM [sheet1$]"，表单要使用"[]"和"$"

            // 1、HDR表示要把第一行作为数据还是作为列名，作为数据用HDR=no，作为列名用HDR=yes；
            // 2、通过IMEX=1来把混合型作为文本型读取，避免null值。
            var strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{0}';Extended Properties='Excel 12.0;HDR=YES;IMEX=1';";

            string strConnection = string.Format(strConn, excelPath);
            OleDbConnection conn = new OleDbConnection(strConnection);
            conn.Open();
            OleDbDataAdapter oada = new OleDbDataAdapter("select * from [" + excelName + "$]", strConnection);

            dtConfig.TableName = "isoInfo";
            oada.Fill(dtConfig);//获得datatable
            conn.Close();
            conn.Dispose();
            oada.Dispose();
            return dtConfig;

        }

        public DataTable GetAlcData()
        {
            DataTable dtConfig = new DataTable();

            string excelPath = @"D:\ISOFIX_config.xlsx";

            const string excelName = "ALC";

            //注意：把一个excel文件看做一个数据库，一个sheet看做一张表。语法 "SELECT * FROM [sheet1$]"，表单要使用"[]"和"$"

            // 1、HDR表示要把第一行作为数据还是作为列名，作为数据用HDR=no，作为列名用HDR=yes；
            // 2、通过IMEX=1来把混合型作为文本型读取，避免null值。
            var strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{0}';Extended Properties='Excel 12.0;HDR=YES;IMEX=1';";

            string strConnection = string.Format(strConn, excelPath);
            OleDbConnection conn = new OleDbConnection(strConnection);
            conn.Open();
            OleDbDataAdapter oada = new OleDbDataAdapter("select * from [" + excelName + "$]", strConnection);

            dtConfig.TableName = "alcInfo";
            oada.Fill(dtConfig);//获得datatable
            conn.Close();
            conn.Dispose();
            oada.Dispose();
            return dtConfig;

        }

        private void ISOFixChk_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                _barCode += e.KeyChar.ToString().Replace("\r", "");

                if (e.KeyChar == 13)
                {
                    label2.Text = "";
                    label4.Text = "";
                    label6.Text = "";
                    pictureBox1.Image = null;

                    string assyNo = "";
                    string acl = _barCode.Substring(1, 5);
                    BLL.T_Workbay t_Workbay = new BLL.T_Workbay();
                    label2.Text = t_Workbay.GetProductNo(_barCode);
                    for (int i = 0; i < _alcCode.Rows.Count; i++)
                    {
                        if (_alcCode.Rows[i]["ALCCode"].ToString() == acl)
                        {
                            assyNo = _alcCode.Rows[i]["AssyNo"].ToString();
                            break;
                        }
                    }
                    foreach (DataRow item in _isoData.AsEnumerable())
                    {
                        if (item["总成号"].ToString() == assyNo)
                        {

                            label4.Text = item["配置"].ToString();
                            label6.Text = item["描述"].ToString();
                            pictureBox1.Image = Image.FromFile(@"d:\" + item["照片"] + ".jpg");

                        }
                    }
                    _barCode = "";
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ISOFixChk_Activated(object sender, EventArgs e)
        {
            scanTitle.Focus();
        }
    }
}
