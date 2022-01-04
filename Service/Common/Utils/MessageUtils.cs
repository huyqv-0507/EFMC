using System;
using System.Collections.Generic;
using EFMC.Service.Models;
using Microsoft.Extensions.Caching.Memory;

namespace EFMC.Service.Common.Utils
{
    public class MessageUtils
    {
        // Get value of message by key
        public static Message Message(string key, List<Message> messages)
        {
            foreach (Message message in messages)
            {
                if (message.Key == key)
                    return message;
            }
            return null;
        }
    }
}
