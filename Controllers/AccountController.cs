using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VacationSplit.Models;

namespace VacationSplit.Controllers
{
    public class AccountController : Controller
    {
         static List<User> _users = new List<User>();
        // GET: AcountController
        public ActionResult Index()
        {
            return View(_users);
        }

        // GET: AcountController/Details/5
        public ActionResult Details(Models.User user)
        {
            _users.Add(user);
            return View("Details", user);
        }

        // GET: AcountController/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: AcountController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: AcountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AcountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AcountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AcountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
