using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TutorialAction.Models
{
    public class Login
    {
        [Key]
        public int loginID { get; set; }

        [Index(IsUnique = true)]
        [StringLength(32)]
        public string apiKey { get; set; }

        public int? userID { get; set; }
        [ForeignKey("userID")]
        public virtual User user { get; set; }
    }
}