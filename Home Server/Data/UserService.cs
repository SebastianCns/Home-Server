/*Author: SebastianCns Date: 27.01.23
 * 
 *Description:
 *Code to handel the available users. You can add, get all, get by id, delete and edit a user.
 *A user is an instance of the 'UserModel'. The data are stored in a database, which can be 
 *accessed with the DBUserService class.
 * 
 */

using Home_Server.Models;
using Microsoft.AspNetCore.Components;
using MySqlConnector;

namespace Home_Server.Data
{
    public class UserService
    {
        private IDBService<UserModel, List<UserModel>> _database;
        private List<UserModel> _users;  // Local list of UserModels
        private int _nextUserID;

        public UserService(MySqlConnection dbConnection)
        {
            _nextUserID = 1;
            _database = new DBUserService<UserModel, List<UserModel>>(dbConnection);
        }

        public async Task Init()
        {
            _users = await _database.GetAllAsync();
            SetUserID(_users.Last().Id);
        }

        public void SetUserID(int uID)
        {
            _nextUserID = uID + 1;
        }

        public void AddUser(UserModel user)
        {
            user.Id = _nextUserID;
            _users.Add(user);
            _database.AddAsync(user);
            _nextUserID++;
        }

        public List<UserModel> GetUsers()
        {
            return _users;
        }

        public UserModel GetUser(int id)
        {
            foreach(var user in _users)
            {
                if (user.Id == id)
                {
                    return user;
                }
            }

            return null;
        }

        public void DeleteUser(int id)
        {
            foreach(var user in _users)
            {
                if(user.Id == id)
                {
                    _users.Remove(user);
                    _database.DeleteAsync(user);
                    return;
                }
            }
        }

        public void EditUser(UserModel changedUser)
        {
            foreach(var user in _users)
            {
                if(user.Id == changedUser.Id)
                {
                    user.Name = changedUser.Name;
                    user.FamilyName = changedUser.FamilyName;
                    user.DayOfBirth = changedUser.DayOfBirth;
                    user.Email = changedUser.Email;
                    user.Home =  changedUser.Home;

                    _database.UpdateAsync(user);
                    return;
                }
            }
        }
    }
}
