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
using Microsoft.AspNetCore.Identity;

namespace UrLead.Controllers
{
    [Authorize(Roles = "Admin,Sales")]
    public class LeadsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public LeadsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Leads
        public async Task<IActionResult> Index()
        {
            // Load user's leads if he is salesperson; If he is an admin, load all leads
            IdentityUser currentUser = await _userManager.GetUserAsync(User);
            var applicationDbContext = User.IsInRole("Sales") ? _context
                .Lead
                .Where(l => l.OrganizationId == currentUser.Id)
                .Include(l => l.Category)
                .Include(l => l.Organization) : _context
                .Lead
                .Include(l => l.Category)
                .Include(l => l.Organization);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Leads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lead == null)
            {
                return NotFound();
            }

            var lead = await _context.Lead
                .Include(l => l.Category)
                .Include(l => l.Organization)
                .FirstOrDefaultAsync(m => m.LeadId == id);
            if (lead == null)
            {
                return NotFound();
            }

            IdentityUser currentUser = await _userManager.GetUserAsync(User);

            if (User.IsInRole("Sales") && lead.OrganizationId != currentUser.Id)
            {
                return Forbid();
            }

            return View(lead);
        }

        // GET: Leads/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.LeadCategory, "LeadCategoryId", "Title");
            return View();
        }

        // POST: Leads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeadId,FirstName,LastName,Age,Email,PhoneNumber,Description,Probability,CategoryId")] Lead lead)
        {

            if (ModelState.IsValid)
            {
                _context.Add(lead);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.LeadCategory, "LeadCategoryId", "Title", lead.CategoryId);
            return View(lead);
        }

        // GET: Leads/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lead == null)
            {
                return NotFound();
            }

            var lead = await _context.Lead.FindAsync(id);
            if (lead == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.LeadCategory, "LeadCategoryId", "Title", lead.CategoryId);
            return View(lead);
        }

        // POST: Leads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("LeadId,FirstName,LastName,Age,Email,PhoneNumber,Description,Probability,CategoryId")] Lead lead)
        {
            if (id != lead.LeadId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lead);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeadExists(lead.LeadId))
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
            ViewData["CategoryId"] = new SelectList(_context.LeadCategory, "LeadCategoryId", "Title", lead.CategoryId);
            return View(lead);
        }

        // GET: Leads/ChangeCategory/5
        [Authorize(Roles = "Sales")]
        public async Task<IActionResult> ChangeCategory(int? id)
        {
            if (id == null || _context.Lead == null)
            {
                return NotFound();
            }

            var lead = await _context.Lead.FindAsync(id);
            if (lead == null)
            {
                return NotFound();
            }

            IdentityUser currentUser = await _userManager.GetUserAsync(User);

            if (lead.OrganizationId != currentUser.Id)
            {
                return Forbid();
            }

            ViewData["CategoryId"] = new SelectList(_context.LeadCategory, "LeadCategoryId", "Title", lead.CategoryId);
            return View(lead);
        }

        // POST: Leads/ChangeCategory/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Sales")]
        public async Task<IActionResult> ChangeCategory(int id, int categoryId)
        {
            var lead = await _context.Lead.FindAsync(id);

            string organizationId = (await _context.Lead.FindAsync(id)).OrganizationId;

            IdentityUser currentUser = await _userManager.GetUserAsync(User);

            if (organizationId != currentUser.Id)
            {
                return Forbid();
            }

            if (categoryId > 0)
            {
                try
                {
                    lead.CategoryId = categoryId;
                    _context.Update(lead);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeadExists(lead.LeadId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { Id = id });
            } else
            {
                ModelState.AddModelError("categoryId", "Category is required");
            }
            ViewData["CategoryId"] = new SelectList(_context.LeadCategory, "LeadCategoryId", "Title", lead.CategoryId);
            return View(lead);
        }

        // GET: Leads/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lead == null)
            {
                return NotFound();
            }

            var lead = await _context.Lead
                .Include(l => l.Category)
                .Include(l => l.Organization)
                .FirstOrDefaultAsync(m => m.LeadId == id);
            if (lead == null)
            {
                return NotFound();
            }

            return View(lead);
        }

        // POST: Leads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lead == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Lead'  is null.");
            }
            var lead = await _context.Lead.FindAsync(id);
            if (lead != null)
            {
                _context.Lead.Remove(lead);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeadExists(int id)
        {
          return (_context.Lead?.Any(e => e.LeadId == id)).GetValueOrDefault();
        }
    }
}
