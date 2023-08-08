using Microsoft.AspNetCore.Mvc;
using VacationSplit.Models;
using VacationSplit.Data;
using VacationSplit.Services;
using System.IO;
using VacationSplit.IServices;

namespace VacationSplit.Controllers
{
    public class AccountController : Controller
    {
         List<User> _users = new List<User>();
        private readonly VacationSplitContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISecurityService _securityService;


        public AccountController(IWebHostEnvironment webHostEnvironment, VacationSplitContext context, ISecurityService securityService)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _securityService = securityService;

        }
        // GET: AcountController
        public ActionResult Index()
        {
            _users = _context.Users.ToList();

            return View(_users);


        }

        // GET: AcountController/Details/5
        public ActionResult Details(int id)
        {
            User user = _context.Users.Where(p => p.Id == id).FirstOrDefault();
            return View(user);

        }

        // GET: AcountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AcountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Models.User user, IFormFile file)
        {
            try
            {

                if (_securityService.IsValidEmail(user.Email))
                {
                    return View();
                }
                string uniqueFileName = null;
                if (user.ProfileImg.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    uniqueFileName = Guid.NewGuid().ToString() + "-" + user.ProfileImg.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        user.ProfileImg.CopyTo(fileStream);
                    }
                }
                string password = user.Password.Trim();
                user.Password = _securityService.Encrypt(password);
                user.ProfileImage = "/img/" + uniqueFileName;

                _context.Add(user);
                _context.SaveChanges();

                return View("Details", user);
            }
            catch
            {
                return View();
            }
        }

        // GET: AcountController/Edit/5
        public ActionResult Edit(int id)
        {
            User user = _context.Users.Where(p => p.Id == id).FirstOrDefault();
            return View(user);
        }

        // POST: AcountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.User updatedUser)
        {
            try
            {
                
                User user = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id);

                if (user != null)
                {
                    user.FirstName = updatedUser.FirstName;
                    user.LastName = updatedUser.LastName;
                    user.Email = updatedUser.Email;
                    string password = updatedUser.Password.Trim();

                    
                    if (password != null)
                    {
                        user.Password = _securityService.Encrypt(password);
                    }

                    
                    _context.Update(user);
                    _context.SaveChanges();
                    return View("Details", _context.Users.Where(p => p.Id == user.Id).FirstOrDefault());
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            User user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}