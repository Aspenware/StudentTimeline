using System;
using System.Collections.Generic;

namespace StudentTimeline.CourseModel
{
    public class Course
    {
        CourseId _Id = new CourseId();
        /// <summary>
        /// Unique identifier for each user
        /// </summary>
        public CourseId Id
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
        public List<Guid> TaskIds { get; set; }

        /// <summary>
        /// User Ids
        /// </summary>
        public List<Guid> UserIds { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
    }
}
