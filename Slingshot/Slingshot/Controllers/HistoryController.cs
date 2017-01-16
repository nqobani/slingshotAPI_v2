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
    [RoutePrefix("api/history")]
    public class HistoryController : ApiController
    {
        UserService obj = new UserService();
        [Route("getuserhistory")]
        public IEnumerable<History> GetUserHistory(long userId)
        {
            return obj.GetUserHistory(userId);
        }
    }
}
