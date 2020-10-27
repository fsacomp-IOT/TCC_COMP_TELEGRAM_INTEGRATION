using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TelegramIntegration.Domain.Entities.Receive_Messages;
using TelegramIntegration.Repository.Telegram;

namespace TelegramIntegration.Service.Telegram
{
    public class TelegramService
    {
        CallTelegramAPI repo = new CallTelegramAPI();

        public async Task<TelegramReturn> getMessages()
        {
            return await repo.getMessagesAsync();
        }
    }
}
