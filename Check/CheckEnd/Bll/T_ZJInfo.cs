using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace CheckEnd.Bll
{
    public class T_ZJInfo
    {

        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion

        DBUtility.SqlHelper sqlHelper = new DBUtility.SqlHelper();

        /// <summary>
        /// 获取终检部分选项Code
        /// </summary>
        /// <param name="ImagePath"></param>
        /// <returns></returns>
        public DataTable GetZJPartConfigCode(string ImagePath)
        {
            DataTable dt;

            string sql = string.Format("select * from T_ZJPartConfig where ImagePath = '{0}'", ImagePath);

            dt = sqlHelper.ExecuteDataTable(connstr, sql);

            return dt;

        }

        /// <summary>
        /// 获取所有终检部分选项配置
        /// </summary>
        /// <returns></returns>
        public DataTable GetZJPartConfig()
        {
            DataTable dt;

            string sql = "  select * from T_ZJPartConfig order by CreateTime ";

            dt = sqlHelper.ExecuteDataTable(connstr, sql);

            return dt;
        }


        /// <summary>
        /// 获取安卓照相地址
        /// </summary>
        /// <returns></returns>
        public DataTable GetAnPicPath()
        {
            DataTable dt = null;
            XML.XmlConfig xmlConfig = new XML.XmlConfig();
            xmlConfig.GetIPXML();
            string stationName = xmlConfig.staionName;
            string path = $"select * from T_ANPicRecord where AnPicState = 0 and stationname='{stationName}'";
            dt = sqlHelper.ExecuteDataTable(connstr, path);
            return dt;
        }

  
        /// <summary>
        /// 删除数据库安卓图片
        /// </summary>
        /// <param name="path"></param>
        public void DeleteAnPIc(string path)
        {
            string sql = string.Format("delete form T_ANPicRecord where AnPicPath='{0}'",path);

            sqlHelper.ExecuteNonQuery(connstr,sql);

        }

        /// <summary>
        /// 更新安卓数据库状态
        /// </summary>
        /// <param name="path"></param>
        public void UpdateAnPic(string path,string masterBarCode)
        {
            string sql = string.Format("update T_ANPicRecord  set AnPicState = 1 ,MasterBarCode='{0}' where AnPicPath='{1}'", masterBarCode,path);

            sqlHelper.ExecuteNonQuery(connstr,sql);
        }
    }
}
