using Qin.Blog.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.ISerivce
{
    public interface IArticleTypeService : IServiceBase<ArticleType>
    {
        /// <summary>
        /// 通过ID获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        new ArticleType GetById(string id);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        new bool Insert(ArticleType model);

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        new List<ArticleType> GetList(out int total);
    }
}
