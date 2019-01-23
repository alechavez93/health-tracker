using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebHost.Application;
using WebHost.Models;

namespace WebHost.Controllers
{
    /// <summary>
    /// The API controller providing user related service endpoints
    /// </summary>
    [RoutePrefix("webhost/v1/users")]
    public class UserController : ApiController
    {
        private readonly IUserService appService;

        public UserController(IUserService appService)
            : base()
        {
            this.appService = appService;
        }

        [HttpGet]
        [Route("{userId}")]
        [ResponseType(typeof(User))]
        public async Task<HttpResponseMessage> GetUser(Guid userId)
        {
            var data = await this.appService.FindOne(userId);

            var response = (data != null) ?
                this.Request.CreateResponse(HttpStatusCode.OK, data) :
                this.Request.CreateResponse(HttpStatusCode.NotFound);

            return response;
        }
    }
}
