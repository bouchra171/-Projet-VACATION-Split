using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using VacationSplit.Models;
using VacationSplit.Repositories;
using VacationSplit.Services;

namespace VacationSplit.Controllers
{
    public class AccountController : Controller
    {
        static List<User> _users = new List<User>();
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
        public ActionResult Create(Models.User user)
        {
            try
            {
                string fileName = Path.GetFileName(user.ProfileImage);
                user.ProfileImage = "../UsersPictures/" + fileName;
                //string strFileName =
                SecurityService securityService = new SecurityService();
                string password = user.Password.Trim();
                user.Password = securityService.Encrypt(password);
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
                User user = context.Users.Where(p => p.Id == id).FirstOrDefault();
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