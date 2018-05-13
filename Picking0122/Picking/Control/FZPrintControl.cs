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
    public partial class FZPrintControl : UserControl
    {

        public string productNo { get; set; }

        public string carType { get; set; }

        public string carModelName { get; set; }

        public Color FontColor { get; set; }

        public FZPrintControl()
        {
            InitializeComponent();
        }

        public void SetInfo(int width,int height)
        {
            this.Width = width;
            this.Height = height;

            this.label1.Size = new Size(this.Width/3,this.Height);
            this.label1.Location = new Point(0, 0);
            this.label1.Text = productNo;

            this.label2.Size = new Size(this.Width/3,this.Height);
            this.label2.Location = new Point(this.label1.Right,0);
            this.label2.Text = carType;

            this.label3.Size = new Size(this.Width/3,this.Height);
            this.label3.Location = new Point(this.label2.Right,0);
            this.label3.Text = carModelName;

            this.label1.ForeColor = FontColor;
            this.label2.ForeColor = FontColor;
            this.label3.ForeColor = FontColor;

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
            if(this.label1.BackColor == Color.GreenYellow)
            {
                this.label1.BackColor = Color.White;
                this.label2.BackColor = Color.White;
                this.label3.BackColor = Color.White;
                Model.FZOrderModel.ProductNo.Remove(this.label1.Text);
            }
            else
            {
                this.label1.BackColor = Color.GreenYellow;
                this.label2.BackColor = Color.GreenYellow;
                this.label3.BackColor = Color.GreenYellow;
                Model.FZOrderModel fZOrderModel = new Model.FZOrderModel();

                fZOrderModel.AddNo(this.label1.Text);

            }


        }



    }
}
