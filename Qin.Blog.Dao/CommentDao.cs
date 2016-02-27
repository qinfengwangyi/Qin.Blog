using MySql.Data.MySqlClient;
using Qin.Blog.Data;
using Qin.Blog.Entity;
using Qin.Blog.Entity.DBModel;
using Qin.Blog.IDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Dao
{
    public class CommentDao : ICommentDao
    {
        DataBase _DataBase = new DataBase();

        public Comment GetById(string id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Comment model)
        {
            if (model != null)
            {
                if (_DataBase.InsertModel<Comment>(model))
                    return true;
                return false;
            }
            return false;
        }

        public int Update(Comment model)
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

        public List<Comment> GetList(out int total)
        {
            throw new NotImplementedException();
        }

        public List<Comment> Pages(int pageIndex, int pageSize, string conditions, out int total)
        {
            throw new NotImplementedException();
        }

        public List<CommentDBModel> CommentPages(int pageIndex, int pageSize, string articleId, out int total)
        {
            total = 0;
            var sql = @"SELECT
							a.ArticleId,
	                        a.Id,
	                        a.ParentId,
	                        a.Content,
	                        a.UserId,
	                        CASE WHEN c.UserName is NULL THEN '游客'
	                        ELSE c.NickName END as NickName,
	                        b.Id PId,
	                        b.ParentId PParentId,
	                        b.Content PContent,
	                        b.UserId PUserId,
	                        CASE WHEN d.UserName is NULL THEN '游客'
	                        ELSE d.NickName END as PNickName,
	                        a.CreateTime,
	                        CASE WHEN b.CreateTime is NULL THEN a.CreateTime
	                        ELSE b.CreateTime END as  PCreateTime
                        FROM
	                        `comment` a
                        LEFT JOIN `comment` b ON a.ParentId = b.Id
                        LEFT JOIN `user` c ON a.UserId = c.Id
                        LEFT JOIN `user` d ON b.UserId = d.Id
                        WHERE a.ArticleId = @ArticleId
                        ORDER BY
	                        a.CreateTime DESC LIMIT @PageIndex,@PageSize";
            var sql_total = @"Select Count(*) From leavemessage;";
            MySqlParameter[] paraList = new MySqlParameter[]
            {
                new MySqlParameter("@ArticleId", articleId),
                new MySqlParameter("@PageIndex", --pageIndex * pageSize),
                new MySqlParameter("@PageSize", pageSize)
            };

            var list = _DataBase.QueryList<CommentDBModel>(sql, paraList.ToList());
            total = _DataBase.QueryTotal(sql_total, null); //查询总数
            return list;
        }
    }
}
