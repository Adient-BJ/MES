using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace CheckEnd.Bll
{
    public class T_AnswerPic
    {

        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion

        DBUtility.SqlHelper sqlhelper = new DBUtility.SqlHelper();

        public string GetPicPath(string mjAnswer)
        {
            string sql = string.Format("select PicPath from T_AnswerPic where MJAnswer ='{0}'",mjAnswer);

            string picPath = "";

            if(sqlhelper.ExecuteScalar(connstr, sql) != null)
            {
                picPath = sqlhelper.ExecuteScalar(connstr, sql).ToString();
            }

            return picPath;
        }

    }
}
