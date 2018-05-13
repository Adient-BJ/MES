using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CheckEnd.XML
{
   public class XmlConfig
    {
        public string line;
        public string staionName;

        /// <summary>
        /// 从XML文件中获取指定节点的值
        /// </summary>
        /// <param name="NodeID"></param>
        public void GetIPXML()
        {
            XmlDocument xmlDom = new XmlDocument();
            if (!System.IO.File.Exists("IP.xml"))
            {
                return;
            }
            xmlDom.Load("IP.xml");
            line = xmlDom.SelectSingleNode("appSettings/LineName").InnerText.ToString().Trim();
            staionName = xmlDom.SelectSingleNode("appSettings/StationName").InnerText.ToString().Trim();

            return;
        }
    }
}
