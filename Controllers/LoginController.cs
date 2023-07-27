using Microsoft.AspNetCore.Mvc;
using VacationSplit.Models;
using VacationSplit.Services;

namespace VacationSplit.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessLogin(User user)
        {
            SecurityService securityService = new SecurityService();

            if (securityService.IsValid(user)) 
            {
                //return View("LoginSuccess", user);
            } else 
            {
                return View("LoginFailure", user);
            }


            HttpContext.Session.SetString("IsLoggedIn", "true");
            return RedirectToAction("Index", "Home");

        }

        // Action pour la déconnexion
        public IActionResult Logout()
        {
            // Supprimez la valeur de la session pour indiquer que l'utilisateur n'est plus connecté
            HttpContext.Session.Remove("IsLoggedIn");
            return RedirectToAction("Index", "Home");
        }

    }
}
