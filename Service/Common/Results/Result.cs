using System;
using System.Text.Json.Serialization;
using EFMC.Service.Models;

namespace EFMC.Service.Common.Results
{
    public class Result<dynamic>
    {
        public bool Success { get; set; }
        [JsonIgnore]
        public bool Client { get; set; } = false;
        public dynamic Data { get; set; }
#nullable enable
        public string? MessageError { get; set; }
        public Message? Message { get; set; }
    }
}
