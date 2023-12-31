﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Driver;
using qwerty_chat_api.Domain.Attributes;

namespace qwerty_chat_api.Infrastructure.Models
{
    [BsonCollection("Messages")]
    public class Message : BaseEntity
    {
        [BsonElement("text")]
        public string? Text { get; set; } = string.Empty;
        [BsonElement("file")]
        public string? File { get; set; } = string.Empty;
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
        public User User { get; set; }
        public Chat Chat { get; set; }
    }
}
