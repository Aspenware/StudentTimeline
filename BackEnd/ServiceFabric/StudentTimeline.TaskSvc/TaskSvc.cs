using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;

namespace StudentTimeline.TaskSvc
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class TaskSvc : StatefulService, TaskModel.ITaskSvc
    {
        private const string TaskItemDictionaryName = "taskItems";

        public TaskSvc(StatefulServiceContext context)
            : base(context)
        { }
        
        public async Task<TaskModel.Task> GetTaskByIdAsync(TaskModel.TaskId id)
        {
            IReliableDictionary<TaskModel.TaskId, TaskModel.Task> taskItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<TaskModel.TaskId, TaskModel.Task>>(TaskItemDictionaryName);

            TaskModel.Task returnTask = null;
            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                var result = await taskItems.TryGetValueAsync(tx, id);

                if (result.HasValue)
                {
                    returnTask = result.Value;
                    ServiceEventSource.Current.ServiceMessage(this, "GetTaskByIDAsync Task item: {0}-{1}", returnTask.Id.ToString(), returnTask.Title);
                }
                else
                    ServiceEventSource.Current.ServiceMessage(this, "GetTaskByIDAsync no Task found for itemid: {0}", id.ToString());
            }

            return returnTask;
        }
        
        public async Task<List<TaskModel.Task>> GetAllTasksAsync(CancellationToken ct)
        {
            IReliableDictionary<TaskModel.TaskId, TaskModel.Task> taskItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<TaskModel.TaskId, TaskModel.Task>>(TaskItemDictionaryName);

            List<TaskModel.Task> results = new List<TaskModel.Task>();

            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                ServiceEventSource.Current.Message("Getting all {0} users.", await taskItems.GetCountAsync(tx));

                IAsyncEnumerator<KeyValuePair<TaskModel.TaskId, TaskModel.Task>> enumerator =
                    (await taskItems.CreateEnumerableAsync(tx)).GetAsyncEnumerator();

                while (await enumerator.MoveNextAsync(ct))
                {
                    results.Add(enumerator.Current.Value);
                }
            }

            return results;
        }

        public async Task<TaskModel.Task> CreateTaskAsync(TaskModel.Task taskToCreate)
        {
            IReliableDictionary<TaskModel.TaskId, TaskModel.Task> taskItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<TaskModel.TaskId, TaskModel.Task>>(TaskItemDictionaryName);

            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                await taskItems.AddAsync(tx, taskToCreate.Id, taskToCreate);
                await tx.CommitAsync();
                ServiceEventSource.Current.ServiceMessage(this, "Created Task Titled: {0}", taskToCreate.Title);
            }

            return taskToCreate;
        }

        public async Task<TaskModel.Task> UpdateTaskAsync(TaskModel.Task taskToUpdate)
        {
            IReliableDictionary<TaskModel.TaskId, TaskModel.Task> taskItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<TaskModel.TaskId, TaskModel.Task>>(TaskItemDictionaryName);

            ConditionalValue<TaskModel.Task> result;

            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                result = await taskItems.TryGetValueAsync(tx, taskToUpdate.Id);

                if (result.HasValue)
                {
                    result.Value.CourseId = taskToUpdate.CourseId;
                    result.Value.Description = taskToUpdate.Description;
                    result.Value.DueDate = taskToUpdate.DueDate;
                    result.Value.TaskType = taskToUpdate.TaskType;
                    result.Value.Title = taskToUpdate.Title;
                    result.Value.UserIds = taskToUpdate.UserIds;

                    // We have to store the item back in the dictionary in order to actually save it.
                    // This will then replicate the updated item for
                    await taskItems.SetAsync(tx, result.Value.Id, result.Value);
                }
                else
                    ServiceEventSource.Current.ServiceMessage(this, "UpdateTask no Task found for itemid: {0}", taskToUpdate.Id.ToString());
            }

            return result.Value;
        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or TaskModel.Task requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see http://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[]
            {
                new ServiceReplicaListener(context => this.CreateServiceRemotingListener(context))
            };
        }

        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            IReliableDictionary<TaskModel.TaskId, TaskModel.Task> taskItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<TaskModel.TaskId, TaskModel.Task>>(TaskItemDictionaryName);

            var fileContents = string.Empty;
            List<TaskModel.Task> tasks = new List<TaskModel.Task>();

            try
            {
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("StudentTimeline.TaskSvc.TaskData.json"))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        fileContents = reader.ReadToEnd();
                    }
                    tasks = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TaskModel.Task>>(fileContents);
                }
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.ServiceMessage(this, "Error loading Data.json. Error: {0} Stack: {1}", ex.Message, ex.StackTrace);
            }

            foreach (var taskToCreate in tasks)
            {

                using (ITransaction tx = this.StateManager.CreateTransaction())
                {
                    await taskItems.AddAsync(tx, taskToCreate.Id, taskToCreate);
                    await tx.CommitAsync();
                    ServiceEventSource.Current.ServiceMessage(this, "Created task: {0}", taskToCreate);
                }
            }
        }
    }
}
