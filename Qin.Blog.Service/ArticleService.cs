using Qin.Blog.Dao;
using Qin.Blog.Entity;
using Qin.Blog.Entity.DBModel;
using Qin.Blog.IDao;
using Qin.Blog.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Service
{
    public class ArticleService : IArticleService
    {
        IArticleDao _IArticleDao = new ArticleDao();

        public ArticleDBModel GetArticleById(string id)
        {
            return _IArticleDao.GetArticleById(id);
        }

        public List<Article> GetList(out int total)
        {
            return _IArticleDao.GetList(out total);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(Article model)
        {
            return _IArticleDao.Insert(model);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<ArticleDBModel> CategoryPage(int pageIndex, int pageSize, string conditions, out int total)
        {
            return _IArticleDao.CategoryPage(pageIndex, pageSize, conditions, out total);
        }



        public bool Exsit(string keyWord)
        {
            throw new NotImplementedException();
        }

        public int Count(string keyWord)
        {
            return _IArticleDao.Count(keyWord);
        }


        public List<Article> GetUserArticles(string userId, out int total)
        {
            return _IArticleDao.GetUserArticles(userId, out total);
        }


        public int Update(Article model)
        {
            return _IArticleDao.Update(model);
        }


        List<Article> IServiceBase<Article>.Pages(int pageIndex, int pageSize, string conditions, out int total)
        {
            throw new NotImplementedException();
        }

        Article IServiceBase<Article>.GetById(string id)
        {
            throw new NotImplementedException();
        }


        public List<ArticleDBModel> TagPages(int pageIndex, int pageSize, string conditions, out int total)
        {
            return _IArticleDao.TagPages(pageIndex, pageSize, conditions, out total);
        }
    }
}
