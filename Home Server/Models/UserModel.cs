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
        public UserModel()
        {
            DayOfBirth = new DateTime(1990, 1, 1); // Set default for day of birth
        }

        public int Id { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Name is to long")]
        public string Name { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Name is to long")]
        public string FamilyName { get; set; }

        [Required]
        public DateTime DayOfBirth { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "Name is to long")]
        [EmailAddress(ErrorMessage = "Input is not an email adress")]
        public string Email { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "Adress is to long")]
        public string Home { get; set; }
    }
}
