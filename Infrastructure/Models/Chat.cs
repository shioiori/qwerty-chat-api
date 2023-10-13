using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper.Execution;
using MongoDB.Driver;
using qwerty_chat_api.Domain.Attributes;

namespace qwerty_chat_api.Infrastructure.Models
{
    [BsonCollection("Chats")]
    public class Chat : BaseEntity
    {
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

        [BsonElement("avatar")]
        public string Avatar { get; set; } = "https://i.imgur.com/su4KGCT.png";
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
