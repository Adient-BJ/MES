using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace CheckEnd.Bll
{
    public  class T_ErrorMes
    {
        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion

        DBUtility.SqlHelper sqlHelper = new DBUtility.SqlHelper();


        public DataSet errorMessage ()
        {
            string sql = "";

            DataSet ds = sqlHelper.ExecuteDataSet(connstr, sql);

            return ds;
        }


    }
}
