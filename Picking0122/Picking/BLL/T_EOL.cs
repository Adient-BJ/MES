using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Picking.BLL
{
    public class T_EOL
    {



        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr1"].ConnectionString;
        public static string connstrR = ConfigurationManager.ConnectionStrings["connstr2"].ConnectionString;

        #endregion
        DB.SqlHelper sqlHelper = new DB.SqlHelper();

        public bool GetEOLResult(string masterBarCode)
        {
            string result = "";
            string sql = string.Format("select ISOKNOK from EOLRESULT where BARCODE ='{0}' order by DATE desc", masterBarCode);

            if(sqlHelper.ExecuteScalar(connstr, sql) !=null)
            {
                result = sqlHelper.ExecuteScalar(connstr, sql).ToString();
            }
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;

        }


        public bool GetRearEOLResult(string masterBarCode)
        {
            string result = "";
            string sql = string.Format("SELECT DTU_Total_TestResult  FROM RearEOL_TestResult where DTUCode ='{0}' order by Test_time desc", masterBarCode);

            if (sqlHelper.ExecuteScalar(connstrR, sql) != null)
            {
                result = sqlHelper.ExecuteScalar(connstrR, sql).ToString();
            }
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "OK")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }


    }
}
