using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CheckEnd.Bll
{
    
    public  class T_WorkbayFuntionTag
    {
        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion
        DBUtility.SqlHelper sqlhelper = new DBUtility.SqlHelper();



        #region KEPServer TAG点查询
        public List<Model.T_WorkbayFuntionTag> FindWorkBayTag(string workName)
        {
            try
            {
                string sql = "SELECT * FROM T_WorkbayFuntionTag where WorkbayName=@workbayName";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@workbayName", SqlDbType.VarChar, 36);
                parameter[0].Value = workName;
                return sqlhelper.QueryList<Model.T_WorkbayFuntionTag>(connstr, sql, CommandType.Text, parameter);
            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion
    }
}
