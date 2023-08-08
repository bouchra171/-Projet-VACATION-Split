using Microsoft.AspNetCore.Mvc;
using VacationSplit.Models;
using VacationSplit.Data;
using VacationSplit.IServices;

namespace VacationSplit.Controllers
{
    public class AccountController : Controller
    {
        static List<User> _users = new List<User>();
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

            //// Définir la valeur de ViewBag.IsLoggedIn en fonction de l'état de connexion de l'utilisateur
            //ViewBag.IsLoggedIn = HttpContext.Session.GetString("IsLoggedIn") == "true";

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
                if (user.ProfileImg != null)
                { 
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    uniqueFileName = Guid.NewGuid().ToString()+ "-" + user.ProfileImg.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        user.ProfileImg.CopyTo(fileStream);
                    }
                    user.ProfileImage = "/img/" + uniqueFileName;
                }
                else
                {
                    user.ProfileImage = "/img/avatar.jpg";
                }
                
                string password = user.Password.Trim();
                user.Password = _securityService.Encrypt(password);
                user.ProfileImage ="/img/"+ uniqueFileName;

                    _context.Add(user);
                    _context.SaveChanges();

                // Connecter l'utilisateur après la création du compte
                HttpContext.Session.SetString("UserName", user.FirstName); // Enregistrez le nom de l'utilisateur dans la session
                HttpContext.Session.SetString("IsLoggedIn", "true"); // Marquez l'utilisateur comme connecté


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
        public ActionResult Edit(Models.User user)
        {
            try
            {
                    string password = user.Password.Trim();
                    user.Password = _securityService.Encrypt(password);
                    _context.Update(user);
                    _context.SaveChanges();
                    return View("Details", _context.Users.Where(p => p.Id == user.Id).FirstOrDefault());

           
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