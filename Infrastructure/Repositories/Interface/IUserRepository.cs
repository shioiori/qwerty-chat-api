﻿using qwerty_chat_api.Infrastructure.Models;

namespace qwerty_chat_api.Infrastructure.Repositories.Interface
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserAuthenticatedAsync(string username, string password);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUsernameAsync(string username);
        Task<List<User>> GetUserByPhoneOrEmail(string? search_value);
    }
}
