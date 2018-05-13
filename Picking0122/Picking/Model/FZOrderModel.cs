using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Picking.Model
{
    public class FZOrderModel
    {

        public static List<string> ProductNo { get; set; }

        public void AddNo(string productNo)
        {
            if(ProductNo==null)
            {
                ProductNo = new List<string>();
                ProductNo.Add(productNo);
            }
            else
            {

                ProductNo.Add(productNo);
            }
        }

    }
}
