using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace CheckEnd.Bll
{
    public class T_CheckResult
    {

        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion

        DBUtility.SqlHelper sqlHelper = new DBUtility.SqlHelper();

        public DataTable GetCheckResult (string checkedPicCode)
        {
            string sql = string.Format("select ZJOptionConfigCode,ZJOptionConfigName from T_ZJOptionConfig where ZJPartConfigCode = '{0}'", checkedPicCode);
            DataTable dt =  sqlHelper.ExecuteDataTable(connstr, sql);

            return dt;
        }


        public void SaveCheckResult(string remarks)
        {
            string ZJFlawRecordID = Guid.NewGuid().ToString();
            //string zJPartConfigCode = Model.CheckResult.ZJPartConfigCode;
            string masterBarCode = Model.UserAnswerQuestions.BarCode;

            string[] flawRecord = { };
            foreach (var item in Model.CheckResult.FlawRecord)
            {
                flawRecord.ToList().Add(item);
            }
            
            string userId = Bll.User.UserID;
            DateTime dt = DateTime.Now;

            string sql = string.Format(" insert into T_ZJFlawRecord " +
                "(ZJFlawRecordID,MasterBarCode,ZJFlawDetail,ZJRemark,UserID,CreateTime)" +
                " values ('{0}','{1}','{2}','{3}','{4}','{5}') ", 
                ZJFlawRecordID, masterBarCode,flawRecord.ToString(), remarks, userId,dt);
            sqlHelper.ExecuteNonQuery(connstr, sql);


        }


        public string GetZJConfigCode(string ZJConfigName)
        {

            string ZJConfig = "";
            string sql = string.Format("select * from T_ZJOptionConfig where ZJOptionConfigName = '{0}'",ZJConfigName);
            ZJConfig = sqlHelper.ExecuteScalar(connstr, sql).ToString();

            return ZJConfig;
        }

        public DataTable GetZJFlawDetail(string ZJOptionConfigCode)
        {
            string ZJOptionCode = GetZJConfigCode(ZJOptionConfigCode);
            if(ZJOptionCode!="")
            {
                string sql = string.Format("select ZJFlawDetail,ZJFlawName from T_ZJFlawDetail where ZJOptionConfigCode = '{0}' order by ZJFlawName", ZJOptionCode);
                DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);
                return dt;
            }else
            {
                return null;
            }
            
        }


        public DataTable GetZJPartName()
        {

            string sql = "select ZJPartConfigName from T_ZJPartConfig";

            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);

            return dt;

        }

        public DataTable GetZJOptionName()
        {
            string sql = "select ZJOptionConfigName from T_ZJOptionConfig";

            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);

            return dt;
        }

        public DataTable GetZJOptionName(string ZJFlawName)
        {
            string sql = string.Format(@"select ZJOptionConfigCode,ZJOptionConfigName  from T_ZJOptionConfig where ZJOptionConfigCode 
   = (select ZJOptionConfigCode from  T_ZJFlawDetail where ZJFlawName = '{0}')", ZJFlawName);
            DataTable dt = sqlHelper.ExecuteDataTable(connstr,sql);
            return dt;
        }



        /// <summary>
        /// 根据终检图片地址获取保存的信息
        /// </summary>
        /// <param name="PicPath"></param>
        /// <returns></returns>
        public DataTable GetZJFlawSaved(string PicPath)
        {
            string sql = string.Format("select * from T_ZJFlawSaved where PicPath ='{0}'",PicPath);

            DataTable dt = sqlHelper.ExecuteDataTable(connstr, sql);

            return dt;
        }
      
        /// <summary>
        /// 保存终检图片对应缺陷及备注
        /// </summary>
        /// <param name="masterBarCode"></param>
        /// <param name="picPath"></param>
        /// <param name="ZJFlawDetail"></param>
        /// <param name="remark"></param>
        public void SaveZJFlawSaved(string masterBarCode,string picPath,string ZJPartName,string ZJFlawDetail,string remark)
        {
            DateTime dt = DateTime.Now;
            string UserID = Bll.User.UserID;

            string sql = string.Format(@"insert into T_ZJFlawSaved (MasterBarCode,PicPath,ZJPartName,ZJFlawDetail,Remark,CreateTime,CreateUser)
                    values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", masterBarCode,picPath,ZJPartName,ZJFlawDetail,remark,dt,UserID);
            sqlHelper.ExecuteNonQuery(connstr,sql);
            
        }

        /// <summary>
        /// 更新终检图片对应缺陷及备注
        /// </summary>
        /// <param name="masterBarCode"></param>
        /// <param name="picPath"></param>
        /// <param name="ZJFlawDetail"></param>
        /// <param name="remark"></param>
        public void UpdateZJFlawSaved(string masterBarCode, string picPath,string ZJPartName, string ZJFlawDetail, string remark)
        {
            string sql = string.Format("update T_ZJFlawSaved set ZJPartName ='{0}',ZJFlawDetail ='{1}',Remark ='{2}'" +
                " where PicPath = '{3}'",ZJPartName,ZJFlawDetail,remark,picPath);

            sqlHelper.ExecuteDataTable(connstr, sql);

        }


        public int GetZJPicCount(string masterBarCode)
        {
            int result = 0;
            string sql = string.Format("select count(*)from T_ZJFlawSaved  where masterbarcode = '{0}'", masterBarCode);

            result= Convert.ToInt32(sqlHelper.ExecuteScalar(connstr, sql));

            return result;
        }

    }
}
