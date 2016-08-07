using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using StudentTimeline.Common;
using StudentTimeline.CourseModel;
using StudentTimeline.UserModel;
using StudentTimeline.WebApi.ReturnModels;
using Task = StudentTimeline.TaskModel.Task;

namespace StudentTimeline.WebApi.Controllers
{
    public class TaskController : ApiController
    {
        private const string TaskServiceName = "TaskSvc";

        // GET api/Task 
        public IQueryable<ReturnTask> Get()
        {
            ServiceUriBuilder builder = new ServiceUriBuilder(TaskServiceName);
            TaskModel.ITaskSvc taskServiceClient = ServiceProxy.Create<TaskModel.ITaskSvc>(builder.ToUri());

            List<ReturnTask> returnTasks = new List<ReturnTask>();

            try
            {
                var tasks = taskServiceClient.GetAllTasksAsync(CancellationToken.None).Result.AsQueryable();

                //Limit this call to only return 1000 taks
                FillReturnTasks(tasks.Take(1000), returnTasks);

                return returnTasks.AsQueryable();
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("Task Controller: Exception getting all Tasks", ex);
                throw;
            }
        }
        
        // GET api/Task/Guid
        public ReturnTask Get(string id)
        {
            TaskModel.TaskId taskId = new TaskModel.TaskId(id);

            ServiceUriBuilder builder = new ServiceUriBuilder(TaskServiceName);
            TaskModel.ITaskSvc taskServiceClient = ServiceProxy.Create<TaskModel.ITaskSvc>(builder.ToUri());

            try
            {
                return new ReturnTask(taskServiceClient.GetTaskByIdAsync(taskId).Result);
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("Task Controller: Exception getting {0}: {1}", id, ex);
                throw;
            }
        }


        // GET api/Task/User/Guid
        public IQueryable<ReturnTask> GetByUserId(string userId)
        {

            ServiceUriBuilder builderTasks = new ServiceUriBuilder(TaskServiceName);
            TaskModel.ITaskSvc taskServiceClient = ServiceProxy.Create<TaskModel.ITaskSvc>(builderTasks.ToUri());

            List<ReturnTask> returnTasks = new List<ReturnTask>();

            try
            {
                var tasks = taskServiceClient.GetAllTasksAsync(CancellationToken.None).Result.Where(x=> x.UserIds.Contains(new Guid(userId)));

                FillReturnTasks(tasks, returnTasks);

                return returnTasks.AsQueryable();
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("Task Controller: Exception getting all Tasks", ex);
                throw;
            }
        }

        // GET api/Task/Course/Guid
        public IQueryable<ReturnTask> GetByCourseId(string courseId)
        {

            ServiceUriBuilder builderTasks = new ServiceUriBuilder(TaskServiceName);
            TaskModel.ITaskSvc taskServiceClient = ServiceProxy.Create<TaskModel.ITaskSvc>(builderTasks.ToUri());

            List<ReturnTask> returnTasks = new List<ReturnTask>();

            try
            {
                var tasks = taskServiceClient.GetAllTasksAsync(CancellationToken.None).Result.Where(x => x.CourseId == new Guid(courseId));

                FillReturnTasks(tasks, returnTasks);

                return returnTasks.AsQueryable();
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("Task Controller: Exception getting all Tasks", ex);
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
        
        private static void FillReturnTasks(IEnumerable<Task> tasks, List<ReturnTask> returnTasks)
        {
            ReturnTask thisTask;

            foreach (var task in tasks)
            {
                thisTask = new ReturnTask(task);
                
                returnTasks.Add(thisTask);
            }
        }
    }
}
