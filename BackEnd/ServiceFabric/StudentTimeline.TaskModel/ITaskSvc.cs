using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace StudentTimeline.TaskModel
{
    public interface ITaskSvc : IService
    {
        Task<Task> GetTaskByIdAsync(TaskId id);

        Task<List<Task>> GetTaskListByUserIdAsync(Guid userId);

        Task<List<Task>> GetTaskListByCourseIdAsync(Guid courseId);

        Task<bool> CreateTaskAsync(Task userToCreate);

        Task<bool> UpdateTaskAsync(Task userToUpdate);
    }
}
