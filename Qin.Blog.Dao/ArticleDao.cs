using log4net;
using MySql.Data.MySqlClient;
using Qin.Blog.Data;
using Qin.Blog.Entity;
using Qin.Blog.Entity.DBModel;
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

    public class ArticleDao : IArticleDao
    {
        private DateTime minTime = Convert.ToDateTime("1900-01-01");

        ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        DataBase _DataBase = new DataBase();
        private string[] _1_WebFront = { "Html/CSS", "JavaScript" }; //对应导航栏的【前端】
        private string[] _2_BackStage = { "ASP.NET", "Sql" };  //2对应【后台】
        private string _3_Essay = "随笔";       //3对应【随笔】


        /// <summary>
        /// 通过ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns>没有数据的返回null</returns>
        public Article GetById(string id)
        {
            Article model = new Article();
            DataTable dt = _DataBase.GetOnlyById4MySql<Article>(id, model).Tables[0];
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
        /// 查看文章详细内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ArticleDBModel GetArticleById(string id)
        {
            var sql = @"Select a.*,b.NickName,b.Sex,b.UserName From article a LEFT JOIN user b ON a.UserId = b.Id Where 1=1 And a.Id = @Id;UPDATE article SET ViewCount=ViewCount+1 WHERE Id = @Id;";
            MySqlParameter[] para = new MySqlParameter[] { new MySqlParameter("@Id", id) };
            return _DataBase.QueryModel<ArticleDBModel>(sql, para.ToList());
        }

        public bool Exsit(string keyWord)
        {
            throw new NotImplementedException();
        }

        public int Count(string keyWord)
        {
            int a = 1, b = 0;
            int c = a / b;
            return 1;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert(Article model)
        {
            if (model != null)
            {
                string sql = FillData.AppendSqlToInsert(model);
                MySqlCommand cmd = new MySqlCommand(sql, DataBase.GetOpenConn4MySql());
                MySqlParameter[] para = FillData.AppendParas<Article>(model);
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
        /// 获取所有文章列表
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<Article> GetList(out int total)
        {
            DataSet ds = _DataBase.GetList4MySql<Article>(new Article());
            DataTable dt = ds.Tables[0];
            total = Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"]);
            List<Article> list = FillData.FillDataToList(new Article(), dt);
            return list;
        }

        /// <summary>
        /// 分页查询(无条件)
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <param name="total"></param>
        /// <returns>没有数据的返回null</returns>
        public List<Article> Pages(int pageIndex, int pageSize, string userId, out int total)
        {
            if (pageIndex <= 1)
                pageIndex = 1;
            total = 0;
            var sql = @"SELECT
	                        a.*, b.NickName,
	                        b.Sex,
	                        b.UserName
                        FROM
	                        article a
                        LEFT JOIN USER b ON a.UserId = b.Id
                        WHERE
	                        UserId = @UserId
                        ORDER BY
	                        IsTop DESC,
	                        CreateTime DESC
                        LIMIT @PageIndex,
                         @PageSize;";
            MySqlParameter[] para = new MySqlParameter[]
            {

                new MySqlParameter("@PageIndex", --pageIndex*10),
                new MySqlParameter("@PageSize", pageSize)
            };
            var list = _DataBase.QueryList<Article>(sql, para.ToList());

            var sql_total = @"SELECT
	                            a.*, b.NickName,
	                            b.Sex,
	                            b.UserName
                            FROM
	                            article a
                            LEFT JOIN USER b ON a.UserId = b.Id
                            WHERE
	                            UserId = @UserId";

            DataSet ds = _DataBase.GetPage4MySql(new Article(), pageIndex, pageSize);
            DataTable pages = ds.Tables[0];
            total = _DataBase.QueryTotal(sql_total, para.ToList());
            return list;
        }

        /// <summary>
        /// 条件查询分类文章
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<ArticleDBModel> CategoryPage(int pageIndex, int pageSize, string conditions, out int total)
        {
            total = 0;
            var sql = "Select a.*, b.NickName,b.Sex,b.UserName From article a LEFT JOIN user b ON a.UserId = b.Id ";
            var sql_total = new StringBuilder().Append("Select Count(*) From article a LEFT JOIN user b ON a.UserId = b.Id ");
            switch (conditions)
            {
                case "1":
                    sql += string.Format(" Where TypeId In (Select Id From articletype Where TypeName = '{0}' Or TypeName = '{1}')", _1_WebFront[0], _1_WebFront[1]);
                    sql_total.Append(string.Format(" Where TypeId In (Select Id From articletype Where TypeName = '{0}' Or TypeName = '{1}')", _1_WebFront[0], _1_WebFront[1]));
                    break;
                case "2":
                    sql += string.Format(" Where TypeId In (Select Id From articletype Where TypeName = '{0}' Or TypeName = '{1}')", _2_BackStage[0], _2_BackStage[1]);
                    sql_total.Append(string.Format(" Where TypeId In (Select Id From articletype Where TypeName = '{0}' Or TypeName = '{1}')", _2_BackStage[0], _2_BackStage[1]));
                    break;
                case "3":
                    sql += string.Format(" Where TypeId In (Select Id From articletype Where TypeName = '{0}' )", _3_Essay);
                    sql_total.Append(string.Format(" Where TypeId In (Select Id From articletype Where TypeName = '{0}' )", _3_Essay));
                    break;
                default:
                    break;
            }
            sql += string.Format(" Order By CreateTime Desc Limit @PageIndex,@PageSize");

            List<MySqlParameter> paraslist = new List<MySqlParameter>()
            {
                new MySqlParameter("@PageIndex", --pageIndex * pageSize),
                new MySqlParameter("@PageSize", pageSize)
            };

            var list = _DataBase.QueryList<ArticleDBModel>(sql, paraslist);
            total = _DataBase.QueryTotal(sql_total.ToString(), null);  //查询总数
            if (list != null && list.Count > 0)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 批量写入
        /// </summary>
        /// <param name="list">数据模型列表</param>
        /// <returns></returns>
        public bool InsertList(List<Article> list)
        {
            bool result = _DataBase.InsertList<Article>(list);
            return result;
        }

        /// <summary>
        /// 查询用户的文章列表
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<Article> GetUserArticles(string userId, out int total)
        {
            string sql = @"Select * From article Where UserId = @UserId";
            string sql_total = @"Select Count(*) From article Where UserId = @UserId";
            MySqlParameter[] para = new MySqlParameter[]
            {
                new MySqlParameter("@UserId", userId),
            };
            var list = _DataBase.QueryList<Article>(sql, para.ToList());
            var totallist = _DataBase.QueryTotal(sql_total, para.ToList());
            total = totallist;
            return list;
        }

        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Article model)
        {
            return _DataBase.UpdateModel<Article>(model);
        }



        /// <summary>
        /// 标签分类
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<ArticleDBModel> TagPages(int pageIndex, int pageSize, string conditions, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
