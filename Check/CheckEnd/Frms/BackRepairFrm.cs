using CheckEnd.MyControl;
using CheckEnd.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckEnd
{
    public partial class BackRepairFrm : Form
    {
        

        public BackRepairFrm()
        {
            InitializeComponent();
         
        }

        private void BackRepair_Load(object sender, EventArgs e)
        {
            Frm_Initialize();
            ShowCanRepairList(false);
            ShowConfirmedReworkList(false);
         
        }


        #region 初始化窗体
        private void Frm_Initialize()
        {

            this.BackColor = Color.WhiteSmoke;
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
            lb_top.Text = "北京江森返修系统";
            lb_top.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lb_top.Font = new Font("微软雅黑", 22, FontStyle.Regular);
            lb_top.Location = new Point(width / 2 - lb_top.Width / 2, 0);
            lb_top.AutoSize = false;
            lb_top.ForeColor = Color.FromArgb(110, 129, 137);

            Panel p_mid = new Panel();
            p_mid.Width = width;
            p_mid.Height = height - p_top.Height;
            p_mid.Location = new Point(0, p_top.Height);
            // p_mid.BackColor = Color.Orange;

            this.panel1.Size = new System.Drawing.Size(Convert.ToInt32(this.Width * 0.454), Convert.ToInt32(this.Height * 0.364));
            this.panel1.Location = new Point(Convert.ToInt32(this.Width * 0.030), (lb_top.Height + this.Height / 27));

            this.canRepairListTitle.Size = new System.Drawing.Size(this.panel1.Width, this.panel1.Height / 9);
            this.canRepairListTitle.Location = new Point(0, 0);

            this.canRepairAllCheck.Size = new System.Drawing.Size(this.panel1.Width / 25, this.panel1.Height / 9);
            this.canRepairAllCheck.Location = new Point(this.panel1.Width / 25, this.canRepairListTitle.Height + 10);

            this.label1.Size = new System.Drawing.Size(this.panel1.Width / 3, this.canRepairAllCheck.Height);
            this.label1.Location = new Point(this.canRepairAllCheck.Right, this.canRepairListTitle.Height + 10);

            this.label2.Size = new System.Drawing.Size(this.panel1.Width / 2, this.label1.Height);
            this.label2.Location = new Point(this.label1.Right, this.canRepairListTitle.Height + 10);

            this.canRepairList.Size = new System.Drawing.Size(this.panel1.Width, this.panel1.Height - (this.label2.Height + this.canRepairListTitle.Height + this.panel1.Height / 5));
            this.canRepairList.Location = new Point(0, this.canRepairListTitle.Height + this.label2.Height + 15);

            this.confirm.Size = new System.Drawing.Size(this.panel1.Width / 6, this.panel1.Height / 9);
            this.confirm.Location = new Point(this.panel1.Width / 5, (this.panel1.Height - this.panel1.Height / 8));

            this.panel2.Size = new Size(this.panel1.Width, this.panel1.Height);
            this.panel2.Location = new Point(this.panel1.Right + this.Width / 40, (lb_top.Height + this.Height / 27));

            this.confirmedRepairTitle.Size = this.canRepairListTitle.Size;
            this.confirmedRepairTitle.Location = this.canRepairListTitle.Location;

            this.confirmedCheckAll.Size = this.canRepairAllCheck.Size;
            this.confirmedCheckAll.Location = this.canRepairAllCheck.Location;

            this.label4.Size = this.label1.Size;
            this.label4.Location = this.label1.Location;

            this.label5.Size = this.label2.Size;
            this.label5.Location = this.label2.Location;

            this.confirmedRework.Size = this.canRepairList.Size;
            this.confirmedRework.Location = this.canRepairList.Location;

            this.changePartLogTitle.Size = new Size(this.panel1.Width * 2 + Convert.ToInt32(this.Width * 0.030), this.canRepairListTitle.Height);
            this.changePartLogTitle.Location = new Point(Convert.ToInt32(this.Width * 0.030), this.Height / 2 );

            this.changePartLog.Size = new System.Drawing.Size(this.panel1.Width * 2 + Convert.ToInt32(this.Width * 0.030),this.panel1.Height-40);
            this.changePartLog.Location = new Point(Convert.ToInt32(this.Width * 0.030),(this.Height/2+this.changePartLogTitle.Height));

            this.submit.Size = new Size(Convert.ToInt32(this.Width*0.1),Convert.ToInt32(this.Height*0.06));
            this.submit.Location = new Point(Convert.ToInt32(this.Width*0.42),Convert.ToInt32(this.Height*0.861));
            

            this.confirm.BackgroundImage = Resources.确认1;
            this.submit.BackgroundImage = Resources.提交1;

            p_top.Controls.Add(lb_top);
            p_top.Controls.Add(logo);

            this.Controls.Add(p_top);
            this.Controls.Add(p_mid);


        }
        #endregion


        #region 初始化可返修列表
        private void ShowCanRepairList(bool isCheck)
        {

            this.canRepairList.Controls.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("可返修的零件");
            dt.Columns.Add("描述");

            for (int i = 0; i <= 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "零件" + i;
                dr[1] = "描述qqqqqqqqqqqqqqqqqq" + i;
                dt.Rows.Add(dr);
            }

            int locationX = 0;
            int locationY = 0;
            foreach (DataRow item in dt.Rows)
            {
                CanRepairListControl canRepairListC = new CanRepairListControl();
                canRepairListC.Part = item[0].ToString();
                canRepairListC.Description = item[1].ToString();
                canRepairListC.IsCheck = isCheck;
                canRepairListC.SetInfo(this.canRepairList.Width, this.canRepairList.Height);
                canRepairListC.Location = new Point(locationX, locationY);

                this.canRepairList.Controls.Add(canRepairListC);
                locationY += canRepairListC.Height;
            }
        }


        private void canRepairAllCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.canRepairAllCheck.CheckState == CheckState.Checked)
            {
                ShowCanRepairList(true);
            }
            else
            {
                ShowCanRepairList(false);
            }

        }

        #endregion


        #region 初始化已确认返修列表
        private void ShowConfirmedReworkList(bool isCheck)
        {
            this.confirmedRework.Controls.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("可返修的零件");
            dt.Columns.Add("描述");

            for (int i = 0; i <= 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "零件" + i;
                dr[1] = "描述qqqqqqqqqqqqqqqqqq" + i;
                dt.Rows.Add(dr);
            }

            int locationX = 0;
            int locationY = 0;
            foreach (DataRow item in dt.Rows)
            {
                CanRepairListControl canRepairListC = new CanRepairListControl();
                canRepairListC.Part = item[0].ToString();
                canRepairListC.Description = item[1].ToString();
                canRepairListC.IsCheck = isCheck;
                canRepairListC.SetInfo(this.confirmedRework.Width, this.confirmedRework.Height);
                canRepairListC.Location = new Point(locationX, locationY);

                this.confirmedRework.Controls.Add(canRepairListC);
                locationY += canRepairListC.Height;
            }
        }

        private void confirmedCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.confirmedCheckAll.CheckState == CheckState.Checked)
            {
                ShowConfirmedReworkList(true);
            }
            else
            {
                ShowConfirmedReworkList(false);
            }

        }

        #endregion


        #region 确认按钮
        private void confirm_Click(object sender, EventArgs e)
        {

        }

        private void confirm_MouseEnter(object sender, EventArgs e)
        {
            this.confirm.BackgroundImage = Resources.确认3;
        }

        private void confirm_MouseDown(object sender, MouseEventArgs e)
        {
            this.confirm.BackgroundImage = Resources.确认2;
        }

        private void confirm_MouseLeave(object sender, EventArgs e)
        {
            this.confirm.BackgroundImage = Resources.确认1;
        }

        private void confirm_MouseUp(object sender, MouseEventArgs e)
        {
            this.confirm.BackgroundImage = Resources.确认3;
        }
        

        #endregion

   
        #region 提交按钮
        private void submit_Click(object sender, EventArgs e)
        {

        }

        private void submit_MouseEnter(object sender, EventArgs e)
        {
            this.submit.BackgroundImage = Resources.提交3;
        }

        private void submit_MouseDown(object sender, MouseEventArgs e)
        {
            this.submit.BackgroundImage = Resources.提交2;
        }

        private void submit_MouseLeave(object sender, EventArgs e)
        {
            this.submit.BackgroundImage = Resources.提交1;
        }



        #endregion


    }


  




}

