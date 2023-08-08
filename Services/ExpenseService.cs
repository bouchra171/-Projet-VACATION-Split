using VacationSplit.IServices;
using VacationSplit.ViewsModel;

namespace VacationSplit.Services
{
    public class ExpenseService :IExpenseService
    {
        private readonly ICategoryService _servicecaterorie;
        public ExpenseService(ICategoryService servicecategorie)
        {
            _servicecaterorie = servicecategorie;
        }
        public ExpenseCreateViewModel CreateExepence(int userId)
        {
            var expense = new ExpenseCreateViewModel()
            {                
                Categories = _servicecaterorie.GetCategories()             
            };
            return expense;
        }
    }
}
