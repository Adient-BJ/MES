using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace Picking.BLL
{
    public class T_FZOrderConfirm
    {


        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion
        DB.SqlHelper sqlHelper = new DB.SqlHelper();

        /// <summary>
        /// 获取已打印的左分装单信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get0FZOrder()
        {
            //state 0/未打印，1/已打印，2/已验证
            DataTable dt = null;
            string sql = "select * from T_FZOrderRecord where state = '1' and LorR = '0'";
            dt = sqlHelper.ExecuteDataTable(connstr, sql);

            return dt;

        }

        /// <summary>
        /// 获取已打印的右分装单信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get1FZOrder()
        {
            //state 0/未打印，1/已打印，2/已验证
            DataTable dt = null;
            string sql = "select * from T_FZOrderRecord where state = '1' and LorR = '1'";
            dt = sqlHelper.ExecuteDataTable(connstr, sql);

            return dt;
        }

        /// <summary>
        /// 保存分装单状态 0:未打印 1：已打印 2：已验证
        /// </summary>
        /// <param name="productNo"></param>
        /// <param name="state"></param>
        public void SaveFZOrderState(string productNo,int state,int LorR)
        {

            string sql = string.Format("update T_FZOrderRecord set state ='{0}' where ProductNo ='{1}' and LorR ='{2}'", state ,productNo,LorR);
            sqlHelper.ExecuteNonQuery(connstr, sql);
        }



        /// <summary>
        /// 根据生产号获取分装单信息
        /// </summary>
        /// <param name="productNo"></param>
        /// <returns></returns>
        public DataTable GetFZOrder(string productNo,int LorR)
        {
            DataTable dt = null;

            string sql = string.Format("select * from T_FZOrderRecord where ProductNo = '{0}' and LorR ='{1}'",productNo,LorR);

            dt = sqlHelper.ExecuteDataTable(connstr, sql);

            return dt;

        }
    }
}
