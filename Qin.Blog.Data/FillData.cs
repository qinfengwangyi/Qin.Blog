using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Data
{
    /// <summary>
    /// 填充数据
    /// </summary>
    public class FillData
    {
        private readonly DateTime minTime = Convert.ToDateTime("1900-1-1");  //空时间和时间出错标示

        #region 填充sql查询的数据

        /// <summary>
        /// 获取实体的字段和字段类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<TypeInfo> GetEntityInfo<T>(T entity)
        {
            List<TypeInfo> properties = new List<TypeInfo>();
            Type _Type = entity.GetType();
            var entityproperties = entity.GetType().GetProperties();
            foreach (var item in entityproperties)
            {
                var key_value = new TypeInfo
                {
                    Type = item.PropertyType,
                    Field = item.Name
                };
                properties.Add(key_value);
            }
            return properties;
        }

        /// <summary>
        /// 获取实体的字段和字段类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetModelInfo<T>(T entity)
        {
            var entityproperties = entity.GetType().GetProperties();
            return entityproperties;
        }


        /// <summary>
        /// 填充单个模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static T FillDataToEntity<T>(T model, DataTable dt)
        {
            PropertyInfo[] typeInfos = GetModelInfo<T>(model);
            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in typeInfos)
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

        /// <summary>
        /// 取DataRow填充实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="dw"></param>
        /// <returns></returns>
        public static T FillDataToEntity<T>(T model, DataRow dw)
        {
            PropertyInfo[] typeInfos = GetModelInfo<T>(model);
            foreach (var item in typeInfos)
            {
                if (dw.Table.Columns.Contains(item.Name))
                {
                    if (dw[item.Name] != DBNull.Value)
                    {
                        item.SetValue(model, Convert.ChangeType(dw[item.Name], item.PropertyType));
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 填充List
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="model">模型</param>
        /// <param name="dt">DataTable数据</param>
        /// <returns></returns>
        public static List<T> FillDataToList<T>(T model, DataTable dt) where T : new()
        {
            var list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                var modelnew = new T();
                foreach (var item in model.GetType().GetProperties())
                {
                    if (row.Table.Columns.Contains(item.Name))
                    {
                        if (DBNull.Value != row[item.Name])
                        {
                            item.SetValue(modelnew, Convert.ChangeType(row[item.Name], item.PropertyType), null);
                        }
                    }
                }
                list.Add(modelnew);
            }
            return list;
        }



        /// <summary>
        /// 百度搜到的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static T GetModel<T>(object model, DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in model.GetType().GetProperties())
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
            return (T)model;
        }

        #endregion

        #region 填充字段

        /// <summary>
        /// 填充Insert语句字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string AppendSqlToInsert<T>(T model)
        {
            StringBuilder sql = new StringBuilder();
            var sbproperties1 = new StringBuilder();
            var sbproperties2 = new StringBuilder();
            string tableName = model.GetType().Name.ToLower();  //获取实体类名
            foreach (var item in model.GetType().GetProperties())
            {
                sbproperties1.Append(item.Name + ",");
                sbproperties2.Append("@" + item.Name + ",");
            }
            //去除最后一个逗号
            sbproperties1.Remove(sbproperties1.Length - 1, 1);
            sbproperties2.Remove(sbproperties2.Length - 1, 1);
            sql.Append("Insert Into " + tableName + "(" + sbproperties1 + ")" + " Values(" + sbproperties2 + ");");
            return sql.ToString();
        }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">带有填充数据的模型</param>
        /// <returns></returns>
        public static MySqlParameter[] AppendParas<T>(T model)
        {
            List<MySqlParameter> paras = new List<MySqlParameter>();
            //MySqlParameter[] para = new MySqlParameter[] { };
            foreach (var item in model.GetType().GetProperties())
            {
                paras.Add(new MySqlParameter(string.Format("@{0}", item.Name), model.GetType().GetProperty(item.Name).GetValue(model, null)));
            }
            return paras.ToArray();
        }

        #endregion




        #region MySqlParameter To List


        public static List<MySqlParameter> paras = new List<MySqlParameter>();


        public static List<MySqlParameter> AddParameters(string Value)
        {
            paras.Add(new MySqlParameter("@" + Value, Value));
            return paras;
        }

        #endregion
    }



    /// <summary>
    /// 记录实体的字段和字段类型
    /// </summary>
    public class TypeInfo
    {
        private Type _type;
        private String _field;

        public Type Type { get { return _type; } set { _type = value; } }
        public String Field { get { return _field; } set { _field = value; } }
    }

}
