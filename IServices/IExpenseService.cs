using VacationSplit.Models;
using VacationSplit.ViewsModel;

namespace VacationSplit.IServices
{
    public interface IExpenseService
    {
        public ExpenseCreateViewModel CreateExepense(int userId);
        public  Task SaveExpense(ExpenseCreateViewModel model, int userId);
        public Task<List<ExpenseListViewModel>> GetAllExpense(int userId);
    }
}
