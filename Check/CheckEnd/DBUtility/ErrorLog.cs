using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace CheckEnd.DBUtility
{
    public class ErrorLog
    {
        public enum logType
        { ERRORLOG, SYSTEM }

        public void writeTxt(string path, logType type, string content)
        {
            try
            {
                string time = System.DateTime.Now.ToString();
                string fileName = "";
                if (type == logType.SYSTEM)
                    fileName = "SYSTEM_" + System.DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                if (type == logType.ERRORLOG)
                    fileName = "ERRORLOG_" + System.DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                //指定路径
                //如果文件a.txt存在就打开，不存在就新建 .append 是追加写 
                if (!Directory.Exists(path + "\\LOG"))
                {
                    Directory.CreateDirectory(path + "\\LOG");
                }
                //if (!File.Exists(path + "\\LOG\\" + fileName))
                //{
                //    File.Create(path + "\\LOG\\" + fileName);
                //}
                FileStream fst = new FileStream(path + "\\LOG\\" + fileName, FileMode.Append);
                //写数据到a.txt格式 
                StreamWriter swt = new StreamWriter(fst, System.Text.Encoding.GetEncoding("utf-8"));
                //写入 
                swt.WriteLine("");
                swt.Write("[" + time + "] " + content);
                swt.Close();
                fst.Close();
            }
            catch (Exception e)
            {

            }
        }

        public bool Ping(string ip)
        {
            try
            {
                System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
                System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
                options.DontFragment = true;
                string data = "Test Data!";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 1000; // Timeout 时间，单位：毫秒  
                System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout, buffer, options);
                if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
