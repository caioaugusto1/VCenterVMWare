using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VSphere.Models;
using VSphere.Models.Identity;

namespace VSphere.Controllers
{
    [Authorize(Roles = "Admin, Manager, Role")]
    public class RoleController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationIdentityUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationIdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.Where(x => x.Name != "Admin").ToListAsync();

            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            List<ApplicationIdentityUser> members = new List<ApplicationIdentityUser>();
            List<ApplicationIdentityUser> nonMember = new List<ApplicationIdentityUser>();

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var getUser = await _userManager.FindByIdAsync(userId);

            var getAllUser = await _userManager.Users.ToListAsync();

            foreach (ApplicationIdentityUser user in getAllUser)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name)
                    ? members
                    : nonMember;
                list.Add(user);
            }

            return View("Edit", new EditRoleViewModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMember
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ModifyRoleViewModel modifyRole)
        {
            IdentityResult result = new IdentityResult();

            if (ModelState.IsValid)
            {
                IdentityRole role = await _roleManager.FindByIdAsync(modifyRole.RoleId);
                role.Name = modifyRole.RoleName;
                result = await _roleManager.UpdateAsync(role);

                if (!result.Succeeded)
                    return BadRequest(result.Errors.SelectMany(x => x.Code));

                foreach (string userId in modifyRole.IdsToAdd ?? new string[] { })
                {
                    var user = _userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user.Result, modifyRole.RoleName);

                        if (!result.Succeeded)
                            return BadRequest(result.Errors.SelectMany(x => x.Code));
                    }
                }

                foreach (string userId in modifyRole.IdsToRemove ?? new string[] { })
                {
                    var user = _userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user.Result, modifyRole.RoleName);

                        if (!result.Succeeded)
                            return BadRequest(result.Errors.SelectMany(x => x.Code));
                    }
                }
            }

            return RedirectToAction("Index", "Role");
        }

    }
}