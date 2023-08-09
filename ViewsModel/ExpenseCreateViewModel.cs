using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using VacationSplit.Models;

namespace VacationSplit.ViewsModel
{
    public class ExpenseCreateViewModel
    {
        public DateTime DateExpense { get; set; }
        public double Amount { get; set; }
        [DisplayName("Catégorie :")]
        public int SelectedCategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public Category Categoty { get; set; }
        public User User { get; set; }
    }
}
