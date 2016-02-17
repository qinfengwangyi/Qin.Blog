using MySql.Data.MySqlClient;
using Qin.Blog.Data;
using Qin.Blog.Entity;
using Qin.Blog.IDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Dao
{
    public class SysLogDao : ISysLogDao
    {
        public SysLog GetById(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 写入系统日志
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(SysLog entity)
        {
            string sql = FillData.AppendSqlToInsert(entity);
            MySqlCommand cmd = new MySqlCommand(sql, DataBase.GetOpenConn4MySql());
            MySqlParameter[] para = FillData.AppendParas(entity); //拼接参数
            cmd.Parameters.AddRange(para); //添加参数
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 查询所有的日志
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<SysLog> GetList(out int total)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 日志分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<SysLog> Pages(int pageIndex, int pageSize, string conditions, out int total)
        {
            throw new NotImplementedException();
        }


        public bool Exsit(string keyWord)
        {
            throw new NotImplementedException();
        }

        public int Count(string keyWord)
        {
            throw new NotImplementedException();
        }


        public int Update(SysLog model)
        {
            throw new NotImplementedException();
        }
    }
}
