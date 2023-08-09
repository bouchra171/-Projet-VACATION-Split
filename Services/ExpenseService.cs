using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using VacationSplit.Data;
using VacationSplit.IServices;
using VacationSplit.Models;
using VacationSplit.ViewsModel;

namespace VacationSplit.Services
{
    public class ExpenseService :IExpenseService
    {
        private readonly ICategoryService _servicecaterorie;
        private readonly VacationSplitContext _context;
        

        public ExpenseService(ICategoryService servicecategorie, VacationSplitContext context )
        {
            _servicecaterorie = servicecategorie;
            _context = context;
        }
        public ExpenseCreateViewModel CreateExepense(int userId)
        {
            var expense = new ExpenseCreateViewModel()
            {
                User = _context.Users.FirstOrDefault(x => x.Id == userId),
                Categories = _servicecaterorie.GetCategories()            
            };
            return expense;
        }

        public async Task<List<ExpenseListViewModel>> GetAllExpense(int userId)
        {
            List<ExpenseListViewModel> lstExpenseviewModel= new List<ExpenseListViewModel>(); 
            List<Expense> expenses = await _context.Expenses.Include(x => x.Category).Include(p => p.User).ToListAsync();
            foreach (Expense expense in expenses)
            {
                Category cat = await _context.Categories.FindAsync(expense.Category.Id);
                User usr = await _context.Users.FindAsync(expense.User.Id);
                var expViewmodel = new ExpenseListViewModel()
                {
                    Id = expense.Id,
                    DateExpense = expense.DateExpense,
                    Amount = expense.Amount,
                    CategorieName = expense.Category.Name,
                    UserName = expense.User.LastName + " - " + expense.User.FirstName
                };
                lstExpenseviewModel.Add(expViewmodel);
            }
            return lstExpenseviewModel;
        }
            
         
               

        public async Task SaveExpense(ExpenseCreateViewModel model, int userId)
        {
            try
            {
                if (model != null)
                {
                    User usr = await _context.Users.FindAsync(userId);
                    var expense = new Expense()
                    {
                        DateExpense = model.DateExpense,
                        Amount = Math.Round(model.Amount, 2, MidpointRounding.AwayFromZero),
                        Category = _servicecaterorie.GetCategoryById(model.SelectedCategoryId),
                        User = usr
                    };
                    _context.Add(expense);
                    await _context.SaveChangesAsync();
                   
                }
                
            }
            catch (Exception ex) 
            {
                
            }
        }
    }
}
