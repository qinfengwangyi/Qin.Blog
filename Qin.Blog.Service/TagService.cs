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
    public class TagService : ITagService
    {
        ITagDao _ITagDao = new TagDao();

        public Tag GetById(string id)
        {
            return _ITagDao.GetById(id);
        }

        public bool Insert(Tag entity)
        {
            return _ITagDao.Insert(entity);
        }

        public bool Exsit(string keyWord)
        {
            return _ITagDao.Exsit(keyWord);
        }

        public int Count(string keyWord)
        {
            return _ITagDao.Count(keyWord);
        }

        public List<Tag> GetList(out int total)
        {
            return _ITagDao.GetList(out total);
        }

        public List<Tag> Pages(int pageIndex, int pageSize, string conditions, out int total)
        {
            return _ITagDao.Pages(pageIndex, pageSize, conditions, out total);
        }


        public int Update(Tag model)
        {
            throw new NotImplementedException();
        }
    }
}
