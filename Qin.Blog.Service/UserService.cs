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
    public class UserService : IUserService
    {
        IUserDao _IUserDao = new UserDao();

        public bool LogOn(string userName, string passWord, out User user, out string msg)
        {
            return _IUserDao.LogOn(userName, passWord, out user, out msg);
        }

        public User GetById(string id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(User entity)
        {
            return _IUserDao.Insert(entity);
        }

        public bool Exsit(string keyWord)
        {
            return _IUserDao.Exsit(keyWord);
        }

        public int Count(string keyWord)
        {
            return _IUserDao.Count(keyWord);
        }

        public List<User> GetList(out int total)
        {
            return _IUserDao.GetList(out total);
        }

        public List<User> Pages(int pageIndex, int pageSize, string conditions, out int total)
        {
            return _IUserDao.Pages(pageIndex, pageSize, conditions, out total);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(User model)
        {
            return _IUserDao.Update(model);
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        public bool EditPwd(string id, string oldPwd, string newPwd, out string msg)
        {
            return _IUserDao.EditPwd(id, oldPwd, newPwd, out msg);
        }
    }
}
