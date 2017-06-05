using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TutorialAction.Models
{
    public class Tutorship
    {
        [Key]
        public int tutorshipID { get; set; }

        public string teacherID { get; set; }
        [ForeignKey("teacherID")]
        public virtual User teacher { get; set; }

        public string studentID { get; set; }
        [ForeignKey("studentID")]
        public virtual User student { get; set; }

        public int courseID { get; set; }
        [ForeignKey("courseID")]
        public virtual Course course { get; set; }

        public int? reserveID { get; set; }
        [ForeignKey("reserveID")]
        public virtual Reserve reserve { get; set; }

        public bool reserved { get; set; }
        public int tutorshipType { get; set; }
        public string motive { get; set; }
        public string date { get; set; }
        public string hour { get; set; }
        public string minutes { get; set; }
        public int duration { get; set; }
    }
}