using System;
using System.Collections.Generic;
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
using System.Runtime.InteropServices;
using Picking.Utility;

namespace Picking
{
    public class PrintHzOrders
    {

        public string PrintHZOrders(string path, string productNo, string printNames)
        {
            ////定义
            //Excel.Application xlApp = new Excel.Application();

            //if (xlApp == null)
            //{
            //    return "无法创建Excel对象，可能您的电脑未安装Excel";
            //}

            ////Excel模板文件
            //string strFilePath = path;
            //xlApp.Visible = false;
            //xlApp.UserControl = true;
            //xlApp.DisplayAlerts = false;

            //Excel.Workbooks workbooks = xlApp.Workbooks;
            //Excel.Workbook workbook = workbooks.Add(strFilePath); //目标文件

            //Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
            //try
            //{


            //    if (!File.Exists(strFilePath))
            //    {
            //        return "Excel条码模版不存在，无法导出";
            //    }



            //    string HZOrderID = "";
            //    //赋值
            //    BLL.T_Verifying t_Verifying = new BLL.T_Verifying();
            //    System.Data.DataTable dt = t_Verifying.GetHzOrderByNo(productNo);

            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        HZOrderID = dt.Rows[0]["HZOrderGID"].ToString();

            //        string carType = dt.Rows[0]["CarType"].ToString();

            //        worksheet.Cells[2, 2] = carType + "座椅装箱单"; //第一行
            //        worksheet.Cells[4, 2] = "'" + dt.Rows[0]["ProductNo"].ToString();//第二行
            //        worksheet.Cells[5, 2] = dt.Rows[0]["CreateTime"].ToString(); //第二行
            //                                                                     //worksheet.Cells[8,2] = dt.Rows[0]["ProductNo"].ToString(); //第二行
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
            //        string QRCodepath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Image\\" + dt.Rows[0]["ProductNo"].ToString();
            //        dataMatixCode.Save(QRCodepath);

            //        //Excel.Range rng = (Excel.Range)worksheet.get_Range(RangeName, Type.Missing);
            //        //rng.Select();

            //        //object objOle = (Microsoft.Office.Interop.Excel.OLEObject)worksheet.OLEObjects("B7");

            //        Microsoft.Office.Interop.Excel.Range m_objRange = worksheet.get_Range("B6", Type.Missing);
            //        m_objRange.Select();

            //        Excel.Pictures pics = (Excel.Pictures)worksheet.Pictures();

            //        pics.Insert(QRCodepath, m_objRange);

            //        Dictionary<int, string> DictPrinterName = new Dictionary<int, string>();

            //        //foreach (string sPrint in PrinterSettings.InstalledPrinters)//获取所有打印机名称
            //        //{
            //        //    DictPrinterName.Add(id, sPrint);
            //        //    id++;
            //        //}

            //        //打印Excel：

            //        worksheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;//纸张大小
            //                                                                    /*   worksheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;*///页面横向
            //        worksheet.PageSetup.CenterHorizontally = true; //文字水平居中


            //        //xlApp.Visible = true;

            //        //System.Windows.Forms.Application.DoEvents();
            //        string printer = printNames; // XML.XmlConfig.GetIPXML();
            //        //xlApp.ActivePrinter = printer;

            //        //开始打印
            //        worksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, printer, Type.Missing, Type.Missing, Type.Missing);

            //        //打印预览
            //        //worksheet.PrintPreview();

            //        ////打印结束后清除Excel内存
            //        //workbooks.Close();
            //        //xlApp.Application.Quit();
            //        //xlApp.Quit();

            //        //System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            //        //GC.Collect();//强行销毁


            //        return HZOrderID;
            //    }

            //    else
            //    {
            //        return "未找到该生产号信息";
            //    }


            //}
            //catch (Exception ex)
            //{
            //    //return ex.ToString();
            //    return "";
            //}
            //finally
            //{
            //    //打印结束后清除Excel内存
            //    workbooks.Close();
            //    xlApp.Application.Quit();
            //    xlApp.Quit();
            //    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            //    Kill(xlApp);
            //    GC.Collect(); //强行销毁

            //}

            return "";
        }


        public string PrintHZOrders(string productNo, string printNames)
        {
            try
            {
                string HZOrderID = "";
                //赋值
                BLL.T_Verifying t_Verifying = new BLL.T_Verifying();
                System.Data.DataTable dt = t_Verifying.GetHzOrderByNo(productNo);

                if (dt != null && dt.Rows.Count > 0)
                {
                    HZOrderID = dt.Rows[0]["HZOrderGID"].ToString();

                    string carType = dt.Rows[0]["CarType"].ToString();
                    string ProductNo = dt.Rows[0]["ProductNo"].ToString();
                    string CreateTime = dt.Rows[0]["CreateTime"].ToString();
                    string CarModelName = dt.Rows[0]["CarModelName"].ToString();
                    string Color = dt.Rows[0]["Color"].ToString() + dt.Rows[0]["ColorCode"].ToString(); //第二行
                     
                    DataMatrix.net.DmtxImageEncoder dataMatix = new DataMatrix.net.DmtxImageEncoder();
                    Bitmap dataMatixCode = dataMatix.EncodeImage(dt.Rows[0]["ProductNo"].ToString(), 15);

                    PrintHZ p = new PrintHZ();
                    p.PrintName = printNames;
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
                return HZOrderID;
            }
            catch (Exception ex)
            {
                //return ex.ToString();
                return "";
            }
            finally
            { 
                GC.Collect(); //强行销毁 
            }


        }


        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        public static void Kill(Excel.Application excel)
        {
            IntPtr t = new IntPtr(excel.Hwnd);   //得到这个句柄，具体作用是得到这块内存入口 

            int k = 0;
            GetWindowThreadProcessId(t, out k);   //得到本进程唯一标志k
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);   //得到对进程k的引用
            p.Kill();     //关闭进程k
        }
    }




}


