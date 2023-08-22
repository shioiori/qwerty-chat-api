using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace qwerty_chat_api.DTOs
{
    public class RegisterRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
    }
}
