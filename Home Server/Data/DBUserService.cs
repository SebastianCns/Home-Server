/*Author: SebastianCns Date: 27.01.23
*
*Description:
*This class communicates with the database to add, delete and update users.
*In Additon you can get all users data which are currently saved in the database
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
        public async void AddAsync(T Model) // Add new user to database
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

            SetQueryParameter(Model);

            await command.ExecuteNonQueryAsync();
        }

        public async void DeleteAsync(T Model) // Delete given user
        {
            command.CommandText = 
                @"DELETE FROM users " +
                "WHERE " +
                "UID = @Id;";

            SetQueryParameter(Model);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<T_List> GetAllAsync() // Get all user with userdate
        {
            users.Clear();

            command.CommandText = 
                "SELECT " +
                "users.UID, users.name, users.familyname, " +
                "userdata.dayofbirth, userdata.email, userdata.home " +
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

        public async void UpdateAsync(T Model) // Update user given user
        {
            command.CommandText =
                "UPDATE users " +
                "SET " +
                "name = @Name, familyname = @FamilyName " +
                "WHERE " +
                "UID = @Id; " +
                "UPDATE userdata " +
                "SET " +
                "dayofbirth = @DayOfBirth, email = @Email, home = @Home " +
                "WHERE " +
                "UID = @Id; ";

            SetQueryParameter(Model);

            await command.ExecuteNonQueryAsync();
        }

        private void SetQueryParameter(T Model) // Set the placeholder for the query
        {
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Id", Model.Id);
            command.Parameters.AddWithValue("@Name", Model.Name);
            command.Parameters.AddWithValue("@FamilyName", Model.FamilyName);
            command.Parameters.AddWithValue("@DayOfBirth", Model.DayOfBirth);
            command.Parameters.AddWithValue("@Email", Model.Email);
            command.Parameters.AddWithValue("@Home", Model.Home);
        }
    }
}
