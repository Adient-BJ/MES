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
    public partial class ShipControl : UserControl
    {
        public ShipControl()
        {
            InitializeComponent();
        }

        public void SetInfo(int width, int height, string title)
        {
            this.Width = width;
            this.Height = height;
            this.name.Text = title;
            name.Height = height;
            name.Width = width / 3;
            name.Location = new Point(0, 0);
            barCode.Height = height;
            barCode.Width = width*2 / 3;
            barCode.Location = new Point(name.Right, 0);
        }

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
                    this.barCode.Text = "";
                }
                else
                {
                    this.barCode.Text = thisCode;
                    if (thisCode == TrueCode)
                    {
                        this.barCode.BackColor = Color.Green;
                    }
                    else
                    {
                        this.barCode.BackColor = Color.Red;
                    }
                }
            }
        }
        bool isPass;

        public bool IsPass
        {
            get
            {
                return (this.barCode.BackColor == Color.Green);
            }

        }


    }
}
