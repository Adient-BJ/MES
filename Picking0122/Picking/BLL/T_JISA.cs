using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Picking.BLL
{
    public class T_JISA
    {


        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion
        DB.SqlHelper sqlHelper = new DB.SqlHelper();

        /// <summary>
        /// 根据jisa状态 获取前20行JISA所有序，用于打印分装单
        /// </summary>
        /// <returns></returns>
        public DataTable GetJISA(int state)
        {
            //0：未打印分装单 1:已打印 2：已验证
            string sql = string.Format(@"select a.ProductionNumber,a.JISANumber,b.LocationNo from T_JISA  a 
                                    left join T_KuWei b on a.ProductionNumber = b.ProductNo
                                      where a.State = '{0}' order by JISANumber  ", state);
            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);

            return dt;
        }
        public DataTable GetJISAWait()
        {
            //0：未打印分装单 1:已打印 2：已验证
            string sql = @"select a.ProductionNumber,a.JISANumber,b.LocationNo,a.State from T_JISA  a 
                           left join T_KuWei b on a.ProductionNumber = b.ProductNo
                           where (a.FaYunState is null or a.FaYunState=0) order by a.JISANumber  ";
            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql); 
            return dt;
        }
      
        public DataTable GetYiFaYun(string S_SHC,string S_BTime,string S_ETime,string S_User)
        {
            //0：未打印分装单 1:已打印 2：已验证
            string sql = @"select a.ProductionNumber,a.JISANumber,a.FaYunTime,a.FaYunUser,a.GroupCode from T_JISA  a 
                           left join T_KuWei b on a.ProductionNumber = b.ProductNo
                           where a.State=3 and a.FaYunState=1 ";
            if (!string.IsNullOrEmpty((S_SHC)))
            {
                sql += "and a.ProductionNumber='" + S_SHC + "' ";
            }
            else
            {
                if (!string.IsNullOrEmpty((S_BTime)))
                {
                    sql += "and a.FaYunTime>='" + S_BTime + "'  and a.FaYunTime<='" + S_ETime + "' ";
                }
                if (!string.IsNullOrEmpty((S_User)))
                {
                    sql += "and a.FaYunUser='" + S_User + "' ";
                } 
            }
            sql += " order by a.JISANumber  desc ";
            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);
            return dt;
        }

        /// <summary>
        /// 保存发运状态 0：未发运 1：已发运
        /// </summary>
        /// <param name="productNo"></param>
        /// <param name="state"></param>
        public void SaveFaYunState(string productNo,int state,string fayunUser,string twoFaYun)
        {
            DateTime dt = DateTime.Now;

            string sql = string.Format(@"update T_JISA set FaYunState ='{0}',FaYunTime ='{1}',FaYunUser='{2}',TwoFayunID='{3}'
                where ProductionNumber ='{4}'", state,dt,fayunUser,twoFaYun,productNo);

            sqlHelper.ExecuteNonQuery(connstr, sql);
        }

        public void SetFaYun(string productNo,string fayunUser,string GroupCode)
        {
            DateTime dt = DateTime.Now;
            string sql = string.Format(@"update T_JISA set FaYunState ='{0}',FaYunTime ='{1}',FaYunUser='{2}',GroupCode='{3}'
                where ProductionNumber ='{4}'", 1, dt, fayunUser, GroupCode, productNo); 
            sqlHelper.ExecuteNonQuery(connstr, sql);
        }


        /// <summary>
        /// 根据jisa状态 获取前20行JISA所有序，用于打印分装单
        /// </summary>
        /// <returns></returns>
        public DataTable GetJISAed(int state)
        {
            //0：未打印分装单 1:已打印 2：已验证
            string sql = string.Format("select ProductionNumber,JISANumber from T_JISA  where State ='{0}' order by JISANumber", state);

            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);

            return dt;
        }

        /// <summary>
        /// 根据生产号保存JISA状态 //0：未打印分装单 1:已打印 2：已验证3:已发运
        /// </summary>
        /// <param name="productNo"></param>
        /// <param name="state"></param>
        public void SaveJISA(string productNo, int state)
        {
            string sql = string.Format("update T_JISA set State='{0}' where ProductionNumber ='{1}'", state, productNo);

            sqlHelper.ExecuteNonQuery(connstr, sql);
        }
        /// <summary>
        /// 强制发运
        /// </summary>
        /// <param name="productNo"></param>
        public void ForceShip(string productNo)
        {
            string sql = $"update T_JISA set State=3,FaYunState =1,FaYunTime=GETDATE(), FaYunUser='强制发运' where ProductionNumber ='{productNo}'";

            sqlHelper.ExecuteNonQuery(connstr, sql);
        }


        /// <summary>
        /// 根据JISA信息保存打印的分装单信息
        /// </summary>
        /// <param name="productNo"></param>
        public void SaveFZOrderRecord(string productNo)
        {
            string sql = string.Format("select * from T_HZOrderRecord where ProductNo = '{0}'", productNo);

            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    string carType = item["CarType"].ToString();
                    string productionNo = item["ProductNo"].ToString();
                    string CarModelName = item["CarModelName"].ToString();
                    string CarType = item["CarType"].ToString();
                    string Color = item["Color"].ToString();
                    string ColorCode = item["ColorCode"].ToString();
                    string MasterBarCodeL = item["MasterBarCodeL"].ToString();
                    string MasterBarCode60 = item["MasterBarCode60"].ToString();

                    string MasterBarCodeR = item["MasterBarCodeR"].ToString();
                    string MasterBarCodeC = item["MasterBarCodeC"].ToString();
                    string MasterBarCode40 = item["MasterBarCode40"].ToString();

                    DateTime createTime = DateTime.Now;
                    if (carType == "X156")
                    {
                        //左 左，60
                        string LGID = Guid.NewGuid().ToString();
                        string sqlL = string.Format(@"insert into T_FZOrderRecord
                (FZOrderID, ProductNo, LorR, CarModelName, CarType, Color, ColorCode, MasterBarCodeL, MasterBarCode60, State, CreateTime)
                  values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}')"
                  , LGID, productionNo, 0, CarModelName, CarType, Color, ColorCode, MasterBarCodeL, MasterBarCode60, 0, createTime
                  );
                        sqlHelper.ExecuteNonQuery(connstr, sqlL);

                        //右 右，40 + 
                        string RGID = Guid.NewGuid().ToString();
                        string sqlR = string.Format(@"insert into T_FZOrderRecord
                (FZOrderID, ProductNo, LorR, CarModelName, CarType, Color, ColorCode, MasterBarCodeR, MasterBarCodeC, MasterBarCode40, State, CreateTime)
                  values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}')",
                  RGID, productionNo, 1, CarModelName, CarType, Color, ColorCode, MasterBarCodeR, MasterBarCodeC, MasterBarCode40, 0, createTime);
                        sqlHelper.ExecuteNonQuery(connstr, sqlR);

                    }

                }
            }
        }


        /// <summary>
        /// 根据生产号获取该合装单所有信息
        /// </summary>
        /// <param name="productNo"></param>
        /// <returns></returns>
        public DataTable GetOrderInfo(string productNo)
        {
            string sql = string.Format("select * from T_HZOrderRecord where ProductNo='{0}'");

            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);

            return dt;

        }

        /// <summary>
        /// 根据生产号获取左分装单信息
        /// </summary>
        /// <param name="productNo"></param>
        /// <returns></returns>
        public DataTable GetFZOrderL(string productNo)
        {
            string sql = string.Format("select * from T_FZOrderRecord where ProductNo = '{0}' and LorR = 0 and state = '0'", productNo);

            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);

            return dt;
        }

        /// <summary>
        /// 根据生产号获取右分装单信息
        /// </summary>
        /// <param name="productNo"></param>
        /// <returns></returns>
        public DataTable GetFZOrderR(string productNo)
        {
            string sql = string.Format("select * from T_FZOrderRecord where ProductNo = '{0}' and LorR = 1 and state ='0'", productNo);

            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);

            return dt;
        }

        /// <summary>
        /// 根据生产号获取EDI状态
        /// </summary>
        /// <param name="ProductionNumber"></param>
        /// <returns></returns>
        public int GetJISAState(string ProductionNumber)
        {
            string sql = string.Format("select State from T_JISA where ProductionNumber ='{0}'", ProductionNumber);

            int dt = Convert.ToInt32(sqlHelper.ExecuteScalar(connstr, sql));

            return dt;

        }


        public DataTable GetJISAOrder(string productNo)
        {
            string sql = string.Format("select * from T_JISA  where ProductionNumber ='{0}' and state < '2' order by JISANumber");

            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);

            return dt;

        }


        public DataTable GetTop2JISA()
        {
            //string productNo = "";
            string sql = "select top(2) ProductionNumber,JISANumber from T_JISA where State =2 order by JISANumber asc ";
            //if(sqlHelper.ExecuteScalar(connstr, sql)!=null)
            //{
            //    productNo = sqlHelper.ExecuteScalar(connstr, sql).ToString();
            //}
            //else
            //{
            //    productNo = "";
            //}
            return sqlHelper.ExecuteDataTable(connstr, sql);

            //return productNo;
        }

        /// <summary>
        /// 根据生产号获取JISA序
        /// </summary>
        /// <param name="productNo"></param>
        /// <returns></returns>
        public string GetJISASer(string productNo)
        {
            string sql =string.Format("select JISANumber  from T_JISA where ProductionNumber ='{0}'",productNo);

            string serialNo = sqlHelper.ExecuteScalar(connstr, sql).ToString();

            return serialNo;

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public DataTable GetShippingPersonInfo(string userId)
        {
            string sql = $"select * from T_ShippingPersonInfo where UserID='{userId}";

            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);
            return dt;
        }
        /// <summary>
        /// 保存发运人信息
        /// </summary>
        /// <param name="proNo"></param>
        /// <param name="userID"></param>
        public void SaveShippingPerson(string proNo, string userID)
        {
            string sql = string.Format("update T_JISA set ShippingPerson ='{0}' where ProductionNumber ='{1}'", userID, proNo);

            sqlHelper.ExecuteNonQuery(connstr, sql);
        }



        public string GetUserNamebyID(string userID)
        {
            string userName = "";
            string sql = string.Format("select StaffName from T_Staff where StaffID='{0}'", userID);

            if(sqlHelper.ExecuteScalar(connstr,sql)!=null)
            {
                userName = sqlHelper.ExecuteScalar(connstr, sql).ToString();
            }

            return userName;
        }

        /// <summary>
        /// 执行赵工存储过程
        /// </summary>
        /// <param name="product"></param>
        /// <param name="jisANo"></param>
        public void ExcuteJisA(string product,string jisANo)
        {
            SqlParameter[] pas = {
                new SqlParameter("@productNo",SqlDbType.VarChar,40),
                new SqlParameter("@seqNo",SqlDbType.VarChar,40)
            };
            pas[0].Value = product;
            pas[1].Value = jisANo;

           sqlHelper.ExecuteProc(connstr, "EDI_DeliveryJISA", pas);

        }

    }
}
