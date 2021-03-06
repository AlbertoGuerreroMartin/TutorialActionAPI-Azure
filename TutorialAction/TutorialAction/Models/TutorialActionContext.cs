﻿using Microsoft.AspNet.Identity.EntityFramework;
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
        public System.Data.Entity.DbSet<TutorialAction.Models.Reserve> Reserves { get; set; }
        public System.Data.Entity.DbSet<TutorialAction.Models.Timetable> Timetables { get; set; }
        public System.Data.Entity.DbSet<TutorialAction.Models.Tutorship> Tutorships { get; set; }
    }
}
