using CheckEnd.DBUtility;
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
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
            textBox1.Text = GetAnPicPath();
        }

        public string GetAnPicPath()
        {
            //DataTable dt = null;
            XML.XmlConfig xmlConfig = new XML.XmlConfig();
            xmlConfig.GetIPXML();
            string stationName = xmlConfig.staionName;
            string path = $"select * from T_ANPicRecord where AnPicState = 0 and stationname='{stationName}'";
            //dt = SqlHelper.ExecuteDataTable(connstr, path);
            return path;
        }

    }
}
