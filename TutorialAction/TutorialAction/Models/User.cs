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
        public string firstname { get; set; }
        public string lastname { get; set; }
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        public string role { get; set; }
    }
}