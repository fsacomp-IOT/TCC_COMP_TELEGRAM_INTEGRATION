namespace TelegramIntegration.Repository.Integration
{
    using Dapper;
    using Npgsql;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TelegramIntegration.CrossCutting;
    using TelegramIntegration.Domain.Entities.Returns;

    public class IntegrationRepository
    {
        private string command = "";

        public async Task<ChatMessages> findMessageBId(int update_id)
        {
            command = "SELECT \"update_id\", \"entry_date\" FROM \"TCC_COMP\".\"ChatMessages\" WHERE update_id = @update_id";

            ChatMessages cm = new ChatMessages();

            using (var connection = new NpgsqlConnection(Config.Banco_TCC))
            {
                try
                {
                    await connection.OpenAsync();
                    var ret = await connection.QueryAsync<ChatMessages>(command, new { update_id = update_id });
                    ChatMessages chatMsg = ret.FirstOrDefault();

                    if (chatMsg != null)
                    {
                        cm.update_id = chatMsg.update_id;
                        cm.entry_date = chatMsg.entry_date;
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            return cm;
        }

        public async Task<bool> includeMessages(ChatMessages newMessage)
        {
            bool ret = false;

            command = "INSERT INTO \"TCC_COMP\".\"ChatMessages\"(update_id, entry_date) VALUES (@update_id, @entry_date)";

            DynamicParameters dynamicParameters = new DynamicParameters(new
            {
                newMessage.update_id,
                newMessage.entry_date
            });

            using (var connection = new NpgsqlConnection(Config.Banco_TCC))
            {
                try
                {
                    await connection.OpenAsync();

                    using (var trans = await connection.BeginTransactionAsync())
                    {
                        var retInsert = await connection.ExecuteAsync(command, dynamicParameters, trans);
                        await trans.CommitAsync();

                        if (retInsert > 0)
                            ret = true;
                    }

                }
                catch (Exception)
                {
                    ret = false;
                }
            }

            return ret;
        }

        public async Task<bool> finRelationByDeviceId(string device_id, int chat_id)
        {
            bool ret = false;

            DynamicParameters dynamicParameters = new DynamicParameters(new {
                device_id, 
                chat_id
            });

            command = "SELECT device_id, chat_id FROM \"TCC_COMP\".\"DeviceChat\" WHERE device_id = @device_id AND chat_id = @chat_id";

            using (var connection = new NpgsqlConnection(Config.Banco_TCC))
            {
                try
                {
                    await connection.OpenAsync();
                    var retSelect = await connection.QueryAsync<DeviceChat>(command, dynamicParameters);

                    if(retSelect.ToList().Count > 0)
                        ret = true;
                }
                catch (Exception)
                {
                    ret = false;
                }
            }

            return ret;
        }

        public async Task<bool> includeDeviceChatRelation(DeviceChat newRelation)
        {
            DynamicParameters dynamicParameters = new DynamicParameters(new { 
                newRelation.device_id,
                newRelation.chat_id
            });

            bool ret = false;

            command = "INSERT INTO \"TCC_COMP\".\"DeviceChat\"(device_id, chat_id) VALUES (@device_id, @chat_id)";

            using (var connection = new NpgsqlConnection(Config.Banco_TCC))
            {
                try
                {
                    await connection.OpenAsync();

                    using (var trans = await connection.BeginTransactionAsync())
                    {
                        var retInsert = await connection.ExecuteAsync(command, dynamicParameters, trans);
                        await trans.CommitAsync();

                        if (retInsert > 0)
                            ret = true;
                    }

                }
                catch (Exception e)
                {
                    ret = false;
                }
            }

            return ret;
        }
    }
}
