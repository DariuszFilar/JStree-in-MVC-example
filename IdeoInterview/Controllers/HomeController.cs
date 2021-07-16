using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdeoInterview.ViewModels;
using Microsoft.AspNet.Identity;
using IdeoInterview.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace IdeoInterview.Controllers
{
    //[Authorize] //TODO: Włączyć
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

        //[Authorize(Roles = "User")] //TODO: Włączyć
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

        [HttpGet]
        public ActionResult Tree()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Nodes()
        {
            var node = new List<JsTreeModel>(_context.JsTreeModel);
            return Json(node, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetLastNodeId()
        {
            var node = new List<JsTreeModel>(_context.JsTreeModel);
            var lastId = node.Last().id;

            return Json(lastId, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddNode(string name, string parent)
        {
            if (String.IsNullOrEmpty(parent))
            {
                parent = "#";
            }
            JsTreeModel test = new JsTreeModel { text = name, parent = parent };
            _context.JsTreeModel.Add(test);


            _context.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteNode(int[] ids)
        {
            foreach( var id in ids)
            {
               var idToRemove = _context.JsTreeModel.FirstOrDefault(x => x.id == id);
                _context.JsTreeModel.Remove(idToRemove);
            }
            _context.SaveChanges();

            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}