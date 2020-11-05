namespace TelegramIntegration.Service.Integration
{
    using System.Threading.Tasks;
    using TelegramIntegration.Domain.Entities.Returns;
    using TelegramIntegration.Domain.Entities.Receive_Messages;
    using TelegramIntegration.Repository.Integration;
    using System;
    using TelegramIntegration.Repository.Telegram;
    using TelegramIntegration.Domain.Entities.Send_Messages;
    using TelegramIntegration.Domain.Entities.Returns.Send;
    using System.Text.RegularExpressions;

    public class IntegrationService
    {
        public async Task<ProcessReturn> ProcessMessages(TelegramReturn messages)
        {
            IntegrationRepository repo = new IntegrationRepository();
            CallTelegramAPI telegram = new CallTelegramAPI();
            ProcessReturn pr = new ProcessReturn();
            DeviceChat dc = new DeviceChat();
            NewMessage newMessage = new NewMessage();

            foreach (TelegramUpdates message in messages.result)
            {
                ChatMessages ret = await repo.findMessageBId(message.update_id);

                if (ret.update_id == 0)
                {
                    ret.entry_date = UnixTimeStampToDateTime(message.message.date);
                    ret.update_id = message.update_id;

                    if (await repo.includeMessages(ret))
                    {
                        if (message.message.text.Contains("DeviceID:"))
                        {
                            message.message.text = Regex.Replace(message.message.text, @"\s+", "");
                            string[] device_id = message.message.text.Split(':');
                           
                            if (device_id[1].Length == 12)
                            {
                                if (!await repo.finRelationByDeviceId(device_id[1], message.message.chat.id))
                                {
                                    dc.device_id = device_id[1];
                                    dc.chat_id = message.message.chat.id;

                                    if (await repo.includeDeviceChatRelation(dc))
                                    {
                                        newMessage.chat_id = dc.chat_id;
                                        newMessage.text = "Integração concluida com sucesso!";

                                        await telegram.sendMessageAsync(newMessage);

                                        pr.QtdSuccess++;
                                    }
                                    else
                                    {
                                        newMessage.chat_id = message.message.chat.id;
                                        newMessage.text = "Houve um erro na integração, favor inserir um DeviceID valido";
                                        await telegram.sendMessageAsync(newMessage);
                                        pr.QtdFail++;
                                    }
                                }
                                    
                            }
                            else
                            {
                                newMessage.chat_id = message.message.chat.id;
                                newMessage.text = "Houve um erro na integração, quantidade de caracteres incorreta!";
                                await telegram.sendMessageAsync(newMessage);
                                pr.QtdFail++;
                            }
                        }
                        else
                        {
                            newMessage.chat_id = message.message.chat.id;
                            newMessage.text = "Houve um erro na integração, favor inserir uma mensagem válida";
                            await telegram.sendMessageAsync(newMessage);
                            pr.QtdFail++;
                        }
                    }
                    else
                    {
                        newMessage.chat_id = message.message.chat.id;
                        newMessage.text = "Houve um erro na integração, tente novamente mais tarde";
                        await telegram.sendMessageAsync(newMessage);
                        pr.QtdFail++;
                    }
                    
                }
                    
            }

            return pr;
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

    }
}
