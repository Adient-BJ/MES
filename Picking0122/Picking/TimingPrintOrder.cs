using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using Picking.Utility;
using Excel = Microsoft.Office.Interop.Excel;

namespace Picking
{
    public class TimingPrintOrder
    {


        public string PrintEntruckOrder(string path)
        {
            return "";
            ////Excel模板文件
            //string strFilePath = path;
            ////定义
            //Excel.Application xlApp = new Excel.Application();
            //Excel.Workbooks workbooks = xlApp.Workbooks;
            //Excel.Workbook workbook = workbooks.Add(strFilePath); //目标文件

            //Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
            //try
            //{


            //    if (!File.Exists(strFilePath))
            //    {
            //        return "Excel条码模版不存在，无法导出";
            //    }



            //    if (xlApp == null)
            //    {
            //        return "无法创建Excel对象，可能您的电脑未安装Excel";
            //    }


            //    xlApp.Visible = false;
            //    xlApp.UserControl = true;
            //    xlApp.DisplayAlerts = false;



            //    string HZOrderID = "";
            //    //赋值
            //    BLL.T_Verifying t_Verifying = new BLL.T_Verifying();
            //    System.Data.DataTable dt = t_Verifying.PrintHZOrder();
            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        HZOrderID = dt.Rows[0]["HZOrderGID"].ToString();

            //        string carType = dt.Rows[0]["CarType"].ToString();

            //        worksheet.Cells[2, 2] = carType + "座椅装箱单"; //第一行
            //        worksheet.Cells[4, 2] = "'" + dt.Rows[0]["ProductNo"].ToString(); //第二行
            //        worksheet.Cells[5, 2] = dt.Rows[0]["CreateTime"].ToString(); //第二行
            //        //worksheet.Cells[8,2] = dt.Rows[0]["ProductNo"].ToString(); //第二行
            //        worksheet.Cells[10, 2] = dt.Rows[0]["CarModelName"].ToString(); //第二行
            //        worksheet.Cells[12, 2] = dt.Rows[0]["Color"].ToString() + dt.Rows[0]["ColorCode"].ToString(); //第二行

            //        int row = 14;
            //        //if (dt.Rows[0]["MasterBarCodeL"].ToString() != "")
            //        //{
            //        //    worksheet.Cells[row++, 2] = "左前";
            //        //}
            //        //if (dt.Rows[0]["MasterBarCodeR"].ToString() != "")
            //        //{
            //        //    worksheet.Cells[row++, 2] = "右前";
            //        //}
            //        //if (dt.Rows[0]["MasterBarCodeC"].ToString() != "")
            //        //{
            //        //    worksheet.Cells[row++, 2] = "后座椅整垫";
            //        //}
            //        //if (dt.Rows[0]["MasterBarCode40"].ToString() != "")
            //        //{
            //        //    worksheet.Cells[row++, 2] = "后背40%";
            //        //}
            //        //if (dt.Rows[0]["MasterBarCode60"].ToString() != "")
            //        //{
            //        //    worksheet.Cells[row++, 2] = "后背60%";
            //        //}
            //        //if (dt.Rows[0]["MasterBarCodeB"].ToString() != "")
            //        //{
            //        //    worksheet.Cells[row++, 2] = "后整背";
            //        //}


            //        worksheet.Cells[row++, 2] = "左前";

            //        worksheet.Cells[row++, 2] = "右前";

            //        worksheet.Cells[row++, 2] = "后座椅整垫";


            //        worksheet.Cells[row++, 2] = "后背40%";

            //        worksheet.Cells[row++, 2] = "后背60%";

            //        //worksheet.Cells[row++, 2] = "后整背";


            //        DataMatrix.net.DmtxImageEncoder dataMatix = new DataMatrix.net.DmtxImageEncoder();

            //        Bitmap dataMatixCode = dataMatix.EncodeImage(dt.Rows[0]["ProductNo"].ToString(), 15);
            //        string QRCodepath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Image\\" +
            //                            dt.Rows[0]["ProductNo"].ToString();
            //        dataMatixCode.Save(QRCodepath);

            //        //Excel.Range rng = (Excel.Range)worksheet.get_Range(RangeName, Type.Missing);
            //        //rng.Select();

            //        //object objOle = (Microsoft.Office.Interop.Excel.OLEObject)worksheet.OLEObjects("B7");

            //        Microsoft.Office.Interop.Excel.Range m_objRange = worksheet.get_Range("B6", Type.Missing);
            //        m_objRange.Select();

            //        Excel.Pictures pics = (Excel.Pictures) worksheet.Pictures();

            //        pics.Insert(QRCodepath, m_objRange);

            //        //Dictionary<int, string> DictPrinterName = new Dictionary<int, string>();

            //        //foreach (string sPrint in PrinterSettings.InstalledPrinters)//获取所有打印机名称
            //        //{
            //        //    DictPrinterName.Add(id, sPrint);
            //        //    id++;
            //        //}

            //        //打印Excel：
            //        worksheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4; //纸张大小
            //        /*   worksheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;*/ //页面横向
            //        worksheet.PageSetup.CenterHorizontally = true; //文字水平居中


            //        //xlApp.Visible = true;

            //        //System.Windows.Forms.Application.DoEvents();
            //        string printer = XML.XmlConfig.PrintName; //XML.XmlConfig.GetIPXML();
            //        //xlApp.ActivePrinter = printer;

            //        //开始打印
            //        worksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, printer, Type.Missing,
            //            Type.Missing, Type.Missing);


            //        //打印预览
            //        //worksheet.PrintPreview();

            //        ////打印结束后清除Excel内存
            //        //workbooks.Close();
            //        //xlApp.Application.Quit();
            //        //xlApp.Quit();


            //        return HZOrderID;

            //    }
            //    else
            //    {
            //        return HZOrderID;
            //    }

            //}
            //catch (Exception ex)
            //{

            //    return "";
            //}
            //finally
            //{
            //    //打印结束后清除Excel内存
            //    workbooks.Close();
            //    xlApp.Application.Quit();
            //    xlApp.Quit();
            //    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            //    GC.Collect(); //强行销毁
            //    PrintHzOrders.Kill(xlApp);
            //}

        }

        public void PrintEntruckOrderNew(DataRow item)
        { 
            try
            {
                string HZOrderID = item["HZOrderGID"].ToString();
                string carType = item["CarType"].ToString();
                string ProductNo = item["ProductNo"].ToString();
                string CreateTime = item["CreateTime"].ToString();
                string CarModelName = item["CarModelName"].ToString();
                string Color = item["Color"].ToString() + item["ColorCode"].ToString(); //第二行

                DataMatrix.net.DmtxImageEncoder dataMatix = new DataMatrix.net.DmtxImageEncoder();
                Bitmap dataMatixCode = dataMatix.EncodeImage(item["ProductNo"].ToString(), 15);

                PrintHZ p = new PrintHZ();
                p.PrintName = XML.XmlConfig.PrintName;
                p.Title = carType + "座椅装箱单";
                p.ProductNo = ProductNo;
                p.PrintTime = CreateTime;
                p.Images = dataMatixCode;
                p.CarType = CarModelName;
                p.CarType1 = carType;
                p.Color = Color;

                //p.YuLan();
                p.Start();
            }
            catch (Exception ex)
            { 
                 
            }
            finally
            { 
                GC.Collect(); //强行销毁 
            } 
        } 
    }




}

