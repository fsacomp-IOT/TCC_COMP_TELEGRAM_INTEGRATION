using System;
using TelegramIntegration.Domain.Entities.Receive_Messages;
using TelegramIntegration.Service.Telegram;
using System.Threading.Tasks;
using TelegramIntegration.Domain.Entities.Returns;
using TelegramIntegration.Service.Integration;

namespace TelegramIntegration.Program
{
    class Program
    {
        static async Task Main(string[] args)
        {
            TelegramService telegram = new TelegramService();
            IntegrationService integration = new IntegrationService();

            #region [Search for new integration messages]

            Console.WriteLine("Searching integration messages");
            TelegramReturn messages = await telegram.getMessages();
            Console.WriteLine("Found " + messages.result.Count + " messages");
            ProcessReturn pr = await integration.ProcessMessages(messages);
            Console.WriteLine(pr.QtdSuccess + " successfully processed");
            Console.WriteLine(pr.QtdFail + " failed");

            #endregion
        }
    }
}
