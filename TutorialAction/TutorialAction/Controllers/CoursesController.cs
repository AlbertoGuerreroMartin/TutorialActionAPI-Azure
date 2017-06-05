using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TutorialAction.Models;

namespace TutorialAction.Controllers
{
    [RoutePrefix("api/courses")]
    [Authorize(Roles = "admin")]
    public class CoursesController : ApiController
    {
        private TutorialActionContext tutorialActionContext = new TutorialActionContext();

        // GET: api/courses
        [Route("")]
        [ResponseType(typeof(List<Course>))]
        public IQueryable<Course> GetCourses()
        {
            return tutorialActionContext.Courses;
        }

        // GET: api/courses/5
        [Route("{courseID:int}")]
        [ResponseType(typeof(Course))]
        public async Task<IHttpActionResult> GetCourse(int courseID)
        {
            Course course = await tutorialActionContext.Courses.FindAsync(courseID);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        // POST: api/courses
        /// <summary>
        /// Inserts a new course into DB.
        /// </summary>
        /// <param name="course">Course to insert. For users, only userID key is needed (user must existe before executing this request).</param>
        [Route("")]
        [ResponseType(typeof(Course))]
        public async Task<IHttpActionResult> PostCourse(CourseRegisterViewModel courseRegisterViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = new Course
            {
                courseName = courseRegisterViewModel.courseName
            };
            foreach (var userID in courseRegisterViewModel.users)
            {
                User user = tutorialActionContext.Users.Find(userID);
                if (user != null)
                {
                    course.users.Add(user);
                }
            }

            tutorialActionContext.Courses.Add(course);
            await tutorialActionContext.SaveChangesAsync();

            return Ok("Course registered successfully.");
        }

        // DELETE: api/courses/5
        [Route("{courseID:int}")]
        [ResponseType(typeof(Course))]
        public async Task<IHttpActionResult> DeleteCourse(int courseID)
        {
            Course course = await tutorialActionContext.Courses.FindAsync(courseID);
            if (course == null)
            {
                return NotFound();
            }

            tutorialActionContext.Courses.Remove(course);
            await tutorialActionContext.SaveChangesAsync();

            return Ok(course);
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