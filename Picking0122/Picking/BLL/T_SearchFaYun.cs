using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace Picking.BLL
{
    public  class T_SearchFaYun
    {

        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion
        DB.SqlHelper sqlHelper = new DB.SqlHelper();

        public DataTable SearchJISAInfo(string where)
        {

            string sql = "select ProductionNumber,JISANumber,FaYunTime,FaYunUser from T_JISA   ";
            string sss = sql + where;
            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sss);
            return dt;

        }


    }
}
