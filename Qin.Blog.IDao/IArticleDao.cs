using Qin.Blog.Entity;
using Qin.Blog.Entity.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.IDao
{
    public interface IArticleDao : IDaoBase<Article>
    {
        /// <summary>
        /// 批量写入
        /// </summary>
        /// <param name="list">数据模型列表</param>
        /// <returns></returns>
        bool InsertList(List<Article> list);



        List<ArticleDBModel> CategoryPage(int pageIndex, int pageSize, string conditions, out int total);

        /// <summary>
        /// 获取用户的文章
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<Article> GetUserArticles(string userId, out int total);


        ArticleDBModel GetArticleById(string id);



        List<ArticleDBModel> TagPages(int pageIndex, int pageSize, string conditions, out int total);

    }
}
