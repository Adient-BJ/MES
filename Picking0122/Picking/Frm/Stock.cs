using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Picking.Frm
{
    public partial class Stock : Form
    {
        public Stock()
        {
            InitializeComponent();
        }

        #region 初始化组件
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
            lb_top.Text = "北京安道拓验证分装单系统";
            lb_top.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lb_top.Font = new Font("微软雅黑", 22, FontStyle.Regular);

            lb_top.Location = new Point(width / 2 - lb_top.Width / 2, 0);
            lb_top.AutoSize = false;

            Panel p_mid = new Panel();
            p_mid.Width = width;
            p_mid.Height = height - p_top.Height;
            p_mid.Location = new Point(0, p_top.Height);
            // p_mid.BackColor = Color.Orange;




            p_top.Controls.Add(lb_top);
            p_top.Controls.Add(logo);

            this.Controls.Add(p_top);
            this.Controls.Add(p_mid);

        }

        private void Stock_Load(object sender, EventArgs e)
        {

        }
        #endregion



    }
}
