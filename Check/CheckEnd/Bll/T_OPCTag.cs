using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace CheckEnd.Bll
{
    public class T_OPCTag
    {

        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion

        DBUtility.SqlHelper sqlHelper = new DBUtility.SqlHelper();


        public DataTable GetAllOPCTag(string workBayName)
        {

            string tagSql = string.Format("select * from T_WorkbayFuntionTag  where  WorkbayName='{0}'",workBayName);

            DataTable kepserverAllName = sqlHelper.ExecuteDataTable(connstr, tagSql);

            return kepserverAllName;
        }

        public string GetLine(string workBayName)
        {
            string lineName = "";
            string sql = string.Format(@"select LineName from T_Line where LineID = ( select LineID from T_Workbay where WorkbayName= 
                        (select GetWorkOpcTag from T_WorkbayIPConfig where WorkbayName = '{0}'))", workBayName);
            lineName = sqlHelper.ExecuteScalar(connstr, sql).ToString();

            return lineName;
        }
            

    }
}
