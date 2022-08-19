using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace UrLead.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IdentityUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            List<IdentityUser> usersExceptCurrent = await _userManager.Users.ToListAsync();
            return View(usersExceptCurrent);
        }
    }
}
