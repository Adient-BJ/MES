using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace CheckEnd.Bll
{
    public class T_Bypass
    {
        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion

        DBUtility.SqlHelper sqlHelper = new DBUtility.SqlHelper();

        public bool bypass (string code)
        {
            string sql = "select * from [V_ByPass] where MasterBarCode ='"+code+"'";
            DataTable bypasss  = sqlHelper.ExecuteDataTable(connstr, sql);
            
            if(bypasss.Rows.Count==0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public DataTable bypassErrorMes(string code)
        {
            string sql = "select WorkbayName,ProductionNumber,CarType,CreateTime from [V_ByPass] where MasterBarCode ='" + code + "'";
            DataTable bypassMes = sqlHelper.ExecuteDataTable(connstr, sql);

            return bypassMes;

        }
        


    }
}
