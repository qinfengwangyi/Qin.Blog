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
    public class LeaveMessageDao : ILeaveMessageDao
    {
        DataBase _DataBase = new DataBase();


        public LeaveMessage GetById(string id)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 添加留言
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(LeaveMessage model)
        {
            if (model != null)
            {
                if (_DataBase.InsertModel<LeaveMessage>(model))
                    return true;
                return false;
            }
            return false;
        }

        public int Update(LeaveMessage model)
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

        public List<LeaveMessage> GetList(out int total)
        {
            throw new NotImplementedException();
        }

        public List<LeaveMessage> Pages(int pageIndex, int pageSize, string conditions, out int total)
        {
            total = 0;
            var sql = @"SELECT * FROM leavemessage ORDER BY CreateTime DESC LIMIT @PageIndex,@PageSize;";
            var sql_total = @"Select Count(*) From leavemessage;";
            MySqlParameter[] paraList = new MySqlParameter[]
            {
                new MySqlParameter("@PageIndex", --pageIndex * pageSize),
                new MySqlParameter("@PageSize", pageSize)
            };

            var list = _DataBase.QueryList<LeaveMessage>(sql, paraList.ToList());
            total = _DataBase.QueryTotal(sql_total, null); //查询总数
            return list;
        }
    }
}
