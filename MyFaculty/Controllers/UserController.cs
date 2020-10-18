using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MyFaculty.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index() => View(_userManager.Users.OrderBy(u => u.UserName).ToList());

        [HttpPost]
        public async Task<IActionResult> Delete(string userId)
        {
            IdentityUser user = await _userManager.FindByIdAsync(userId);
            if(user != null)
            {   
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserName(string userId, string newUserName)
        {
            IdentityUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                IdentityResult result = await _userManager.SetUserNameAsync(user, newUserName);
            }
            return RedirectToAction("Index");
        }

        
    }
}