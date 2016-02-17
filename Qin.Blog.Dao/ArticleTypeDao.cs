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
    public class ArticleTypeDao : IArticleTypeDao
    {
        private static readonly string tableName = @"articletype";
        private DateTime minTime = Convert.ToDateTime("1970-01-01");

        ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        DataBase dataBase = new DataBase();


        /// <summary>
        /// 通过ID获取文章类型
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ArticleType GetArticleTypeById(int Id)
        {
            ArticleType model = new ArticleType();
            DataTable dt = dataBase.GetOnlyById4MySql(Id.ToString(), tableName).Tables[0];
            if (dt != null)
            {
                return FillData.FillDataToEntity(model, dt);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加文章分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert(ArticleType model)
        {
            if (model != null)
            {
                string sql = FillData.AppendSqlToInsert(model);
                MySqlCommand cmd = new MySqlCommand(sql, DataBase.GetOpenConn4MySql());
                MySqlParameter[] para = FillData.AppendParas(model);
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

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<ArticleType> Pages(int pageIndex, int pageSize, string conditions, out int total)
        {
            List<ArticleType> list = new List<ArticleType>();
            DataSet ds = dataBase.GetPage(tableName, pageIndex, pageSize);
            DataTable pages = ds.Tables[0];
            total = Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"]);
            if (pages != null && pages.Rows.Count > 0)
            {
                return FillData.FillDataToList(new ArticleType(), pages);
            }
            return list;
        }


        public ArticleType GetById(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询所有的文章类型
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<ArticleType> GetList(out int total)
        {
            List<ArticleType> list = new List<ArticleType>();
            DataSet ds = dataBase.GetList4MySql(new ArticleType().GetType().Name.ToLower());
            DataTable pages = ds.Tables[0];
            total = Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"]);
            if (pages != null && pages.Rows.Count > 0)
            {
                return FillData.FillDataToList(new ArticleType(), pages);
            }
            else
            {
                return null;
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


        public int Update(ArticleType model)
        {
            throw new NotImplementedException();
        }
    }
}
