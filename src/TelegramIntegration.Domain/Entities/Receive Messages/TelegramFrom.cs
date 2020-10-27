namespace TelegramIntegration.Domain.Entities.Receive_Messages
{
    public class TelegramFrom
    {
        public int id { get; set; }
        public bool is_bot { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string languagecde { get; set; }
    }
}
