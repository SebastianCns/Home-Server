/*Author: SebastianCns Date: 02.03.23
*
*Description:
*This class represents a user account. Equals
*to the structure of a database row
*
*/

namespace Home_Server.Authentication
{
    public class UserAccount
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
