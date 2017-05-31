using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TutorialAction.Models;

namespace TutorialAction.Controllers
{
    public class LoginController : ApiController
    {
        private TutorialActionContext db = new TutorialActionContext();

        // GET: api/Login
        public IQueryable<Login> GetLogins()
        {
            return db.Logins;
        }

        /*
        // GET: api/Login/5
        [ResponseType(typeof(Login))]
        public async Task<IHttpActionResult> GetLogin(int id)
        {
            Login login = await db.Logins.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }

            return Ok(login);
        }
        */

        /*
        // PUT: api/Login/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLogin(int id, Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != login.loginID)
            {
                return BadRequest();
            }

            db.Entry(login).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginExists(id))
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
        */

        // POST: api/Login
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> PostLogin(string username, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            User user = db.Users.Where(
                u => u.username == username && u.password == password
            ).FirstOrDefault();

            if (user == null)
            {
                return Content(HttpStatusCode.NotFound, "The username '" + username + "' does not exist.");
            }

            string apiKey = GenerateApiKey();
            Login login = new Login(
                apiKey,
                user.userID
            );
            db.Logins.Add(login);
            await db.SaveChangesAsync();

            var response = new
            {
                role = user.role,
                api_key = apiKey
            };

            return Ok(response); //CreatedAtRoute("DefaultApi", new { id = login.loginID }, login);
        }

        private string GenerateApiKey()
        {
            string guid = Guid.NewGuid().ToString();
            return CalculateMD5Hash(guid);
        }

        private string CalculateMD5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }


        // DELETE: api/Login/5
        [ResponseType(typeof(Login))]
        public async Task<IHttpActionResult> DeleteLogin(int id)
        {
            Login login = await db.Logins.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }

            db.Logins.Remove(login);
            await db.SaveChangesAsync();

            return Ok(login);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoginExists(int id)
        {
            return db.Logins.Count(e => e.loginID == id) > 0;
        }
    }
}