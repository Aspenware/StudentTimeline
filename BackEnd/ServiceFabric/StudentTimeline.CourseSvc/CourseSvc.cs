﻿using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using StudentTimeline.CourseModel;

namespace StudentTimeline.CourseSvc
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class CourseSvc : StatefulService, ICourseSvc
    {
        private const string CourseItemDictionaryName = "courseItems";

        public CourseSvc(StatefulServiceContext context)
            : base(context)
        { }
        
        public async Task<Course> GetCourseByIdAsync(CourseId id)
        {
            IReliableDictionary<CourseId, Course> courseItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<CourseId, Course>>(CourseItemDictionaryName);

            Course courseTask = null;
            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                var result = await courseItems.TryGetValueAsync(tx, id);

                if (result.HasValue)
                {
                    courseTask = result.Value;
                    ServiceEventSource.Current.ServiceMessage(this, "GetCourseToCreateByIDAsync Course item: {0}-{1}", courseTask.Id.ToString(), courseTask.Title);
                }
                else
                    ServiceEventSource.Current.ServiceMessage(this, "GetCourseByIDAsync no Course found for itemid: {0}", id.ToString());
            }

            return courseTask;
        }

        public async Task<List<Course>> GetAllCoursesAsync(CancellationToken ct)
        {
            IReliableDictionary<CourseId, Course> courseItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<CourseId, Course>>(CourseItemDictionaryName);

            List<Course> results = new List<Course>();

            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                ServiceEventSource.Current.Message("Getting all {0} courses.", await courseItems.GetCountAsync(tx));

                IAsyncEnumerator<KeyValuePair<CourseId, Course>> enumerator =
                    (await courseItems.CreateEnumerableAsync(tx)).GetAsyncEnumerator();

                while (await enumerator.MoveNextAsync(ct))
                {
                    results.Add(enumerator.Current.Value);
                }
            }

            return results;
        }

        public async Task<Course> CreateCourseAsync(Course courseToCreate)
        {
            IReliableDictionary<CourseId, Course> courseItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<CourseId, Course>>(CourseItemDictionaryName);

            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                await courseItems.AddAsync(tx, courseToCreate.Id, courseToCreate);
                await tx.CommitAsync();
                ServiceEventSource.Current.ServiceMessage(this, "Created course Titled: {0}", courseToCreate.Title);
            }

            return courseToCreate;
        }

        public async Task<Course> UpdateCourseAsync(Course courseToUpdate)
        {
            IReliableDictionary<CourseId, Course> courseItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<CourseId, Course>>(CourseItemDictionaryName);

            ConditionalValue<Course> result;

            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                result = await courseItems.TryGetValueAsync(tx, courseToUpdate.Id);

                if (result.HasValue)
                {
                    result.Value.Id = courseToUpdate.Id;
                    result.Value.Description = courseToUpdate.Description;
                    result.Value.Title = courseToUpdate.Title;
                    result.Value.UserIds = courseToUpdate.UserIds;

                    // We have to store the item back in the dictionary in order to actually save it.
                    // This will then replicate the updated item for
                    await courseItems.SetAsync(tx, result.Value.Id, result.Value);
                }
                else
                    ServiceEventSource.Current.ServiceMessage(this, "UpdateCourse no course found for itemid: {0}", courseToUpdate.Id.ToString());
            }

            return result.Value;
        }


        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
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
