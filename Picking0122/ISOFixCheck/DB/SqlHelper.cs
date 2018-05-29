using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ISOFixCheck.DB
{
    public class SqlHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// 
        public SqlHelper()
        {

        }

        #region ExecuteNonquery：增、删、改

        /// <summary>
        /// ExecuteNonQuery操作，对对数据库进行 增、删、改 操作（1）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string connectionString, string sql)
        {
            return ExecuteNonQuery(connectionString, sql, CommandType.Text, null);
        }

        /// <summary>
        /// ExecuteNonQuery操作，对对数据库进行 增、删、改 操作（2）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="commandType">要执行的查询类型（存储过程，sql文本）</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string connectionString, string sql, CommandType commandType)
        {
            return ExecuteNonQuery(connectionString, sql, commandType, null);
        }

        /// <summary>
        /// ExecuteNonQuery操作，对对数据库进行 增、删、改 操作（3）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="commandType">要执行的查询类型（存储过程，sql文本）</param>
        /// <param name="parameters">参数数据</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string connectionString, string sql, CommandType commandType, SqlParameter[] parameters)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    connection.Open();
                    count = command.ExecuteNonQuery();
                }
            }
            return count;
        }

        #endregion

        #region ExecuteDataSet：查询多个表

        /// <summary>
        /// sqlDataAdapter的Fill方法进行一个查询，并返回一个DataSet类型的结果（1）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string connectionString, string sql)
        {
            return ExecuteDataSet(connectionString, sql, CommandType.Text, null);
        }

        /// <summary>
        /// sqlDataAdapter的Fill方法进行一个查询，并返回一个DataSet类型的结果（2）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="commandType">要执行的查询类型（存储过程、sql文本）</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string connectionString, string sql, CommandType commandType)
        {
            return ExecuteDataSet(connectionString, sql, commandType, null);
        }

        /// <summary>
        /// sqlDataAdapter的Fill方法进行一个查询，并返回一个DataSet类型的结果（3）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="commandType">要执行的查询类型（存储过程、sql文本）</param>
        /// <param name="parameters">参数数组</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string connectionString, string sql, CommandType commandType, SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                }
            }
            return ds;
        }

        #endregion

        #region ExecuteDataTable：查询单个表

        /// <summary>
        /// SqlDataAdapter的Fill方法执行一个查询语句，并返回一个DataTable类型的结果（1）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string connectionString, string sql)
        {
            return ExecuteDataTable(connectionString, sql, CommandType.Text, null);
        }

        /// <summary>
        /// SqlDataAdapter的Fill方法执行一个查询语句，并返回一个DataTable类型的结果（2）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="commandType">要执行的查询类型（存储过程、sql文本）</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string connectionString, string sql, CommandType commandType)
        {
            return ExecuteDataTable(connectionString, sql, commandType, null);
        }

        /// <summary>
        /// SqlDataAdapter的Fill方法执行一个查询语句，并返回一个DataTable类型的结果（3）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="commandType">要执行的查询类型（存储过程、sql文本）</param>
        /// <param name="parameters">参数数组</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string connectionString, string sql, CommandType commandType, SqlParameter[] parameters)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);
                }
            }
            return data;
        }

        #endregion

        #region ExecuteReader：读取指定列的数据

        /// <summary>
        /// ExecuteReader执行一查询，返回一sqlDataReader对象实例（1）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(string connectionString, string sql)
        {
            return ExecuteReader(connectionString, sql, CommandType.Text, null);
        }

        /// <summary>
        /// ExecuteReader执行一查询，返回一sqlDataReader对象实例（2）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="commandType">要执行的查询类型（存储过程、sql文本）</param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(string connectionString, string sql, CommandType commandType)
        {
            return ExecuteReader(connectionString, sql, commandType, null);
        }

        /// <summary>
        /// ExecuteReader执行一查询，返回一sqlDataReader对象实例（3）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(string connectionString, string sql, CommandType commandType, SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            command.CommandType = commandType;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        #endregion

        #region ExecuteScalar：查询第一行第一列

        /// <summary>
        /// ExecuteScalar执行一查询，返回查询结果的第一行第一列（1）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public Object ExecuteScalar(string connectionString, string sql)
        {
            return ExecuteScalar(connectionString, sql, CommandType.Text, null);
        }

        /// <summary>
        /// ExecuteScalar执行一查询，返回查询结果的第一行第一列（2）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="commandType">要执行的查询类型（存储过程、sql文本）</param>
        /// <returns></returns>
        public Object ExecuteScalar(string connectionString, string sql, CommandType commandType)
        {
            return ExecuteScalar(connectionString, sql, commandType, null);
        }

        /// <summary>
        /// ExecuteScalar执行一查询，返回查询结果的第一行第一列（3）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="commandType">要执行的查询类型（存储过程、sql文本）</param>
        /// <param name="parameters">参数数组</param>
        /// <returns></returns>
        public Object ExecuteScalar(string connectionString, string sql, CommandType commandType, SqlParameter[] parameters)
        {
            object result = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    connection.Open();
                    result = command.ExecuteScalar();
                }
            }
            return result;
        }

        #endregion

        #region 实例方法  
        public List<T> QueryList<T>(string ConnectionString, string sql)
        {
            IDataReader reader = this.ExecuteReader(ConnectionString, sql);
            //实例化一个List<>泛型集合    
            List<T> DataList = new List<T>();
            while (reader.Read())
            {
                T RowInstance = Activator.CreateInstance<T>();//动态创建数据实体对象    
                //通过反射取得对象所有的Property    
                foreach (PropertyInfo Property in typeof(T).GetProperties())
                {
                    try
                    {
                        //取得当前数据库字段的顺序    
                        int Ordinal = reader.GetOrdinal(Property.Name);
                        if (reader.GetValue(Ordinal) != DBNull.Value)
                        {
                            //将DataReader读取出来的数据填充到对象实体的属性里    
                            Property.SetValue(RowInstance, Convert.ChangeType(reader.GetValue(Ordinal), Property.PropertyType), null);
                        }
                    }
                    catch
                    {
                        break;
                    }
                }
                DataList.Add(RowInstance);
            }
            return DataList;
        }

        public List<T> QueryList<T>(string ConnectionString, string sql, CommandType commandText, params SqlParameter[] parms)
        {
            IDataReader reader = this.ExecuteReader(ConnectionString, sql, commandText, parms);
            //实例化一个List<>泛型集合    
            List<T> DataList = new List<T>();
            while (reader.Read())
            {
                T RowInstance = Activator.CreateInstance<T>();//动态创建数据实体对象    
                //通过反射取得对象所有的Property    
                foreach (PropertyInfo Property in typeof(T).GetProperties())
                {
                    try
                    {
                        //取得当前数据库字段的顺序    
                        int Ordinal = reader.GetOrdinal(Property.Name);
                        if (reader.GetValue(Ordinal) != DBNull.Value)
                        {
                            //将DataReader读取出来的数据填充到对象实体的属性里    
                            Property.SetValue(RowInstance, Convert.ChangeType(reader.GetValue(Ordinal), Property.PropertyType), null);
                        }
                    }
                    catch
                    {
                        break;
                    }
                }
                DataList.Add(RowInstance);
            }
            return DataList;
        }
        #endregion  

        /// <summary>  
        /// 执行多条SQL语句，实现数据库事务。  
        /// </summary>  
        /// <param name="SQLStringList">多条SQL语句</param>       
        public int ExecuteSqlTran(string connectionString, List<String> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); //打开数据库连接  
                SqlCommand cmd = new SqlCommand(); //创建SqlCommand命令  
                cmd.Connection = conn; //设置命令连接  
                SqlTransaction tx = conn.BeginTransaction();//开始事务  
                cmd.Transaction = tx;//设置执行命令的事务  
                try
                {
                    int count = 0;//定义int类型变量，存放该函数返回值  
                    for (int n = 0; n < SQLStringList.Count; n++)//循环传入的sql语句  
                    {
                        string strsql = SQLStringList[n]; //第n条sql语句  
                        if (strsql.Trim().Length > 1) //如果第n条sql语句不为空  
                        {
                            cmd.CommandText = strsql; //设置执行命令的sql语句  
                            count += cmd.ExecuteNonQuery(); //调用执行增删改sql语句的函数ExecuteNonQuery(),执行sql语句  
                        }
                    }
                    tx.Commit();//提交事务  
                    return count;//返回受影响行数  
                }
                catch
                {
                    tx.Rollback();
                    return 0;
                }
                finally
                {
                    conn.Close();

                }
            }
        }
        public int ExecuteProc(string connectionString, string procName, params SqlParameter[] pas)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand comm = new SqlCommand(procName, conn);
            conn.Open();
            comm.Parameters.AddRange(pas);
            comm.CommandType = CommandType.StoredProcedure;
            int r = comm.ExecuteNonQuery();

            conn.Dispose();
            comm.Dispose();
            return r;
        }
        public DataSet QueryProc(string connectionString, string procName, params SqlParameter[] pas)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand comm = new SqlCommand(procName, conn);
            conn.Open();
            comm.Parameters.AddRange(pas);
            comm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            da.Fill(ds);

            conn.Dispose();
            comm.Dispose();
            da.Dispose();
            return ds;
        }
        #region ExecuteDataReader

        /// <summary>  
        /// 执行SQL语句,返回只读数据集  
        /// </summary>  
        /// <param name="commandText">SQL语句</param>  
        /// <param name="parms">查询参数</param>  
        /// <returns>返回只读数据集</returns>  
        public SqlDataReader ExecuteDataReader(string ConnectionString, string commandText, params SqlParameter[] parms)
        {
            return ExecuteDataReader(ConnectionString, CommandType.Text, commandText, parms);
        }

        /// <summary>  
        /// 执行SQL语句,返回只读数据集  
        /// </summary>  
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>  
        /// <param name="commandText">SQL语句或存储过程名称</param>  
        /// <param name="parms">查询参数</param>  
        /// <returns>返回只读数据集</returns>  
        public SqlDataReader ExecuteDataReader(string ConnectionString, CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            return ExecuteDataReader(ConnectionString, commandType, commandText, parms);
        }
        #endregion  
    }
}
