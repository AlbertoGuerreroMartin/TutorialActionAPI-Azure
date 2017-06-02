using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TutorialAction.Models
{
    public class TutorialActionContext : IdentityDbContext<User>
    {
        public TutorialActionContext() : base("name=TutorialActionContext")
        {
        }

        public System.Data.Entity.DbSet<TutorialAction.Models.Course> Courses { get; set; }
    }
}
