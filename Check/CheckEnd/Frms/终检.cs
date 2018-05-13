using CheckEnd;
using CheckEnd.DBUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class 终检 : Form
    {
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;

        SqlHelper sqlHelper = new SqlHelper();

        public 终检(string 拍照本地路径)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            int SH = Screen.PrimaryScreen.Bounds.Height;
            int SW = Screen.PrimaryScreen.Bounds.Width;
            this.Size = new Size(SW, SH);


            PanelCom.Location = new Point(246,752);

            PanelPart.Size = new Size(888, 720);
            PanelPart.Location = new Point(40, 30);
            label5.Location = new Point((PanelPart.Width - label5.Width) / 2, 0);
            PicPart.Size = new Size(PanelPart.Width, PanelPart.Height - label5.Height);
            PicPart.Location = new Point(0, label5.Top + label5.Height);

            PanelPic.Size = this.PanelPart.Size;
            PanelPic.Location = new Point(80+PanelPart.Width,30);
            label6.Location = new Point((PanelPic.Width - label6.Width) / 2, 0);
            Pic_Pic.Size = new Size(PanelPic.Width, PanelPic.Height - label6.Height);
            Pic_Pic.Location = new Point(0, label6.Top + label6.Height);

            PanelBut.Size = new Size(417,219);
            PanelBut.Location = new Point(1105,766);

            int butLocationX = (PanelBut.Width - ButSubmit.Width - ButCancel.Width - ButDel.Width) / 2;
            ButSubmit.Location = new Point(butLocationX, (PanelBut.Height - ButSubmit.Height) / 2);
            ButCancel.Location = new Point(ButSubmit.Left + ButSubmit.Width + 5, (PanelBut.Height - ButCancel.Height) / 2);
            ButDel.Location = new Point(ButCancel.Left + ButCancel.Width + 5, (PanelBut.Height - ButDel.Height) / 2);
            
            BindCom1();
            BindCom2();

            Pic_Pic.Image = new Bitmap(拍照本地路径);
            this.AndoridPicPath = 拍照本地路径;

            this.Key = Guid.NewGuid().ToString();
        }
        //所有大部分的字段集合，key=id，value=所有属性
        Dictionary<string, Dictionary<string, object>> Com1Dic = new Dictionary<string, Dictionary<string, object>>();
        //所有选项
        Dictionary<string, Dictionary<string, object>> Com2Dic = new Dictionary<string, Dictionary<string, object>>();
        //所有错误代码
        Dictionary<string, Dictionary<string, object>> Com3Dic = new Dictionary<string, Dictionary<string, object>>();
        private void BindCom1()
        {
            DataTable dt = sqlHelper.ExecuteDataSet(connstr, "select * from T_ZJPartConfig ").Tables[0];
            //添加到字段集合
            Com1Dic.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Com1Dic.Add(dt.Rows[i]["ZJPartConfigCode"].ToString(), DataRowToDic(dt, i));
            }
            comboBox1.DisplayMember = "ZJPartConfigName";
            comboBox1.ValueMember = "ZJPartConfigCode";
            comboBox1.DataSource = dt;

        }
        private void BindCom2()
        {
            DataTable dt = sqlHelper.ExecuteDataSet(connstr, "select * from T_ZJOptionConfig ").Tables[0];
            //添加到字段集合
            Com2Dic.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Com2Dic.Add(dt.Rows[i]["ZJOptionConfigCode"].ToString(), DataRowToDic(dt, i));
            }
            comboBox2.DisplayMember = "ZJOptionConfigName";
            comboBox2.ValueMember = "ZJOptionConfigCode";
            comboBox2.DataSource = dt;
        }
        private void BindCom3()
        {
            if (comboBox2.SelectedValue != null)
            {
                string ZJOptionConfigCode = comboBox2.SelectedValue.ToString();
                DataTable dt = sqlHelper.ExecuteDataSet(connstr, "select * from T_ZJFlawDetail where ZJOptionConfigCode='" + ZJOptionConfigCode + "' ").Tables[0];
                comboBox3.DisplayMember = "ZJFlawName";
                comboBox3.ValueMember = "ZJFlawDetail";
                comboBox3.DataSource = dt;
                //添加到字段集合
                Com3Dic.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Com3Dic.Add(dt.Rows[i]["ZJFlawDetail"].ToString(), DataRowToDic(dt, i));
                }
            }
            else
            {
                comboBox3.DataSource = null;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                string ZJPartConfigCode = comboBox1.SelectedValue.ToString();
                //获取图片路径展示大部分的图
                string ImagePath = Com1Dic[ZJPartConfigCode]["ImagePath"].ToString();
                string localHostPath = Application.StartupPath + "\\downimages\\" + Path.GetFileName(ImagePath);
                if (!File.Exists(localHostPath))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(localHostPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(localHostPath));
                    }
                    //下载
                    new WebClient().DownloadFile(ImagePath, localHostPath);
                }
                PicPart.Image = new Bitmap(localHostPath);
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCom3();
        }
        /// <summary>
        /// 指定dt某行 转成字典
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private Dictionary<string, object> DataRowToDic(DataTable dt, int rowIndex)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (DataColumn item in dt.Columns)
            {
                result.Add(item.ColumnName, dt.Rows[rowIndex][item]);
            }
            return result;
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        //点击提交
        private void ButSubmit_Click(object sender, EventArgs e)
        {
            //画圈结果图片保存
            Bitmap bm = new Bitmap(PicPart.Width, PicPart.Height);
            Rectangle r = new Rectangle(0, 0, PicPart.Width, PicPart.Height);
            PicPart.DrawToBitmap(bm, r);
            string savePath = Application.StartupPath + "\\hostfile\\" + Guid.NewGuid().ToString() + ".png";
            if (!Directory.Exists(Path.GetDirectoryName(savePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            }
            bm.Save(savePath);


            string ZJPartConfigCode = comboBox1.SelectedValue.ToString();
            string ZJPartConfigName = Com1Dic[ZJPartConfigCode]["ZJPartConfigName"].ToString();
            string ZJOptionConfigCode = comboBox2.SelectedValue.ToString();
            string ZJOptionConfigName = Com2Dic[ZJOptionConfigCode]["ZJOptionConfigName"].ToString();
            string ZJFlawDetail = comboBox3.SelectedValue.ToString();
            string ZJFlawName = Com3Dic[ZJFlawDetail]["ZJFlawName"].ToString();

            this.PartPicPath = savePath;
            this.ZJPartConfigCode = ZJPartConfigCode;
            this.ZJPartConfigName = ZJPartConfigName;
            this.ZJOptionConfigCode = ZJOptionConfigCode;
            this.ZJOptionConfigName = ZJOptionConfigName;
            this.ZJFlawDetail = ZJFlawDetail;
            this.ZJFlawName = ZJFlawName;
            this.Remark = richTextBox1.Text.Trim();


            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        private void ButCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        private void ButDel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
        }

        #region 画圈代码
        private Point startPoint;
        private bool beginDragging;
        private void Form3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                beginDragging = true;
                this.PicPart.Capture = true;

                circle = new Circle();
                circle.Location = e.Location;
                Circles.Add(circle);

                this.PicPart.Cursor = System.Windows.Forms.Cursors.Cross;
            }

        }

        private void Form3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && beginDragging)
            {
                circle.Size = new Size(circle.Size.Width + e.X - startPoint.X, circle.Size.Height + e.Y - startPoint.Y);
                startPoint = new Point(e.X, e.Y);
            }
            this.PicPart.Invalidate();
        }

        private void Form3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && beginDragging)
            {
                beginDragging = false;
                this.PicPart.Capture = false;

                this.PicPart.Cursor = System.Windows.Forms.Cursors.Default;
            }
            this.PicPart.Invalidate();
        }

        private void Form3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (Circle each in Circles)
            {
                g.DrawEllipse(new Pen(Brushes.Red, 10), new Rectangle(each.Location, each.Size));
            }
        }
        private List<Circle> Circles = new List<Circle>();
        private Circle circle = new Circle();
        public class Circle
        {
            public int Radii { get; set; }
            public Size Size { get; set; }
            public Point Location { get; set; }
        }
        #endregion

        #region 自己的属性
        public string Key { get; set; }
        public bool IsModify { get; set; }
        public string ModifyKey { get; set; }
        public string ZJPartConfigCode { get; set; }
        public string ZJPartConfigName { get; set; }
        public string ZJOptionConfigCode { get; set; }
        public string ZJOptionConfigName { get; set; }
        public string ZJFlawDetail { get; set; }
        public string ZJFlawName { get; set; }
        public string Remark { get; set; }
        public string PartPicPath { get; set; }
        public string AndoridPicPath { get; set; }

        public string AndoridServerPicPath { get; set; }
        #endregion


        private void 终检_Load(object sender, EventArgs e)
        {
            if (this.IsModify == true)
            {
                this.Key = this.ModifyKey;
                comboBox1.SelectedValue = EndCheckFrm.dic[this.Key].ZJPartConfigCode;
                comboBox2.SelectedValue = EndCheckFrm.dic[this.Key].ZJOptionConfigCode;
                comboBox3.SelectedValue = EndCheckFrm.dic[this.Key].ZJFlawDetail;
                richTextBox1.Text = EndCheckFrm.dic[this.Key].Remark;
                PicPart.Image = new Bitmap(EndCheckFrm.dic[this.Key].PartPicPath);
                Pic_Pic.Image = new Bitmap(EndCheckFrm.dic[this.Key].AndoridPicPath);
            }
            else
            {
                this.ButDel.Hide();

            }


        }







    }
}
