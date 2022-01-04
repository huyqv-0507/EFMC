using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using EFMC.Service.Models;
using Microsoft.Extensions.Caching.Memory;

namespace EFMC.Service.Common
{
    public class CacheEngine
    {
        public MemoryCache memoryCache { get; } = new MemoryCache(new MemoryCacheOptions());

        public static MessageResult GetMessages(string domain)
        {
            // Get the current directory.
            string path = Directory.GetCurrentDirectory();
            string fileName = $"{path}/Common/Resources/MessageResult.json";
            string jsonString = File.ReadAllText(fileName);

            // Deserialize MessageResult.json to List of Message result
            List<MessageResult> messageResults = JsonSerializer.Deserialize<List<MessageResult>>(
                jsonString,
                new JsonSerializerOptions()
                {
                    IgnoreNullValues = true
                });
            foreach (MessageResult messageResult in messageResults)
            {
                if (messageResult.Domain == domain)
                {
                    return messageResult;
                }
            }
            return null;
        }
    }
}
