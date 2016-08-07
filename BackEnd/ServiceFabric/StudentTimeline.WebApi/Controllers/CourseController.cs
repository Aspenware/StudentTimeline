using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using StudentTimeline.Common;
using StudentTimeline.CourseModel;

namespace StudentTimeline.WebApi.Controllers
{
    public class CourseController : ApiController
    {

        private const string CourseServiceName = "CourseSvc";

        // GET api/Course 
        public IQueryable<Course> Get()
        {
            ServiceUriBuilder builder = new ServiceUriBuilder(CourseServiceName);
            ICourseSvc courseServiceClient = ServiceProxy.Create<ICourseSvc>(builder.ToUri());

            try
            {
                return courseServiceClient.GetAllCoursesAsync(CancellationToken.None).Result.AsQueryable();
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("Course Controller: Exception getting all Courses", ex);
                throw;
            }
        }

        // GET api/Course/Guid
        public Task<Course> Get(string id)
        {
            CourseId courseId = new CourseId(id);

            ServiceUriBuilder builder = new ServiceUriBuilder(CourseServiceName);
            ICourseSvc courseServiceClient = ServiceProxy.Create<ICourseSvc>(builder.ToUri());

            try
            {
                return courseServiceClient.GetCourseByIdAsync(courseId);
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("Course Controller: Exception getting {0}: {1}", id, ex);
                throw;
            }
        }

        // POST api/Course 
        public Task<Course> Post(Course thisCourse)
        {
            ServiceUriBuilder builder = new ServiceUriBuilder(CourseServiceName);
            ICourseSvc courseServiceClient = ServiceProxy.Create<ICourseSvc>(builder.ToUri());

            try
            {
                return courseServiceClient.CreateCourseAsync(thisCourse);
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("Course Controller: Exception creating {0}: {1}", thisCourse, ex);
                throw;
            }
        }

        // PUT api/Course 
        public Task<Course> Put(Course thisCourse)
        {
            ServiceUriBuilder builder = new ServiceUriBuilder(CourseServiceName);
            ICourseSvc courseServiceClient = ServiceProxy.Create<ICourseSvc>(builder.ToUri());

            try
            {
                return courseServiceClient.UpdateCourseAsync(thisCourse);
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("Course Controller: Exception updating {0}: {1}", thisCourse, ex);
                throw;
            }
        }
    }
}
