﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace CheckEnd.Bll
{
   public class T_ZhiWen
    {
        #region 数据库连接字符串获取
        public static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        #endregion

        DBUtility.SqlHelper sqlhelper = new DBUtility.SqlHelper();
        public List<Model.T_ZhiWen> GetAll()
        {
            string sql = "select * from T_ZhiWen";
            return sqlhelper.QueryList<Model.T_ZhiWen>(connstr, sql);

        }

    }
}
