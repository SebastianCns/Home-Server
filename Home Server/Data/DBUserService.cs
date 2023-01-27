/*Author: SebastianCns Date: 26.01.23
*
*Description:
*
*
*/

using MySqlConnector;
using Home_Server.Models;

namespace Home_Server.Data
{
    public class DBUserService<T, T_List> : IDBService<T, T_List> 
        where T : UserModel, new() // new: to instantiate T (T = UserModel)
        where T_List : List<UserModel>, new() // (T_List = List<UserModel>)
    {
        private MySqlCommand command;
        private T_List users;
        public DBUserService(MySqlConnection connection) 
        {
            users = new T_List();
            command = connection.CreateCommand();
        }
        public async void AddAsync(T Model)
        {
            command.CommandText =
                @"INSERT INTO users " +
                "(UID, name, familyname) " +
                "VALUES " +
                "(@Id, @Name, @FamilyName); " +
                "INSERT INTO userdata " +
                "(UID, dayofbirth, email, home) " +
                "VALUES " +
                "(@Id, @DayOfBirth, @Email, @Home);";

            command.Parameters.AddWithValue("@Id", Model.Id);
            command.Parameters.AddWithValue("@Name", Model.Name);
            command.Parameters.AddWithValue("@FamilyName", Model.FamilyName);
            command.Parameters.AddWithValue("@DayOfBirth", Model.DayOfBirth);
            command.Parameters.AddWithValue("@Email", Model.Email);
            command.Parameters.AddWithValue("@Home", Model.Home);

            await command.ExecuteNonQueryAsync();
        }

        public void DeleteAsync(T Model)
        {
            throw new NotImplementedException();
        }

        public async Task<T_List> GetAllAsync()
        {
            users.Clear();

            command.CommandText = 
                "SELECT " +
                "users.UID, users.name, users.familyname, userdata.dayofbirth, userdata.email, userdata.home " +
                "FROM " +
                "users " +
                "INNER JOIN " +
                "userdata " +
                "ON " +
                "users.UID = userdata.UID";

            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                users.Add(new UserModel
                {
                    Id = reader.GetInt32("UID"),
                    Name = reader.GetString("name"), 
                    FamilyName = reader.GetString("familyname"),
                    DayOfBirth = reader.GetDateTime("dayofbirth"),
                    Email = reader.GetString("email"),
                    Home = reader.GetString("home")
                });
            }

            return users;
        }

        public void UpdateAsync(T Model)
        {
            throw new NotImplementedException();
        }
    }
}
