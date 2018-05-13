using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace Picking.BLL
{
    public class T_PickConfig
    {

        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion
        DB.SqlHelper sqlHelper = new DB.SqlHelper();

        BLL.GetPickData getPickData = new BLL.GetPickData();

        /// <summary>
        /// 根据主条码，ALCCode，区域号获取 需要亮灯拾取的对应点的信息
        /// </summary>
        /// <param name="masterBarCode"></param>
        /// <param name="ALCCode"></param>
        /// <param name="areaID"></param>
        /// <returns></returns>
        public List<string> GetPickItem(string masterBarCode, string ALCCode, int areaID)
        {
            List<string> OpcTag = new List<string>();

            string sql = string.Format("select AssyNo from T_AssyALC where AlCCode = '{0}'", ALCCode);

            object obj = sqlHelper.ExecuteScalar(connstr, sql);
            string AssyNo = "";
            if (obj != null)
            {
                AssyNo = obj.ToString();
                string LorR = "";
                string A = masterBarCode.Substring(0, 1);
                if (A == "A")
                {
                    LorR = "L";
                }
                else if (A == "E")
                {
                    LorR = "R";
                } 
                DataTable dt = getPickData.GetData();

                if (areaID == 1)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        if(item[AssyNo]!=null)
                        {
                            string ss = item[AssyNo].ToString();
                            string area = item["分区"].ToString();
                            if (ss.Contains(LorR) && area == "1")
                            {
                                OpcTag.Add(item["OPCSite"].ToString());
                            }
                        }
                     
                    }
                }

                else if (areaID == 2)
                {

                    foreach (DataRow item in dt.Rows)
                    {
                        string ss = item[AssyNo].ToString();
                        string area = item["分区"].ToString();
                        if (ss.Contains(LorR) && area == "2")
                        {
                            OpcTag.Add(item["OPCSite"].ToString());
                        }
                    }
                }

            }


            return OpcTag;

        }




        /// <summary>
        /// 保存亮灯拾取错误记录
        /// </summary>
        public void SaveErrorLog()
        {
            string errorIogID = Guid.NewGuid().ToString();
            string sql = "insert into T_PickErrorLog () values()";

            sqlHelper.ExecuteNonQuery(connstr, sql);

        }

        /// <summary>
        /// 保存亮灯拾取记录
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="costTime"></param>
        public void SavePickedRecord(string costTime)
        {
            string userID = Model.User.UserID;

            string sql = string.Format("");

            sqlHelper.ExecuteNonQuery(connstr, sql);

        }

    }
}
