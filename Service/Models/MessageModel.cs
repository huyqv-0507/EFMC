using System;
using System.Collections.Generic;

namespace EFMC.Service.Models
{
    public class MessageResult
    {
        public string Domain { get; set; }
        public List<Message> Messages { get; set; }
    }
    public class Message
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
