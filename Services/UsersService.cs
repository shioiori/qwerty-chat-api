using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwerty_chat_api.Models;
using qwerty_chat_api.Repositories.Interface;
using qwerty_chat_api.Services.Interface;

namespace qwerty_chat_api.Services
{
    public class UsersService : IUser
    {
        private readonly IUserRepository _userRepository;

        public UsersService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateAsync(User obj)
        {
           await _userRepository.CreateAsync(obj);
        }

        public Task<List<User>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }

        public Task<User> GetAsync(string id)
        {
            return _userRepository.GetAsync(id);
        }

        public Task<User> GetUserAuthenticatedAsync(string username, string password)
        {
            return _userRepository.GetUserAuthenticatedAsync(username, password);
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            return _userRepository.GetUserByEmailAsync(email);
        }

        public Task<List<User>> GetUserByPhoneOrEmail(string search_value)
        {
            return _userRepository.GetUserByPhoneOrEmail(search_value);
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            return _userRepository.GetUserByUsernameAsync(username);
        }

        public Task RemoveAsync(string id)
        {
            return _userRepository.RemoveAsync(id);
        }

        public async Task UpdateAsync(string id, User obj)
        {
            await _userRepository.UpdateAsync(id, obj);
        }
    }
}
