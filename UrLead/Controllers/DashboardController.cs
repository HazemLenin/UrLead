using Microsoft.AspNetCore.Mvc;
using UrLead.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using UrLead.ViewModels;
using UrLead.Models;

namespace UrLead.Controllers
{
    [Authorize(Roles = "Admin,Sales")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IdentityUser currentUser = await _userManager.GetUserAsync(User);

            List<LeadCategory> categories = await _context.LeadCategory.Include(c => c.Leads).ToListAsync();
            List<DashboardComponentViewModel> dashboardComponentViewModels = new List<DashboardComponentViewModel>();
            foreach (LeadCategory category in categories)
            {
                DashboardComponentViewModel dashboardComponentViewModel = new DashboardComponentViewModel();
                dashboardComponentViewModel.Title = category.Title;
                if (User.IsInRole("Sales"))
                {
                    dashboardComponentViewModel.Count = category.Leads.Where(l => l.OrganizationId == currentUser.Id).Count();
                } else
                {
                    dashboardComponentViewModel.Count = category.Leads.Count();
                }
                dashboardComponentViewModels.Add(dashboardComponentViewModel);
            }
            return View(dashboardComponentViewModels);
        }
    }
}
