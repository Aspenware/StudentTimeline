using System;
using System.Collections.Generic;

namespace StudentTimeline.TaskModel
{
    public class Task
    {

        TaskId _Id = new TaskId();
        /// <summary>
        /// Unique identifier for each user
        /// </summary>
        public TaskId Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value; ;
            }
        }

        /// <summary>
        /// User Ids
        /// </summary>
        public List<Guid> UserIds { get; set; }

        /// <summary>
        /// Course Id
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Task Type
        /// </summary>
        public TaskType TaskType { get; set; }

        /// <summary>
        /// Due Date
        /// </summary>
        public DateTime DueDate { get; set; }
    }
}
