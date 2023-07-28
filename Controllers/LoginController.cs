using Microsoft.AspNetCore.Mvc;
using VacationSplit.IServices;
using VacationSplit.Models;
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
            return View();
        }

        public IActionResult ProcessLogin(User user)
        {

            if (_securityService.IsValid(user)) 
            {
                return View("LoginSuccess", user);
            } else 
            {
                return View("LoginFailure", user);
            }
            
        }

    }
}
