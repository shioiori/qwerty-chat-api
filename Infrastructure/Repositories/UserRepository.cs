using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwerty_chat_api.Domain.Utils;
using qwerty_chat_api.Infrastructure.Models;
using qwerty_chat_api.Infrastructure.Repositories.Interface;

namespace qwerty_chat_api.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(
            IOptions<ChatDatabaseSettings> ChatDatabaseSettings) : base(ChatDatabaseSettings)
        {
        }
        public async Task<User> GetUserAuthenticatedAsync(string username, string password)
        {
            return await _TCollection.Find(x => x.Username == username && x.Password == password).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _TCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _TCollection.Find(x => x.Username == username).FirstOrDefaultAsync();

        }
        public async Task<List<User>> GetUserByPhoneOrEmail(string? search_value)
        {
            return await _TCollection.Find(x => string.IsNullOrEmpty(search_value)
                                                 || x.Phone.Contains(search_value)
                                                 || x.Email.Contains(search_value)).ToListAsync();
        }
    }
}
