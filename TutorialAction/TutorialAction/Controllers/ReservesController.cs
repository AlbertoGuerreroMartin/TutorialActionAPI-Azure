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
    [RoutePrefix("api/reserves")]
    public class ReservesController : ApiController
    {
        private TutorialActionContext tutorialActionContext = new TutorialActionContext();
        private UserManager<User> userManager { get; set; }
        private RoleManager<IdentityRole> roleManager { get; set; }

        public ReservesController()
        {
            userManager = new UserManager<User>(new UserStore<User>(this.tutorialActionContext));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(this.tutorialActionContext));
        }

        // GET: api/reserves
        [Authorize(Roles = "student,teacher")]
        [Route("")]
        [ResponseType(typeof(List<ReserveResponseViewModel>))]
        public List<ReserveResponseViewModel> GetReserves()
        {
            var currentUser = userManager.FindById(User.Identity.GetUserId());
            var userRole = roleManager.FindById(currentUser.Roles.First().RoleId).Name;
            if (userRole == "teacher")
            {
                return tutorialActionContext.Reserves
                    .Where(r => r.teacherID == currentUser.Id)
                    .Select(Reserve.parseToReserveResponseViewModel(userRole))
                    .ToList();                
            }
            else if (userRole == "student")
            {
                return tutorialActionContext.Reserves
                    .Where(r => r.studentID == currentUser.Id)
                    .Select(Reserve.parseToReserveResponseViewModel(userRole))
                    .ToList();
            }
            else
            {
                return new List<ReserveResponseViewModel>();
            }
        }

        // POST: api/reserves/create
        [Authorize(Roles = "student")]
        [Route("create")]
        [ResponseType(typeof(GenericResponseViewModel))]
        public GenericResponseViewModel PostCreateReserve(CreateReserveParametersViewModel parameters)
        {
            var currentUser = userManager.FindById(User.Identity.GetUserId());
            var reserve = new Reserve
            {
                teacherID = parameters.teacherID.ToString(),
                studentID = currentUser.Id,
                courseID = parameters.courseID,
                tutorshipType = parameters.tutorshipType,
                motive = parameters.motive,
                date = parameters.date,
                hour = parameters.hour
            };
            tutorialActionContext.Reserves.Add(reserve);
            tutorialActionContext.SaveChanges();

            return new GenericResponseViewModel
            {
                statusCode = "200",
                message = "Tutoría reservada correctamente."
            };
        }

        // POST: api/reserves
        [Authorize(Roles = "teacher")]
        [Route("complete")]
        [ResponseType(typeof(GenericResponseViewModel))]
        public IHttpActionResult Post(ReserveCompletionParametersViewModel parameters)
        {
            var currentUser = userManager.FindById(User.Identity.GetUserId());
            var reserve = tutorialActionContext.Reserves.Find(parameters.reserveID);

            if(reserve != null && reserve.teacherID == currentUser.Id)
            {
                var tutorship = new Tutorship
                {
                    teacherID = reserve.teacherID,
                    studentID = reserve.studentID,
                    courseID = reserve.courseID,
                    reserved = true,
                    tutorshipType = reserve.tutorshipType,
                    motive = reserve.motive,
                    date = reserve.date,
                    hour = reserve.hour,
                    duration = parameters.duration
                };

                tutorialActionContext.Reserves.Remove(reserve);
                tutorialActionContext.Tutorships.Add(tutorship);
                tutorialActionContext.SaveChanges();

                return Ok(new GenericResponseViewModel
                {
                    statusCode = "200",
                    message = "Reserva completada correctamente."
                });
            }
            else
            {
                return Ok(new GenericResponseViewModel
                {
                    statusCode = "400",
                    message = "Error en la consulta: no existe una reserva con ese ID"
                });
            }
        }

        // DELETE: api/reserves/5
        [Authorize(Roles = "student")]
        [Route("{reserveID:int}")]
        [ResponseType(typeof(ReserveResponseViewModel))]
        public IHttpActionResult Delete(int reserveID)
        {
            var currentUser = userManager.FindById(User.Identity.GetUserId());
            var userRole = roleManager.FindById(currentUser.Roles.First().RoleId).Name;
            var reserveToDelete = tutorialActionContext.Reserves.Find(reserveID);
            if(reserveToDelete != null)
            {
                var reserveViewModel = reserveToDelete.toReserveResponseViewModel(userRole);
                tutorialActionContext.Reserves.Remove(reserveToDelete);
                tutorialActionContext.SaveChanges();
                return Ok(reserveViewModel);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
