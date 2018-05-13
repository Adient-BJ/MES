using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using Excel = Microsoft.Office.Interop.Excel;

namespace Picking.BLL
{
    public class PrintFZOrder
    {

        public void PrintOrder(string path, System.Data.DataTable datatable,int LorR)
        {
            //Excel模板文件
            string strFilePath = path;

            if (!File.Exists(strFilePath))
            {
                throw new Exception("Excel条码模版不存在，无法导出");
            }

            //定义
            Microsoft.Office.Interop.Excel.Application xlApp = new Excel.Application();

            if (xlApp == null)
            {
                throw new Exception("无法创建Excel对象，可能您的电脑未安装Excel");
            }

            xlApp.Visible = false;
            xlApp.UserControl = true;
            xlApp.DisplayAlerts = false;

            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Excel.Workbook workbook = workbooks.Add(strFilePath); //目标文件

            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];//取得sheet1

            //赋值
 
            System.Data.DataTable dt = datatable;

            if(dt.Rows.Count>0)
            {
                if(Convert.ToInt32(dt.Rows[0]["LorR"])==0)
                {
                    worksheet.Cells[1, 1] = "【左】"; //左座椅还是右
                }
                else
                {
                    worksheet.Cells[1, 1] = "【右】";
                }
                string carType = dt.Rows[0]["CarType"].ToString();
                worksheet.Cells[2, 2] = carType + "座椅分装单"; //第一行
                worksheet.Cells[4, 2] = dt.Rows[0]["ProductNo"].ToString(); //第二行
                BLL.T_JISA t_JISA = new T_JISA();
                string JISASer =  t_JISA.GetJISASer(dt.Rows[0]["ProductNo"].ToString());
                worksheet.Cells[5, 2] = JISASer;
                worksheet.Cells[6, 2] = dt.Rows[0]["CreateTime"].ToString(); //第二行
                                                                             //worksheet.Cells[8,2] = dt.Rows[0]["ProductNo"].ToString(); //第二行
                worksheet.Cells[11, 2] = dt.Rows[0]["CarModelName"].ToString(); //第二行
                worksheet.Cells[13, 2] = dt.Rows[0]["Color"].ToString() + dt.Rows[0]["ColorCode"].ToString(); //第二行


                int row = 15;
                if (dt.Rows[0]["MasterBarCodeL"].ToString() != "")
                {
                    worksheet.Cells[row++, 2] = "左前";
                }
                if (dt.Rows[0]["MasterBarCodeR"].ToString() != "")
                {
                    worksheet.Cells[row++, 2] = "右前";
                }
                if (dt.Rows[0]["MasterBarCodeC"].ToString() != "")
                {
                    worksheet.Cells[row++, 2] = "后座椅整垫";
                }
                if (dt.Rows[0]["MasterBarCode40"].ToString() != "")
                {
                    worksheet.Cells[row++, 2] = "后背40%";
                }
                if (dt.Rows[0]["MasterBarCode60"].ToString() != "")
                {
                    worksheet.Cells[row++, 2] = "后背60%";
                }
                if (dt.Rows[0]["MasterBarCodeB"].ToString() != "")
                {
                    worksheet.Cells[row++, 2] = "后整背";
                }

                DataMatrix.net.DmtxImageEncoder dataMatix = new DataMatrix.net.DmtxImageEncoder();


                string QRCodepath = "";
                if (LorR==0)
                {
                    Bitmap dataMatixCode = dataMatix.EncodeImage( 0+dt.Rows[0]["ProductNo"].ToString(), 15);
                    QRCodepath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Image\\" + dt.Rows[0]["ProductNo"].ToString();
                    dataMatixCode.Save(QRCodepath);
                }
                else if(LorR ==1)
                {
                    Bitmap dataMatixCode = dataMatix.EncodeImage( 1 + dt.Rows[0]["ProductNo"].ToString(), 15);
                     QRCodepath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Image\\" + dt.Rows[0]["ProductNo"].ToString();
                    dataMatixCode.Save(QRCodepath);
                }
                

                Microsoft.Office.Interop.Excel.Range m_objRange = worksheet.get_Range("B6", Type.Missing);
                m_objRange.Select();

                Excel.Pictures pics = (Excel.Pictures)worksheet.Pictures();

                pics.Insert(QRCodepath, m_objRange);

                Dictionary<int, string> DictPrinterName = new Dictionary<int, string>();

                worksheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;//纸张大小
                //worksheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape; 页面横向
                worksheet.PageSetup.CenterHorizontally = true; //文字水平居中

                xlApp.Visible = true;
                System.Windows.Forms.Application.DoEvents();

                //开始打印
                worksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                //打印预览
                //worksheet.PrintPreview();

                //打印结束后清除Excel内存
                workbooks.Close();
                xlApp.Application.Quit();
                xlApp.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                GC.Collect();//强行销毁

            }


        }




    }
}
