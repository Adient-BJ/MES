using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckEnd
{
    public partial class ByPassErrorInfo : UserControl
    {

       public string WorkByName{ get; set; }

       public string ProductionNumber { get; set; }

       public string CarType { get; set; }

       public string CreateTime { get; set; }


        public ByPassErrorInfo()
        {
            InitializeComponent();
        }

        public void SetInfo(int width , int height)
        {

            this.Width = width;
            this.Height = height/7;

            this.workByName.Size = new Size(Convert.ToInt32(width * 0.156),height/7);
            this.workByName.Location = new Point(Convert.ToInt32(width*0.06),0);
            this.workByName.Text = WorkByName;

            this.productionNumber.Size = new Size(Convert.ToInt32( width * 0.23),height/7);
            this.productionNumber.Location = new Point(this.workByName.Right + (Convert.ToInt32(width * 0.06)),0);
            this.productionNumber.Text = ProductionNumber;
            
            this.carType.Size = new Size(Convert.ToInt32(width * 0.156), height/7);
            this.carType.Location = new Point( this.productionNumber.Right + Convert.ToInt32(width * 0.06) , 0 );
            this.carType.Text = CarType;

            this.createTime.Size = new Size(Convert.ToInt32(width * 0.23), height / 7);
            this.createTime.Location = new Point( this.carType.Right + Convert.ToInt32(width * 0.06 ) , 0);
            this.createTime.Text = CreateTime;


        }


   
    }
}
