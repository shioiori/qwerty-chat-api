﻿using qwerty_chat_api.Models;

namespace qwerty_chat_api.Services.Interface
{
    public interface IUser : IBaseService<User>
    {
        Task<User> GetUserAuthenticatedAsync(string username, string password);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUsernameAsync(string username);
        Task<List<User>> GetUserByPhoneOrEmail(string search_value);
    }
}