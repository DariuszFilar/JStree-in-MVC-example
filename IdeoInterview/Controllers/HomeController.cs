using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdeoInterview.ViewModels;
using Microsoft.AspNet.Identity;
using IdeoInterview.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdeoInterview.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IdeoInterviewContext _context;
        public HomeController()
        {
            _context = new IdeoInterviewContext();
        }
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var userProfile = _context.UserProfile.FirstOrDefault(x => x.Id == userId);
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            if (userProfile != null)
            {
                UserProfileViewModel model = new UserProfileViewModel(userProfile);
                return View(model);
            }
            else
            {
                UserProfileViewModel model = new UserProfileViewModel(userId);
                return View(model);
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult Index(UserProfileViewModel model)
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            if (model.Role == "Admin")
            {
                UserManager.AddToRole(userId, "Admin");           
            }
            else
            {
                UserManager.RemoveFromRole(userId, "Admin");
            }            
                   
            if (user.UserProfile != null)
            {
                user.UserProfile.Role = model.Role;
            }
            else
            {
                user.UserProfile = new UserProfile { Id = model.Id, Role = model.Role };
            }
            _context.SaveChanges();

            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("login", "account");
        }
    }
}