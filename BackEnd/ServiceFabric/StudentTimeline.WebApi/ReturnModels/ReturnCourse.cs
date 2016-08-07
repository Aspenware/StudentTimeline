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
    public class ReturnCourse
    {
        private const string UserServiceName = "UserSvc";
        private const string TaskServiceName = "TaskSvc";

        public ReturnCourse(Course course)
        {

            ServiceUriBuilder builderUsers = new ServiceUriBuilder(UserServiceName);
            IUserSvc userServiceClient = ServiceProxy.Create<IUserSvc>(builderUsers.ToUri());

            ServiceUriBuilder builderTasks = new ServiceUriBuilder(TaskServiceName);
            ITaskSvc taskServiceClient = ServiceProxy.Create<ITaskSvc>(builderTasks.ToUri());

            Id = new Guid(course.Id.ToString());
            Title = course.Title;
            Description = course.Description;
            Users = new List<ReturnUser>();

            foreach (var thisId in course.UserIds)
            {
                Users.Add(new ReturnUser(userServiceClient.GetUserByIdAsync(new UserId(thisId.ToString())).Result));
            }

            Tasks = new List<ReturnTaskShort>();

            foreach (var thisId in course.TaskIds)
            {
                Tasks.Add(new ReturnTaskShort(taskServiceClient.GetTaskByIdAsync(new TaskId(thisId.ToString())).Result));
            }
        }

        /// <summary>
        /// Unique identifier for each task
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Users
        /// </summary>
        [JsonProperty(PropertyName = "users")]
        public List<ReturnUser> Users { get; set; }

        /// <summary>
        /// Tasks
        /// </summary>
        [JsonProperty(PropertyName = "tasks")]
        public List<ReturnTaskShort> Tasks { get; set; }

    }
}
