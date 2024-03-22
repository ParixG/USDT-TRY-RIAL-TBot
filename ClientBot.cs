using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace tBot
{
    internal class ClientBot
    {
        public static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken token)
        {

            if (update.Message?.Type != null)
            {
                long chatId = update.Message.Chat.Id;
                Console.WriteLine(chatId);
                if (update.Message.Text == "/start")
                {
                    await botClient.SendTextMessageAsync(chatId,
                    await prices(),
                    replyMarkup: updateButton());
                }
                else
                {
                    Console.WriteLine($"Message Error: {chatId}");
                }
            }
            else if (update.CallbackQuery?.Data != null)
            {
                long chatId = update.CallbackQuery.Message.Chat.Id;
                Console.WriteLine(chatId);
                if (update.CallbackQuery.Data == "update")
                {
                    await botClient.EditMessageTextAsync(
                        chatId: chatId,
                        messageId: update.CallbackQuery.Message.MessageId,
                        text: await prices(),
                        replyMarkup:updateButton());
                }
                else
                {
                    Console.WriteLine($"CallbackQuery Error: {chatId}");
                }
            }
            else
            {
                Console.WriteLine($"Type Error.");
            }
        }
        static async Task<string> prices()
        {
            double r = await Prices.RialMain();
            double t = await Prices.TryMain();
            double rt = r / t;
            return $"📉 USDT/TUMAN: {r}\n📈 USDT/LIR: {t}\n-----------------------------\n📊 LIR/TUMAN : {rt}";
        }
        static InlineKeyboardMarkup updateButton()
        {
            InlineKeyboardMarkup key = new(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Update","update"),
                }
            });
            return key;
        }
        public static async Task ErrorHandler(ITelegramBotClient bot, Exception exception, CancellationToken token)
        {
            Console.WriteLine(exception.Message);
            Program.RunBot();
        }
        public static async Task UpdateHandlerDebug(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            Console.WriteLine("Fixed.");
        }
    }
}
