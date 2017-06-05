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

        public bool reserved { get; set; }
        public int tutorshipType { get; set; }
        public string motive { get; set; }
        public string date { get; set; }
        public string hour { get; set; }
        public int duration { get; set; }

        public static Func<Tutorship, TutorshipResponseViewModel> parseToTutorshipResponseViewModel(string role)
        {
            return r => r.toTutorshipResponseViewModel(role);
        }

        public TutorshipResponseViewModel toTutorshipResponseViewModel(string role)
        {
            if (role == "student")
            {
                return new TutorshipResponseViewModel
                {
                    tutorshipid = tutorshipID,
                    firstname = teacher.firstname,
                    lastname = teacher.lastname,
                    email = teacher.Email,
                    tutorshipType = tutorshipType,
                    motive = motive,
                    date = date,
                    hour = hour,
                    duration = duration,
                    courseName = course.courseName
                };
            }
            else
            {
                return new TutorshipResponseViewModel
                {
                    tutorshipid = tutorshipID,
                    firstname = student.firstname,
                    lastname = student.lastname,
                    email = student.Email,
                    tutorshipType = tutorshipType,
                    motive = motive,
                    date = date,
                    hour = hour,
                    duration = duration,
                    courseName = course.courseName
                };
            }
        }
    }

    public class TutorshipResponseViewModel
    {
        public int tutorshipid;
        public string firstname;
        public string lastname;
        public string email;
        public int tutorshipType;
        public string motive;
        public string date;
        public string hour;
        public int duration;
        public string courseName;
    }

    public class RegisterTutorshipParametersViewModel
    {
        public string teacherID;
        public string studentID;
        public int courseID;
        public bool reserved;
        public int tutorshipType;
        public string motive;
        public string date;
        public string hour;
        public int duration;
    }
}