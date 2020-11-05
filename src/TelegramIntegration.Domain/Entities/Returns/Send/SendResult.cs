namespace TelegramIntegration.Domain.Entities.Returns.Send
{
    public class SendResult
    {
        public double message_id { get; set; }
        public SendFrom from { get; set; }
        public SendChat chat { get; set; }
        public double date { get; set; }
        public string text { get; set; }
    }
}
