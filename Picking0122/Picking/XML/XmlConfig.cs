using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Picking.XML
{
   public class XmlConfig
   {
       /// <summary>
       /// 打印机名称用全局变量，暂时不用xml
       /// </summary>
       public static string PrintName = "";
        ///// <summary>
        ///// 从XML文件中获取指定节点的值
        ///// </summary>
        ///// <param name="NodeID"></param>
        //public static string GetIPXML()
        //{
        //    string value = string.Empty;
            
        //    if (!System.IO.File.Exists("Config.xml"))
        //    {
        //        return null;
        //    }
        //    XDocument xmlDom = XDocument.Load("Config.xml");
        //    XElement xmlElement = xmlDom.Root;
        //    XElement e =xmlElement.Element("Print");

        //    value = e.Value;

        //    return value;
        //}
    }
}
