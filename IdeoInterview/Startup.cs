using IdeoInterview.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Collections.Generic;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(IdeoInterview.Startup))]
namespace IdeoInterview
{
    public partial class Startup
    {
        private IdeoInterviewContext _context;
        public Startup()
        {
            _context = new IdeoInterviewContext();
        }
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
            createFirstFolder();
        }
        private void createRolesandUsers()
        {

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("User"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
        }

        public void createFirstFolder()
        {            
            List<JsTreeModel> nodes = _context.JsTreeModel.ToList();
            if (nodes?.Count < 1)
            {
                JsTreeModel firstFolder = new JsTreeModel { text = "Folder", parent = "#", type = "default" };                
                _context.JsTreeModel.Add(firstFolder);                
                _context.SaveChanges();
            }
            if (nodes?.Count < 2)
            {
                JsTreeModel secondFolder = new JsTreeModel { text = "Folder", parent = "#", type = "default" };
                _context.JsTreeModel.Add(secondFolder);
                _context.SaveChanges();
            }
        }
    }
}
