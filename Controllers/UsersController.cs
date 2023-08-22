using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using qwerty_chat_api.Models;
using qwerty_chat_api.Services.Interface;
using System.Security.Claims;

namespace qwerty_chat_api.Controllers
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
        public async Task<List<User>> GetSearchConnection(string search_value)
        {
            return await _usersService.GetUserByPhoneOrEmail(search_value);
        }

        [HttpGet]
        public async Task<User> GetUser(string? id)
        {
            id = id == null ? id : User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _usersService.GetAsync(id);
        }
    }
}
