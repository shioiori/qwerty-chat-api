﻿using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using qwerty_chat_api.Domain.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qwerty_chat_api.Infrastructure.Models
{
    [BsonCollection("Users")]
    public class User : BaseEntity
    {
        [BsonElement("username")]
        public string Username { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("name")]

        public string Name { get; set; }
        [BsonElement("gender")]

        public bool? Gender { get; set; } = true;
        [BsonElement("email")]

        public string? Email { get; set; }
        [BsonElement("phone")]

        public string? Phone { get; set; }
        [BsonElement("avatar")]

        public string Avatar { get; set; } = "https://i.imgur.com/su4KGCT.png";
        [BsonElement("cover_photo")]

        public string? CoverPhoto { get; set; } = "https://i.imgur.com/su4KGCT.png";
    }
}
