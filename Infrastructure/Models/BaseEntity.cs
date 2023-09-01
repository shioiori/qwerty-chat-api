using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace qwerty_chat_api.Infrastructure.Models
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
