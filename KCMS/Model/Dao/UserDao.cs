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

        public bool Login(string username, string password)
        {
            var parameters = new object[]
            {
                new SqlParameter("@username", username),ss
                new SqlParameter("@password", password)
            };

            var res = context.Database.SqlQuery<bool>("Sp_User_Login @username,@password", parameters).SingleOrDefault();
            return res;
        }
    }
}
