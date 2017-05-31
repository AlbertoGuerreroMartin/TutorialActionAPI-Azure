namespace TutorialAction.Migrations
{
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
            //  This method will be called after migrating to the latest version.
            User jessica = new User
            {
                username = "jessica",
                password = "password",
                firstname = "Jessica",
                lastname = "Díaz Fernández",
                email = "jdiaz@etsisi.upm.es",
                role = "teacher"
            };

            User jennifer = new User
            {
                username = "jennifer",
                password = "password",
                firstname = "Jennifer",
                lastname = "Pérez Benedí",
                email = "jenifer.perez@etsisi.upm.es",
                role = "teacher"
            };

            User alberto = new User
            {
                username = "alberto",
                password = "password",
                firstname = "Alberto",
                lastname = "Guerrero Martín",
                email = "alberto.guerrero.martin@alumnos.upm.es",
                role = "student"
            };

            AuthRepository _repo = new AuthRepository();
            Task.Run(async () =>
                await Task.WhenAll(
                    _repo.RegisterUser(jessica),
                    _repo.RegisterUser(jennifer),
                    _repo.RegisterUser(alberto)
                )
            );

            context.Users.AddOrUpdate(jessica, jennifer, alberto);

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
