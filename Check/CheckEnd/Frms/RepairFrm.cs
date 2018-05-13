using CheckEnd.MyControl;
using CheckEnd.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckEnd
{
    public partial class RepairFrm : Form
    {
        public RepairFrm()
        {
            InitializeComponent();
        }

        private void RepairFrm_Load(object sender, EventArgs e)
        {
            Frm_Initialize();
            GetOrderInfo();
        }

        #region 初始化窗体  
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

            this.scanLabel.Size = new Size(Convert.ToInt32(this.Width * 0.232), Convert.ToInt32(this.Height * 0.119));
            this.scanLabel.Location = new Point(this.Width / 10, p_top.Height + 20);

            this.scan.Size = new Size(Convert.ToInt32(this.scanLabel.Width * 1.4), Convert.ToInt32(this.scanLabel.Height * 0.5));
            this.scan.Location = new Point(this.scanLabel.Right, p_top.Height + Convert.ToInt32(this.Height * 0.05));

            this.search.Size = new Size(Convert.ToInt32(this.Width * 0.070), Convert.ToInt32(this.Height * 0.050));
            this.search.Location = new Point(this.scan.Right + 100, p_top.Height + Convert.ToInt32(this.Height * 0.058));
            //this.search.Image = Resources.正常;
            this.search.BackgroundImageLayout = ImageLayout.Stretch;

            this.repairOrderInfo.Size = new Size(Convert.ToInt32(this.Width * 0.928), Convert.ToInt32(this.Height * 0.562));
            this.repairOrderInfo.Location = new Point(Convert.ToInt32(this.Width * 0.026), Convert.ToInt32(this.Height * 0.235));

            this.label1.Size = new Size(this.repairOrderInfo.Width / 4, this.repairOrderInfo.Height / 12);
            this.label1.Location = new Point(0, 0);

            this.label2.Size = new Size(this.repairOrderInfo.Width / 4, this.repairOrderInfo.Height / 12);
            this.label2.Location = new Point(this.label1.Right, 0);

            this.label3.Size = new Size(this.repairOrderInfo.Width / 4, this.repairOrderInfo.Height / 12);
            this.label3.Location = new Point(this.label2.Right, 0);

            this.label4.Size = new Size(this.repairOrderInfo.Width / 4, this.repairOrderInfo.Height / 12);
            this.label4.Location = new Point(this.label3.Right, 0);

            this.InfoPanel.Size = new Size(this.repairOrderInfo.Width, (this.repairOrderInfo.Height - this.label1.Height));
            this.InfoPanel.Location = new Point(0, this.label1.Height);


            this.dismantle.Size = new Size(Convert.ToInt32(this.Width * 0.147), Convert.ToInt32(this.Height * 0.076));
            this.dismantle.Location = new Point(Convert.ToInt32(this.Width * 0.214), Convert.ToInt32(this.Height * 0.867));
            this.dismantle.BackgroundImage = Resources.拆_正常_;

            this.repair.Size = new Size(Convert.ToInt32(this.Width * 0.147), Convert.ToInt32(this.Height * 0.076));
            this.repair.Location = new Point(Convert.ToInt32(this.Width * 0.621), Convert.ToInt32(this.Height * 0.867));
            this.repair.BackgroundImage = Resources.修_正常_;


            p_top.Controls.Add(lb_top);
            p_top.Controls.Add(logo);

            this.Controls.Add(p_top);
            this.Controls.Add(p_mid);



        }

        #endregion
      
        #region 按钮
        private void dismantle_MouseEnter(object sender, EventArgs e)
        {
            this.dismantle.BackgroundImage = Resources.拆_选中_;
        }

        private void dismantle_MouseDown(object sender, MouseEventArgs e)
        {
            this.dismantle.BackgroundImage = Resources.拆_按下_;
        }

        private void dismantle_MouseLeave(object sender, EventArgs e)
        {
            this.dismantle.BackgroundImage = Resources.拆_正常_;
        }

        private void repair_MouseDown(object sender, MouseEventArgs e)
        {
            this.repair.BackgroundImage = Resources.修_按下_;
        }

        private void repair_MouseEnter(object sender, EventArgs e)
        {
            this.repair.BackgroundImage = Resources.修_选中_;
        }

        private void repair_MouseLeave(object sender, EventArgs e)
        {
            this.repair.BackgroundImage = Resources.修_正常_;
        }
        #endregion


        #region 获取订单信息
        public void GetOrderInfo()
        {
            this.InfoPanel.Controls.Clear();

            DataTable dt = new DataTable();
            dt.Columns.Add("订单号");
            dt.Columns.Add("描述");
            dt.Columns.Add("描述2");
            dt.Columns.Add("描述3");

            for (int i = 0; i < 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "订单" + i;
                dr[1] = "描述" + i;
                dr[2] = "描述" + i;
                dr[3] = "描述" + i;
                dt.Rows.Add(dr);
            }

            float index = 0;
            int locationY = 0;
            foreach (DataRow item in dt.Rows)
            {
                RepairOrderInfo ro = new RepairOrderInfo();
                ro.Label1 = item[0].ToString();
                ro.Label2 = item[1].ToString();
                ro.Label3 = item[2].ToString();
                ro.Label4 = item[3].ToString();
                ro.SetInfo(this.InfoPanel.Width, this.InfoPanel.Height);
                ro.Location = new Point(0, locationY);

                index += 1;
                if (index % 2 > 0)
                {
                    ro.BackColor = Color.White;
                }
                else
                {
                    ro.BackColor = Color.FromArgb(247, 249, 248);
                }

                locationY += ro.Height;
                this.InfoPanel.Controls.Add(ro);

            }

        }
        #endregion
      

        private void dismantle_Click(object sender, EventArgs e)
        {


        }

        private void repair_Click(object sender, EventArgs e)
        {
            
            BackRepairFrm repair = new BackRepairFrm();
            this.Visible = false;
            repair.ShowDialog();
            this.Dispose();

        }

        private void search_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序
            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine("osk.exe&exit");
            p.StandardInput.AutoFlush = true;
            p.Close();
        }


    }
}
