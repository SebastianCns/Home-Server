/*Author: SebastianCns Date: 25.01.23
 * 
 *Description:
 *Model of a user 
 *
 */

using System.ComponentModel.DataAnnotations;

namespace Home_Server.Models
{
    [Serializable]
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Name is to long")]
        public string Name { get; set; }

        [MaxLength(20, ErrorMessage = "Name is to long")]
        public string FamilyName { get; set; }
        public DateTime DayOfBirth { get; set; }

        [MaxLength(40, ErrorMessage = "Name is to long")]
        [EmailAddress(ErrorMessage = "Input is not an email adress")]
        public string Email { get; set; }

        [MaxLength(40, ErrorMessage = "Adress is to long")]
        public string Home { get; set; }
    }
}
