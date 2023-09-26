using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace WebServer.Lib.Data
{
    public partial class MySqlObjects
    {
        public static async Task<IList<Dictionary<string, object>>> QueryAsync(MySqlConnection connection, MySqlCommand sqlCommand)
        {
            return await ExecuteQueryAsync(connection, sqlCommand);
        }

        public static async Task<IList<Dictionary<string, object>>> QueryAsync(MySqlConnection connection, string queryString)
        {
            MySqlCommand cmd = new MySqlCommand(queryString, connection);
            return await ExecuteQueryAsync(connection, cmd);
        }

        public static async Task<IList<Dictionary<string, object>>> QueryAsync(MySqlConnection connection, string queryString, Dictionary<string, object> parameters)
        {
            MySqlCommand cmd = new MySqlCommand(queryString, connection);
            MySqlCommand parameterizedCmd = Parameterize(connection, cmd, parameters);
            return await ExecuteQueryAsync(connection, parameterizedCmd);
        }

        private static async Task<IList<Dictionary<string, object>>> ExecuteQueryAsync(MySqlConnection connection, MySqlCommand cmd)
        {
            try
            {
                await connection.OpenAsync();
                using (connection)
                {
                    DbDataReader reader = await cmd.ExecuteReaderAsync();
                    IList<Dictionary<string, object>> recordset = new List<Dictionary<string, object>>();

                    while (reader.Read())
                    {
                        recordset.Add(GetRowObject(reader));
                    }

                    return recordset;
                }
            }
            catch (Exception e)
            {
                IList<Dictionary<string, object>> error =
                new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object>
                    {
                        { "Error", e.ToString() }
                    }
                };
                return error;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public static async Task<bool> CommandAsync(MySqlConnection connection, MySqlCommand sqlCommand)
        {
            return await ExecuteCommandAsync(connection, sqlCommand);
        }

        public static async Task<bool> CommandAsync(MySqlConnection connection, string commandString)
        {
            MySqlCommand cmd = new MySqlCommand(commandString, connection);
            return await ExecuteCommandAsync(connection, cmd);
        }

        public static async Task<bool> CommandAsync(MySqlConnection connection, string commandString, Dictionary<string, object> parameters)
        {
            MySqlCommand cmd = new MySqlCommand(commandString, connection);
            MySqlCommand parameterizedCmd = Parameterize(connection, cmd, parameters);
            return await ExecuteCommandAsync(connection, parameterizedCmd);
        }

        private static async Task<bool> ExecuteCommandAsync(MySqlConnection connection, MySqlCommand cmd)
        {
            try
            {
                await connection.OpenAsync();
                using (connection)
                {
                    await cmd.ExecuteNonQueryAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());//
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}
