namespace TelegramIntegration.Domain.Entities.Returns.Send
{
    public class SendFrom
    {
        public double id { get; set; }
        public bool is_bot { get; set; }
        public string first_name { get; set; }
        public string username { get; set; }
    }
}
