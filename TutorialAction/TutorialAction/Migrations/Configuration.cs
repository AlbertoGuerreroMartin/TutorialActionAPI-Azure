namespace TutorialAction.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;

    internal sealed class Configuration : DbMigrationsConfiguration<TutorialAction.Models.TutorialActionContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TutorialAction.Models.TutorialActionContext context)
        {
            var userManager = new UserManager<User>(new UserStore<User>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            roleManager.Create(new IdentityRole("admin"));
            roleManager.Create(new IdentityRole("student"));
            roleManager.Create(new IdentityRole("teacher"));

            var jessica = new User
            {
                UserName = "jessica",
                Email = "jdiaz@etsisi.upm.es",
                firstname = "Jessica",
                lastname = "Díaz Fernández"
            };
            userManager.Create(jessica, "password");
            userManager.AddToRole(jessica.Id, "teacher");

            var jennifer = new User
            {
                UserName = "jennifer",
                Email = "jenifer.perez@etsisi.upm.es",
                firstname = "Jennifer",
                lastname = "Pérez Benedí"
            };
            userManager.Create(jennifer, "password");
            userManager.AddToRole(jennifer.Id, "teacher");

            var alberto = new User
            {
                UserName = "alberto",
                Email = "alberto170693@gmail.com",
                firstname = "Alberto",
                lastname = "Guerrero Martín"
            };
            userManager.Create(alberto, "password");
            userManager.AddToRole(alberto.Id, "student");

            var admin = new User
            {
                UserName = "admin",
                Email = "alberto170693@gmail.com",
                firstname = "Admin",
                lastname = "Admin Admin"
            };
            userManager.Create(admin, "password");
            userManager.AddToRole(admin.Id, "admin");
        }
    }
}
