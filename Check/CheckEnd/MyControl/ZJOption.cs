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
    public partial class ZJOption : UserControl
    {

        public delegate void ShowDetail();

        public string ZJOptionConfigCode { get; set; }

        public string ZJOptionConfigName { get; set; }

        public ZJOption()
        {
            InitializeComponent();
        }

        public void SetInfo(int width ,int height)
        {
            this.Width = width;
            this.Height = height/8;

            this.detail.Size = new Size(width, height / 8);
            this.detail.Location = new Point(0, 0);
            this.detail.Text = ZJOptionConfigName;

        }

        public event ShowDetail showDetail;

        private void detail_Click(object sender, EventArgs e)
        {
            try
            {
                Model.CheckResult.ZJOptionConfigCode = ZJOptionConfigCode;
                showDetail();
            }
            catch
            {
                throw ;
            }
        
            
        }

    }
}
