using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VacationSplit.Data;
using VacationSplit.IServices;
using VacationSplit.Models;
using VacationSplit.ViewsModel;

namespace VacationSplit.Controllers
{
    public class ExpenseController : Controller
    {
        List<Expense> _expenseList;
        private readonly VacationSplitContext _context;
        private readonly ICategoryService _categoryservice;
        private readonly IExpenseService _expenseservice;

        public ExpenseController(VacationSplitContext context, IExpenseService expenseservice)
        {
            _context = context;
            _expenseservice = expenseservice;
        }


        // GET: ExpenseController
        // GET: ExpenseController
        public async Task<ActionResult> Index()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") == "true")
            {
                // L'utilisateur est connecté, afficher les onglets "Catégories" et "Dépenses"
                ViewBag.IsLoggedIn = true;
                ViewBag.UserName = HttpContext.Session.GetString("UserName"); // Récupérer le nom de l'utilisateur connecté
                if (HttpContext.Session.TryGetValue("UserId", out var userIdBytes) &&
                    int.TryParse(Encoding.UTF8.GetString(userIdBytes), out int userId))
                {
                    List<ExpenseListViewModel> _expenseList = await _expenseservice.GetAllExpense(userId);
                    return View(_expenseList);
                }
                else
                {
                    
                    return RedirectToAction("Login", "Account"); 
                }
            }
            else
            {
                // L'utilisateur n'est pas connecté, ne pas afficher les onglets "Catégories" et "Dépenses"
                ViewBag.IsLoggedIn = false;
                return View();
            }
        }


        // GET: ExpenseController/Create
        public async Task<ActionResult> Create()
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
            int userId = Int32.Parse(HttpContext.Session.GetString("UserId"));
            var expense = _expenseservice.CreateExepense(userId);           
            
            return View(expense);
        }

        // POST: ExpenseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ExpenseCreateViewModel model)
        {
            if (model != null)
            {
                if (int.TryParse(HttpContext.Session.GetString("UserId"), out int userId))
                {
                    await _expenseservice.SaveExpense(model, userId);
                    return RedirectToAction("Index", "Expense"); 
                }
                else
                {
                    
                    return RedirectToAction("Login", "Account"); 
                }
            }
            else
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Expense expense = _context.Expenses.Find(id);
            if (expense == null)
            {
                return NotFound();
            }
            _context.Expenses.Remove(expense);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: ExpenseController/Delete/5
        
        
    }
}
