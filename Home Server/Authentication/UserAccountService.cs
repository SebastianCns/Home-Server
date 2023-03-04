/*Author: SebastianCns Date: 04.03.23
*
*Description:
*This class add's and update's user accounts for authentication.
*You can also get an useraccount for validating access to 
*the Home-Server project.
*
*/
using MySqlConnector;

namespace Home_Server.Authentication
{
    public class UserAccountService
    {
        private MySqlCommand _cmd;
        private UserAccount _admin = new UserAccount
        {
            UserName = "admin",
            Password = "admin",
            Role = "Admin"
        };

        public UserAccountService(MySqlConnection con)
        {
            _cmd = con.CreateCommand();
        }

        public async Task<UserAccount?> GetByUserName(string userName)
        {
            _cmd.CommandText =
                "SELECT " +
                "password, userrole " +
                "FROM " +
                "logininformation " +
                "WHERE " +
                "username = @userName";

            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@userName", userName);

            using var reader = await _cmd.ExecuteReaderAsync();
            // There can be only one row to read
            if(reader.Read() == true) 
            {
                return new UserAccount
                {
                    UserName = userName,
                    Password = reader.GetString("password"),
                    Role = reader.GetString("userrole")
                };
            }
            else if(userName == _admin.UserName)
            {
                return _admin;
            }

            // Need to return null when user does not exist
            return null;
        }

        public void UpdatePassword(string userName)
        {

        }

        public void AddUserAccount(UserAccount userAccount, int userId)
        {

        }
    }
}
