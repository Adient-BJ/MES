using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckEnd.Model
{
    public   class T_Staff
    {
         public string StaffID { get; set; }
        public int RoleID { get; set; }
        public string ZhiWenID { get; set; }
        public string StaffCardCode { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public string StaffName { get; set; }
        public string TitileImagePath { get; set; }
        public string SkillClass { get; set; }
        public string UserID { get; set; }
        public DateTime? CreateTime { get; set; }
        public string ModifyUser { get; set; }
        public DateTime? ModifyTime { get; set; }

    }
}
