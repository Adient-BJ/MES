using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Picking.BLL
{
    public class T_PickOrder
    {

        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion
        DB.SqlHelper sqlHelper = new DB.SqlHelper();

        /// <summary>
        /// 获取亮灯拾取序
        /// </summary>
        /// <returns></returns>
        public DataTable GetOrder()
        {

            //state 0:未扫描 1：已扫描 2：已完成
            string sql = "select * from  T_Picking  where state  <2 order by SerialNumber ,MasterBarCode";

            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);

            return dt;

        }

        /// <summary>
        /// 获取未扫描的序列的第一条
        /// </summary>
        /// <returns></returns>
        public string GetScanOrder()
        {
            string sql = "select top(1) MasterBarCode from  T_Picking  where state = '0' order by SerialNumber ,MasterBarCode";
            return sqlHelper.ExecuteScalar(connstr, sql) == null ? "" : sqlHelper.ExecuteScalar(connstr, sql).ToString();

        }

        /// <summary>
        /// 获取1区未拾取序列的第一条
        /// </summary>
        /// <returns></returns>
        public DataTable GetArea1Order()

        {
            string sql = "select top(1)* from  T_Picking  where Area1 = '0' and state <> 2  order by SerialNumber ,MasterBarCode ";
            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);
            return dt;
        }

        /// <summary>
        /// 获取2区未拾取序列的第一条
        /// </summary>
        /// <returns></returns>
        public DataTable GetArea2Order()
        {
            string sql = "select top(1)* from  T_Picking  where Area2 = '0' and state <> 2  order by SerialNumber ,MasterBarCode";
            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);
            return dt;
        }

        /// <summary>
        /// 保存主序状态 0：未扫描 1：已扫描 2：已完成
        /// </summary>
        /// <param name="MasterBarCode"></param>
        /// <param name="state"></param>
        public void SaveScanedOrder(string MasterBarCode, int state)
        {
            string sql = string.Format("update T_Picking set State='{0}' where MasterBarCode = '{1}'", state, MasterBarCode);
            sqlHelper.ExecuteNonQuery(connstr, sql);
        }



        /// <summary>
        /// 保存主序对应1区的状态 
        /// </summary>
        /// <param name="MasterBarCode"></param>
        /// <param name="state1"></param>
        public void SaveArea1Order(string MasterBarCode, int state1)
        {
            string sql = string.Format("update T_Picking set Area1 ='{0}' where MasterBarCode ='{1}'", state1, MasterBarCode);
            sqlHelper.ExecuteNonQuery(connstr, sql);
        }

        /// <summary>
        /// 保存主序对应2区的状态
        /// </summary>
        /// <param name="MasterBarCode"></param>
        /// <param name="state2"></param>
        public void SaveArea2Order(string MasterBarCode, int state2)
        {
            string sql = string.Format("update T_Picking set Area2 ='{0}' where MasterBarCode ='{1}'", state2, MasterBarCode);
            sqlHelper.ExecuteNonQuery(connstr, sql);
        }




        /// <summary>
        /// 通过ALCCode获取总成号
        /// </summary>
        /// <param name="AssyNo"></param>
        /// <returns></returns>
        public string GetAssyNo(string ALCCode)
        {
            string sql = string.Format("select AssyNo from T_AssyALC where ALCCode = '{0}'", ALCCode);
            string result = "";
            if(sqlHelper.ExecuteScalar(connstr, sql) != null)
            {
                result = sqlHelper.ExecuteScalar(connstr, sql).ToString();
            }
            return result;

        }

        /// <summary>
        /// 执行赵工存储过程
        /// </summary>
        /// <param name="barCode"></param>
        public bool ExcuteScanBar(string barCode)
        {
            SqlParameter[] pas = {
                new SqlParameter("@BarCode",SqlDbType.VarChar,40)
            };
            pas[0].Value = barCode;

            return (sqlHelper.ExecuteProc(connstr, "EDI_ScanBarCode", pas) > 0);

        }

    }
}
