using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using VacationSplit.Models;

namespace VacationSplit.ViewsModel
{
    public class ExpenseListViewModel
    {
        public int Id { get; set; }
        public DateTime DateExpense { get; set; }
        public double Amount { get; set; }
        [DisplayName("Nom utilisateur")]
        public string UserName { get; set; }
        [DisplayName("Catégorie")]
        public string CategorieName { get; set; }
    }
}
