using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Newtonsoft.Json;
using StudentTimeline.Common;
using StudentTimeline.CourseModel;
using StudentTimeline.TaskModel;
using StudentTimeline.UserModel;

namespace StudentTimeline.WebApi.ReturnModels
{
    public class ReturnUser
    {
        public ReturnUser(User user)
        {
            Id = new Guid(user.Id.ToString());
            Name = user.Name;
            Email = user.Email;
            Password = user.Password;
            Blurb = user.Blurb;
            ProfileImageUrl = user.ProfileImageUrl;
        }

        /// <summary>
        /// Unique identifier for each user
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        /// <summary>
        /// Blurb
        /// </summary>
        [JsonProperty(PropertyName = "blurb")]
        public string Blurb { get; set; }

        /// <summary>
        /// ProfileImageUrl
        /// </summary>
        [JsonProperty(PropertyName = "profileImageUrl")]
        public string ProfileImageUrl { get; set; }

    }
}
