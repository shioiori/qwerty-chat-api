﻿namespace qwerty_chat_api.DTOs
{
    public class LogResponse
    {
        public bool Success { get; set; }
        public string AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? Message { get; set; }
        public string? UserId { get; set; }
    }
}
