namespace TelegramIntegration.Domain.Entities.Receive_Messages
{
    using System.Collections.Generic;

    public class TelegramReturn
    {
        public bool ok { get; set; }
        public List<TelegramUpdates> result { get; set; }
    }
}
