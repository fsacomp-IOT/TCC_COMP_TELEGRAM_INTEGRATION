namespace TelegramIntegration.CrossCutting
{
    using System.Configuration;

    public static class Config
    {
        public static string Bot_ID => GetKey("Bot_ID");
        public static string Banco_TCC => GetConnection("Banco_TCC");
        
        private static string GetKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private static string GetConnection(string connection)
        {
            return ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        }
    }
}
