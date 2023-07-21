using Microsoft.AspNetCore.Mvc;
using VacationSplit.Models;
using VacationSplit.Repositories;
using VacationSplit.Services;
using System.IO;


namespace VacationSplit.Controllers
{
    public class AccountController : Controller
    {
        static List<User> _users = new List<User>();
        private readonly IWebHostEnvironment _webHostEnvironment;


        public AccountController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: AcountController
        public ActionResult Index()
        {
            using (var context = new VacationSplitContext())
            {           
                _users = context.Users.ToList();

                return View(_users);
            }
            
        }

        // GET: AcountController/Details/5
        public ActionResult Details(int id)
        {
            using (var context = new VacationSplitContext())
            {
                User user = context.Users.Where(p => p.Id == id).FirstOrDefault();
                return View(user);
            }

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
                SecurityService securityService = new SecurityService();
                if (securityService.IsValidEmail(user.Email))
                {
                    return View();
                }
                string uniqueFileName = null;                
                if (user.ProfileImg.Length > 0)
                { 
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    uniqueFileName = Guid.NewGuid().ToString()+ "-" + user.ProfileImg.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        user.ProfileImg.CopyTo(fileStream);
                    }                        
                }                
                string password = user.Password.Trim();
                user.Password = securityService.Encrypt(password);
                user.ProfileImage ="/img/"+ uniqueFileName;
                using (var context = new VacationSplitContext())
                {
                    context.Add(user);
                    context.SaveChanges();
                }
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
            using (var context = new VacationSplitContext())
            {
                User user = context.Users.Where(p=>p.Id == id).FirstOrDefault();
                return View(user);
            }
           
        }

        // POST: AcountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.User user)
        {
            try
            {
                using (var context = new VacationSplitContext())
                {
                    SecurityService securityService = new SecurityService();
                    string password = user.Password.Trim();
                    user.Password = securityService.Encrypt(password);
                    context.Update(user);
                    context.SaveChanges();
                    return View("Details", context.Users.Where(p => p.Id == user.Id).FirstOrDefault());

                }
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Delete(int id)
        {
            using (var context = new VacationSplitContext())
            {
                User user = context.Users.Find(id);
                context.Users.Remove(user);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            
        }
    
    }
}