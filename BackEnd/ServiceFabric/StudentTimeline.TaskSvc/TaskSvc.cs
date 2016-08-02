using System;
using System.Collections.Generic;
using System.Fabric;
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
        
        public async Task<List<TaskModel.Task>> GetTaskListByUserIdAsync(Guid userId)
        {
            IReliableDictionary<TaskModel.TaskId, TaskModel.Task> taskItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<TaskModel.TaskId, TaskModel.Task>>(TaskItemDictionaryName);

            return null;
        }
        
        public async Task<List<TaskModel.Task>> GetTaskListByCourseIdAsync(Guid courseId)
        {
            IReliableDictionary<TaskModel.TaskId, TaskModel.Task> taskItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<TaskModel.TaskId, TaskModel.Task>>(TaskItemDictionaryName);

            return null;
        }

        public async Task<bool> CreateTaskAsync(TaskModel.Task taskToCreate)
        {
            IReliableDictionary<TaskModel.TaskId, TaskModel.Task> taskItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<TaskModel.TaskId, TaskModel.Task>>(TaskItemDictionaryName);

            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                await taskItems.AddAsync(tx, taskToCreate.Id, taskToCreate);
                await tx.CommitAsync();
                ServiceEventSource.Current.ServiceMessage(this, "Created Task Titled: {0}", taskToCreate.Title);
            }

            return true;
        }

        public async Task<bool> UpdateTaskAsync(TaskModel.Task taskToUpdate)
        {
            IReliableDictionary<TaskModel.TaskId, TaskModel.Task> taskItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<TaskModel.TaskId, TaskModel.Task>>(TaskItemDictionaryName);

            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                var result = await taskItems.TryGetValueAsync(tx, taskToUpdate.Id);

                if (result.HasValue)
                {

                    // We have to store the item back in the dictionary in order to actually save it.
                    // This will then replicate the updated item for
                    await taskItems.SetAsync(tx, result.Value.Id, result.Value);
                }
                else
                    ServiceEventSource.Current.ServiceMessage(this, "UpdateTask no Task found for itemid: {0}", taskToUpdate.Id.ToString());
            }

            return true;
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
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            //var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<string, long>>("myDictionary");

            //while (true)
            //{
            //    cancellationToken.ThrowIfCancellationRequested();

            //    using (var tx = this.StateManager.CreateTransaction())
            //    {
            //        var result = await myDictionary.TryGetValueAsync(tx, "Counter");

            //        ServiceEventSource.Current.ServiceMessage(this, "Current Counter Value: {0}",
            //            result.HasValue ? result.Value.ToString() : "Value does not exist.");

            //        await myDictionary.AddOrUpdateAsync(tx, "Counter", 0, (key, value) => ++value);

            //        // If an exception is thrown before calling CommitAsync, the transaction aborts, all changes are 
            //        // discarded, and nothing is saved to the secondary replicas.
            //        await tx.CommitAsync();
            //    }

            //    await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            //}
        }
    }
}
