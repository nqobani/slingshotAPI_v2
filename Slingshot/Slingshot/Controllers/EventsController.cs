using Slingshot.Data.Models;
using Slingshot.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Slingshot.Controllers
{
    [RoutePrefix("api/event")]
    public class EventsController : ApiController
    {
        UserService obj = new UserService();
        [Route("add")]
        public Event createEvent(long creatorId, string title, string location, DateTime startDateTime, DateTime endDateTime)
        {
            Initilaizer init = new Initilaizer();
            init.MigrateToLatest();
            return obj.CreateEvent( creatorId, title, location, startDateTime, endDateTime);
        }
    }
}
