using Microsoft.AspNetCore.Mvc;
using VacationSplit.Models;
using Microsoft.EntityFrameworkCore;
using VacationSplit;
using VacationSplit.Data;


public class CategoriesController : Controller
{
    private readonly VacationSplitContext _context;

    public CategoriesController(VacationSplitContext context)
    {
        _context = context;
    }

    // GET: Categories
    public async Task<IActionResult> Index()
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

        return View(await _context.Categories.ToListAsync());
    }

    // POST: Categories/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
    {
        if (ModelState.IsValid)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    // POST: Categories/Edit/5
    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] Category updateCategory)
    {
        if (updateCategory == null || updateCategory.Id == 0)
        {
            return BadRequest();
        }

        var category = await _context.Categories.FindAsync(updateCategory.Id);
        if (category == null)
        {
            return NotFound();
        }

        category.Name = updateCategory.Name;
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();

        return Ok();
    }

    // POST: Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

