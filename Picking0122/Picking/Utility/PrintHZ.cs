using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Picking.Utility
{
    public class PrintHZ:PrintDocument
    {
        public string PrintName { get; set; }
        public string Title { get; set; }
        public string ProductNo { get; set; }
        public string PrintTime { get; set; }
        public Bitmap Images { get; set; }
        public string CarType { get; set; }

        public string CarType1 { get; set; }
        public string Color { get; set; } 

        public PrintHZ()
        {
            this.PrintPage += printDocument1_PrintPage;
        }

        public void Start()
        {
            this.DocumentName = this.PrintName;
            this.Print();
        }
        public void YuLan()
        {
            this.DocumentName = this.PrintName;
            PrintPreviewDialog p = new PrintPreviewDialog();
            p.Document = this;
            p.ShowDialog(); 
        }

        private int top = 40;
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawString(this.Title, new Font("微软雅黑", 18), Brushes.Black, new PointF(300, 30+ top));
            g.DrawString(this.ProductNo, new Font("微软雅黑", 72), Brushes.Black, new PointF(220, 100 + top));
            g.DrawString(this.PrintTime, new Font("微软雅黑", 14), Brushes.Black, new PointF(310, 240 + top));
            g.DrawImage(this.Images, new PointF(310, 310));

            g.DrawString(this.CarType, new Font("微软雅黑", 26), Brushes.Black, new PointF(260, 500 + top));
            g.DrawString(this.Color, new Font("微软雅黑", 26), Brushes.Black, new PointF(340, 600 + top));
            g.DrawString("左前", new Font("微软雅黑", 20), Brushes.Black, new PointF(340, 700 + top));
            g.DrawString("右前", new Font("微软雅黑", 20), Brushes.Black, new PointF(340, 750 + top));
            g.DrawString("后座椅整垫", new Font("微软雅黑", 20), Brushes.Black, new PointF(300, 800 + top));
            if (CarType1=="X156")
            {
                g.DrawString("后背40%", new Font("微软雅黑", 20), Brushes.Black, new PointF(340, 850 + top));
                g.DrawString("后背60%", new Font("微软雅黑", 20), Brushes.Black, new PointF(340, 900 + top));
            }
            else if (CarType1=="Z177")
            {
                g.DrawString("后座椅整背", new Font("微软雅黑", 20), Brushes.Black, new PointF(340, 850 + top));

            }

            this.Dispose();
        }
    }
}
