using Qin.Blog.Entity;
using Qin.Blog.Entity.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.IDao
{
    public interface ICommentDao:IDaoBase<Comment>
    {
        List<CommentDBModel> CommentPages(int pageIndex, int pageSize, string articleId, out int total);
    }
}
