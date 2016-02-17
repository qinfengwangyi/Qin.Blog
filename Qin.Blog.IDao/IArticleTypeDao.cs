using Qin.Blog.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.IDao
{
    public interface IArticleTypeDao : IDaoBase<ArticleType>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        new bool Insert(ArticleType entity);

        /// <summary>
        /// 通过ID获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        new ArticleType GetById(string id);

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        new List<ArticleType> GetList(out int total);

    }
}
