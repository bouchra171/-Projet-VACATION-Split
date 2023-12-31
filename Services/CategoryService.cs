﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using VacationSplit.Data;
using VacationSplit.IServices;
using VacationSplit.Models;

namespace VacationSplit.Services
{ 
    public class CategoryService : ICategoryService
    {
        private readonly VacationSplitContext _context;
        public CategoryService(VacationSplitContext context)
        {
            _context= context;
        }
        public IEnumerable<SelectListItem> GetCategories()
        {
            List<SelectListItem> categories = _context.Categories.AsNoTracking()
                                        .OrderBy(c => c.Name)
                                            .Select(c =>
                                            new SelectListItem
                                            {
                                                Value = c.Id.ToString(),
                                                Text = c.Name
                                            }).ToList();
            var categorietip = new SelectListItem()
            {
                Value = null,
                Text = "---select categorie---"
            };
            categories.Insert(0, categorietip);
            return new SelectList(categories, "Value", "Text");
        }
        public Task<List<Category>> FindAllAsync()
        {
            return _context.Categories.ToListAsync();
        }

        public  Category GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(x => x.Id == id);
        }
    }
}

