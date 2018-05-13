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
    public partial class SearchFayun : Form
    {
        public SearchFayun()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();

            if (!string.IsNullOrEmpty(S_SHC))
            {
                this.SCH.Text = S_SHC;
            }
            if (!string.IsNullOrEmpty(S_BTime))
            {
                this.beginDate.Value = Convert.ToDateTime(S_BTime);
            }
            if (!string.IsNullOrEmpty(S_ETime))
            {
                this.endDate.Value = Convert.ToDateTime(S_ETime);
            }
            if (!string.IsNullOrEmpty(S_User))
            {
                this.user.Text = S_User;
            }
        }

        public static string S_SHC = "";
        public static string S_BTime = "";
        public static string S_ETime = "";
        public static string S_User = "";


        private void button1_Click(object sender, EventArgs e)
        {
            S_SHC = this.SCH.Text.Trim();
            S_BTime = this.beginDate.Value.ToString();
            S_ETime = this.endDate.Value.ToString();
            S_User = this.user.Text.Trim();

            this.Close();
        }

        private void SearchFayun_Load(object sender, EventArgs e)
        {
         
        }

        
    }
}
