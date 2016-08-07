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
    public class ReturnCourseShort
    {

        public ReturnCourseShort(Course course)
        {
            Id = new Guid(course.Id.ToString());
            Title = course.Title;
            Description = course.Description;
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

    }
}
