﻿using System.Text.Json.Serialization;

namespace AdForm.SDK
{
    public class ModelErrorResponse
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
