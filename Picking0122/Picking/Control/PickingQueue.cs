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
    public partial class PickingQueue : UserControl
    {

        public string part { get; set; }

        public string seat { get; set; }


        public PickingQueue()
        {
            InitializeComponent();
        }


        public void SetInfo(int width,int height)
        {
            this.Width = width;
            this.Height = height / 7;

            this.partNo.Size = new Size(this.Width / 2, this.Height);
            this.partNo.Location = new Point(0, 0);

            this.seatNo.Size = new Size(this.Width/2,this.Height);
            this.seatNo.Location = new Point(this.partNo.Width, 0);


            this.partNo.Text = part;
            this.seatNo.Text = seat;


        }



   
    }
}
