using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TutorialAction.Models
{
    public class UserToken
    {
        [Key]
        public string apiKey { get; set; }
        [Timestamp]
        public byte[] timestamp { get; set; }
        [ForeignKey("userID")]
        public User user { get; set; }
    }
}