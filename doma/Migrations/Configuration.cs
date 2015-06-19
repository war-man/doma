namespace doma.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using doma.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<doma.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(doma.Models.ApplicationDbContext context)
        {
            // Create Role
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "User" };

                manager.Create(role);
            }

            //Create Admin Account
            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "admin" };

                manager.Create(user, "adminhieu");
                manager.AddToRole(user.Id, "admin");
            }
        }
    }
}
