using Microsoft.AspNetCore.Mvc;
using VacationSplit.Models;
using VacationSplit.Data;
using VacationSplit.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

namespace VacationSplit.Controllers
{
    public class AccountController : Controller
    {
        private readonly VacationSplitContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISecurityService _securityService;

        public AccountController(IWebHostEnvironment webHostEnvironment, VacationSplitContext context, ISecurityService securityService)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _securityService = securityService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") == "true")
            {
                ViewBag.IsLoggedIn = true;
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
                var users = _context.Users.ToList();
                return View(users);
            }
            else
            {
                ViewBag.IsLoggedIn = false;
                return View();
            }
        }

        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("IsLoggedIn") == "true")
            {
                ViewBag.IsLoggedIn = true;
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
            }
            else
            {
                ViewBag.IsLoggedIn = false;
            }

            int userId = id > 0 ? id : int.Parse(HttpContext.Session.GetString("UserId"));
            User user = _context.Users.Find(userId);

            if (user == null)
            {
                // Gérer l'utilisateur non trouvé ici
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") == "true")
            {
                ViewBag.IsLoggedIn = true;
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
            }
            else
            {
                ViewBag.IsLoggedIn = false;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user, IFormFile file)
        {
            try
            {
                if (_securityService.IsValidEmail(user.Email))
                {
                    // Gérer l'email non valide ici
                    return View();
                }

                string uniqueFileName = null;

                if (file != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    uniqueFileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    user.ProfileImage = "/img/" + uniqueFileName;
                }
                else
                {
                    user.ProfileImage = "/img/avatar.jpg";
                }

                string password = user.Password.Trim();
                user.Password = _securityService.Encrypt(password);

                _context.Add(user);
                _context.SaveChanges();

                HttpContext.Session.SetString("UserName", user.FirstName);
                HttpContext.Session.SetString("IsLoggedIn", "true");

                return RedirectToAction("Details", new { id = user.Id });
            }
            catch
            {
                // Gérer l'erreur ici
                return View();
            }
        }

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("IsLoggedIn") == "true")
            {
                ViewBag.IsLoggedIn = true;
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
            }
            else
            {
                ViewBag.IsLoggedIn = false;
            }

            User user = _context.Users.Find(id);

            if (user == null)
            {
                // Gérer l'utilisateur non trouvé ici
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user, IFormFile file)
        {
            try
            {
                string uniqueFileName = null;

                if (file != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    uniqueFileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    user.ProfileImage = "/img/" + uniqueFileName;
                }
                else
                {
                    user.ProfileImage = "/img/avatar.jpg";
                }

                _context.Update(user);
                _context.SaveChanges();

                return RedirectToAction("Details", new { id = user.Id });
            }
            catch
            {
                // Gérer l'erreur ici
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            User user = _context.Users.Find(id);

            if (user == null)
            {
                // Gérer l'utilisateur non trouvé ici
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
