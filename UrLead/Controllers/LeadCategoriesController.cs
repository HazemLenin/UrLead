using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UrLead.Data;
using UrLead.Models;
using Microsoft.AspNetCore.Authorization;

namespace UrLead.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LeadCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeadCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeadCategories
        public async Task<IActionResult> Index()
        {
              return _context.LeadCategory != null ? 
                          View(await _context.LeadCategory.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.LeadCategory'  is null.");
        }

        // GET: LeadCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LeadCategory == null)
            {
                return NotFound();
            }

            var leadCategory = await _context.LeadCategory
                .FirstOrDefaultAsync(m => m.LeadCategoryId == id);
            if (leadCategory == null)
            {
                return NotFound();
            }

            return View(leadCategory);
        }

        // GET: LeadCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeadCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeadCategoryId,Title")] LeadCategory leadCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leadCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View(leadCategory);
        }

        // GET: LeadCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LeadCategory == null)
            {
                return NotFound();
            }

            var leadCategory = await _context.LeadCategory.FindAsync(id);
            if (leadCategory == null)
            {
                return NotFound();
            }
            return View(leadCategory);
        }

        // POST: LeadCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeadCategoryId,Title")] LeadCategory leadCategory)
        {
            if (id != leadCategory.LeadCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leadCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeadCategoryExists(leadCategory.LeadCategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leadCategory);
        }

        // GET: LeadCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LeadCategory == null)
            {
                return NotFound();
            }

            var leadCategory = await _context.LeadCategory
                .FirstOrDefaultAsync(m => m.LeadCategoryId == id);
            if (leadCategory == null)
            {
                return NotFound();
            }

            return View(leadCategory);
        }

        // POST: LeadCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LeadCategory == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LeadCategory'  is null.");
            }
            var leadCategory = await _context.LeadCategory.FindAsync(id);
            if (leadCategory != null)
            {
                _context.LeadCategory.Remove(leadCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeadCategoryExists(int id)
        {
          return (_context.LeadCategory?.Any(e => e.LeadCategoryId == id)).GetValueOrDefault();
        }
    }
}
