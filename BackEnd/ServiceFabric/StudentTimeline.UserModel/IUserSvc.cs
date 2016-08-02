using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace StudentTimeline.UserModel
{
    public interface IUserSvc : IService
    {
        Task<List<User>> GetAllUsersAsync(CancellationToken ct);

        Task<User> GetUserByIdAsync(UserId id);

        Task<User> CreateUserAsync(User userToCreate);

        Task<User> UpdateUserAsync(User userToUpdate);
    }
}
