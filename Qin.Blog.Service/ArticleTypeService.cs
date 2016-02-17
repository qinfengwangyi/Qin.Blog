using Qin.Blog.Dao;
using Qin.Blog.Entity;
using Qin.Blog.IDao;
using Qin.Blog.ISerivce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Service
{
    public class ArticleTypeService : IArticleTypeService
    {

        IArticleTypeDao _IArticleTypeDao = new ArticleTypeDao();


        public ArticleType GetById(string id)
        {
            return _IArticleTypeDao.GetById(id);
        }


        public List<ArticleType> GetList(out int total)
        {
            return _IArticleTypeDao.GetList(out total);
        }


        public bool Insert(ArticleType model)
        {
            return _IArticleTypeDao.Insert(model);
        }


        public List<ArticleType> Pages(int pageIndex, int pageSize, string conditions, out int total)
        {
            return _IArticleTypeDao.Pages(pageIndex, pageSize, conditions, out total);
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
