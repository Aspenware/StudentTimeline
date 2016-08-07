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
    public class ReturnTaskShort
    {
        public ReturnTaskShort(TaskModel.Task task)
        {

            Id = new Guid(task.Id.ToString());
            Title = task.Title;
            Description = task.Description;
            DueDate = task.DueDate;
            this.TaskType = new ReturnTaskType(task.TaskType);
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
        /// Task Type
        /// </summary>
        [JsonProperty(PropertyName = "taskType")]
        public ReturnTaskType TaskType { get; set; }
    }
}
