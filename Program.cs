using Newtonsoft.Json;
using Telegram.Bot;

namespace tBot
{
    internal class Program
    {
        public class BotSetup
        {
            public string ApiKey { get; set; }
        }

        public static void RunBot(bool run)
        {
            var config = LoadConfig();

            if (string.IsNullOrEmpty(config.ApiKey))
            {
                Console.WriteLine("API key not found. Please enter your Telegram API key:");
                var apiKey = Console.ReadLine();

                config.ApiKey = apiKey;
                SaveConfig(config);
            }

            if (run)
            {
                SrartBot(config.ApiKey);
            }  
            else if (run==false)
            {
                StartDebug(config.ApiKey);
            }
                
            Console.WriteLine("Press any key to STOP.");
            Console.ReadKey();
        }
        static async Task StartDebug(string api)
        {
            TelegramBotClient botClient = new TelegramBotClient(api);
            botClient.StartReceiving(ClientBot.UpdateHandlerDebug, ClientBot.ErrorHandlerDebug);
            Console.WriteLine("Debug Running...");
        }
        public static async Task SrartBot(string api)
        {
            TelegramBotClient botClient = new TelegramBotClient(api);
            botClient.StartReceiving(ClientBot.UpdateHandler, ClientBot.ErrorHandler);
            Console.WriteLine("Bot Running...");
        }
        private const string ConfigFilePath = "appsettings.json";

        public static void Main(string[] args)
        {
            Console.WriteLine("1. Run Bot");
            Console.WriteLine("2. Debug Bot");
            Console.WriteLine("3. Reset Configuration");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        RunBot(true);
                        break;
                    case 2:
                        RunBot(false);
                        break;
                    case 3:
                        ResetConfig();
                        RunBot(true);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        private static BotSetup LoadConfig()
        {
            if (!File.Exists(ConfigFilePath))
                return new BotSetup();

            var json = File.ReadAllText(ConfigFilePath);
            return JsonConvert.DeserializeObject<BotSetup>(json);
        }

        private static void SaveConfig(BotSetup config)
        {
            var json = JsonConvert.SerializeObject(config);
            File.WriteAllText(ConfigFilePath, json);
        }

        static void ResetConfig()
        {
            if (File.Exists(ConfigFilePath))
            {
                File.Delete(ConfigFilePath);
                Console.WriteLine("Configuration reset successfully.");
            }
            else
            {
                Console.WriteLine("No configuration found.");
            }
        }
    }
}