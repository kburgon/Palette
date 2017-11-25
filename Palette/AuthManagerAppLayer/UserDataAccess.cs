using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AuthManagerAppLayer
{
    public static class UserDataAccess
    {
        public static List<User> GetUsers()
        {
            using (var conn = new SqlConnection(GetConnectionString()))
            using (var command = new SqlCommand(@"SELECT * FROM Authentication.dbo.UserInfo", conn))
            {
                conn.Open();
                var results = command.ExecuteReader();

                var users = new List<User>();
                while (results.Read())
                {
                    users.Add(new User
                    {
                        UserId = Convert.ToInt32(results["UserID"]),
                        UserName = results["UserName"].ToString(),
                        Password = results["Password"].ToString()
                    });
                }

                return users;
            }
        }

        public static int GetUserId(string username, string password)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserName", SqlDbType.VarChar) { Value = username },
                new SqlParameter("@Password", SqlDbType.VarChar) { Value = password }
            };
            return Convert.ToInt32(ExecuteScalarProc(@"Authentication.dbo.GetUser", parameters));
        }

        public static int CreateUser(string username, string password)
        {
            var proc = @"Authentication.dbo.CheckCreateUser";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserName", SqlDbType.VarChar) {Value = username},
                new SqlParameter("@Password", SqlDbType.VarChar) {Value = password}
            };

            var results = Convert.ToInt32(ExecuteScalarProc(proc, parameters));
            return results;
        }

        private static object ExecuteScalarProc(string proc, List<SqlParameter> parameters)
        {
            object result;

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var command = new SqlCommand(proc, conn))
            {
                conn.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters.ToArray());

                result = command.ExecuteScalar();
            }

            return result;
        }

        public static void DeleteUser(int userId)
        {
            var proc = @"Authentication.dbo.DeleteUser";
            var parameters = new List<SqlParameter> {new SqlParameter("@UserID", SqlDbType.Int) {Value = userId}};

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var command = new SqlCommand(proc, conn))
            {
                conn.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters.ToArray());
                command.ExecuteNonQuery();
            }
        }

        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["AuthDbConnection"].ConnectionString;
        }
    }
}
