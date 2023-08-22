using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace qwerty_chat_api.DTOs
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
