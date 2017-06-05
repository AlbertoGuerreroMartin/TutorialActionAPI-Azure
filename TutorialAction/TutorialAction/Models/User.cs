using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutorialAction.Models
{
    public class User : IdentityUser
    {
        public string firstname { get; set; }
        public string lastname { get; set; }

        // IMPORTANTE: Entity Framework doesn't detect that two courses with the same info are, in fact, the same. It just duplicates the courses.
        public virtual ICollection<Course> courses { get; set; } = new List<Course>();

        public static Func<User, JObject> userJsonParser()
        {
            return u => new JObject(new JProperty[] {
                new JProperty("userID", u.Id),
                new JProperty("username", u.UserName),
                new JProperty("firstname", u.firstname),
                new JProperty("lastname", u.lastname),
                new JProperty("email", u.Email),
                new JProperty("role", u.Roles.First())
            });
        }

        public static Func<User, UserResponseViewModel> parseToUserResponseViewModel(RoleManager<IdentityRole> roleManager)
        {
            return u => {
                var roleName = roleManager.FindById(u.Roles.First().RoleId).Name;
                return u.toUserResponseViewModel(roleName);
            };
        }

        public UserResponseViewModel toUserResponseViewModel(string role)
        {
            return new UserResponseViewModel
            {
                Id = Id,
                username = UserName,
                firstname = firstname,
                lastname = lastname,
                Email = Email,
                role = role,
                courses = courses.ToList()
            };
        }
    }

    public class UserRegisterViewModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string role { get; set; }
        public IList<string> courses { get; set; }  // List of courses IDs

        public User toUser()
        {
            var user = new User
            {
                UserName = username,
                Email = email,
                firstname = firstname,
                lastname = lastname
            };

            return user;
        }
    }

    public class UserResponseViewModel
    {
        public string Id;
        public string username;
        public string firstname;
        public string lastname;
        public string Email;
        public string role;
        public List<Course> courses;
    }
}