using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Driver;

namespace qwerty_chat_api.Models
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("text")]
        public string? Text { get; set; } = String.Empty;
        [BsonElement("file")]
        public string? File { get; set; } = String.Empty;
        [BsonElement("created_date")]
        public DateTime CreatedDate { get; set; }
        [BsonElement("is_stored")]
        public bool IsStored { get; set; } = false;
        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        [BsonElement("chat_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ChatId { get; set; }
        public MongoDBRef? User { get; set; }
        public MongoDBRef? Chat { get; set; }
    }
}
