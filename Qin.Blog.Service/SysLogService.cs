using Qin.Blog.Dao;
using Qin.Blog.Entity;
using Qin.Blog.IDao;
using Qin.Blog.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Service
{
    public class SysLogService : ISysLogService
    {
        ISysLogDao _ISysLogDao = new SysLogDao();

        public SysLog GetById(string id)
        {
            return _ISysLogDao.GetById(id);
        }

        public bool Insert(SysLog entity)
        {
            return _ISysLogDao.Insert(entity);
        }

        public bool Exsit(string keyWord)
        {
            return _ISysLogDao.Exsit(keyWord);
        }

        public int Count(string keyWord)
        {
            return _ISysLogDao.Count(keyWord);
        }

        public List<SysLog> GetList(out int total)
        {
            return _ISysLogDao.GetList(out total);
        }

        public List<SysLog> Pages(int pageIndex, int pageSize, string conditions, out int total)
        {
            return _ISysLogDao.Pages(pageIndex, pageSize, conditions, out total);
        }


        public int Update(SysLog model)
        {
            throw new NotImplementedException();
        }
    }
}
