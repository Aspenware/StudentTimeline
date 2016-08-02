using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using StudentTimeline.Common;
using StudentTimeline.UserModel;

namespace StudentTimeline.WebApi.Controllers
{
    public class UserController : ApiController
    {

        private const string UserServiceName = "UserSvc";

        // GET api/User 
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/User/5 
        public Task<User> Get(string id)
        {
            UserId userId = new UserId(id);

            ServiceUriBuilder builder = new ServiceUriBuilder(UserServiceName);
            IUserSvc userServiceClient = ServiceProxy.Create<IUserSvc>(builder.ToUri());

            try
            {
                return userServiceClient.GetUserByIDAsync(userId);
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("User Controller: Exception getting {0}: {1}", id, ex);
                throw;
            }
        }

        // POST api/User 
        public bool Post(User thisUser)
        {
            ServiceUriBuilder builder = new ServiceUriBuilder(UserServiceName);
            IUserSvc userServiceClient = ServiceProxy.Create<IUserSvc>(builder.ToUri());

            try
            {
                return userServiceClient.CreateUserAsync(thisUser).Result;
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("User Controller: Exception creating {0}: {1}", thisUser, ex);
                throw;
            }
        }

        // PUT api/User/5 
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}
