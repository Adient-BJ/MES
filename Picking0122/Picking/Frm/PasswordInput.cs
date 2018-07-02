using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Picking.Frm
{
    public partial class PasswordInput : Form
    {
        public PasswordInput()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        private void PasswordInput_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            label2.Text = "";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="")
            {
                if (textBox1.Text.ToLower()=="ngcc2018")
                {
                    this.Hide();
                    this.Dispose();

                }
                else
                {
                    label2.Text = "密码输入错误，请重新输入！";
                    textBox1.Text = "";
                }

            }
        }
    }
}
