using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdeoInterview.ViewModels;
using Microsoft.AspNet.Identity;
using IdeoInterview.Models;

namespace IdeoInterview.Controllers
{
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
                UserProfileViewModel model = new UserProfileViewModel();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Index(UserProfileViewModel model)
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            if (user.UserProfile != null)
            {
                user.UserProfile.Role = model.Role;
            }
            else
            {
                user.UserProfile = new UserProfile { Id = model.Id, Role = model.Role };
            }
            _context.SaveChanges();
            return View(model);
        }
    }
}