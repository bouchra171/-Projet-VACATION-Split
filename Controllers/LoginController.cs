using Microsoft.AspNetCore.Mvc;
using VacationSplit.IServices;
using VacationSplit.Models;
using Microsoft.AspNetCore.Http;
using VacationSplit.Services;

namespace VacationSplit.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISecurityService _securityService;

        public LoginController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("IsLoggedIn") == "true")
            {
                // L'utilisateur est connecté, afficher les onglets "Catégories" et "Dépenses"
                ViewBag.IsLoggedIn = true;
                ViewBag.UserName = HttpContext.Session.GetString("UserName"); // Récupérer le nom de l'utilisateur connecté
            }
            else
            {
                // L'utilisateur n'est pas connecté, ne pas afficher les onglets "Catégories" et "Dépenses"
                ViewBag.IsLoggedIn = false;
            }


            return View();
        }

        public IActionResult ProcessLogin(User user)
        {

            if (_securityService.IsValid(user)) 
            {

                
                    // Authentification réussie, connecter l'utilisateur
                    HttpContext.Session.SetString("IsLoggedIn", "true");

                    // Récupérer les informations de l'utilisateur à partir de la base de données
                    User userInfo = _securityService.GetUserByEmail(user.Email);
                    if (userInfo != null)
                    {
                        HttpContext.Session.SetString("UserName", userInfo.FirstName); // Enregistrez le prénom de l'utilisateur dans la session
                        HttpContext.Session.SetString("UserLastName", userInfo.LastName);
                        HttpContext.Session.SetString("UserId", userInfo.Id.ToString());// Enregistrez le nom de famille de l'utilisateur dans la session
                }

                

                    return RedirectToAction("Index", "Home");

            } else 
            {
                return View("LoginFailure", user);
            }


        }

        // Action pour la déconnexion
        public IActionResult Logout()
        {
            // Supprimez la valeur de la session pour indiquer que l'utilisateur n'est plus connecté
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("IsLoggedIn");
            

            return RedirectToAction("Index", "Home");
        }

    }
}
