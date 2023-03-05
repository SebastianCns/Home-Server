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
        private MySqlCommand _command;
        private T_List _users;
        public DBUserService(MySqlConnection connection) 
        {
            _users = new T_List();
            _command = connection.CreateCommand();
        }
        public async void AddAsync(T Model) // Add new user to database
        {
            _command.CommandText =
                @"INSERT INTO users " +
                "(UID, name, familyname) " +
                "VALUES " +
                "(@Id, @Name, @FamilyName); " +
                "INSERT INTO userdata " +
                "(UID, dayofbirth, email, home) " +
                "VALUES " +
                "(@Id, @DayOfBirth, @Email, @Home);";

            SetQueryParameter(Model);

            await _command.ExecuteNonQueryAsync();
        }

        public async void DeleteAsync(T Model) // Delete given user
        {
            _command.CommandText = 
                @"DELETE FROM users " +
                "WHERE " +
                "UID = @Id;";

            SetQueryParameter(Model);

            await _command.ExecuteNonQueryAsync();
        }

        public async Task<T_List> GetAllAsync() // Get all user with userdate
        {
            _users.Clear();

            _command.CommandText = 
                "SELECT " +
                "users.UID, users.name, users.familyname, " +
                "userdata.dayofbirth, userdata.email, userdata.home " +
                "FROM " +
                "users " +
                "INNER JOIN " +
                "userdata " +
                "ON " +
                "users.UID = userdata.UID";

            using var reader = await _command.ExecuteReaderAsync();
            while (reader.Read())
            {
                _users.Add(new UserModel
                {
                    Id = reader.GetInt32("UID"),
                    Name = reader.GetString("name"), 
                    FamilyName = reader.GetString("familyname"),
                    DayOfBirth = reader.GetDateTime("dayofbirth"),
                    Email = reader.GetString("email"),
                    Home = reader.GetString("home")
                });
            }

            return _users;
        }

        public async Task UpdateAsync(T Model) // Update user given user
        {
            _command.CommandText =
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

            await _command.ExecuteNonQueryAsync();
        }

        private void SetQueryParameter(T Model) // Set the placeholder for the query
        {
            _command.Parameters.Clear();
            _command.Parameters.AddWithValue("@Id", Model.Id);
            _command.Parameters.AddWithValue("@Name", Model.Name);
            _command.Parameters.AddWithValue("@FamilyName", Model.FamilyName);
            _command.Parameters.AddWithValue("@DayOfBirth", Model.DayOfBirth);
            _command.Parameters.AddWithValue("@Email", Model.Email);
            _command.Parameters.AddWithValue("@Home", Model.Home);
        }
    }
}
