using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace Picking.BLL
{
    public class T_KuWei
    {

        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion
        DB.SqlHelper sqlHelper = new DB.SqlHelper();

        /// <summary>
        /// 根据生产号查询库位信息
        /// </summary>
        /// <param name="productNo"></param>
        /// <returns></returns>
        public DataTable GetKuWeiInfo(string productNo)
        {
            DataTable dt = null;
            string sql = string.Format("select LocationNo,CreateTime from T_KuWei where ProductNo='{0}'",productNo);
            dt = sqlHelper.ExecuteDataTable(connstr, sql);

            return dt;
        }



        /// <summary>
        /// 查询库位信息
        /// </summary>
        /// <param name="productNo"></param>
        /// <returns></returns>
        public string SearchKuWei(string productNo)
        {
            string sql = string.Format("select LocationNo from T_KuWei where ProductNo = '{0}'",productNo);

            string result = null;
            if(sqlHelper.ExecuteScalar(connstr, sql)!=null)
            {
                result = sqlHelper.ExecuteScalar(connstr, sql).ToString();
            }

            return result;
        }

        /// <summary>
        /// 根据生产号查询库存状态
        /// </summary>
        /// <param name="productNo"></param>
        /// <returns></returns>
        public string IsInStock(string productNo)
        {
            string sql = string.Format("select LocationNo from T_KuWei where ProductNo = '{0}' and state=1", productNo);

            string result = null;
            if (sqlHelper.ExecuteScalar(connstr, sql) != null)
            {
                result = sqlHelper.ExecuteScalar(connstr, sql).ToString();
            }

            return result;

        }


        /// <summary>
        /// 根据生产号保存库位信息
        /// </summary>
        /// <param name="productNo"></param>
        public bool SaveKuWeiInfo(string productNo)
        {
            DateTime dateTime = DateTime.Now;
            int state = 1;

            string kuweiSql = "SELECT locationNo FROM T_KuWei  where State ='0' order by SerialNo";
            string locationNo = "";
            if (sqlHelper.ExecuteScalar(connstr, kuweiSql)!=null)
            {
                locationNo = sqlHelper.ExecuteScalar(connstr, kuweiSql).ToString();

                string sql = string.Format("update T_KuWei set  ProductNo ='{0}' ,CreateTime ='{1}',State = '{2}' where LocationNo ='{3}'", productNo, dateTime, state, locationNo);
                sqlHelper.ExecuteNonQuery(connstr, sql);
                return true;
            }
            else
            {
                locationNo = "";
                return false;
            }
            
           
        }

        /// <summary>
        /// 保存库位状态 0：为占用 1：已占用 
        /// </summary>
        /// <param name="productNo"></param>
        /// <param name="state"></param>
        public void SaveKuWeiState(string productNo,int state)
        {
            string sql = string.Format("update T_KuWei set State ='{0}' where ProductNo ='{1}'",state,productNo);

            sqlHelper.ExecuteNonQuery(connstr, sql);
        }

        public void StateClearAll()
        {
            string sql = "update T_KuWei set State ='0'";
            sqlHelper.ExecuteNonQuery(connstr, sql);
        }

    }
}
