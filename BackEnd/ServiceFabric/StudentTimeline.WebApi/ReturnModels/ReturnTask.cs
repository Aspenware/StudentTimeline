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
    public class ReturnTask
    {
        private const string UserServiceName = "UserSvc";
        private const string CourseServiceName = "CourseSvc";

        public ReturnTask(TaskModel.Task task)
        {

            ServiceUriBuilder builderUsers = new ServiceUriBuilder(UserServiceName);
            IUserSvc userServiceClient = ServiceProxy.Create<IUserSvc>(builderUsers.ToUri());

            ServiceUriBuilder builderCourses = new ServiceUriBuilder(CourseServiceName);
            ICourseSvc courseServiceClient = ServiceProxy.Create<ICourseSvc>(builderCourses.ToUri());

            Id = new Guid(task.Id.ToString());
            Title = task.Title;
            Description = task.Description;
            DueDate = task.DueDate;
            this.TaskType = new ReturnTaskType(task.TaskType);
            Course = new ReturnCourseShort(courseServiceClient.GetCourseByIdAsync(new CourseId(task.CourseId.ToString())).Result);
            Users = new List<ReturnUser>();

            foreach (var thisId in task.UserIds)
            {
                Users.Add(new ReturnUser(userServiceClient.GetUserByIdAsync(new UserId(thisId.ToString())).Result));
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
        /// Due Date
        /// </summary>
        [JsonProperty(PropertyName = "dueDate")]
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Course Id
        /// </summary>
        [JsonProperty(PropertyName = "course")]
        public ReturnCourseShort Course { get; set; }

        /// <summary>
        /// Task Type
        /// </summary>
        [JsonProperty(PropertyName = "taskType")]
        public ReturnTaskType TaskType { get; set; }

        /// <summary>
        /// User Ids
        /// </summary>
        [JsonProperty(PropertyName = "users")]
        public List<ReturnUser> Users { get; set; }

    }
}
