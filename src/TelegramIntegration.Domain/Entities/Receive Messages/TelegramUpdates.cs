namespace TelegramIntegration.Domain.Entities.Receive_Messages
{
    public class TelegramUpdates
    {
        public int update_id { get; set; }
        public TelegramMessage message { get; set; }
    }
}
