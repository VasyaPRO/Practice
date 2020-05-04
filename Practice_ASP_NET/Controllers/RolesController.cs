using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Practice_ASP_NET.Models;
using Practice_ASP_NET.ViewModels;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Authorization;

namespace Practice_ASP_NET.Controllers
{
    [Authorize(Roles = "admin")]
    public class RolesController : Controller
    {
        RoleManager<Role> roleManager;
        UserManager<User> userManager;
        public RolesController(RoleManager<Role> roleMgr, UserManager<User> userMgr)
        {
            roleManager = roleMgr;
            userManager = userMgr;
        }
        public IActionResult UserList()
        {
            return View(userManager.Users.ToList());
        }

        public async Task<IActionResult> Edit(string userID)
        {
            User user = await userManager.FindByIdAsync(userID);
            if (user != null)
            {
                EditUserViewModel model = new EditUserViewModel
                {
                    UserID = user.Id,
                    Active = user.Active
                };
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userID, bool active)
        {
            User user = await userManager.FindByIdAsync(userID);
            if (user != null)
            {
                user.Active = active;
                await userManager.UpdateAsync(user);
                return RedirectToAction("UserList");
            }
            return NotFound();
        }
    }
}