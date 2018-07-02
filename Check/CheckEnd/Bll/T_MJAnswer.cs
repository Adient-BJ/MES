using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CheckEnd.Bll
{
    public class T_MJAnswer
    {
        #region 数据库连接字符串获取
        public static string Connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion

        DBUtility.SqlHelper sqlHelper = new DBUtility.SqlHelper();
        Random r = new Random();
        /// <summary>
        /// 查询正确答案
        /// </summary>
        /// <returns></returns>
        public DataTable GetAnswer()
        {

            string sql = "select * from T_MJProblem ";
            DataSet ds = sqlHelper.ExecuteDataSet(Connstr, sql);
            DataTable answer = new DataTable();
            answer.Columns.Add("MJProblemCode");
            answer.Columns.Add("Problem");
            answer.Columns.Add("Answers");
            answer.Columns.Add("Answers2");
            answer.Columns.Add("Answers3");
            answer.Columns.Add("Answers4");
            answer.Columns.Add("ZhengQue");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow question = answer.NewRow();
                question[0] = ds.Tables[0].Rows[i]["MJProblemCode"].ToString();
                question[1] = ds.Tables[0].Rows[i]["Problem"].ToString();
                //string[] arrs = ds.Tables[0].Rows[i]["Answers"].ToString().Split('|');
                //int j = 0;
                //List<string> slist = new List<string>();
                //Dictionary<int, string> jdic = new Dictionary<int, string>();
                //int xiaoyu = arrs.Length > 3 ? 3 : arrs.Length;
                //while (j < xiaoyu)
                //{
                //    int rani = r.Next(0, arrs.Length);
                //    if (!jdic.ContainsKey(rani))
                //    {
                //        slist.Add(arrs[rani]);
                //        jdic.Add(rani, "");
                //        j++;
                //    }
                //}
                //string trueAnswer = GetTrueAnswer(Model.UserAnswerQuestions.BarCode, ds.Tables[0].Rows[i]["MJProblemCode"].ToString());
                //if (!slist.Contains(trueAnswer))
                //{
                //    slist.Add(trueAnswer);
                //}
                //question[2] = slist.Count > 0 ? slist[0] : "";
                //question[3] = slist.Count > 1 ? slist[1] : "";
                //question[4] = slist.Count > 2 ? slist[2] : "";
                //question[5] = slist.Count > 3 ? slist[3] : "";

                string[] ans = ds.Tables[0].Rows[i]["Answers"].ToString().Split('|');

                string a = string.Join(",", ans.OrderBy(d => Guid.NewGuid()).Take(4));

                string[] an = a.Split(',');

                string trueAnswer = GetTrueAnswer(Model.UserAnswerQuestions.BarCode, question[0].ToString());

                if (!an.Contains(trueAnswer))
                {
                    an[0] = trueAnswer;
                }

                for (int j = 0; j < an.Count(); j++)
                {
                    int index = j + 2;
                    if (index >= answer.Columns.Count)
                    {
                        break;
                    }
                    question[index] = an[j];

                }

                question[6] = trueAnswer;
                answer.Rows.Add(question);

            }

            return answer;


        }


        public string GetTrueAnswer(string masterBarCode, string mjProblemCode)
        {

            //string sql = string.Format(@"declare @CarType varchar(30)
            //               declare @Year varchar(10)
            //               declare @CarModelName varchar(64)
            //               declare @ColorNumber varchar(64)
            //               declare @CarModelConfigID varchar(32)
            //               declare @ColorConfigID varchar(36)
            //               declare @CarPartID varchar(36)
            //               declare @MasterBarCode varchar(64)
            //        set @MasterBarCode = '{0}'

            //        select Top(1) @CarType =  T.CarType , @Year = T.Year ,  @CarModelName =  T.CarModelName, @ColorNumber = T.ColorNumber  
            //            from T_WorkbayRecord T where MasterBarCode = @MasterBarCode

            //        select @CarModelConfigID = CarModelConfigID
            //            from T_CarModelConfig where CarTypeName = @CarType and  YearName =@Year and CarModelName = @CarModelName

            //        select  @ColorConfigID = ColorConfigID
            //            from T_ColorConfig  where CarModelConfigID = @CarModelConfigID and ColorNumber = @ColorNumber

            //        select @CarPartID = CarPartID from T_CarPart where Letter = LEFT(@MasterBarCode,1)

            //        select ZQAnswer from T_MJConfigAnswer
            //        where ColorConfigID = @ColorConfigID and carPartID =  @CarPartID and MJProblemCode = '{1}'", masterBarCode, mjProblemCode);

            string AlCCode = masterBarCode.Substring(0, 6);

            string sql = string.Format("select ZQAnswer from T_MJConfigAnswer  where AlCCode ='{0}' and MJProblemCode ='{1}'", AlCCode, mjProblemCode);
            DataTable dt = sqlHelper.ExecuteDataTable(Connstr, sql);

            string ZQAnswer = dt.Rows[0][0].ToString();
            return ZQAnswer;



        }


        public void SaveErrorAnswer(string mJProblemCode, string masterBarCode, string answersError, string userID)
        {

            DateTime createTime = DateTime.Now;
            string mJProblemErrorID = Guid.NewGuid().ToString();
            string sql =
                "insert into T_MJProblemError ( MJProblemErrorID,MJProblemCode,MasterBarCode,AnswersError,UserID,CreateTime)" +
                $"values('{mJProblemErrorID}','{mJProblemCode}','{masterBarCode}','{answersError}','{userID}','{createTime}')";
            sqlHelper.ExecuteNonQuery(Connstr, sql);
        }
        public int GetQuestionsCount()
        {
            string sql = " select count(MJProblemCode) from T_MJProblem ";
            int questionsCount = Convert.ToInt32(sqlHelper.ExecuteScalar(Connstr, sql));
            return questionsCount;
        }


        /// <summary>
        /// 保存盲检记录
        /// </summary>
        /// <param name="materBarCode"></param>
        /// <param name="mJFlag"></param>
        /// <param name="userID"></param>
        /// <param name="createTime"></param>
        public int SaveMJRecode(string materBarCode, int mJFlag, string userID, DateTime createTime)
        {
            string MJRecordID = Guid.NewGuid().ToString();
            string sql = string.Format("insert into T_MJRecord (MJRecordID,MasterBarCode,MJFlag,UserID,CreateTime)" +
                "values('{0}','{1}','{2}','{3}','{4}')", MJRecordID, materBarCode, mJFlag, userID, createTime);
            int result = sqlHelper.ExecuteNonQuery(Connstr, sql);
            return result;
        }

        /// <summary>
        /// 保存盲检强制放行记录
        /// </summary>
        /// <param name="masterBarCode"></param>
        /// <param name="mJFlag"></param>
        /// <param name="userID"></param>
        /// <param name="createTime"></param>
        public void SaveErrorPassLog(string masterBarCode, int mJFlag, string userID, DateTime createTime)
        {
            string MJRecordID = Guid.NewGuid().ToString();

            string sql = string.Format("insert into T_MJRecord(MJRecordID, MasterBarCode, MJFlag, UserID, CreateTime)" +
                "values('{0}','{1}','{2}','{3}','{4}')", MJRecordID, masterBarCode, mJFlag, userID, createTime);
            sqlHelper.ExecuteNonQuery(Connstr, sql);

        }

        /// <summary>
        /// 保存盲检强制放行信息
        /// </summary>
        /// <param name="masterBarCode"></param>
        /// <param name="Remark"></param>
        public void SaveErrorPassRecord(string masterBarCode, string Remark)
        {
            string GID = Guid.NewGuid().ToString();
            DateTime dt = DateTime.Now;
            string userID = Bll.User.UserID;
            string sql = string.Format("insert into T_MJRemark (MJPassInfoID,MasterBarCode,MJRemark,CreateTime,UserID)", GID, masterBarCode, Remark, dt, userID);
            sqlHelper.ExecuteNonQuery(Connstr, sql);
        }


        public string GetWorkBay(string IP)
        {
            string sql = string.Format("select WorkbayName from T_WorkbayIPConfig where IPAddress = '{0}'", IP);
            string workBay = sqlHelper.ExecuteScalar(Connstr, sql).ToString();

            return workBay;
        }


        public string GetWorkBayTag(string IP)
        {
            string sql = string.Format("select GetWorkOpcTag from T_WorkbayIPConfig where IPAddress = '{0}'", IP);
            string workBay = sqlHelper.ExecuteScalar(Connstr, sql).ToString();

            return workBay;
        }
    }
}
