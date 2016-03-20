using Qin.Blog.Entity;
using Qin.Blog.Entity.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.IService
{
    public interface IArticleService : IServiceBase<Article>
    {
        List<ArticleDBModel> CategoryPage(int pageIndex, int pageSize, string conditions, out int total);

        List<Article> GetUserArticles(string userId, out int total);

        ArticleDBModel GetArticleById(string id);

        /// <summary>
        /// 标签分类
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<ArticleDBModel> TagPages(string tag ,int pageIndex, int pageSize, out int total);
    }
}
