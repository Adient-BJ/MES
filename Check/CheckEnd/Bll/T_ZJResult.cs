using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace CheckEnd.Bll
{
    public class T_ZJResult
    {

        #region 数据库连接字符串获取
        public static string Connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion

        DBUtility.SqlHelper sqlhelper = new DBUtility.SqlHelper();

        /// <summary>
        /// 保存终检结果 0：成功 1：失败
        /// </summary>
        /// <param name="masterBarCode"></param>
        /// <param name="state"></param>
        public void SaveZJResult(string masterBarCode,int state)
        {
            DateTime dt = DateTime.Now;
            string sql = $@"insert into T_ZJResultSaved (masterBarCode,State,CreateTime,UserID)
                values ('{masterBarCode}','{state}','{dt}','{Bll.User.UserID}')";

            sqlhelper.ExecuteNonQuery(Connstr, sql);
        }


    }
}
