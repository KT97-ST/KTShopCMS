using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    
    public class UserDao
    {
        KTShopDbContext context = null;

        public UserDao()
        {
            context = new KTShopDbContext();
        }

        public User GetById(int userID)
        {
            var res = context.Database.SqlQuery<User>("sp_GetUserByID @UserID", userID).SingleOrDefault();
            return res;
        }

        public User GetByUserName(string userName)
        {
            var parameters = new SqlParameter("@UserName", userName);
            var res = context.Database.SqlQuery<User>("sp_GetUserByUserName @UserName", parameters).SingleOrDefault();
            return res;
        }


        public int Insert(User user)
        {
            var parameters = new object[]
           {
                new SqlParameter("@UserName", user.UserName),
                new SqlParameter("@PassWord", user.Password),
                new SqlParameter("@Name", user.Name),
                new SqlParameter("@Address", user.Address),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Phone", user.Phone)
           };

            int res = context.Database.ExecuteSqlCommand("sp_InsertUser @UserName, @PassWord, @Name, @Address, @Email, @Phone", parameters);
            return res;
        }

        public int Update(User user)
        {
            var parameters = new object[]
            {
                new SqlParameter("@UserID", user.ID),
                new SqlParameter("@UserName", user.UserName),
                new SqlParameter("@PassWord", user.Password),
                new SqlParameter("@Name", user.Name),
                new SqlParameter("@Address", user.Address),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Phone", user.Phone)
            };

            int res = context.Database.ExecuteSqlCommand("sp_UpdateUser @UserID, @UserName, @PassWord, @Name, @Address, @Email, @Phone", parameters);
            return res;
        }

        public int Delete(int userId)
        {
            var parameter = new SqlParameter("@UserID", userId);

            int res = context.Database.ExecuteSqlCommand("sp_DeleteUser @UserID", parameter);
            return res;
        }

        public int Login(string username, string password)
        {
            var parameters = new object[]
            {
                new SqlParameter("@username", username),
                new SqlParameter("@password", password)
            };

            var res = context.Database.SqlQuery<int>("Sp_User_Login @username,@password", parameters).SingleOrDefault();
            return res;
        }
    }
}
