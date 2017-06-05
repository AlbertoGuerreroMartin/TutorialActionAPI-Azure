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
    [RoutePrefix("api/tutorships")]
    public class TutorshipsController : ApiController
    {
        private TutorialActionContext tutorialActionContext = new TutorialActionContext();
        private UserManager<User> userManager { get; set; }
        private RoleManager<IdentityRole> roleManager { get; set; }

        public TutorshipsController()
        {
            userManager = new UserManager<User>(new UserStore<User>(this.tutorialActionContext));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(this.tutorialActionContext));
        }

        // GET: api/tutorships
        [Authorize(Roles = "student,teacher")]
        [Route("")]
        [ResponseType(typeof(List<TutorshipResponseViewModel>))]
        public List<TutorshipResponseViewModel> Get()
        {
            var currentUser = userManager.FindById(User.Identity.GetUserId());
            var userRole = roleManager.FindById(currentUser.Roles.First().RoleId).Name;
            if (userRole == "teacher")
            {
                return tutorialActionContext.Tutorships
                    .Where(t => t.teacherID == currentUser.Id)
                    .Select(Tutorship.parseToTutorshipResponseViewModel(userRole))
                    .ToList();
            }
            else if (userRole == "student")
            {
                return tutorialActionContext.Tutorships
                    .Where(r => r.studentID == currentUser.Id)
                    .Select(Tutorship.parseToTutorshipResponseViewModel(userRole))
                    .ToList();
            }
            else
            {
                return new List<TutorshipResponseViewModel>();
            }
        }

        // POST: api/tutorships/create
        [Authorize(Roles = "teacher")]
        [Route("")]
        [ResponseType(typeof(GenericResponseViewModel))]
        public GenericResponseViewModel Post(RegisterTutorshipParametersViewModel parameters)
        {
            var currentUser = userManager.FindById(User.Identity.GetUserId());
            var tutorship = new Tutorship
            {
                teacherID = parameters.teacherID,
                studentID = currentUser.Id,
                courseID = parameters.courseID,
                reserved = parameters.reserved,
                tutorshipType = parameters.tutorshipType,
                motive = parameters.motive,
                date = parameters.date,
                hour = parameters.hour,
                duration = parameters.duration
            };
            tutorialActionContext.Tutorships.Add(tutorship);
            tutorialActionContext.SaveChanges();

            return new GenericResponseViewModel
            {
                statusCode = "200",
                message = "Tutoría registrada correctamente."
            };
        }
    }
}
