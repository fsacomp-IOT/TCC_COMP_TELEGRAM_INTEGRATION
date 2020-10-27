namespace TelegramIntegration.Repository.Telegram
{
    using Newtonsoft.Json;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using TelegramIntegration.CrossCutting;
    using TelegramIntegration.Domain.Entities.Receive_Messages;
    using TelegramIntegration.Domain.Entities.Returns.Send;
    using TelegramIntegration.Domain.Entities.Send_Messages;

    public class CallTelegramAPI
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<TelegramReturn> getMessagesAsync()
        {
            client.DefaultRequestHeaders.Accept.Clear();

            string url = "https://api.telegram.org/" + Config.Bot_ID + "/getUpdates";

            HttpResponseMessage response = await client.GetAsync(url);
            var ret = JsonConvert.DeserializeObject<TelegramReturn>(await response.Content.ReadAsStringAsync());

            return ret;
        }

        public async Task sendMessageAsync(NewMessage message) 
        {
            TelegramSend ret = new TelegramSend();

            client.DefaultRequestHeaders.Accept.Clear();

            string url = "https://api.telegram.org/" + Config.Bot_ID + "/sendMessage";

            var dataAsString = JsonConvert.SerializeObject(message);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response =  await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                ret = JsonConvert.DeserializeObject<TelegramSend>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
