namespace Home_Server.Authentication
{
    public class UserAccountService
    {
        // Temporary local with a list
        // Next implementing a db-connection with direct communication !!!

        private List<UserAccount> _users;

        public UserAccountService()
        {
            _users = new List<UserAccount>
            {
                new UserAccount { UserName = "admin", Password = "admin", Role = "Admin" },
                new UserAccount { UserName = "user", Password = "user", Role = "User" }
            };
        }

        public UserAccount? GetByUserName(string userName)
        {
            //Need to return null when user does not exist
            return _users.FirstOrDefault(x => x.UserName == userName);
        }
    }
}
