using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServer.Lib.Data;
using WebServer.Models;

namespace WebServer.DataAccess
{
    public class UsersTbl
    {
        public static IList<Dictionary<string, object>> GetAllUsers(MySqlConnection connection)
        {
            return MySqlObjects.Query(connection,
                @"SELECT *
                  FROM Users"
            );
        }

        public static async Task<IList<Dictionary<string, object>>> GetUserByEmailAsync(MySqlConnection connection, string username)
        {
            MySqlCommand query = new MySqlCommand(
                @"SELECT *
                  FROM aspnetusers
                  WHERE Email = @Email", 
                connection
            );
            query.Parameters.AddWithValue("@Email", username);

            return await MySqlObjects.QueryAsync(connection, query);
        }

        public static async Task<bool> InsertNewUserAsync(MySqlConnection connection, User user)
        {
            return await MySqlObjects.CommandAsync(connection,
                @"INSERT INTO aspnetusers (Email, Password)
                  VALUES (@Username, @Password)",
                new Dictionary<string, object>
                {
                    { "@Username", user.Username },
                    { "@Password", user.Password }
                }
            );
        }
    }
}
