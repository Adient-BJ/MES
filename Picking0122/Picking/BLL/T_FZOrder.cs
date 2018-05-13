using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace Picking.BLL
{
    public class T_FZOrder
    {
        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion
        DB.SqlHelper sqlHelper = new DB.SqlHelper();

        /// <summary>
        /// 获取左分装单的条码信息
        /// </summary>
        /// <param name="Pno"></param>
        /// <returns></returns>
        public DataTable GetLOrder(string Pno)
        {
            DataTable dt = null;
            string sql = string.Format("select MasterBarCodeL,MasterBarCode60 from T_FZOrderRecord where ProductNo ='{0}'",Pno);

            dt = sqlHelper.ExecuteDataTable(connstr, sql);
            return dt;
        }

        /// <summary>
        /// 获取右分装单的条码信息
        /// </summary>
        /// <param name="Pno"></param>
        /// <returns></returns>
        public DataTable GetROrder(string Pno)
        {
            DataTable dt = null;
            string sql = string.Format("select MasterBarCodeR,MasterBarCodeC,MasterBarCode40 from T_FZOrderRecord where ProductNo ='{0}'", Pno);

            dt = sqlHelper.ExecuteDataTable(connstr, sql);
            return dt;

        }

    }
}
