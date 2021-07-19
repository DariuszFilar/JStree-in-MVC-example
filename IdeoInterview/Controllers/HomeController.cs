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
            int lastId = 0;
            var node = new List<JsTreeModel>(_context.JsTreeModel);

            var test = node.Where(x => x.id == 0).Count();
            if (node.Count() > 0)
            {
                lastId = node.Last().id;
            }

            return Json(lastId, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddFolder(string name, string parent, string type)
        {
            if (String.IsNullOrEmpty(parent))
            {
                parent = "#";
            }

            var parentNode = _context.JsTreeModel.FirstOrDefault(x => x.id.ToString() == parent);
            if (parentNode?.type != "file")
            {
                JsTreeModel test = new JsTreeModel { text = name, parent = parent, type = type };
                _context.JsTreeModel.Add(test);

            }
            _context.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddForm(string name, string parent, string type)
        {
            if (String.IsNullOrEmpty(parent))
            {
                parent = "#";
            }

            var parentNode = _context.JsTreeModel.FirstOrDefault(x => x.id.ToString() == parent);
            if (parentNode?.type == "default")
            {
                JsTreeModel newForm = new JsTreeModel { text = name, parent = parent, type = type };
                _context.JsTreeModel.Add(newForm);
            }

            _context.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteNode(int[] ids)
        {
            if (!ids.Contains(0))
            {
                foreach (var id in ids)
                {
                    var idToRemove = _context.JsTreeModel.FirstOrDefault(x => x.id == id);
                    _context.JsTreeModel.Remove(idToRemove);
                }
                _context.SaveChanges();
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult MoveNode(int id, string parentId)
        {
            var nodeToChange = _context.JsTreeModel.SingleOrDefault(x => x.id == id);
            if (nodeToChange != null)
            {
                nodeToChange.parent = parentId;
                _context.SaveChanges();
            }

            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}