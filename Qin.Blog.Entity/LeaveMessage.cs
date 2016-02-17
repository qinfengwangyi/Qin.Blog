using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Entity
{
    public class LeaveMessage: BaseEntity
    {
        public string Content { get; set; }


        public string ParentId { get; set; }


        public string UserId { get; set; }

    }
}
