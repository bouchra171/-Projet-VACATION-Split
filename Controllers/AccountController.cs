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
                User user = context.Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

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
        public IActionResult Create(User user, IFormFile ProfileImg)
        {
            try
            {
                SecurityService securityService = new SecurityService();
                if (securityService.IsValidEmail(user.Email))
                {
                    return View();
                }
                string uniqueFileName = null;

                if (ProfileImg != null && ProfileImg.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    uniqueFileName = Guid.NewGuid().ToString() + "-" + ProfileImg.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ProfileImg.CopyTo(fileStream);
                    }
                    user.ProfileImage = "/img/" + uniqueFileName;
                }
                string password = user.Password.Trim();
                user.Password = securityService.Encrypt(password);
                using (var context = new VacationSplitContext())
                {
                    context.Add(user);
                    context.SaveChanges();
                }
                return RedirectToAction("Details", new { id = user.Id });
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
                User user = context.Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Models.User updatedUser, IFormFile ProfileImg)
        {
            try
            {
                using (var context = new VacationSplitContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.Id == id);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        user.Email = updatedUser.Email;
                        user.FirstName = updatedUser.FirstName;
                        user.LastName = updatedUser.LastName;
                        user.Ville = updatedUser.Ville;

                        SecurityService securityService = new SecurityService();
                        string password = updatedUser.Password.Trim();
                        user.Password = securityService.Encrypt(password);

                        if (ProfileImg != null && ProfileImg.Length > 0)
                        {
                            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                            string uniqueFileName = Guid.NewGuid().ToString() + "-" + ProfileImg.FileName;
                            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                ProfileImg.CopyTo(fileStream);
                            }
                            user.ProfileImage = "/img/" + uniqueFileName;
                        }

                        else
                        {
                            user.ProfileImage = updatedUser.ProfileImage;
                        }

                        context.Update(user);
                        context.SaveChanges();
                        return RedirectToAction("Details", new { id = user.Id });
                    }

                    return View(user);
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

        // POST: AcountController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        using (var context = new VacationSplitContext())
        //        {
        //            User user = context.Users.Find(id);
        //            context.Users.Remove(user);
        //            context.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Index");
        //    }
       // }
    }
}