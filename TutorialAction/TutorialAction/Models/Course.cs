using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutorialAction.Models
{
    public class Course
    {
        [Key]
        public int courseID { get; set; }
        public string courseName { get; set; }

        public virtual ICollection<User> users { get; set; }

        public Course()
        {
            courseID = 0;
            courseName = "";
            users = new List<User>();
        }
        public Course(int courseID, string courseName, List<User> users)
        {
            this.courseID = courseID;
            this.courseName = courseName;
            this.users = users;
        }
        public static Func<Course, Course> filterUsersByRole(string role, RoleManager<IdentityRole> roleManager)
        {
            var oppositeRole = role == "teacher" ? "student" : "teacher";
            return u => new Course(
                u.courseID,
                u.courseName,
                u.users.Where(u1 =>
                {
                    var roleName = roleManager.FindById(u1.Roles.First().RoleId).Name;
                    return roleName == oppositeRole;
                }).ToList()
            );
        }
    }
}