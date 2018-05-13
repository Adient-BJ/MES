using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace CheckEnd.Bll
{
    public class T_MJCarInfo
    {
        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion

        DBUtility.SqlHelper sqlHelper = new DBUtility.SqlHelper();

        public DataTable GetCarType(string masterBarCode)
        {

            string sql = string.Format("select top 1 * from [dbo].[T_WorkbayRecord] where MasterBarCode = '{0}'",masterBarCode);
            DataTable dataTable = sqlHelper.ExecuteDataTable(connstr, sql);
            return dataTable;
        }
    }
}
