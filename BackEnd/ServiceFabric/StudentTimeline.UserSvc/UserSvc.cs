using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using StudentTimeline.UserModel;

namespace StudentTimeline.UserSvc
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class UserSvc : StatefulService, IUserSvc
    {
        private const string UserItemDictionaryName = "userItems";

        /// <summary>
        /// This constructor is used in unit tests to inject a different state manager for unit testing.
        /// </summary>
        /// <param name="stateManager"></param>
        public UserSvc(StatefulServiceContext serviceContext) : this(serviceContext, (new ReliableStateManager(serviceContext)))
        {
        }

        public UserSvc(StatefulServiceContext context, IReliableStateManagerReplica stateManagerReplica) : base(context, stateManagerReplica)
        {
        }

        public async Task<User> GetUserByIDAsync(UserId id)
        {
            IReliableDictionary<UserId, User> userItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<UserId, User>>(UserItemDictionaryName);

            User returnUser = null;
            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                var result = await userItems.TryGetValueAsync(tx, id);

                if (result.HasValue)
                {
                    returnUser = result.Value;
                    ServiceEventSource.Current.ServiceMessage(this, "GetUserByID user item: {0}", result.Value.Name);
                }
                else
                    ServiceEventSource.Current.ServiceMessage(this, "GetUserByID no user found for itemid: {0}", id.ToString());
            }

            return returnUser;
        }
        public async Task<bool> CreateUserAsync(User userToCreate)
        {
            IReliableDictionary<UserId, User> userItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<UserId, User>>(UserItemDictionaryName);

            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                await userItems.AddAsync(tx, userToCreate.Id, userToCreate);
                await tx.CommitAsync();
                ServiceEventSource.Current.ServiceMessage(this, "Created user: {0}", userToCreate);
            }

            return true;
        }
        public async Task<bool> UpdateUserAsync(User userToUpdate)
        {
            IReliableDictionary<UserId, User> userItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<UserId, User>>(UserItemDictionaryName);
            
            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                var result = await userItems.TryGetValueAsync(tx, userToUpdate.Id);

                if (result.HasValue)
                {
                    result.Value.Name = userToUpdate.Name;
                    result.Value.Blurb = userToUpdate.Blurb;
                    result.Value.Email = userToUpdate.Email;
                    result.Value.Password = userToUpdate.Password;
                    result.Value.ProfileImageUrl = userToUpdate.ProfileImageUrl;

                    // We have to store the item back in the dictionary in order to actually save it.
                    // This will then replicate the updated item for
                    await userItems.SetAsync(tx, result.Value.Id, result.Value);
                }
                else
                    ServiceEventSource.Current.ServiceMessage(this, "UpdateUser no user found for itemid: {0}", userToUpdate.Id.ToString());
            }

            return true;
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
            return new ServiceReplicaListener[0];
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
