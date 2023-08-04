namespace VacationSplit.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public DateTime DateExpense { get; set; }
        public double Amount { get; set; }
        public Category Category { get; set; }
        public User User { get; set; }
    }
}
