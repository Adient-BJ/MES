using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading.Tasks;
using System.IO;

namespace CheckEnd.MyControl
{
    public delegate void ClickD();
    public delegate void ClickDD(object obj);

    public partial class OptionInfo : UserControl
    {
        Bll.T_ZJCarInfo zJCarInfo = new Bll.T_ZJCarInfo();
        Bll.T_ZJInfo T_ZJInfo = new Bll.T_ZJInfo();

        public OptionInfo()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
        }

        public void SetInfo(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this.title.Width = width;
            this.title.Height = Convert.ToInt32(height * 0.08);
            this.title.Location = new Point(0, 0);

            this.divInfo.Width = width;
            this.divInfo.Height = Convert.ToInt32(height * 0.264);
            this.divInfo.Location = new Point(0, this.title.Height);

            this.pictureBox1.Width = width / 5;
            this.pictureBox1.Height = this.Height - (this.title.Height + this.divInfo.Height + this.Width / 22);
            this.pictureBox1.Location = new Point(0, (this.title.Height + this.divInfo.Height + this.Width / 22));

            this.pictureBox2.Width = width / 5;
            this.pictureBox2.Height = this.pictureBox1.Height;
            this.pictureBox2.Location = new Point(pictureBox1.Right, (this.title.Height + this.divInfo.Height + this.Width / 22));

            this.pictureBox3.Width = width / 5;
            this.pictureBox3.Height = this.pictureBox1.Height;
            this.pictureBox3.Location = new Point(pictureBox2.Right, (this.title.Height + this.divInfo.Height + this.Width / 22));

            this.pictureBox4.Width = width / 5;
            this.pictureBox4.Height = this.pictureBox1.Height;
            this.pictureBox4.Location = new Point(pictureBox3.Right, (this.title.Height + this.divInfo.Height + this.Width / 22));

            this.pictureBox5.Width = width / 5;
            this.pictureBox5.Height = this.pictureBox1.Height;
            this.pictureBox5.Location = new Point(pictureBox4.Right, (this.title.Height + this.divInfo.Height + this.Width / 22));


            this.part1.Size = new Size(this.pictureBox1.Width, this.Width / 22);
            this.part1.Location = new Point(0, (this.title.Height + this.divInfo.Height));
            this.part1.Text = "头枕";

            this.part2.Size = this.part1.Size;
            this.part2.Location = new Point(this.part1.Right, (this.title.Height + this.divInfo.Height));
            this.part2.Text = "靠垫";

            this.part3.Size = this.part1.Size;
            this.part3.Location = new Point(this.part2.Right, (this.title.Height + this.divInfo.Height));
            this.part3.Text = "正靠背";

            this.part4.Size = this.part1.Size;
            this.part4.Location = new Point(this.part3.Right, (this.title.Height + this.divInfo.Height));
            this.part4.Text = "背靠背";

            this.part5.Size = this.part1.Size;
            this.part5.Location = new Point(this.part4.Right, (this.title.Height + this.divInfo.Height));
            this.part5.Text = "底垫";

            this.label2.Width = Convert.ToInt32(width * 0.288);
            this.label2.Height = Convert.ToInt32(height * 0.072);
            this.label2.Location = new Point(Convert.ToInt32(width * 0.022), Convert.ToInt32(this.divInfo.Height * 0.130));

            this.label4.Width = Convert.ToInt32(width * 0.288);
            this.label4.Height = Convert.ToInt32(height * 0.072);
            this.label4.Location = new Point(Convert.ToInt32(width * 0.349), Convert.ToInt32(this.divInfo.Height * 0.130));

            this.label5.Width = Convert.ToInt32(width * 0.288);
            this.label5.Height = Convert.ToInt32(height * 0.072);
            this.label5.Location = new Point(Convert.ToInt32(width * 0.680), Convert.ToInt32(this.divInfo.Height * 0.130));

            this.barCode.Width = Convert.ToInt32(width * 0.288);
            this.barCode.Height = Convert.ToInt32(height * 0.072);
            this.barCode.Location = new Point(Convert.ToInt32(width * 0.022), Convert.ToInt32(this.divInfo.Height * 0.489));
            this.barCode.Text = Model.UserAnswerQuestions.BarCode;

            this.typeColor.Width = Convert.ToInt32(width * 0.288);
            this.typeColor.Height = Convert.ToInt32(height * 0.072);
            this.typeColor.Location = new Point(Convert.ToInt32(width * 0.349), Convert.ToInt32(this.divInfo.Height * 0.489));
            this.typeColor.Text = zJCarInfo.CarTypeColor(Model.UserAnswerQuestions.BarCode);

            this.carPartName.Width = Convert.ToInt32(width * 0.288);
            this.carPartName.Height = Convert.ToInt32(height * 0.072);
            this.carPartName.Location = new Point(Convert.ToInt32(width * 0.680), Convert.ToInt32(this.divInfo.Height * 0.489));
            this.carPartName.Text = zJCarInfo.CarTypeName(Model.UserAnswerQuestions.BarCode);

            GetAllPics();

        }

        public event ClickD showResult;
        public event ClickD clearDetail;

        private object Pic1 { get; set; }
        private object Pic2 { get; set; }
        private object Pic3 { get; set; }
        private object Pic4 { get; set; }
        private object Pic5 { get; set; }


        List<string> allPic = new List<string>();
        List<string> allName = new List<string>();

        public void GetAllPics()
        {

            Bll.T_ZJInfo t_ZJInfo = new Bll.T_ZJInfo();
            DataTable dt = t_ZJInfo.GetZJPartConfig();

            WebClient wc = new WebClient();

     
            foreach (DataRow item in dt.Rows)
            {
                allPic.Add(item["ImagePath"].ToString());
                allName.Add(item["ZJPartConfigName"].ToString());
            }


            for (int i = 0; i < allPic.Count; i++)
            {
                if(!System.IO.File.Exists(i.ToString()))
                {
                    wc.DownloadFile(allPic[i], i.ToString());
                }
               
            }

            if (allPic.Count == 1)
            {
                this.pictureBox1.Image = Image.FromFile("0");
                Pic1 = "0";
                this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                this.part1.Text = allName[0];

                this.pictureBox2.Hide();
                this.pictureBox3.Hide();
                this.pictureBox4.Hide();
                this.pictureBox5.Hide();
            }
            else if (allPic.Count == 2)
            {
                this.pictureBox1.Image = Image.FromFile("0");
                Pic1 = "0";
                this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                this.part1.Text = allName[0];

                this.pictureBox2.Image = Image.FromFile("1");
                Pic2 = "1";
                this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                this.part2.Text = allName[1];

                this.pictureBox3.Hide();
                this.pictureBox4.Hide();
                this.pictureBox5.Hide();
            }

            else if(allPic.Count ==3)
            {
                this.pictureBox1.Image = Image.FromFile("0");
                Pic1 = "0";
                this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                this.part1.Text = allName[0];

                this.pictureBox2.Image = Image.FromFile("1");
                Pic2 = "1";
                this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                this.part2.Text = allName[1];

                this.pictureBox3.Image = Image.FromFile("2");
                Pic3 = "2";
                this.pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                this.part3.Text = allName[2];

                this.pictureBox4.Hide();
                this.pictureBox5.Hide();
            }
            else if(allPic.Count ==4 )
            {
                this.pictureBox1.Image = Image.FromFile("0");
                Pic1 = "0";
                this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                this.part1.Text = allName[0];

                this.pictureBox2.Image = Image.FromFile("1");
                Pic2 = "1";
                this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                this.part2.Text = allName[1];

                this.pictureBox3.Image = Image.FromFile("2");
                Pic3 = "2";
                this.pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                this.part3.Text = allName[2];

                this.pictureBox4.Image = Image.FromFile("3");
                Pic4 = "3";
                this.pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                this.part4.Text = allName[3];

                this.pictureBox5.Hide();
            }

            else if(allPic.Count==5)
            {
                this.pictureBox1.Image = Image.FromFile("0");
                Pic1 = "0";
                this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                this.part1.Text = allName[0];

                this.pictureBox2.Image = Image.FromFile("1");
                Pic2 = "1";
                this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                this.part2.Text = allName[1];

                this.pictureBox3.Image = Image.FromFile("2");
                Pic3 = "2";
                this.pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                this.part3.Text = allName[2];

                this.pictureBox4.Image = Image.FromFile("3");
                Pic4 = "3";
                this.pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                this.part4.Text = allName[3];

                this.pictureBox5.Image = Image.FromFile("4");
                Pic5 = "4";
                this.pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
                this.part5.Text = allName[4];
            }

            wc.Dispose();

        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

            Bll.T_ZJInfo t_ZJInfo = new Bll.T_ZJInfo();

            DataTable dt = t_ZJInfo.GetZJPartConfigCode(allPic[0]);

            Model.CheckResult.ZJPartConfigCode = dt.Rows[0]["ZJPartConfigCode"].ToString();
            Model.CheckResult.ZJPartConfigName = dt.Rows[0]["ZJPartConfigName"].ToString();

            showResult();
            //showBigPic(Pic1);
            clearDetail();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Bll.T_ZJInfo t_ZJInfo = new Bll.T_ZJInfo();
            DataTable dt = t_ZJInfo.GetZJPartConfigCode(allPic[1]);

            Model.CheckResult.ZJPartConfigCode = dt.Rows[0]["ZJPartConfigCode"].ToString();
            Model.CheckResult.ZJPartConfigName = dt.Rows[0]["ZJPartConfigName"].ToString();

            showResult();
            //showBigPic(Pic1);
            clearDetail();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Bll.T_ZJInfo t_ZJInfo = new Bll.T_ZJInfo();
            DataTable dt = t_ZJInfo.GetZJPartConfigCode(allPic[2]);

            Model.CheckResult.ZJPartConfigCode = dt.Rows[0]["ZJPartConfigCode"].ToString();
            Model.CheckResult.ZJPartConfigName = dt.Rows[0]["ZJPartConfigName"].ToString();

            showResult();
            //showBigPic(Pic1);
            clearDetail();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Bll.T_ZJInfo t_ZJInfo = new Bll.T_ZJInfo();
            DataTable dt = t_ZJInfo.GetZJPartConfigCode(allPic[3]);

            Model.CheckResult.ZJPartConfigCode = dt.Rows[0]["ZJPartConfigCode"].ToString();
            Model.CheckResult.ZJPartConfigName = dt.Rows[0]["ZJPartConfigName"].ToString();

            showResult();
            //showBigPic(Pic1);
            clearDetail();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Bll.T_ZJInfo t_ZJInfo = new Bll.T_ZJInfo();
            DataTable dt = t_ZJInfo.GetZJPartConfigCode(allPic[4]);

            Model.CheckResult.ZJPartConfigCode = dt.Rows[0]["ZJPartConfigCode"].ToString();
            Model.CheckResult.ZJPartConfigName = dt.Rows[0]["ZJPartConfigName"].ToString();
            
            showResult();
            //showBigPic(Pic1);
            clearDetail();
        }


    }
}
