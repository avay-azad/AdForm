using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AdForm.SDK
{
    public class Error
    {

        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("error_user_msg")]
        public string Message { get; set; }
        [JsonPropertyName("field_errors")]
        public IList<ModelErrorResponse> FieldErrors { get; set; }
    }
}
