using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.IDao
{
    public interface IDaoBase<T>
    {
        T GetById(string id);

        bool Insert(T model);

        int Update(T model);

        bool Exsit(string keyWord);

        int Count(string keyWord);

        List<T> GetList(out int total);

        List<T> Pages(int pageIndex, int pageSize, string conditions, out int total);
    }
}
