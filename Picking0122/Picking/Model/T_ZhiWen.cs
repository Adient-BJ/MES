using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Picking.Model
{
    public class T_ZhiWen
    {
         public string ZhiWenID { get; set; }
        public string BytesStr { get; set; }
        public string SizeLength { get; set; }
        public string UserID { get; set; }
        public DateTime? CreateTime { get; set; }
        public string ModifyUser { get; set; }
        public DateTime? ModifyTime { get; set; }
    }
}
