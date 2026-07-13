using System.Threading.Tasks;

namespace MusicStore.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(string username, string password, string email);
        Task<bool> ValidateUserAsync(string username, string password);
    }
}
