using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CheckEnd.Properties;

namespace CheckEnd.MyControl
{
    public partial class BigPicControl : Form
    {

        public Bitmap BigPic { get; set; }

        public delegate void HidePic();


        public BigPicControl()
        {

        }

        public BigPicControl(Bitmap pic)
        {
            try
            {
                InitializeComponent();
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Delete.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Delete.Image = Resources.Delete;
                BigPic = pic;
                this.bigPicShow.SizeMode = PictureBoxSizeMode.Zoom;
                this.bigPicShow.Image = BigPic;
            }
            catch
            {

            }

        }
        
        public event HidePic HidePics;

        public void SetInfo(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this.bigPicShow.Size = new Size(this.Width, this.Height);
            this.bigPicShow.Location = new Point(0, 0);

            this.Delete.Size = new Size(Convert.ToInt32(this.bigPicShow.Width * 0.06), Convert.ToInt32(this.bigPicShow.Height * 0.07));
            this.Delete.Location = new Point(this.bigPicShow.Width - this.Delete.Width, 0);

        }

        private void Delete_Click(object sender, EventArgs e)
        {

            MessageBoxButtons messageBoxButtons = MessageBoxButtons.OKCancel;
            DialogResult dialogResult = MessageBox.Show("确定删除吗？", "删除图片", messageBoxButtons);
            if (dialogResult == DialogResult.OK)
            {
                HidePics();
                this.Close();
                this.Visible = false;
                this.Dispose();

            }

        }

    }
}
