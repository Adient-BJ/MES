using System.Configuration;

namespace ISOFixCheck.BLL
{
    class T_Workbay
    {
        #region 数据库连接字符串获取
        public static string Connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion
        DB.SqlHelper sqlHelper = new DB.SqlHelper();



        public string GetProductNo(string masterCode)
        {
            string sql = $"SELECT ProductionNumber  FROM T_WorkbayRecord where MasterBarCode='{masterCode}'";
            string result = sqlHelper.ExecuteScalar(Connstr, sql).ToString();
            return result;
        }
    }
}
