using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationSplit.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Veuillez saisir un email valide")]
        [DisplayName("Email de connexion")]       
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Veuillez saisir un mot de passe valide")]
        [DisplayName("Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Veuillez saisir un prénom valide")]
        [DisplayName("Prénom")]
        [StringLength(20, MinimumLength = 4)]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Veuillez saisir un Nom valide")]
        [DisplayName("Nom")]
        [StringLength(20, MinimumLength = 4)]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Veuillez saisir une ville valide")]
        [DisplayName("Ville")]
        [StringLength(20)]
        [DataType(DataType.Text)]
        public string Ville { get; set; }
        [DisplayName("Phot de profil")]
        [DataType(DataType.ImageUrl)]
        public string ProfileImage { get; set; }
        [NotMapped]
        public IFormFile? ProfileImg { get; set; }
    }
}
