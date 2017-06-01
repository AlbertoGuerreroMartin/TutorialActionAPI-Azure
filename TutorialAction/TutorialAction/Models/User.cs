using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutorialAction.Models
{
    public class User
    {
        [Key]
        public int userID { get; set; }
        public string username { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }

        public string firstname { get; set; }
        public string lastname { get; set; }
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        public string role { get; set; }

        public static Func<User, JObject> userJsonParser()
        {
            return u => new JObject(new JProperty[] {
                new JProperty("userID", u.userID),
                new JProperty("username", u.username),
                new JProperty("firstname", u.firstname),
                new JProperty("lastname", u.lastname),
                new JProperty("email", u.email),
                new JProperty("role", u.role)
            });
        }
    }
}