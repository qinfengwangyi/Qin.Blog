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
    public class LeaveMessageService : ILeaveMessageService
    {
        ILeaveMessageDao _ILeaveMessageDao = new LeaveMessageDao();

        public LeaveMessage GetById(string id)
        {
            return _ILeaveMessageDao.GetById(id);
        }

        public bool Insert(LeaveMessage model)
        {
            return _ILeaveMessageDao.Insert(model);
        }

        public int Update(LeaveMessage model)
        {
            return _ILeaveMessageDao.Update(model);
        }

        public bool Exsit(string keyWord)
        {
            return _ILeaveMessageDao.Exsit(keyWord);
        }

        public int Count(string keyWord)
        {
            return _ILeaveMessageDao.Count(keyWord);
        }

        public List<LeaveMessage> GetList(out int total)
        {
            return _ILeaveMessageDao.GetList(out total);
        }

        public List<LeaveMessage> Pages(int pageIndex, int pageSize, string conditions, out int total)
        {
            return _ILeaveMessageDao.Pages(pageIndex, pageSize, conditions, out total);
        }
    }
}
