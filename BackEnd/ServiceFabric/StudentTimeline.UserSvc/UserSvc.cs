using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using StudentTimeline.UserModel;
using System.Reflection;

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

        public async Task<List<User>> GetAllUsersAsync(CancellationToken ct)
        {
            IReliableDictionary<UserId, User> userItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<UserId, User>>(UserItemDictionaryName);

            List<User> results = new List<User>();

            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                ServiceEventSource.Current.Message("Getting all {0} users.", await userItems.GetCountAsync(tx));

                IAsyncEnumerator<KeyValuePair<UserId, User>> enumerator =
                    (await userItems.CreateEnumerableAsync(tx)).GetAsyncEnumerator();

                while (await enumerator.MoveNextAsync(ct))
                {
                    results.Add(enumerator.Current.Value);
                }
            }
            
            return results;
        }

        public async Task<User> GetUserByIdAsync(UserId id)
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
                    ServiceEventSource.Current.ServiceMessage(this, "GetUserByID user item: {0}-{1}", returnUser.Id.ToString(), returnUser.Name);
                }
                else
                    ServiceEventSource.Current.ServiceMessage(this, "GetUserByID no user found for itemid: {0}", id.ToString());
            }

            return returnUser;
        }

        public async Task<User> CreateUserAsync(User userToCreate)
        {
            IReliableDictionary<UserId, User> userItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<UserId, User>>(UserItemDictionaryName);

            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                await userItems.AddAsync(tx, userToCreate.Id, userToCreate);
                await tx.CommitAsync();
                ServiceEventSource.Current.ServiceMessage(this, "Created user: {0}", userToCreate);
            }

            return userToCreate;
        }
        public async Task<User> UpdateUserAsync(User userToUpdate)
        {
            IReliableDictionary<UserId, User> userItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<UserId, User>>(UserItemDictionaryName);

            ConditionalValue<User> result;

            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                result = await userItems.TryGetValueAsync(tx, userToUpdate.Id);

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
            IReliableDictionary<UserId, User> userItems =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<UserId, User>>(UserItemDictionaryName);
            
            var fileContents = string.Empty;
            List<User> users = new List<User>();

            try
            {
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("StudentTimeline.UserSvc.UserData.json"))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        fileContents = reader.ReadToEnd();
                    }
                    users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(fileContents);
                }
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.ServiceMessage(this, "Error loading Data.json. Error: {0} Stack: {1}", ex.Message, ex.StackTrace);
            }

            foreach (var userToCreate in users)
            {

                using (ITransaction tx = this.StateManager.CreateTransaction())
                {
                    await userItems.AddAsync(tx, userToCreate.Id, userToCreate);
                    await tx.CommitAsync();
                    ServiceEventSource.Current.ServiceMessage(this, "Created user: {0}", userToCreate);
                }
            }
        }
    }
}
