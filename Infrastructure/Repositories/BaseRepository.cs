using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwerty_chat_api.Domain.Utils;
using qwerty_chat_api.Infrastructure.Models;
using qwerty_chat_api.Infrastructure.Repositories.Interface;

namespace qwerty_chat_api.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {

        private readonly IMongoCollection<T> _TCollection;
        public BaseRepository(
            IOptions<ChatDatabaseSettings> ChatDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ChatDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ChatDatabaseSettings.Value.DatabaseName);

            _TCollection = mongoDatabase.GetCollection<T>(CollectionUtils<T>.GetCollectionName());
        }


        public async Task<T> GetAsync(string id) =>
            await _TCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<List<T>> GetAllAsync() =>
            await _TCollection.Find(_ => true).ToListAsync();

        public async Task CreateAsync(T newT) =>
            await _TCollection.InsertOneAsync(newT);
        public async Task UpdateAsync(string id, T updatedT) =>
            await _TCollection.ReplaceOneAsync(x => x.Id == id, updatedT);
        public async Task RemoveAsync(string id) =>
            await _TCollection.DeleteOneAsync(x => x.Id == id);
    }
}
