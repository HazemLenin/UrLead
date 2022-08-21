using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using UrLead.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace UrLead.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRolesController : Controller
    {
        private readonly SignInManager<IdentityUser> _singInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRolesController(SignInManager<IdentityUser> singInManager, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _singInManager = singInManager;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string id)
        {
            // Prepare the list of the roles with selected boolean value
            List<UserRolesViewModel> userRolesViewModelList = new List<UserRolesViewModel>();
            IdentityUser user = await _userManager.FindByIdAsync(id);

            // Create new UserRole for each role and add it to ManageUserRoles
            foreach(var role in _roleManager.Roles.ToList())
            {
                UserRolesViewModel userRolesViewModel = new UserRolesViewModel()
                {
                    RoleName = role.Name,
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }

                userRolesViewModelList.Add(userRolesViewModel);
            }

            ManageUserRolesViewModel manageUserRolesViewModel = new ManageUserRolesViewModel()
            {
                UserId = user.Id,
                UserRoles = userRolesViewModelList
            };

            return View(manageUserRolesViewModel);
        }

        public async Task<IActionResult> Update(string id, ManageUserRolesViewModel model)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            IList<string> roles = await _userManager.GetRolesAsync(user);

            // Remove all roles
            await _userManager.RemoveFromRolesAsync(user, roles);

            Console.WriteLine("################");
            Console.WriteLine(model);
            Console.WriteLine(model.UserRoles);
            Console.WriteLine("################");

            // Add selected roles if selected
            if (model.UserRoles.Count() > 0)
            {
                await _userManager
                .AddToRolesAsync(
                    user,
                    model.UserRoles
                    .Where(UserRole => UserRole.Selected)
                    .Select(UserRole => UserRole.RoleName)
                );
            }

            IdentityUser currentUser = await _userManager.GetUserAsync(HttpContext.User);

            return RedirectToAction(nameof(Index), new { id = id });
        }
    }
}
