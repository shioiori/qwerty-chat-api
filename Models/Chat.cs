using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper.Execution;
using MongoDB.Driver;

namespace qwerty_chat_api.Models
{
    public class Chat
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string? Name { get; set; }
        [BsonElement("created_date")]
        public DateTime? CreatedDate { get; set; }
        [BsonElement("updated_date")]
        public DateTime? UpdatedDate { get; set; }
        [BsonElement("created_by")]
        public string? CreatedBy { get; set; }
        [BsonElement("updated_by")]
        public string? UpdatedBy { get; set; }
        [BsonElement("is_stored")]
        public bool? IsStored { get; set; } = false;
        [BsonElement("is_limited")]
        public bool? IsLimited { get; set; } = true;
        [BsonElement("member_ids")]
        public IEnumerable<string> MemberIds { get; set; }
        [BsonElement("message_ids")]
        public IEnumerable<string> MessageIds { get; set; }
        [BsonElement("members")]
        public IEnumerable<User> Members { get; set; }
        [BsonElement("messages")]
        public IEnumerable<Message> Messages { get; set; }
    }
}
