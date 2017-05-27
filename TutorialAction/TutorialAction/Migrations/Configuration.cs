namespace TutorialAction.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TutorialAction.Models.TutorialActionContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TutorialAction.Models.TutorialActionContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Users.AddOrUpdate(
                new User
                {
                    username = "jessica",
                    firstname = "Jessica",
                    lastname = "Díaz Fernández",
                    email = "jdiaz@etsisi.upm.es",
                    role = "teacher"
                },
                new User
                {
                    username = "jennifer",
                    firstname = "Jennifer",
                    lastname = "Pérez Benedí",
                    email = "jenifer.perez@etsisi.upm.es",
                    role = "teacher"
                },
                new User
                {
                    username = "alberto",
                    firstname = "Alberto",
                    lastname = "Guerrero Martín",
                    email = "alberto.guerrero.martin@alumnos.upm.es",
                    role = "student"
                }
            );

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
