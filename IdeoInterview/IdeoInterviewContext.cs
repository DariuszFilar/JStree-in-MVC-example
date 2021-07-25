using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using IdeoInterview.Models;

namespace IdeoInterview
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public partial class IdeoInterviewContext : IdentityDbContext<ApplicationUser>
    {
        public IdeoInterviewContext()
            : base("name=IdeoInterviewContext") { }

        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<JsTreeModel> JsTreeModel { get; set; }
        public virtual DbSet<Form> Form { get; set; }
        public static IdeoInterviewContext Create()
        {
            return new IdeoInterviewContext();
        }
    }
}
