using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramIntegration.Domain.Entities.Returns
{
    public class DeviceChat
    {
        public bool already_exist { get; set; }
        public string device_id { get; set; }
        public int chat_id { get; set; }
    }
}
