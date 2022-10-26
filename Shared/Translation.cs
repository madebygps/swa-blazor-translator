using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BlazorApp.Shared
{
    public class Translation
    {
        [JsonPropertyName("to")]
        public string ToLang { get; set; }
        [JsonPropertyName("from")]
        public string? FromLang { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
