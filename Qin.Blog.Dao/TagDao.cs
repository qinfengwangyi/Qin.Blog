using log4net;
using MySql.Data.MySqlClient;
using Qin.Blog.Data;
using Qin.Blog.Entity;
using Qin.Blog.IDao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Dao
{
    public class TagDao : ITagDao
    {
        DataBase dataBase = new DataBase();

        /// <summary>
        /// 查询单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns>存在返回model，否则返回null</returns>
        public Tag GetById(string id)
        {
            Tag model = new Tag();
            DataTable dt = dataBase.GetOnlyById4MySql(id, model).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return FillData.FillDataToEntity(model, dt);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(Tag entity)
        {
            if (entity != null)
            {
                string sql = FillData.AppendSqlToInsert(entity);
                MySqlCommand cmd = new MySqlCommand(sql, DataBase.GetOpenConn4MySql());
                MySqlParameter[] para = FillData.AppendParas(entity);
                cmd.Parameters.AddRange(para);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public bool Exsit(string keyWord)
        {
            throw new NotImplementedException();
        }

        public int Count(string keyWord)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取所有标签
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<Tag> GetList(out int total)
        {
            total = 0;

            DataSet ds = dataBase.GetList4MySql<Tag>(new Tag());
            DataTable dt = ds.Tables[0];
            total = Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"]);
            List<Tag> list = FillData.FillDataToList(new Tag(), dt);
            if (list != null && list.Count > 0)
            {
                return list;
            }
            return null;

        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<Tag> Pages(int pageIndex, int pageSize, string conditions, out int total)
        {
            total = 0;
            DataSet ds = dataBase.GetPage4MySql(new Tag(), pageIndex, pageSize);
            DataTable pages = ds.Tables[0];
            total = Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"]);
            if (pages != null && pages.Rows.Count > 0)
            {
                return FillData.FillDataToList<Tag>(new Tag(), pages);
            }
            else
            {
                return null;
            }
        }


        public int Update(Tag model)
        {
            throw new NotImplementedException();
        }
    }
}
