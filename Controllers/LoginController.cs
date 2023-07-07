using Microsoft.AspNetCore.Mvc;
using VacationSplit.Models;

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
            if (user.Email == "BillGates" && user.Password == "bigbucks") 
            {
                return View("LoginSuccess", user);
            } else 
            {
                return View("LoginFailure", user);
            }
            
        }
    }
}
