using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TutorialAction.Models;

namespace TutorialAction.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private TutorialActionContext tutorialActionContext = new TutorialActionContext();
        private UserManager<User> userManager { get; set; }
        private RoleManager<IdentityRole> roleManager { get; set; }

        public UsersController()
        {
            userManager = new UserManager<User>(new UserStore<User>(this.tutorialActionContext));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(this.tutorialActionContext));
        }

        // GET: api/users/all       <- DEBUG: returns all users
        [Route("all")]
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(List<User>))]
        public List<UserResponseViewModel> GetUsers()
        {
            return tutorialActionContext.Users
                .Select(Models.User.parseToUserResponseViewModel(roleManager))
                .ToList();
        }

        // GET: api/users/info
        [Authorize]
        [Route("info")]
        [ResponseType(typeof(UserResponseViewModel))]
        public UserResponseViewModel GetInfo()
        {
            var currentUser = userManager.FindById(User.Identity.GetUserId());
            var roleName = roleManager.FindById(currentUser.Roles.First().RoleId).Name;
            currentUser.courses = currentUser.courses.Select(Course.filterUsersByRole(roleName, roleManager)).ToList();     // Filter courses users of the opposite role
            return currentUser.toUserResponseViewModel(roleName);
            // return new JObject(new JArray(db.Users.ToList().Select(Models.User.userJsonParser()))).ToString();
        }

        // POST: api/users
        [Authorize(Roles = "admin")]
        [Route("")]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(UserRegisterViewModel userRegisterViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = userRegisterViewModel.toUser();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(tutorialActionContext));
            var identityRole = roleManager.FindByName(userRegisterViewModel.role);
            if(identityRole == null)
            {
                return BadRequest("Role '" + userRegisterViewModel.role + "' is not correct.");
            }

            userManager.Create(user, userRegisterViewModel.password);
            userManager.AddToRole(user.Id, userRegisterViewModel.role);

            return Ok("User '" + userRegisterViewModel.username + "' registered successfully.");
        }

        // DELETE: api/users/5
        [Authorize(Roles = "admin")]
        [Route("{userID:int}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int userID)
        {
            User user = tutorialActionContext.Users.Find(userID);
            if (user == null)
            {
                return NotFound();
            }

            tutorialActionContext.Users.Remove(user);
            tutorialActionContext.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                tutorialActionContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}