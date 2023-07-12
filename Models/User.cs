using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VacationSplit.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Email de connexion")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DisplayName("Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DisplayName("Prénom")]
        [StringLength(20, MinimumLength = 4)]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Nom")]
        [StringLength(20, MinimumLength = 4)]
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
    }
}
