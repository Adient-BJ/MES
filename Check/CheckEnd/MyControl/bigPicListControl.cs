using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace CheckEnd.MyControl
{
    public partial class bigPicListControl : UserControl
    {


        
        public string AndoridServerPicPath { get; set; }
        public string PicPath { get; set; }
        public Bitmap listPic { get; set; }

        public string key { get; set; }

        public delegate void ShowZJInfo(string b) ;

        public bigPicListControl()
        {
            InitializeComponent();
        }



        public void SetInfo(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this.list.Size = new Size(width, height/2);
            this.list.Location = new Point(0, 0);
            this.list.Image = listPic;
            //if(listPic!=null)
            //{
            //    listPic.Dispose();
            //}
            this.label1.Size = new Size(this.Width, Convert.ToInt32(height / 2/4));
            this.label1.Location = new Point(0,this.list.Height);
            this.label1.Text = "请添加图片缺陷";

            this.label2.Size = this.label1.Size;
            this.label2.Location = new Point(0,this.label1.Top+this.label1.Height);
            this.label2.Text = "请添加图片缺陷";

            this.label3.Size = this.label1.Size;
            this.label3.Location = new Point(0, this.label2.Top + this.label2.Height);
            this.label3.Text = "请添加图片缺陷";

            this.label4.Size = this.label1.Size;
            this.label4.Location = new Point(0, this.label3.Top + this.label3.Height);
            this.label4.Text = "请添加图片缺陷";

            this.list.SizeMode = PictureBoxSizeMode.Zoom;



        }

        public event ShowZJInfo deletePic;
        private void list_Click(object sender, EventArgs e)
        {
            if(PicPath!=null)
            {
                终检 f = new 终检(PicPath);
                f.AndoridServerPicPath = this.AndoridServerPicPath;
                f.IsModify = false;
                DialogResult r = f.ShowDialog();
                if (r == System.Windows.Forms.DialogResult.OK)
                {
                    EndCheckFrm.dic.Add(f.Key, f);
                    this.key = f.Key;
                    this.list.Click -= list_Click;
                    this.label1.Text = EndCheckFrm.dic[this.key].ZJPartConfigName;
                    this.label2.Text = EndCheckFrm.dic[this.key].ZJOptionConfigName;
                    this.label3.Text = EndCheckFrm.dic[this.key].ZJFlawName;
                    this.label4.Text = EndCheckFrm.dic[this.key].Remark;

                }
                if (r == System.Windows.Forms.DialogResult.No)
                {
                    EndCheckFrm.dic.Remove(f.Key); 
                }
               
            }
            
        }

        private void DeletePic()
        {
            this.list.Hide();
        }

        private void list_DoubleClick(object sender, EventArgs e)
        {
            if(PicPath!=null)
            {
                if(this.key !=null)
                {
                    终检 f = new 终检(EndCheckFrm.dic[this.key].AndoridPicPath);
                    f.AndoridServerPicPath = this.AndoridServerPicPath;
                    f.IsModify = true;
                    f.ModifyKey = this.key;
                    DialogResult r = f.ShowDialog();
                    if (r == System.Windows.Forms.DialogResult.OK)
                    {
                        EndCheckFrm.dic[this.key] = f;
                        this.label1.Text = EndCheckFrm.dic[this.key].ZJPartConfigName;
                        this.label2.Text = EndCheckFrm.dic[this.key].ZJOptionConfigName;
                        this.label3.Text = EndCheckFrm.dic[this.key].ZJFlawName;
                        this.label4.Text = EndCheckFrm.dic[this.key].Remark;
                    }
                    if (r == System.Windows.Forms.DialogResult.No)
                    {
                        EndCheckFrm.dic.Remove(f.Key);
                        deletePic(PicPath);
                    }
                }
                else
                {
                    MessageBox.Show("请先单机添加");
                }
            }
        }

    }
}