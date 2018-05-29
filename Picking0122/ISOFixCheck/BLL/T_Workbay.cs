using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISOFixCheck.BLL
{
    class T_Workbay
    {
        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion
        DB.SqlHelper sqlHelper = new DB.SqlHelper();



        public string GetProductNo(string masterCode)
        {
            string sql = string.Format("SELECT ProductionNumber  FROM T_WorkbayRecord where MasterBarCode='{0}'", masterCode);
            string result = sqlHelper.ExecuteScalar(connstr, sql).ToString();
            return result;
        }
    }
}
