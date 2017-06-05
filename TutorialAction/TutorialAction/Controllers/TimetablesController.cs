using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TutorialAction.Models;

namespace TutorialAction.Controllers
{
    [RoutePrefix("api/timetables")]
    public class TimetablesController : ApiController
    {
        private TutorialActionContext tutorialActionContext = new TutorialActionContext();
        private UserManager<User> userManager { get; set; }

        public TimetablesController()
        {
            userManager = new UserManager<User>(new UserStore<User>(this.tutorialActionContext));
        }

        // GET: api/timetables
        [Authorize(Roles = "student,teacher")]
        [Route("{teacherID}")]
        [ResponseType(typeof(List<TimetableResponseViewModel>))]
        public IHttpActionResult Get(string teacherID)
        {
            return Ok(tutorialActionContext.Timetables
                .Where(t => t.teacherID == teacherID)
                .Select(Timetable.parseToReserveResponseViewModel())
                .ToList());
        }

        [Authorize(Roles = "teacher")]
        [Route("")]
        // POST: api/timetables
        public IHttpActionResult Post(List<TimetableParametersViewModel> parameters)
        {
            var currentUser = userManager.FindById(User.Identity.GetUserId());
            foreach (TimetableParametersViewModel timetableParameters in parameters)
            {
                var timetable = new Timetable
                {
                    teacherID = currentUser.Id,
                    date = timetableParameters.date,
                    hour = timetableParameters.hour,
                    duration = timetableParameters.duration
                };

                tutorialActionContext.Timetables.Add(timetable);
            }
            tutorialActionContext.SaveChanges();

            return Ok("Timetable values successfully inserted.");
        }

        // DELETE: api/timetables/5
        [Authorize(Roles = "teacher")]
        [Route("{timetableID:int}")]
        public IHttpActionResult Delete(int timetableID)
        {
            var currentUser = userManager.FindById(User.Identity.GetUserId());
            var entryToDelete = tutorialActionContext.Timetables.Find(timetableID);
            if (entryToDelete != null && entryToDelete.teacherID == currentUser.Id)
            {
                tutorialActionContext.Timetables.Remove(entryToDelete);
                tutorialActionContext.SaveChanges();
                return Ok("Timetable entry delete successfully.");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
