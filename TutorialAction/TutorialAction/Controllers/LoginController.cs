using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TutorialAction.Models;

namespace TutorialAction.Controllers
{
    public class LoginController : ApiController
    {
        private TutorialActionContext db = new TutorialActionContext();

        // POST: api/Login
        public IHttpActionResult Post([FromBody]string value)
        {
            return Ok("Hello Login world");
        }
    }
}
