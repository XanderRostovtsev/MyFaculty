using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Data.Storage;

namespace MyFaculty.Controllers
{
    public class HomeController : Controller
    {   
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "User");
            if (User.IsInRole("Teacher"))
                return RedirectToAction("Index", "Teacher");
            if (User.IsInRole("Student"))
                return RedirectToAction("Index", "Student");
            return LocalRedirect("~/Identity/Account/Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
