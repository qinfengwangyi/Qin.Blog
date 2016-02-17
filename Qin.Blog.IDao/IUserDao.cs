using Qin.Blog.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.IDao
{
    public interface IUserDao : IDaoBase<User>
    {
        bool LogOn(string userName, string passWord, out User user, out string msg);


        bool EditPwd(string id, string oldPwd, string newPwd, out string msg);
    }
}
