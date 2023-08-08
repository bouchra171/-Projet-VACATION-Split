using Microsoft.AspNetCore.Mvc.Rendering;
using VacationSplit.Models;

namespace VacationSplit.IServices
{
    public interface ICategoryService
    {
        public Task<List<Category>> FindAllAsync();
        public IEnumerable<SelectListItem> GetCategories();

    }
}
