using Qin.Blog.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.IService
{
    public interface IUserService : IServiceBase<User>
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="user"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool LogOn(string userName, string passWord, out User user, out string msg);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        bool EditPwd(string id, string oldPwd, string newPwd, out string msg);
    }
}
