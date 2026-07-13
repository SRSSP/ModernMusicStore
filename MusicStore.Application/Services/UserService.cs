using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MusicStore.Application.Interfaces;

namespace MusicStore.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> RegisterUserAsync(string username, string password, string email)
        {
            var existing = await _userManager.FindByNameAsync(username);
            if (existing != null)
                return false;

            var user = new IdentityUser { UserName = username, Email = email };
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return false;
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
