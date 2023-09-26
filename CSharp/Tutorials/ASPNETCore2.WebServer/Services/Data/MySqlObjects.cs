using System;
using System.Linq;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace WebServer.Lib.Data
{
    // This is faster than EntityFramework.
    // To refactor in the future.
    public partial class MySqlObjects
    {
        private static readonly string SERVER = DotNetEnv.Env.GetString("SERVER");
        private static readonly string PORT = DotNetEnv.Env.GetString("PORT");
        private static readonly string USER_ID = DotNetEnv.Env.GetString("USER_ID");
        private static string CONNECTION_STRING = $"server={SERVER};port={PORT};database=world;userid={USER_ID};sslmode=none";

        public static string GetConnectionString()
        {
            return CONNECTION_STRING;
        }

        public static MySqlConnection Create()
        {
            MySqlConnection connection = new MySqlConnection(CONNECTION_STRING);
            return connection;
        }

        public static MySqlConnection Create(string connectionString)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }

        public static IList<Dictionary<string, object>> Query(MySqlConnection connection, MySqlCommand sqlCommand)
        {
            return ExecuteQuery(connection, sqlCommand);
        }

        public static IList<Dictionary<string, object>> Query(MySqlConnection connection, string queryString)
        {
            MySqlCommand cmd = new MySqlCommand(queryString, connection);
            return ExecuteQuery(connection, cmd);
        }

        public static IList<Dictionary<string, object>> Query(MySqlConnection connection, string queryString, Dictionary<string, object> parameters)
        {
            MySqlCommand cmd = new MySqlCommand(queryString, connection);
            MySqlCommand parameterizedCmd = Parameterize(connection, cmd, parameters);
            return ExecuteQuery(connection, parameterizedCmd);
        }

        private static IList<Dictionary<string, object>> ExecuteQuery(MySqlConnection connection, MySqlCommand cmd)
        {
            try {
                connection.Open();
                using (connection) {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                connection.Close();
            }
        }

        private static Dictionary<string, object> GetRowObject(DbDataReader reader)
        {
            Dictionary<string, object> record = new Dictionary<string, object>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                record.Add(reader.GetName(i), reader[i]);
            }

            return record;
        }

        public static void Command(MySqlConnection connection, MySqlCommand sqlCommand)
        {
            ExecuteCommand(connection, sqlCommand);
        }

        public static void Command(MySqlConnection connection, string commandString)
        {
            MySqlCommand cmd = new MySqlCommand(commandString, connection);
            ExecuteCommand(connection, cmd);
        }

        public static void Command(MySqlConnection connection, string commandString, Dictionary<string, object> parameters)
        {
            MySqlCommand cmd = new MySqlCommand(commandString, connection);
            MySqlCommand parameterizedCmd = Parameterize(connection, cmd, parameters);
            ExecuteCommand(connection, parameterizedCmd);
        }

        private static void ExecuteCommand(MySqlConnection connection, MySqlCommand cmd)
        {
            try {
                connection.Open();
                using (connection) {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                connection.Close();
            }
        }

        private static MySqlCommand Parameterize(MySqlConnection connection, MySqlCommand cmd, Dictionary<string, object> parameters)
        {
            for (int i = 0; i < parameters.Count; i++)
            {
                string parameterName = parameters.Keys.ElementAt(i);
                cmd.Parameters.AddWithValue(parameterName, parameters[parameterName]);
            }
            return cmd;
        }
    }
}
