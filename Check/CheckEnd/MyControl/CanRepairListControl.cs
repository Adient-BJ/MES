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
    public partial class CanRepairListControl : UserControl
    {

    
        public string Part { get; set; }

        public string Description { set; get; }

        public bool IsCheck { get; set; }

        public CanRepairListControl()
        {
            InitializeComponent();
          
        }


        public void SetInfo(int width , int height)
        {
            this.Width = width;
            this.Height = height / 4;

            this.checkBox1.Location = new Point(this.Width/25,Convert.ToInt32(this.Height*0.4));

            this.part.Size = new Size(this.Width/3,this.Height);
            this.part.Location = new Point(this.checkBox1.Right + 20,0);

            this.description.Size = new Size(this.Width/2,this.Height);
            this.description.Location = new Point(this.part.Right,0);

            this.part.Text = Part;
            this.description.Text = Description;
            Check();
        }



        public void Check()
        {
            if(IsCheck)
            {
                this.checkBox1.CheckState = CheckState.Checked;
            }
            else
            {
                this.checkBox1.CheckState = CheckState.Unchecked;
            }
              
        }

     
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
          

        }

        
    }
}
