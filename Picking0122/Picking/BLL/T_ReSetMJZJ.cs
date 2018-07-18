using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Picking.BLL
{
    public class T_ReSetMJZJ
    {


        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion
        DB.SqlHelper sqlHelper = new DB.SqlHelper();

        public int UpdateMJZJ(string barCode)
        {
            int result = 0;
            string sql = string.Format(@"insert into T_MJRecord (MJRecordID,MasterBarCode,MJFlag,UserID,CreateTime) 
                values ('{0}','{1}','{2}','{3}','{4}')", Guid.NewGuid().ToString(), barCode, 1, "hezhuang", DateTime.Now);

            string sql2 = $@"insert into T_ZJResultSaved (MasterBarCode,State,CreateTime,UserID)
	         values ('{barCode}','{0}','{DateTime.Now}','{"hezhuang"}')";

            int a = sqlHelper.ExecuteNonQuery(connstr, sql);

            int b = sqlHelper.ExecuteNonQuery(connstr, sql2);

            if (a == 1 && b == 1)
            {
                result = 1;
            }

            return result;
        }



    }
}
