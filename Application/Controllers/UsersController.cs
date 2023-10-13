using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using qwerty_chat_api.Domain.Services.Interface;
using qwerty_chat_api.Infrastructure.Models;
using System.Security.Claims;

namespace qwerty_chat_api.Application.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _usersService;

        public UsersController(IUser usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [Route("search")]
        public async Task<List<User>> GetSearchConnection(string? search_value)
        {
            return await _usersService.GetUserByPhoneOrEmail(search_value);
        }

        [HttpGet]
        [Route("get-user-by-id")]
        public async Task<User> GetUser(string id)
        {
            id = string.IsNullOrEmpty(id) ? id : User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _usersService.GetAsync(id);
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<List<User>> GetAllUsers()
        {
            return await _usersService.GetAllAsync();
        }
    }
}
