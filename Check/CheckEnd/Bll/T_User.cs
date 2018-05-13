using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace CheckEnd.Bll
{
    public class T_User
    {

        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion

        DBUtility.SqlHelper sqlHelper = new DBUtility.SqlHelper();

        public string UserLogin(string userName,string userPwd)
        {
            string sql = string.Format("select * from T_Staff where LoginName='{0}' and LoginPwd='{1}'", userName,userPwd);

            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);
            
            string staffID ="";
            if(dt.Rows.Count>0)
            {
                staffID = dt.Rows[0]["StaffID"].ToString();
                string staffName = dt.Rows[0]["StaffName"].ToString();
                Bll.User.UserID = staffID;
                Bll.User.UserName = staffName;
            }

            return staffID;
        }

    }
}
