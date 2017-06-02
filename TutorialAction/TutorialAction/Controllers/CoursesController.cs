using Microsoft.AspNet.Identity;
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
        private TutorialActionContext db = new TutorialActionContext();

        // GET: api/Courses
        public IQueryable<Course> GetCourses()
        {
            return db.Courses;
        }

        // GET: api/Courses/5
        [ResponseType(typeof(Course))]
        public async Task<IHttpActionResult> GetCourse(int id)
        {
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        // PUT: api/Courses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCourse(int id, Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != course.courseID)
            {
                return BadRequest();
            }

            db.Entry(course).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        /// <summary>
        /// Inserts a new course into DB.
        /// </summary>
        /// <param name="course">Course to insert. For users, only userID key is needed (user must existe before executing this request).</param>
        [ResponseType(typeof(Course))]
        public async Task<IHttpActionResult> PostCourse(Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (User user in course.users)
            {
                User userToUpdate = db.Users.Find(user.Id);
                if (userToUpdate != null)
                {
                    userToUpdate.courses.Add(course);
                    course.users.Add(userToUpdate);
                    //db.Users.AddOrUpdate(userToUpdate);
                    db.Entry(userToUpdate).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }

            db.Courses.Add(course);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = course.courseID }, course);
        }

        // DELETE: api/Courses/5
        [ResponseType(typeof(Course))]
        public async Task<IHttpActionResult> DeleteCourse(int id)
        {
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            db.Courses.Remove(course);
            await db.SaveChangesAsync();

            return Ok(course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CourseExists(int id)
        {
            return db.Courses.Count(e => e.courseID == id) > 0;
        }
    }
}