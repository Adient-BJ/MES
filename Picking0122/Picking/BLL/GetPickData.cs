using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Picking.BLL
{
    public class GetPickData
    {

        public DataTable GetData()
        {
            DataTable dtGBPatient = new DataTable();

            string excelPath = @"D:\PickingConfig.xlsx";

            string strConn;

            string excelName = "X156配方";

            //注意：把一个excel文件看做一个数据库，一个sheet看做一张表。语法 "SELECT * FROM [sheet1$]"，表单要使用"[]"和"$"

            // 1、HDR表示要把第一行作为数据还是作为列名，作为数据用HDR=no，作为列名用HDR=yes；
            // 2、通过IMEX=1来把混合型作为文本型读取，避免null值。
            strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{0}';Extended Properties='Excel 12.0;HDR=YES;IMEX=1';";

            string strConnection = string.Format(strConn, excelPath);
            OleDbConnection conn = new OleDbConnection(strConnection);
            conn.Open();
            OleDbDataAdapter oada = new OleDbDataAdapter("select * from [" + excelName + "$]", strConnection);

            dtGBPatient.TableName = "gbPatientInfo";
            oada.Fill(dtGBPatient);//获得datatable
            conn.Close();
            conn.Dispose();
            oada.Dispose();
            return dtGBPatient;

        }


    }
}
