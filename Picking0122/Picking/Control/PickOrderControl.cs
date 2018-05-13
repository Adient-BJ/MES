using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Picking.Control
{
    public partial class PickOrderControl : UserControl
    {

         

        public string serial { get; set; }

        public string barCode { get; set; }

        public string carModelNo { get; set; }

        public string carTypeNo { get; set; }


        public PickOrderControl()
        {
            InitializeComponent();
        }

        public void SetInfo(int width,int height)
        {
            this.Width = width;
            this.Height = height ;

            this.serialNo.Size = new Size(this.Width/4,this.Height);
            this.serialNo.Location = new Point(0,0);

            this.barCodes.Size = this.serialNo.Size;
            this.barCodes.Location = new Point(this.serialNo.Right,0);

            this.carModel.Size = this.serialNo.Size;
            this.carModel.Location = new Point(this.barCodes.Right, 0);

            this.carType.Size = this.serialNo.Size;
            this.carType.Location = new Point(this.carModel.Right, 0);

            this.serialNo.Text = "生产号： " + serial;
            this.barCodes.Text = "主条码：" + barCode; 
            this.carModel.Text = "车型：    " + carModelNo;
            this.carType.Text = "类型:      " + carTypeNo;

          

        }

        public void SetChoice(Color c)
        { 
            this.serialNo.BackColor = c;
            this.barCodes.BackColor = c;
            this.carModel.BackColor = c;
            this.carType.BackColor = c;
        }
    }
}
