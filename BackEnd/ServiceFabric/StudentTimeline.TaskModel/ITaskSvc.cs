using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentTimeline.TaskModel
{
    public interface ITaskSvc
    {
        Task<Task> GetTaskByIDAsync(TaskId id);

        Task<List<Task>> GetTaskListByUserIDAsync(Guid userId);

        Task<List<Task>> GetTaskListByCourseIDAsync(Guid courseId);

        Task<bool> CreateTaskAsync(Task userToCreate);

        Task<bool> UpdateTaskAsync(Task userToUpdate);
    }
}
