using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VacationSplit.Data;
using VacationSplit.IServices;
using VacationSplit.Models;

namespace VacationSplit.Controllers
{
    public class ExpenseController : Controller
    {
        List<Expense> _expenseList;
        private readonly VacationSplitContext _context;
        private readonly ICategoryService _categoryservice;

        public ExpenseController(VacationSplitContext context, ICategoryService categoryservice)
        {
            _context = context;
            _categoryservice = categoryservice;
        }


        // GET: ExpenseController
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


            _expenseList = _context.Expenses.ToList();
            return View(_expenseList);
        }

        // GET: ExpenseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExpenseController/Create
        public async Task<ActionResult> Create()
        {
            List<Category> categories = await _categoryservice.FindAllAsync();
            ViewBag.Categories = categories
               .Select(c => new SelectListItem
               {
                   Value = c.Id.ToString(),
                   Text = c.Name
               })
               .ToList();
            return View();
        }

        // POST: ExpenseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
               //collection

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExpenseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExpenseController/Edit/5
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

        // GET: ExpenseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExpenseController/Delete/5
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
