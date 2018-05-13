using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckEnd.Frms
{
    public partial class mangjianConfirm : Form
    {
        string BarCode;
        public mangjianConfirm(string barCode)
        {
            this.BarCode = barCode;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bll.T_ZJResult t_ZJResult = new Bll.T_ZJResult();
            t_ZJResult.SaveZJResult(BarCode,0);
            this.Close();
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bll.T_ZJResult t_ZJResult = new Bll.T_ZJResult();
            t_ZJResult.SaveZJResult(BarCode, 1);
            EndCheckFrm ce = new EndCheckFrm();
            this.Visible = false;
            ce.ShowDialog();
            this.Dispose();
        }


    }
}
