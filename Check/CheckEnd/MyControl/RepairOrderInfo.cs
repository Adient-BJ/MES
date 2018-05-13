using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckEnd.MyControl
{
    public partial class RepairOrderInfo : UserControl
    {
        public RepairOrderInfo()
        {
            InitializeComponent();
        }

        public string Label1 { get; set; }
        public string Label2 { get; set; }
        public string Label3 { get; set; }
        public string Label4 { get; set; }


        public void SetInfo (int width,int height)
        {
            this.Width = width;
            this.Height = height / 12;

            this.label1.Size = new Size(width/4,this.Height);
            this.label1.Location = new Point(0,0);

            this.label2.Size = new Size(width / 4, this.Height);
            this.label2.Location = new Point(this.label1.Right,0);

            this.label3.Size = new Size(width / 4, this.Height);
            this.label3.Location = new Point(this.label2.Right,0);

            this.label4.Size = new Size(width / 4, this.Height);
            this.label4.Location = new Point(this.label3.Right,0);

            this.label1.Text = Label1;
            this.label2.Text = Label2;
            this.label3.Text = Label3;
            this.label4.Text = Label4;

        }




    }
}
