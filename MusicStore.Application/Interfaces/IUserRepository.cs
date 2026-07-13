using System.Threading.Tasks;

namespace MusicStore.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> RegisterUserAsync(string username, string password, string email);
        Task<bool> ValidateUserAsync(string username, string password);
    }
}
