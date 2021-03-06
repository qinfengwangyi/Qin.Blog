﻿using Qin.Blog.Entity;
using Qin.Blog.Entity.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.IDao
{
    public interface ILeaveMessageDao: IDaoBase<LeaveMessage>
    {

        List<LeaveMsgDBModel> LeaveMsgPages(int pageIndex, int pageSize, string conditions, out int total);
    }
}
