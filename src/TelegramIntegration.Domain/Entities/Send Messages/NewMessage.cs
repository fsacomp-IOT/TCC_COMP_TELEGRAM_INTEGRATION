namespace TelegramIntegration.Domain.Entities.Send_Messages
{
    public class NewMessage
    {
        public int chat_id { get; set; }
        public string text { get; set; }
    }
}
