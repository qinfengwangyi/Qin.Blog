using log4net;
using MySql.Data.MySqlClient;
using Qin.Blog.Common;
using Qin.Blog.Data;
using Qin.Blog.Entity;
using Qin.Blog.Entity.Enums;
using Qin.Blog.IDao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Dao
{
    public class UserDao : IUserDao
    {
        ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        DataBase _DataBase = new DataBase();


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="user"></param>
        /// <param name="msg"></param>
        /// <returns>登录成功返回true和User信息，失败返回false和null</returns>
        public bool LogOn(string userName, string passWord, out User user, out string msg)
        {
            int result_u = 0;
            user = null;
            msg = string.Empty;

            string sql_u = @"Select COUNT(*) Count From user Where UserName = @UserName;";
            string sql_up = @"Select * From user Where UserName = @UserName And PassWord = @PassWord;";
            MySqlParameter[] para_u = new MySqlParameter[] { new MySqlParameter("@UserName", userName) };
            MySqlParameter[] para_up = new MySqlParameter[] { new MySqlParameter("@UserName", userName), new MySqlParameter("@PassWord", passWord) };

            //验证用户是否存在
            using (var conn = DataBase.GetOpenConn4MySql())
            {
                MySqlDataReader dr = DataBase.ExecuteQueryReader(sql_u, conn, para_u);
                while (dr.Read())
                {
                    result_u = dr["Count"] != null ? Convert.ToInt32(dr["Count"]) : 0;
                }
                dr.Close();
                if (result_u <= 0)
                {
                    msg = "用户名不存在";
                    return false;
                }
            }

            //验证用户名和密码是否正确
            DataSet ds = new DataSet();
            MySqlDataAdapter da = DataBase.ExecuteQuery4MySql(sql_up, para_up.ToList());
            if (da != null)
            {
                da.Fill(ds);
            }
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                user = FillData.FillDataToEntity<User>(new User(), ds.Tables[0]);
                return true;
            }
            else
            {
                msg = "密码输入错误";
                return false;
            }

        }

        /// <summary>
        /// 根据ID查询单个用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetById(string id)
        {
            User user = new User();
            user = _DataBase.QueryStoredProcedure<User>("aaa", null).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(User entity)
        {
            if (entity != null)
            {
                if (_DataBase.InsertModel<User>(entity))
                    return true;
                return false;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 更新用户昵称和性别信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(User model)
        {
            var sql = @"Update user Set NickName = @NickName, Sex = @Sex Where Id = @Id";
            MySqlParameter[] para = new MySqlParameter[]
            {
                new MySqlParameter("@NickName", model.NickName),
                new MySqlParameter("@Sex", model.Sex),
                new MySqlParameter("@Id", model.Id)
            };
            if (DataBase.ExecuteNonQuery4MySql(sql, para.ToList()) > 0)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 检查名称是否存在
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public bool Exsit(string keyWord)
        {
            string sql = @"Select * From user Where UserName = @UserName;";
            MySqlParameter[] para = new MySqlParameter[] { new MySqlParameter("@UserName", keyWord) };
            List<User> userList = _DataBase.QueryList<User>(sql, para.ToList());
            if (userList != null && userList.Count > 0)
            {
                return true;
            }
            return false;

        }


        public int Count(string keyWord)
        {
            //int result = 0;

            //举个栗子
            var SqlStr = new SqlHelper(@"Select NickName From user Where 1=1 ");
            List<MySqlParameter> paraList = new List<MySqlParameter>();

            if (!string.IsNullOrEmpty(keyWord))
            {
                SqlStr.Where("UserName = @UserName");
                paraList.Add(new MySqlParameter("@UserName", "qinfeng"));
            }
            SqlStr.GroupBy("NickName");
            SqlStr.OrderBy("CreateTime", Operator.Asc.ToString());

            var nickName = _DataBase.QueryObject<string>(SqlStr.Sql, paraList).FirstOrDefault();
            return 1;

        }

        public List<User> GetList(out int total)
        {
            throw new NotImplementedException();
        }

        public List<User> Pages(int pageIndex, int pageSize, string conditions, out int total)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public bool EditPwd(string id, string oldPwd, string newPwd, out string msg)
        {
            msg = "修改失败！";
            var sql_oldpwd = @"Select * From user Where Id=@Id And PassWord=@OldPwd";
            MySqlParameter[] para = new MySqlParameter[]
            {
                new MySqlParameter("@Id", id),
                new MySqlParameter("@OldPwd", oldPwd)
            };
            var result = _DataBase.QueryModel<User>(sql_oldpwd, para.ToList());
            if (string.IsNullOrEmpty(result.Id))
            {
                msg = "原密码输入错误！";
                return false;
            }

            var sql = @"Update user Set PassWord=@PassWord Where Id=@Id";
            MySqlParameter[] paras = new MySqlParameter[]
            {
                new MySqlParameter("@PassWord", newPwd),
                new MySqlParameter("@Id", id)
            };
            if (DataBase.ExecuteNonQuery4MySql(sql, paras.ToList()) > 0)
            {
                msg = "修改成功！";
                return true;
            }
            return false;
        }

    }
}
