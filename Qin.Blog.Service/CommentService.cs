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
    public class CommentService:ICommentService
    {
        ICommentDao _ICommentDao = new CommentDao();

        public Comment GetById(string id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Comment model)
        {
            return _ICommentDao.Insert(model);
        }

        public int Update(Comment model)
        {
            throw new NotImplementedException();
        }

        public bool Exsit(string keyWord)
        {
            throw new NotImplementedException();
        }

        public int Count(string keyWord)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetList(out int total)
        {
            return _ICommentDao.GetList(out total);
        }

        public List<Comment> Pages(int pageIndex, int pageSize, string conditions, out int total)
        {
            return Pages(pageIndex, pageSize, conditions, out total);
        }


        /// <summary>
        /// 查询文章的评论
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="articleId"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CommentDBModel> CommentPages(int pageIndex, int pageSize, string articleId, out int total)
        {
            return _ICommentDao.CommentPages(pageIndex, pageSize, articleId, out total);
        }
    }
}
