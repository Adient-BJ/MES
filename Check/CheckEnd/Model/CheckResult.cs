using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckEnd.Model
{
    public class CheckResult
    {

        public static string ZJPartConfigCode { get; set; }

        public static string ZJPartConfigName { get; set; }

        public static string ZJOptionConfigCode { get; set; }

        public static List<string> FlawRecord { get; set; }



        public void AddFlawRecord(string ZJFlawDetail)
        {
            if(FlawRecord == null)
            {
                FlawRecord = new List<string>();
                FlawRecord.Add(ZJFlawDetail);
            }
            else
            {
                FlawRecord.Add(ZJFlawDetail);
            }

        }



    }
}
