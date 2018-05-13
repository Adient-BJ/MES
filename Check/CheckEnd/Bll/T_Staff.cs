using CheckEnd.DBUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace CheckEnd.Bll
{
   public class T_Staff
    {

        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion

        DBUtility.SqlHelper sqlhelper = new DBUtility.SqlHelper();

        public DataTable GetUserID(string fid)
        {

            DataTable User = null;
            string sql =string.Format("select StaffID from T_Staff where ZhiWenID  like'%{0}%'", fid);

            User = sqlhelper.ExecuteDataTable(connstr, sql);

            return User;


        }


        public bool GetRoleID(string userID)
        {
            bool result = false;
            string sql = string.Format("select RoleID from T_Staff where StaffID = '{0}'",userID);

            List<string> list =  sqlhelper.ExecuteScalar(connstr, sql).ToString().Split(',').ToList();

            if(list.Contains("2"))
            {
                result = true;
            }

            return result;
        }
    }
}
