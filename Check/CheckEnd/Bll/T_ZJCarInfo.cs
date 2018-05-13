using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace CheckEnd.Bll
{
    public  class T_ZJCarInfo
    {

        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion

        DBUtility.SqlHelper sqlHelper = new DBUtility.SqlHelper();

        /// <summary>
        /// 获得座椅类型
        /// </summary>
        /// <param name="masterBarcode"></param>
        /// <returns></returns>
        public string CarTypeName(string masterBarcode)
        {
            try
            {
                //string sql = string.Format(@"select carpartName from t_carpart
                // where CarPartID = (select CarPartID from T_OrderCarPart
                // where MasterBarcode = '{0}')", masterBarcode);
                string sql = string.Format("select CarModelNumber from [dbo].[T_WorkbayRecord] where MasterBarCode = '{0}'",masterBarcode);
                string carTypeName = sqlHelper.ExecuteScalar(connstr, sql).ToString();

                return carTypeName;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获得类型和颜色
        /// </summary>
        /// <param name="masterBarcode"></param>
        /// <returns></returns>
        public string CarTypeColor (string masterBarcode)
        {
            try
            {
                string sql = string.Format(@"select top(1) * from  [dbo].[T_WorkbayRecord] where MasterBarCode = '{0}'", masterBarcode);
                DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);
                string carType = dt.Rows[0]["CarType"].ToString();
                string color = dt.Rows[0]["Color"].ToString();
                string carInfo = string.Format("{0}/{1}", carType, color);
                return carInfo;
            }
            catch
            {
                return null;
            }
        }

    }
}
