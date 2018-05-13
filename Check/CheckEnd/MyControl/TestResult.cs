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
    public partial class TestResult : UserControl
    {

        public string CheckInfo { get; set; }

        public string ZJFlawDetail { get; set; }

        public string Operate { get; set; }

        public bool lockButton { get; set; }

        public string confirmText { get; set; }


        public delegate void SaveRecord(string ZJFlawName,int flag);

        public TestResult()
        {
            InitializeComponent();
        }
     
     
        public void SetInfo (int width , int height)
        {
            this.Width = width;
            this.Height = height / 8;

            this.checkPro.Width = width / 2;
            this.checkPro.Height = height / 8;
            this.checkPro.Location = new Point(0, 0);

            this.label1.Size = new Size(this.checkPro.Width, this.checkPro.Height);
            this.label1.Text = CheckInfo;
            this.label1.Location = new Point(0, 0);
            
            this.handle.Width = width / 2;
            this.handle.Height = height / 8;
            this.handle.Location = new Point(this.checkPro.Width, 0);

            this.confirm.Size = new Size(Convert.ToInt32(this.handle.Width * 0.38),Convert.ToInt32(this.handle.Height * 0.511));
            this.confirm.Text = confirmText;
            if(confirm.Text == "合格")
            {
                confirm.BackColor = Color.Green;
            }
            else
            {
                confirm.BackColor = Color.Red;
            }
            
            if(lockButton == true)
            {
                this.confirm.Enabled = false;
            }
          
        }

    

        public void SetPartState()
        {
          
            if (Model.CheckResult.FlawRecord == null)
            {
                Model.CheckResult.FlawRecord  = new List<string>();
            }
            else
            {
                if (Model.CheckResult.FlawRecord.Contains(ZJFlawDetail))
                {
                    this.confirm.Text = "不合格";
                    this.confirm.BackColor = Color.Red;
                }
            }


        }

        public event SaveRecord Save;

        private void confirm_Click(object sender, EventArgs e)
        {
           
            try
            {
          
                if (this.confirm.Text == "合格")
                {
                    Model.CheckResult checkResult = new Model.CheckResult();
                    checkResult.AddFlawRecord(ZJFlawDetail);
                    this.confirm.Text = "不合格";
                    this.confirm.BackColor = Color.Red;

                    Save(CheckInfo,1);
                }
                else
                {
                    this.confirm.Text = "合格";

                    this.confirm.BackColor = Color.FromArgb(0, 191, 122);
                    Model.CheckResult.FlawRecord.Remove(ZJFlawDetail);

                    Save(CheckInfo, 2);
                }
   
            }
            catch (Exception)
            {
                
            }
        }
    }
}
