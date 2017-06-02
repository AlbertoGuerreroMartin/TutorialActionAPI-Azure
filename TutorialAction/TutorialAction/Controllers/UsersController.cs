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
    [RoutePrefix("api/user")]
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

        // GET: api/user       <- DEBUG: returns all users
        [Route("")]
        [Authorize(Roles = "admin")]
        public IQueryable<User> GetUsers()
        {
            return tutorialActionContext.Users;
        }

        // GET: api/user/info
        [Authorize]
        [Route("info")]
        [ResponseType(typeof(User))]
        public User GetInfo()
        {
            var currentUser = userManager.FindById(User.Identity.GetUserId());
            var roleName = roleManager.FindById(currentUser.Roles.First().RoleId).Name;
            currentUser.courses = currentUser.courses.Select(Course.filterUsersByRole(roleName, roleManager)).ToList();     // Filter courses users of the opposite role
            return currentUser;
            // return new JObject(new JArray(db.Users.ToList().Select(Models.User.userJsonParser()))).ToString();
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = tutorialActionContext.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(string id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (id != user.Id)
            {
                return BadRequest();
            }
            
            tutorialActionContext.Entry(user).State = EntityState.Modified;

            try
            {
                await tutorialActionContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
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

        // POST: api/Login
        //[ResponseType(typeof(String))]
        //public IHttpActionResult PostLogin()
        //{
          //  return Ok("Hello PostLogin world");
        //}

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = tutorialActionContext.Users.Find(id);
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

        private bool UserExists(string id)
        {
            return tutorialActionContext.Users.Count(e => e.Id == id) > 0;
        }
    }
}