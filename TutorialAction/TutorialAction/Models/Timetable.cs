using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TutorialAction.Models
{
    public class Timetable
    {
        [Key]
        public int timetableID { get; set; }

        public string teacherID { get; set; }
        [ForeignKey("teacherID")]
        public virtual User teacher { get; set; }

        public string date { get; set; }
        public string hour { get; set; }
        public int duration { get; set; }

        public static Func<Timetable, TimetableResponseViewModel> parseToReserveResponseViewModel()
        {
            return r => new TimetableResponseViewModel
            {
                timetableid = r.timetableID,
                date = r.date,
                hour = r.hour,
                duration = r.duration
            };
        }
    }

    public class TimetableResponseViewModel
    {
        public int timetableid;
        public string date;
        public string hour;
        public int duration;
    }

    public class TimetableParametersViewModel
    {
        public string date;
        public string hour;
        public int duration;
    }
}