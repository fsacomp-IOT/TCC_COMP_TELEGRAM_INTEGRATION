namespace TelegramIntegration.Domain.Entities.Receive_Messages
{
    public class TelegramMessage
    {
        public int message_id { get; set; }
        public TelegramFrom from { get; set; }
        public TelegramChat chat { get; set; }
        public double date { get; set; }
        public string text { get; set; }
    }
}
