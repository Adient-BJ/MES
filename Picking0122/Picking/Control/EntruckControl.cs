using System;
using System.Drawing;
using System.Windows.Forms;

namespace Picking.Control
{
    public enum EntruckState
    {
        默认, 错误, 正确
    }
    public partial class EntruckControl : UserControl
    {

        public EntruckControl()
        {
            InitializeComponent();
        }

        public void SetInfo(int width, int height, string title)
        {
            this.Width = width;
            this.Height = height / 6;

            this.scanTitle.Size = new Size(Convert.ToInt32(width * 3 / 7), this.Height);
            this.scanTitle.Location = new Point(0, 0);

            this.scanBox.Size = new Size(Convert.ToInt32(width * 4 / 7), this.Height / 2);
            this.scanBox.Location = new Point(this.scanTitle.Right, 0);

            this.d1.Size = new Size(scanBox.Width / 3, scanBox.Height);
            this.m1.Size = d1.Size;
            this.z1.Size = d1.Size;

            this.d1.Location = new Point(scanBox.Left, scanBox.Top + scanBox.Height);  
            this.m1.Location = new Point(d1.Left + d1.Width, d1.Top);
            this.z1.Location = new Point(m1.Left + m1.Width, m1.Top);
         
             
            this.scanTitle.Text = title;
            this.scanBox.Text = "等待扫码";
        }

        public bool IsText
        {
            get
            {
                return !(this.scanBox.Text == "等待扫码");
            }
        }


        //真实的主条码
        public string TrueCode { get; set; }
        //当前扫的主条码
        private string thisCode;
        public string ThisCode
        {
            get { return thisCode; }
            set
            {
                thisCode = value;
                if (string.IsNullOrEmpty(thisCode))
                {
                    this.scanBox.Text = "等待扫码";
                }
                else
                {
                    this.scanBox.Text = thisCode;
                    if (thisCode == TrueCode)
                    {
                        this.scanBox.BackColor = Color.Green;
                    }
                    else
                    {
                        this.scanBox.BackColor = Color.Red;
                    }
                }
            }
        }

        //盲见通过
        private bool mjr;
        private bool zjr;
        private bool djr;
        public bool MJR
        {
            get { return mjr; }
            set
            {
                mjr = value;
                if (mjr)
                {
                    this.m1.BackColor = Color.GreenYellow;
                }
                else
                {
                    this.m1.BackColor = Color.Red;
                }
            }
        }
        //终检通过
        public bool ZJR
        {
            get { return zjr; }
            set
            {
                zjr = value;
                if (zjr)
                {
                    this.z1.BackColor = Color.GreenYellow;
                }
                else
                {
                    this.z1.BackColor = Color.Red;
                }
            }
        }
        //电检查通过
        public bool DJR
        {
            get { return djr; }
            set
            {
                djr = value;
                this.d1.BackColor = djr ? Color.GreenYellow : Color.Red;
            }
        }
        //40 60不用判断盲见终检
        bool is40Or60;
        public bool Is40Or60
        {
            get { return is40Or60; }
            set
            {
                is40Or60 = value;
                if (is40Or60)
                {
                    this.m1.Hide();
                    this.z1.Hide();
                    this.d1.Hide();
                }
            }
        }

        //整背-显示电检结果
        bool isZ177B;
        public bool IsZ177B
        {
            get { return isZ177B; }
            set
            {
                isZ177B = value;
                if (isZ177B)
                {
                    this.m1.Hide();
                    this.z1.Hide();
                }
            }
        }

        public bool IsTG()
        {
            bool isTG = false;
            if (is40Or60 & !isZ177B)
            {
                if (TrueCode == ThisCode)
                {
                    isTG = true; ;
                }
            }
            else if(!is40Or60 & !isZ177B)
            {
                if ((TrueCode == ThisCode) && MJR && ZJR && DJR)
                {
                    isTG = true; ;
                }
            }
            else if(!is40Or60 & isZ177B)
            {
                if ((TrueCode == ThisCode) && DJR)
                {
                    isTG = true; 
                }
            }

            return isTG;
        }
    }
}
