using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace Picking.BLL
{
    public class T_Verifying
    {

        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion
        DB.SqlHelper sqlHelper = new DB.SqlHelper();



        /// <summary>
        /// 获取已打印的合装单信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetHZOrder()
        {
            //state 0/未打印，1/已打印，2/已验证
            DataTable dt = null;
            string sql = "select * from T_HZOrderRecord where state = 1";
            dt = sqlHelper.ExecuteDataTable(connstr, sql);

            //DataTable dat = dt.DefaultView.ToTable(false, new string[] { "MasterBarCodeL" });

            return dt;
        }


        /// <summary>
        /// 根据生产号获取已打印合装单信息
        /// </summary>
        /// <param name="productNo"></param>
        /// <returns></returns>
        public DataTable GetHZOrder(string productNo)
        {
            DataTable dt = null;
            string sql = string.Format("select * from T_HZOrderRecord where productNo = '{0}' and  State = '1'", productNo);
            dt = sqlHelper.ExecuteDataTable(connstr, sql);
            return dt;
        }

        /// <summary>
        /// 根据生产号获取s是否已合装信息
        /// </summary>
        /// <param name="productNo"></param>
        /// <returns></returns>
        public DataTable GetHZOrderP(string productNo)
        {
            DataTable dt = null;
            string sql = string.Format("select * from T_HZOrderRecord where productNo = '{0}' and  State = '2'", productNo);
            dt = sqlHelper.ExecuteDataTable(connstr, sql);
            return dt;
        }



        /// <summary>
        /// 获取要打印的合装单信息
        /// </summary>
        //public DataTable PrintHZOrder()
        //{
        //    DataTable dt = null;

        //    //只找状态为0：未打印的合装单
        //    string workbayName = "FL30";

        //    string sql = string.Format(@"select WorkbayName,productionNumber,SerialNumber,IsSuccess 
        //    from T_WorkbayRecord where WorkbayName ='{0}' and IsSuccess='1'
        //     order by SerialNumber desc", workbayName);

        //    DataTable dd = sqlHelper.ExecuteDataTable(connstr, sql);
        //    if (dd.Rows.Count > 0)
        //    {
        //        string productionNo = dd.Rows[0]["productionNumber"].ToString();
        //        bool isSuccess = Convert.ToBoolean(sqlHelper.ExecuteDataTable(connstr, sql).Rows[0]["IsSuccess"]);
        //        if (isSuccess == true)
        //        {
        //            string orderSql = string.Format("select * from T_HZOrderRecord " +
        //                " where State = '0' and ProductNo ='{0}' order by SerialNumber ", productionNo);
        //            dt = sqlHelper.ExecuteDataTable(connstr, orderSql);
        //        }
        //    }

        //    return dt;
        //}

            //获取需要打印的订单列表
        public DataTable GetPrintHZOrderList()
        { 
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from T_WorkbayRecord a ");
            sql.Append("inner join T_HZOrderRecord b on a.ProductionNumber=b.ProductNo where a.WorkbayName='FL30' and a.IsSuccess=1 and b.State=0;");
          
          return sqlHelper.ExecuteDataTable(connstr, sql.ToString()); 
        }

        /// <summary>
        /// 修改合装单状态 0：未打印 1：已打印 2：已验证 
        /// </summary>
        /// <param name="orderID"></param>
        public void MarkHZOrder(string orderID, int state)
        {
            string sql = string.Format("update T_HZOrderRecord set state = '{0}' where HZOrderGID = '{1}'", state, orderID);

            sqlHelper.ExecuteNonQuery(connstr, sql);
        }
        /// <summary>
        /// 2018-06-07 修改  合装时间  UpdateTime
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="state"></param>
        /// <param name="dt"></param>
        public void MarkHZOrder(string orderID, int state, DateTime dt)
        {
            string sql = string.Format("update T_HZOrderRecord set state = '{0}',UpdateTime='{1}' where HZOrderGID = '{2}'", state, dt, orderID);

            sqlHelper.ExecuteNonQuery(connstr, sql);
        }



        public int GetMJResult(string masterBarCode)
        {
            string sql = string.Format("select * from T_MJRecord where MasterBarCode = '{0}' order by CreateTime desc", masterBarCode);
            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);
            if (dt.Rows.Count > 0)
            {
                int a = Convert.ToInt32(dt.Rows[0]["MJFlag"]);

                //1:成功 2：失败 3:未检测
                return a;

            }
            else
            {
                return 3;
            }

        }
        public bool GetZJResult(string masterBarCode)
        {
            bool result = false;
            string sql = string.Format("select * from T_ZJResultSaved where MasterBarCode = '{0}' order by CreateTime desc", masterBarCode);
            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);
            if(dt.Rows.Count>0)
            {
                ///终检记录 0：成功 1：失败
                if (dt.Rows[0]["State"].ToString() == "0")
                {
                    result = true;
                }
            }
            else
            {
                result = false;
            }
        
            return result;
        }

        /// <summary>
        /// 根据生产号获取合装单信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetHzOrderByNo(string productNo)
        {
            string sql = string.Format("select * from T_HZOrderRecord where ProductNo='{0}'",productNo);
            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);
            return dt;

        }

    }
}
