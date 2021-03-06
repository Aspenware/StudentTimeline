﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using StudentTimeline.Common;
using StudentTimeline.UserModel;
using StudentTimeline.WebApi.ReturnModels;

namespace StudentTimeline.WebApi.Controllers
{
    public class UserController : ApiController
    {

        private const string UserServiceName = "UserSvc";

        // GET api/User 
        public IQueryable<ReturnUser> Get()
        {
            ServiceUriBuilder builder = new ServiceUriBuilder(UserServiceName);
            IUserSvc userServiceClient = ServiceProxy.Create<IUserSvc>(builder.ToUri());

            List<ReturnUser> returnUsers = new List<ReturnUser>();

            try
            {
                var users = userServiceClient.GetAllUsersAsync(CancellationToken.None).Result.AsQueryable();

                foreach (var user in users.Take(1000))
                {
                    returnUsers.Add(new ReturnUser(user));
                }

                return returnUsers.AsQueryable();
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("User Controller: Exception getting all users", ex);
                throw;
            }
        }

        // GET api/User/Guid
        public ReturnUser Get(string id)
        {
            UserId userId = new UserId(id);

            ServiceUriBuilder builder = new ServiceUriBuilder(UserServiceName);
            IUserSvc userServiceClient = ServiceProxy.Create<IUserSvc>(builder.ToUri());

            try
            {
                return new ReturnUser(userServiceClient.GetUserByIdAsync(userId).Result);
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("User Controller: Exception getting {0}: {1}", id, ex);
                throw;
            }
        }

        // POST api/User 
        public Task<User> Post(User thisUser)
        {
            ServiceUriBuilder builder = new ServiceUriBuilder(UserServiceName);
            IUserSvc userServiceClient = ServiceProxy.Create<IUserSvc>(builder.ToUri());

            try
            {
                return userServiceClient.CreateUserAsync(thisUser);
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("User Controller: Exception creating {0}: {1}", thisUser, ex);
                throw;
            }
        }

        // PUT api/User 
        public Task<User> Put(User thisUser)
        {
            ServiceUriBuilder builder = new ServiceUriBuilder(UserServiceName);
            IUserSvc userServiceClient = ServiceProxy.Create<IUserSvc>(builder.ToUri());

            try
            {
                return userServiceClient.UpdateUserAsync(thisUser);
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("User Controller: Exception updating {0}: {1}", thisUser, ex);
                throw;
            }
        }
    }
}
