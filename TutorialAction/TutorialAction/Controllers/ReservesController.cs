using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace TutorialAction.Controllers
{
    [RoutePrefix("api/reserves")]
    public class ReservesController : ApiController
    {
        // GET: api/Reserves
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Reserves/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Reserves
        [Authorize(Roles = "teacher")]
        [Route("create")]
        [ResponseType(typeof(string))]
        public IHttpActionResult Post([FromBody]string value)
        {
            return Ok("POST /reserves/create");
        }

        // PUT: api/Reserves/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Reserves/5
        public void Delete(int id)
        {
        }
    }
}
