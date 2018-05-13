using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace Picking.BLL
{
    public  class T_PickCall
    {
        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion
        DB.SqlHelper sqlHelper = new DB.SqlHelper();

        public DataTable GetPickCall()
        {
            string sql = "select * from T_Material where isdo = '0'";
            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);

            return dt;

        }

        public void SavePickCall()
        {
            string sql = "update T_Material set isdo = '1'";

            sqlHelper.ExecuteNonQuery(connstr, sql);

        }
    }
}
