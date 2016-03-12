using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Data
{
    public class DataBase
    {
        private static readonly string BlogStr = ConfigurationManager.ConnectionStrings["Blog_ConnectionString"].ConnectionString;

        #region SQLServer

        /// <summary>
        /// 打开数据库连接(SQLServer)
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetOpenConn()
        {
            var conn = new SqlConnection(BlogStr);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// 根据sql执行，返回SqlDataReader数据流
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataAdapter ExecuteQuery(string sql, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            return da;
        }

        /// <summary>
        /// 分页查询(SqlServer)
        /// </summary>
        /// <param name="tableName">表明</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="conditions">查询条件</param>
        /// <returns></returns>
        public DataSet GetPage(string tableName, int pageIndex, int pageSize)
        {
            //分页查询
            if (pageIndex < 0)
                pageIndex = 0;
            pageIndex++;
            pageSize = 10;
            var pagesql = new StringBuilder();
            var totalsql = new StringBuilder();

            pagesql.AppendFormat("  Select Top {0} * From (Select Row_number() Over (Order By Id) as CID,* from [{2}])"
                + " T Where Id NOT IN (Select Top ({0}*({1}-1)) Id From [{2}])", pageSize, pageIndex, tableName);
            totalsql = totalsql.AppendFormat("  Select Count(1) as TotalCount from [{0}] Where 1=1 ", tableName);

            DataSet ds = new DataSet();
            using (var conn = DataBase.GetOpenConn())
            {
                var pageandtotal = pagesql.Append(totalsql);
                SqlDataAdapter da = DataBase.ExecuteQuery(pageandtotal.ToString(), conn);
                if (da != null)
                {
                    da.Fill(ds);
                }
            }
            return ds;
        }

        /// <summary>
        /// 查询所有的(SQLServer)
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet GetList(string tableName)
        {
            var pagesql = new StringBuilder();
            var totalsql = new StringBuilder();
            pagesql.AppendFormat(" SELECT * FROM [{0}] WHERE 1=1", tableName);
            totalsql = totalsql.AppendFormat("  Select Count(1) as TotalCount From [{0}] Where 1=1 ", tableName);
            DataSet ds = new DataSet();
            using (var conn = DataBase.GetOpenConn())
            {
                var pageandtotal = pagesql.Append(totalsql);
                SqlDataAdapter da = DataBase.ExecuteQuery(pageandtotal.ToString(), conn);
                if (da != null)
                {
                    da.Fill(ds);
                }
            }
            return ds;
        }



        public DataSet GetOnlyById(int Id, string tableName)
        {
            string sql = string.Format("Select * From [{0}] Where 1=1 And Id = {1}", tableName, Id);
            DataSet ds = new DataSet();
            using (var conn = DataBase.GetOpenConn())
            {
                SqlDataAdapter da = DataBase.ExecuteQuery(sql, conn);
                if (da != null)
                {
                    da.Fill(ds);
                }
            }
            return ds;
        }

        #endregion

        #region MySql

        /// <summary>
        /// 打开数据库连接(MySql)
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection GetOpenConn4MySql()
        {
            var conn = new MySqlConnection(BlogStr);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// 根据sql执行，返回MySqlDataReader数据流(MySql)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="conn"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static MySqlDataReader ExecuteQueryReader(string sql, MySqlConnection conn, MySqlParameter[] paras)
        {
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            if (paras != null && paras.Count() > 0)
            {
                cmd.Parameters.AddRange(paras);
            }
            MySqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        /// <summary>
        /// 根据sql执行，返回MySqlDataAdapter数据流(MySql)
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static MySqlDataAdapter ExecuteQuery4MySql(string sql, List<MySqlParameter> paraList)
        {
            using (var conn = DataBase.GetOpenConn4MySql())
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                if (paraList != null && paraList.Count > 0)
                {
                    cmd.Parameters.AddRange(paraList.ToArray());
                }
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                return da;
            }
        }

        /// <summary>
        /// 执行存储过程，返回MySqlDataAdapter数据流(MySql)
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static MySqlDataAdapter ExecuteSP4MySql(string storedProcedure, List<MySqlParameter> paraList)
        {
            using (var conn = DataBase.GetOpenConn4MySql())
            {
                MySqlCommand cmd = new MySqlCommand(storedProcedure, conn);
                cmd.CommandType = CommandType.StoredProcedure;  //指明是存储过程
                if (paraList != null && paraList.Count > 0)
                {
                    cmd.Parameters.AddRange(paraList.ToArray());
                }
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                return da;
            }
        }

        /// <summary>
        /// 根据sql执行非查询，返回int类型
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="paraList">参数列表</param>
        /// <returns>非0则执行成功</returns>
        public static int ExecuteNonQuery4MySql(string sql, List<MySqlParameter> paraList)
        {
            using (var conn = DataBase.GetOpenConn4MySql())
            {
                if (!string.IsNullOrEmpty(sql))
                {
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    if (paraList != null && paraList.Count > 0)
                    {
                        cmd.Parameters.AddRange(paraList.ToArray());
                    }
                    var result = cmd.ExecuteNonQuery();
                    return result;
                }
                else
                    return -1;
            }
        }

        /// <summary>
        /// 查询所有的(MySql)
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet GetList4MySql(string tableName)
        {
            DataSet ds = new DataSet();
            var pagesql = new StringBuilder();
            pagesql.AppendFormat("Select * From {0} Where 1=1;", tableName);  //mysql中不能有中括号
            pagesql.AppendFormat("SELECT COUNT(1) AS TotalCount FROM {0} WHERE 1=1;", tableName);
            MySqlDataAdapter mda = DataBase.ExecuteQuery4MySql(pagesql.ToString(), null);
            if (mda != null)
            {
                mda.Fill(ds);
            }
            return ds;
        }

        /// <summary>
        /// 分页查询（MySQL）
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">起始页(从1开始)</param>
        /// <param name="pageSize">页数大小</param>
        /// <param name="conditions">条件</param>
        /// <returns></returns>
        public DataSet GetPage4MySql(string tableName, int pageIndex, int pageSize, string conditions)
        {
            DataSet ds = new DataSet();
            string pagesql = string.Format("Select * From {0} Where 1=1 ", tableName);  //查询数据的
            string totalsql = string.Format("Select Count(*) As TotalCount From {0} Where 1=1 ", tableName);  //查询
            if (!string.IsNullOrEmpty(conditions))
            {
                pagesql += conditions;
                totalsql += conditions + ";";
            }
            pagesql += " Limit @PageIndex,@PageSize;";
            string sql = pagesql + totalsql;

            //第一页（0,10），第二页（1,10），第三页（2,10）...
            pageIndex -= 1;  //当前页减一（第一页：0，第二页：1，第三页：2，...）
            if (pageIndex < 1)
                pageIndex = 0;  //从第一页开始，为0
            else
                pageIndex *= 10;

            MySqlParameter[] para = new MySqlParameter[]
            {
                new MySqlParameter("@PageIndex", pageIndex),
                new MySqlParameter("@PageSize", pageSize)
            };
            MySqlDataAdapter mda = DataBase.ExecuteQuery4MySql(sql.ToString(), para.ToList());
            if (mda != null)
            {
                mda.Fill(ds);
            }
            return ds;
        }

        /// <summary>
        /// 根据ID查询一个(MySql)
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet GetOnlyById4MySql(string Id, string tableName)
        {
            string sql = string.Format("SELECT * FROM {0} WHERE 1=1 AND Id = @{1}", tableName, Id);
            MySqlParameter[] para = new MySqlParameter[]
            {
                new MySqlParameter("@Id", Id)
            };
            DataSet ds = new DataSet();
            MySqlDataAdapter da = DataBase.ExecuteQuery4MySql(sql, para.ToList());
            if (da != null)
            {
                da.Fill(ds);
            }
            return ds;
        }

        #endregion

        #region MySql泛型

        ///// <summary>
        ///// 打开数据库连接(MySql)
        ///// </summary>
        ///// <returns></returns>
        //public static MySqlConnection GetOpenConn4MySql()
        //{
        //    var conn = new MySqlConnection(BlogStr);
        //    conn.Open();
        //    return conn;
        //}

        ///// <summary>
        ///// 根据sql执行，返回MySqlDataReader数据流(MySql)
        ///// </summary>
        ///// <param name="sql"></param>
        ///// <returns></returns>
        //public static MySqlDataAdapter ExecuteQuery4MySql(string sql, MySqlConnection conn, MySqlParameter[] para)
        //{
        //    MySqlCommand cmd = new MySqlCommand(sql, conn);
        //    cmd.Parameters.AddRange(para);
        //    MySqlDataAdapter da = new MySqlDataAdapter();
        //    da.SelectCommand = cmd;
        //    return da;
        //}

        /// <summary>
        /// 查询所有的(MySql)
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet GetList4MySql<T>(T model)
        {
            DataSet ds = new DataSet();
            var pagesql = new StringBuilder();
            pagesql.AppendFormat("Select * From {0} Where 1=1;", model.GetType().Name.ToLower());  //mysql中不能有中括号
            pagesql.AppendFormat("SELECT COUNT(1) AS TotalCount FROM {0} WHERE 1=1 Order By CreateTime;", model.GetType().Name.ToLower());
            MySqlDataAdapter mda = DataBase.ExecuteQuery4MySql(pagesql.ToString(), null);
            if (mda != null)
            {
                mda.Fill(ds);
            }
            return ds;
        }

        /// <summary>
        /// 分页查询（MySQL）
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">起始页(从1开始)</param>
        /// <param name="pageSize">页数大小</param>
        /// <param name="conditions">条件</param>
        /// <returns></returns>
        public DataSet GetPage4MySql<T>(T model, int pageIndex, int pageSize)
        {
            DataSet ds = new DataSet();
            string pagesql = string.Format("Select * From {0} Where 1=1 ", model.GetType().Name.ToLower());  //查询数据的
            string totalsql = string.Format("Select Count(*) As TotalCount From {0} Where 1=1 ", model.GetType().Name.ToLower());  //查询
            pagesql += " Order By CreateTime Desc Limit @PageIndex,@PageSize;";
            string sql = pagesql + totalsql;

            //分页：[0,10],[10,19],[20,29]...
            if (pageIndex <= 1)
            {
                pageIndex = 0;
            }
            else
            {
                pageIndex = (pageIndex - 1) * pageSize;
            }

            MySqlParameter[] para = new MySqlParameter[]
                {
                    new MySqlParameter("@PageIndex", pageIndex),
                    new MySqlParameter("@PageSize", pageSize)
                };

            //string info = string.Format("DataBase页发生错误，错误处：GetPage4MySql<T>，sql:{0}。{1},{2}", sql.ToString(), pageIndex, pageSize);
            //log.Info(info);

            MySqlDataAdapter mda = DataBase.ExecuteQuery4MySql(sql.ToString(), para.ToList());
            if (mda != null)
            {
                mda.Fill(ds);
            }

            return ds;
        }

        /// <summary>
        /// 根据ID查询一个(MySql)
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet GetOnlyById4MySql<T>(string Id, T model)
        {
            string sql = string.Format("SELECT * FROM {0} WHERE 1=1 AND Id = @Id;UPDATE {0} SET ViewCount=ViewCount+1 WHERE Id = @Id;", model.GetType().Name.ToLower());
            MySqlParameter[] para = new MySqlParameter[]
            {
                new MySqlParameter("@Id", Id)
            };
            DataSet ds = new DataSet();
            MySqlDataAdapter da = DataBase.ExecuteQuery4MySql(sql, para.ToList());
            if (da != null)
            {
                da.Fill(ds);
            }

            return ds;
        }

        /// <summary>
        /// 根据关键字查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyWord"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataSet GetOnlyByKey4MySql<T>(string keyWord, T model)
        {
            string sql = string.Format("SELECT * FROM {0} WHERE 1=1 AND {1} Like @{1}", model.GetType().Name.ToLower(), keyWord);
            MySqlParameter[] para = new MySqlParameter[]
            {
                new MySqlParameter("@"+keyWord, keyWord)
            };
            DataSet ds = new DataSet();
            MySqlDataAdapter da = DataBase.ExecuteQuery4MySql(sql, para.ToList());
            if (da != null)
            {
                da.Fill(ds);
            }
            return ds;
        }

        #endregion


        #region 查询T模型

        ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 根据ID查询单个模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetOneById<T>(string id) where T : new()
        {
            T model = new T();
            string sql = string.Format("SELECT * FROM {0} WHERE 1=1 AND Id = @Id", model.GetType().Name.ToLower());
            MySqlParameter[] para = new MySqlParameter[]
            {
                new MySqlParameter("@Id", id)
            };
            DataSet ds = new DataSet();
            MySqlDataAdapter da = DataBase.ExecuteQuery4MySql(sql, para.ToList());
            if (da != null)
            {
                da.Fill(ds);
            }
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    foreach (var item in model.GetType().GetProperties())
                    {
                        if (row.Table.Columns.Contains(item.Name))
                        {
                            if (row[item.Name] != DBNull.Value)
                            {
                                item.SetValue(model, Convert.ChangeType(row[item.Name], item.PropertyType), null);
                            }
                        }
                    }
                }
                return model;
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 执行sql查询List整型数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns>有数据返回List<int>，否则返回null</returns>
        public List<int> QueryInt(string sql, List<MySqlParameter> paraList)
        {
            List<int> intList = new List<int>();
            MySqlDataAdapter da = DataBase.ExecuteQuery4MySql(sql, paraList);
            DataSet ds = new DataSet();
            if (da != null)
            {
                da.Fill(ds);
            }
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                int model = new int();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    model = Convert.ToInt32(row[0]);
                    intList.Add(model);
                }
            }
            return intList;
        }

        /// <summary>
        /// 执行sql查询List字符串数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns>有数据返回List<string>，否则返回null</returns>
        public List<string> QueryString(string sql, List<MySqlParameter> paraList)
        {
            List<string> stringList = new List<string>();
            MySqlDataAdapter da = DataBase.ExecuteQuery4MySql(sql, paraList);
            DataSet ds = new DataSet();
            if (da != null)
            {
                da.Fill(ds);
            }
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                string model = string.Empty;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    model = row[0] == null ? "" : row[0].ToString();
                    stringList.Add(model);
                }
            }
            return stringList;
        }

        /// <summary>
        /// 一般用于返回单列整数类型和字符串类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">没有参数替换时可以为null</param>
        /// <returns>List:int 或者List:string</returns>
        public List<T> QueryObject<T>(string sql, List<MySqlParameter> paraList)
        {
            List<T> objectList = new List<T>();
            MySqlDataAdapter da = DataBase.ExecuteQuery4MySql(sql, paraList);
            DataSet ds = new DataSet();
            if (da != null)
            {
                da.Fill(ds);
            }
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                const string _String = "Temp"; //由于string默认为null无法获得类型
                var property = default(T) == null ? _String.GetType() : default(T).GetType();
                if (property == typeof(Int32))  //Int32
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        int model = row[0] == null ? 0 : Convert.ToInt32(row[0]);
                        objectList.Add((T)Convert.ChangeType(model, typeof(T)));
                    }
                }
                else
                {
                    foreach (DataRow row in ds.Tables[0].Rows)//string类型
                    {
                        string model = row[0] == null ? "" : row[0].ToString();
                        objectList.Add((T)Convert.ChangeType(model, typeof(T)));
                    }
                }
            }
            return objectList;
        }

        /// <summary>
        /// 执行sql查询单个模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns>有数据返回model，否则返回default(T)</returns>
        public T QueryModel<T>(string sql, List<MySqlParameter> paraList) where T : new()
        {
            T model = new T();
            MySqlDataAdapter da = DataBase.ExecuteQuery4MySql(sql, paraList);
            DataSet ds = new DataSet();
            if (da != null)
            {
                da.Fill(ds);
            }
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                var properties = model.GetType().GetProperties();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    foreach (var item in properties)
                    {
                        if (row.Table.Columns.Contains(item.Name))
                        {
                            if (DBNull.Value != row[item.Name])
                            {
                                item.SetValue(model, Convert.ChangeType(row[item.Name], item.PropertyType), null);
                            }
                        }
                    }
                }

            }
            return model;
        }

        /// <summary>
        /// 执行sql查询List集合(用于模型)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns>有数据返回list，否则返回null</returns>
        public List<T> QueryList<T>(string sql, List<MySqlParameter> paraList) where T : new()
        {
            List<T> list = new List<T>();
            MySqlDataAdapter da = DataBase.ExecuteQuery4MySql(sql, paraList);
            DataSet ds = new DataSet();
            if (da != null)
            {
                da.Fill(ds);
            }
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    T model = new T();
                    var properties = model.GetType().GetProperties();
                    foreach (var item in properties)
                    {
                        if (row.Table.Columns.Contains(item.Name))
                        {
                            if (DBNull.Value != row[item.Name])
                            {
                                item.SetValue(model, Convert.ChangeType(row[item.Name], item.PropertyType), null);//非索引的为null
                            }
                        }
                    }
                    list.Add(model);
                }
                return list;
            }
            return list;
        }

        /// <summary>
        /// 条件查询总数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paraList"></param>
        /// <returns></returns>
        public int QueryTotal(string sql, List<MySqlParameter> paraList)
        {
            int total = 0;
            MySqlDataAdapter da = DataBase.ExecuteQuery4MySql(sql, paraList);
            DataSet ds = new DataSet();
            if (da != null)
            {
                da.Fill(ds);
            }
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                total = Convert.ToInt32(ds.Tables[0].Rows[0][0] ?? 0);
            }
            return total;
        }

        #endregion

        #region 写入T模型

        /// <summary>
        /// 单个模型写入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">实体模型(必须)</param>
        /// <returns>返回布尔：true(成功)/false(失败)</returns>
        public bool InsertModel<T>(T model) where T : new()
        {
            StringBuilder sql = new StringBuilder();
            List<MySqlParameter> paraList = new List<MySqlParameter>(); //参数列表
            var tableName = new T().GetType().Name.ToLower();  //获取模型名称(表名)
            var sbproperties = new StringBuilder();
            var properties = model.GetType().GetProperties();  //模型属性
            foreach (var item in properties)
            {
                sbproperties.Append(item.Name + ",");
            }

            sql.Append("Insert Into " + tableName + " (" + sbproperties.ToString().TrimEnd(',') + ")  Values ");
            sbproperties.Clear();
            foreach (var item in properties)
            {
                sbproperties.Append("@" + item.Name + ",");
                paraList.Add(new MySqlParameter(string.Format("@{0}", item.Name), model.GetType().GetProperty(item.Name).GetValue(model, null)));  //填充属性参数
            }
            sql.Append("(" + sbproperties.ToString().TrimEnd(',') + ")");

            if (DataBase.ExecuteNonQuery4MySql(sql.ToString(), paraList) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 批量写入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool InsertList<T>(List<T> list) where T : new()
        {
            int i = 0;
            StringBuilder sql = new StringBuilder();
            List<MySqlParameter> paraList = new List<MySqlParameter>(); //参数列表
            var tableName = new T().GetType().Name.ToLower();  //获取模型名称(表名)
            var sbproperties1 = new StringBuilder();  //模型属性

            foreach (var item in new T().GetType().GetProperties())
            {
                sbproperties1.Append(item.Name + ",");
            }
            sbproperties1.Remove(sbproperties1.Length - 1, 1);
            sql.Append("Insert Into " + tableName + " (" + sbproperties1 + ")  Values ");

            //测试填充数据时间
            //Stopwatch timer1 = new Stopwatch();
            //timer1.Start();

            foreach (var model in list)
            {
                var sbproperties2 = new StringBuilder();  //参数属性
                foreach (var item in model.GetType().GetProperties())
                {
                    sbproperties2.Append("@" + item.Name + i + ",");
                    paraList.Add(new MySqlParameter(string.Format("@{0}" + i, item.Name), model.GetType().GetProperty(item.Name).GetValue(model, null)));
                }

                sbproperties2.Remove(sbproperties2.Length - 1, 1);  //去除最后一个逗号
                sql.Append(" (" + sbproperties2 + "),");

                i++;  //标示参数的，防止重名
            }
            //timer1.Stop();
            //string info = string.Format("填充1万条数据的时间: {0}", timer1.Elapsed);
            //log.Info(info);

            //测试写入时间
            //Stopwatch timer2 = new Stopwatch();
            //timer2.Start();
            if (DataBase.ExecuteNonQuery4MySql(sql.ToString().TrimEnd(','), paraList) > 0)
            {
                //timer2.Stop();
                //string info2 = string.Format("写入1万条数据的时间: {0}", timer2.Elapsed);
                //log.Info(info2);
                return true;
            }

            #region 事务写入
            //sql.Append("Start Transaction;");
            ////测试填充数据时间
            //Stopwatch timer1 = new Stopwatch();
            //timer1.Start();
            //foreach (var model in list)
            //{
            //    var sbproperties2 = new StringBuilder();  //参数属性
            //    foreach (var item in model.GetType().GetProperties())
            //    {
            //        sbproperties1.Append(item + ",");
            //        sbproperties2.Append("@" + item.Name + i + ",");
            //        paraList.Add(new MySqlParameter(string.Format("@{0}" + i, item.Name), model.GetType().GetProperty(item.Name).GetValue(model, null)));
            //    }
            //    sbproperties1.Remove(sbproperties1.Length - 1, 1);  //去除最后一个逗号
            //    sbproperties2.Remove(sbproperties2.Length - 1, 1);
            //    sql.Append("Insert Into " + tableName + " Values (" + sbproperties2 + ");");
            //    i++;  //标示参数的，防止重名
            //}
            //sql.Append("Commit;");
            //timer1.Stop();
            //string info = string.Format("填充1万条数据的时间: {0}", timer1.Elapsed);
            //log.Info(info);

            ////测试写入时间
            //Stopwatch timer2 = new Stopwatch();
            //timer2.Start();  //开始计时
            //if (DataBase.ExecuteNonQuery4MySql(sql.ToString(), DataBase.GetOpenConn4MySql(), paraList) > 0)
            //{
            //    timer2.Stop();  //停止计时
            //    string info2 = string.Format("写入1万条数据的时间: {0}", timer2.Elapsed);
            //    log.Info(info2);
            //    return true;
            //}
            #endregion
            return false;

        }

        #endregion

        #region 更新T模型

        /// <summary>
        /// 更新模型(必须含有ID主键)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateModel<T>(T model) where T : new()
        {
            StringBuilder sql = new StringBuilder();
            List<MySqlParameter> paraList = new List<MySqlParameter>(); //参数列表
            var tableName = new T().GetType().Name.ToLower();  //获取模型名称(表名)
            var sbproperties = new StringBuilder();
            var properties = model.GetType().GetProperties();  //模型属性
            foreach (var item in properties)
            {
                if (item.Name != "Id")
                    sbproperties.Append(item.Name + "=@" + item.Name + ",");
            }

            sql.Append("Update " + tableName + " Set " + sbproperties.ToString().TrimEnd(',') + " Where Id=@Id");
            sbproperties.Clear();
            foreach (var item in properties)
            {
                paraList.Add(new MySqlParameter(string.Format("@{0}", item.Name), model.GetType().GetProperty(item.Name).GetValue(model, null)));  //填充属性参数
            }

            if (DataBase.ExecuteNonQuery4MySql(sql.ToString(), paraList) > 0)
            {
                return 1;
            }
            return 0;
        }

        #endregion

        #region 存储过程

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <typeparam name="T">模型</typeparam>
        /// <param name="storedProcedure">存储过程名称</param>
        /// <param name="paraList">参数</param>
        /// <returns></returns>
        public List<T> QueryStoredProcedure<T>(string storedProcedure, List<MySqlParameter> paraList) where T : new()
        {
            List<T> list = new List<T>();
            MySqlDataAdapter dr = DataBase.ExecuteSP4MySql(storedProcedure, paraList);
            DataTable dt = new DataTable();
            dr.Fill(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                T model = new T();
                var properties = model.GetType().GetProperties();
                foreach (DataRow row in dt.Rows)
                {
                    foreach (PropertyInfo property in properties)
                    {
                        if (row.Table.Columns.Contains(property.Name))
                        {
                            if (row[property.Name] != DBNull.Value)
                            {
                                property.SetValue(model, Convert.ChangeType(row[property.Name], property.PropertyType));
                            }
                        }
                    }
                    list.Add(model);
                }
            }
            return list;
        }

        #endregion
    }
}
