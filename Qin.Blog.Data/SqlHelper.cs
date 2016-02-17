using Qin.Blog.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Data
{
    /// <summary>
    /// Sql辅助类
    /// </summary>
    public class SqlHelper
    {
        public SqlHelper(string sql)
        {
            Sql = sql;
        }

        public string Sql { get; set; }

        /// <summary>
        /// Where条件
        /// </summary>
        /// <param name="sqlWhere"></param>
        public void Where(string sqlWhere)
        {
            if (Sql.ToLower().Contains(" where "))
            {
                Sql += " And (" + sqlWhere + ") ";
            }
            else
            {
                Sql += " Where (" + sqlWhere + ") ";
            }
        }


        /// <summary>
        /// Order By排序
        /// </summary>
        /// <param name="sqlOrder"></param>
        /// <param name="orderType"></param>
        public void OrderBy(string sqlOrder, string orderType)
        {
            if (Sql.ToLower().Contains(" order by "))
            {
                if (orderType.ToLower() == "asc")
                {
                    Sql += " " + sqlOrder + " Asc ";
                }
                else
                {
                    Sql += " " + sqlOrder + " Desc ";
                }
            }
            else
            {
                if (orderType.ToLower() == "asc")
                {
                    Sql += " Order By " + sqlOrder + " Asc ";
                }
                else
                {
                    Sql += " Order By " + sqlOrder + " Desc ";
                }
            }
        }

        /// <summary>
        /// Group By分组
        /// </summary>
        /// <param name="sqlGroup"></param>
        public void GroupBy(string sqlGroup)
        {
            if (Sql.ToLower().Contains(" group by "))
            {
                Sql += " " + sqlGroup + " ";
            }
            else
            {
                Sql += " Group By " + sqlGroup + " ";
            }
        }

    }
}
