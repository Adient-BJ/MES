using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISOFixCheck
{
    public partial class ISOFixChk : Form
    {
        string barCode = "";

        public ISOFixChk()
        {
            InitializeComponent();
        }

        #region 初始化窗体组件
        private void Frm_Initialize()
        {

            this.BackColor = Color.White;
            int width = this.Width;
            int height = this.Height;
            Panel p_top = new Panel();
            p_top.Width = width;
            p_top.Height = height / 12;
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
            lb_top.Text = "北京安道拓ISOFIX目视系统";
            lb_top.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lb_top.Font = new Font("微软雅黑", 22, FontStyle.Regular);
            lb_top.Location = new Point(width / 2 - lb_top.Width / 2, 0);
            lb_top.AutoSize = false;

            Panel p_mid = new Panel();
            p_mid.Width = width;
            p_mid.Height = height - p_top.Height;
            p_mid.Location = new Point(0, p_top.Height);
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
                info[i].Font = new Font("微软雅黑", 25F, System.Drawing.FontStyle.Bold);
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
            pictureBox1.Image = Image.FromFile(@"d:\a.jpg");

            //this.scanTitle.Size = new Size(Convert.ToInt32(this.Width * 0.268), Convert.ToInt32(this.Height * 0.07));
            //this.scanTitle.Location = new Point(Convert.ToInt32(this.Width * 0.15), Convert.ToInt32(this.Height * 0.1));

            //this.scanBox.Size = new Size(Convert.ToInt32(this.Width * 0.457), Convert.ToInt32(this.Height * 0.07));
            //this.scanBox.Location = new Point(this.scanTitle.Right, this.Height / 10);

            //this.scanDes.Size = new Size(Convert.ToInt32(this.Width * 0.81), Convert.ToInt32(this.Height * 0.7));
            //this.scanDes.Location = new Point(this.Width / 10, this.Height / 5);

            //this.button1.Location = new Point(1520, 989);



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

        private void ISOFixChk_Load(object sender, EventArgs e)
        {
            Frm_Initialize();
            DataTable dt = GetISOData();
            label2.Text = dt.Rows[0]["总成号"].ToString();
        }

        public DataTable GetISOData()
        {
            DataTable dtConfig = new DataTable();

            string excelPath = @"D:\ISOFIX_config.xlsx";

            string strConn;

            string excelName = "Sheet1";

            //注意：把一个excel文件看做一个数据库，一个sheet看做一张表。语法 "SELECT * FROM [sheet1$]"，表单要使用"[]"和"$"

            // 1、HDR表示要把第一行作为数据还是作为列名，作为数据用HDR=no，作为列名用HDR=yes；
            // 2、通过IMEX=1来把混合型作为文本型读取，避免null值。
            strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{0}';Extended Properties='Excel 12.0;HDR=YES;IMEX=1';";

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

        public DataTable GetALCData()
        {
            DataTable dtConfig = new DataTable();

            string excelPath = @"D:\ISOFIX_config.xlsx";

            string strConn;

            string excelName = "ALC";

            //注意：把一个excel文件看做一个数据库，一个sheet看做一张表。语法 "SELECT * FROM [sheet1$]"，表单要使用"[]"和"$"

            // 1、HDR表示要把第一行作为数据还是作为列名，作为数据用HDR=no，作为列名用HDR=yes；
            // 2、通过IMEX=1来把混合型作为文本型读取，避免null值。
            strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{0}';Extended Properties='Excel 12.0;HDR=YES;IMEX=1';";

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
                barCode += e.KeyChar.ToString().Replace("\r", "");

                if (e.KeyChar == 13)
                {
                    string acl = barCode.Substring(1, 5);
                    string acc=
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ISOFixChk_Activated(object sender, EventArgs e)
        {
            this.scanTitle.Focus();
        }
    }
}
