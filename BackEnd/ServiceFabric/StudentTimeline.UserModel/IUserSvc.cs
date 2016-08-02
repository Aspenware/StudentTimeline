using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace StudentTimeline.UserModel
{
    public interface IUserSvc : IService
    {
        Task<User> GetUserByIDAsync(UserId id);

        Task<bool> CreateUserAsync(User userToCreate);

        Task<bool> UpdateUserAsync(User userToUpdate);
    }
}
