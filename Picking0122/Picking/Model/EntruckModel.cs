using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Picking.Model
{
    public class EntruckModel
    {

        public static Dictionary<string, string> ScanedBarCode;
        
        public void AddBarCode(string part,string barCode)
        {
            if (ScanedBarCode == null)
            {
                ScanedBarCode = new Dictionary<string, string>();
                if(!ScanedBarCode.Keys.Contains(part))
                {
                    ScanedBarCode.Add(part, barCode);
                }
                
            }
            else
            {
                if (!ScanedBarCode.Keys.Contains(part))
                {
                    ScanedBarCode.Add(part, barCode);
                }
            }

        }

 
        public static string FaYunUser { get; set; }

       
    }
}
