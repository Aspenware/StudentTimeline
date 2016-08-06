using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace StudentTimeline.TaskModel
{
    public interface ITaskSvc : IService
    {
        Task<Task> GetTaskByIdAsync(TaskId id);

        Task<List<TaskModel.Task>> GetAllTasksAsync(CancellationToken ct);

        Task<Task> CreateTaskAsync(Task userToCreate);

        Task<Task> UpdateTaskAsync(Task userToUpdate);
    }
}
