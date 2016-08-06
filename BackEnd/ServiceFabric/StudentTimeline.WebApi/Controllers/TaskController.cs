using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using StudentTimeline.Common;

namespace StudentTimeline.WebApi.Controllers
{
    public class TaskController : ApiController
    {
        private const string TaskServiceName = "TaskSvc";

        // GET api/Task 
        public IQueryable<TaskModel.Task> Get()
        {
            ServiceUriBuilder builder = new ServiceUriBuilder(TaskServiceName);
            TaskModel.ITaskSvc taskServiceClient = ServiceProxy.Create<TaskModel.ITaskSvc>(builder.ToUri());

            try
            {
                return taskServiceClient.GetAllTasksAsync(CancellationToken.None).Result.AsQueryable();
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("Task Controller: Exception getting all Tasks", ex);
                throw;
            }
        }

        // GET api/Task/Guid
        public Task<TaskModel.Task> Get(string id)
        {
            TaskModel.TaskId taskId = new TaskModel.TaskId(id);

            ServiceUriBuilder builder = new ServiceUriBuilder(TaskServiceName);
            TaskModel.ITaskSvc taskServiceClient = ServiceProxy.Create<TaskModel.ITaskSvc>(builder.ToUri());

            try
            {
                return taskServiceClient.GetTaskByIdAsync(taskId);
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("Task Controller: Exception getting {0}: {1}", id, ex);
                throw;
            }
        }

        // POST api/Task 
        public Task<TaskModel.Task> Post(TaskModel.Task thisTask)
        {
            ServiceUriBuilder builder = new ServiceUriBuilder(TaskServiceName);
            TaskModel.ITaskSvc taskServiceClient = ServiceProxy.Create<TaskModel.ITaskSvc>(builder.ToUri());

            try
            {
                return taskServiceClient.CreateTaskAsync(thisTask);
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("Task Controller: Exception creating {0}: {1}", thisTask, ex);
                throw;
            }
        }

        // PUT api/Task 
        public Task<TaskModel.Task> Put(TaskModel.Task thisTask)
        {
            ServiceUriBuilder builder = new ServiceUriBuilder(TaskServiceName);
            TaskModel.ITaskSvc taskServiceClient = ServiceProxy.Create<TaskModel.ITaskSvc>(builder.ToUri());

            try
            {
                return taskServiceClient.UpdateTaskAsync(thisTask);
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("Task Controller: Exception updating {0}: {1}", thisTask, ex);
                throw;
            }
        }
    }
}
