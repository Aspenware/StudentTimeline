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
    public class ReturnTaskType
    {
        public ReturnTaskType(TaskModel.TaskType taskType)
        {
            Id = taskType.TaskTypeId;
            Title = taskType.Title;
        }

        /// <summary>
        /// Unique identifier for each Task Type
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}
