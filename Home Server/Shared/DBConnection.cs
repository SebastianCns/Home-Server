/*Author: SebastianCns Date: 26.01.23
*
*Description:
*This class represents a datase connection.
*If you call 'Connect' you got a connection instance to 
*communicate with the database
*
*/

using MySqlConnector;

namespace Home_Server.Shared
{
    public class DBConnection
    {
        private const string userID = "sebastian";
        private const string password = "mysql";

        private MySqlConnectionStringBuilder builder;
        private MySqlConnection connection;

        public DBConnection(string server, string database)
        {
            builder = new MySqlConnectionStringBuilder
            {
                Server = server,
                UserID = userID,
                Password = password,
                Database = database,
            };
        }

        public async Task<MySqlConnection> Connect()
        {
            connection = new MySqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            return connection;
        }

        public async Task Disconnect()
        {
            await connection.CloseAsync();
        }
    }
}
