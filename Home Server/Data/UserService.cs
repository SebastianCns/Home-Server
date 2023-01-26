/*Author: SebastianCns Date: 25.01.23
 * 
 *Description:
 *Code to handel the available users. You can add, get all, get by id, delete and edit a user.
 *A user is an instance of the 'UserModel'.
 * 
 */

using Home_Server.Models;

namespace Home_Server.Data
{
    public class UserService
    {
        private List<UserModel> users;

        public UserService()
        {
            users = new List<UserModel>();
        }

        public void AddUser(UserModel user)
        {
            users.Add(user);
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
