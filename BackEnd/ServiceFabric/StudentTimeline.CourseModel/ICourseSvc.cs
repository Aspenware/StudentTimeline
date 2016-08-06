using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace StudentTimeline.CourseModel
{
    public interface ICourseSvc : IService
    {
        Task<Course> GetCourseByIdAsync(CourseId id);

        Task<List<Course>> GetAllCoursesAsync(CancellationToken ct);

        Task<Course> CreateCourseAsync(Course userToCreate);

        Task<Course> UpdateCourseAsync(Course userToUpdate);
    }
}
