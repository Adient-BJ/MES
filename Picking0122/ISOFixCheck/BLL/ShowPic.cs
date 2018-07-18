using System.Configuration;

namespace ISOFixCheck.BLL
{
    class ShowPic
    {
        #region 数据库连接字符串获取
        public static string Connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion

        private readonly DB.SqlHelper _sqlHelper = new DB.SqlHelper();



        public string GetPicPath(string masterCode)
        {
            string sql = $"SELECT AnPicPath  FROM T_ANPicRecord where MasterBarCode='{masterCode}'";
            string result = _sqlHelper.ExecuteScalar(Connstr, sql).ToString();
            return result;
        }

    }
}
