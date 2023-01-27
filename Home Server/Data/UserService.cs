/*Author: SebastianCns Date: 25.01.23
 * 
 *Description:
 *Code to handel the available users. You can add, get all, get by id, delete and edit a user.
 *A user is an instance of the 'UserModel'.
 * 
 */

using Home_Server.Models;
using Microsoft.AspNetCore.Components;
using MySqlConnector;

namespace Home_Server.Data
{
    public class UserService
    {
        private IDBService<UserModel, List<UserModel>> database;
        private List<UserModel> users;  // Local list of UserModels
        private int nextUserID;

        public UserService(MySqlConnection dbConnection)
        {
            nextUserID = 1;
            database = new DBUserService<UserModel, List<UserModel>>(dbConnection);
        }

        public async Task Init()
        {
            users = await database.GetAllAsync();
            SetUserID(users.Last().Id);
        }

        public void SetUserID(int uID)
        {
            nextUserID = uID + 1;
        }

        public void AddUser(UserModel user)
        {
            user.Id = nextUserID;
            users.Add(user);
            database.AddAsync(user);
            nextUserID++;
        }

        public List<UserModel> GetUsers()
        {
            return users;
        }

        public UserModel GetUser(int id)
        {
            foreach(var user in users)
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
            foreach(var user in users)
            {
                if(user.Id == id)
                {
                    users.Remove(user);
                    database.DeleteAsync(user);
                    return;
                }
            }
        }

        public void EditUser(UserModel changedUser)
        {
            foreach(var user in users)
            {
                if(user.Id == changedUser.Id)
                {
                    user.Name = changedUser.Name;
                    user.FamilyName = changedUser.FamilyName;
                    user.DayOfBirth = changedUser.DayOfBirth;
                    user.Email = changedUser.Email;
                    user.Home =  changedUser.Home;
                }
            }
        }
    }
}
