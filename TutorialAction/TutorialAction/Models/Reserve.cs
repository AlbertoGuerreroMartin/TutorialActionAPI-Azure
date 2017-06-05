using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TutorialAction.Models
{
    public class Reserve
    {
        [Key]
        public int reserveID { get; set; }

        public string teacherID { get; set; }
        [ForeignKey("teacherID")]
        public virtual User teacher { get; set; }

        public string studentID { get; set; }
        [ForeignKey("studentID")]
        public virtual User student { get; set; }

        public int courseID { get; set; }
        [ForeignKey("courseID")]
        public virtual Course course { get; set; }

        public int tutorshipType { get; set; }
        public string motive { get; set; }
        public string date { get; set; }
        public string hour { get; set; }

        public static Func<Reserve, ReserveResponseViewModel> parseToReserveResponseViewModel(string role)
        {
            return r => r.toReserveResponseViewModel(role);
        }

        public ReserveResponseViewModel toReserveResponseViewModel(string role)
        {
            if(role == "student")
            {
                return new ReserveResponseViewModel
                {
                    reserveid = reserveID,
                    courseid = courseID,
                    firstname = teacher.firstname,
                    lastname = teacher.lastname,
                    email = teacher.Email,
                    tutorshipType = tutorshipType,
                    reason = motive,
                    date = date,
                    hour = hour,
                    courseName = course.courseName
                };
            }
            else
            {
                return new ReserveResponseViewModel
                {
                    reserveid = reserveID,
                    courseid = courseID,
                    firstname = student.firstname,
                    lastname = student.lastname,
                    email = student.Email,
                    tutorshipType = tutorshipType,
                    reason = motive,
                    date = date,
                    hour = hour,
                    courseName = course.courseName
                };
            }
        }
    }

    public class CreateReserveParametersViewModel
    {
        public string teacherID;
        public int courseID;
        public int tutorshipType;
        public string motive;
        public string date;
        public string hour;
    }

    public class ReserveResponseViewModel
    {
        public int reserveid;
        public int courseid;
        public string firstname;
        public string lastname;
        public string email;
        public int tutorshipType;
        public string reason;
        public string date;
        public string hour;
        public string courseName;
    }

    public class ReserveCompletionParametersViewModel
    {
        public int reserveID;
        public int duration;
    }
}