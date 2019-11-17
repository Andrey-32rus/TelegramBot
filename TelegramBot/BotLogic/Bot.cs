using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MihaZupan;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UtilsLib;

namespace TelegramBot.BotLogic
{
    public class Bot : IBot
    {
        private readonly TelegramBotClient client;

        public Bot()
        {
            var config = AppConfig.GetSection<BotConfiguration>("BotConfiguration");
            
            var proxy = new WebProxy($"http://{config.ProxyHost}:{config.ProxyPort}");
            client = new TelegramBotClient(config.BotToken, proxy);
            client.SetWebhookAsync(config.WebHooksUrl).Wait();
            Console.WriteLine("WebHook was set");
        }

        public async Task Update(Update update)
        {
            if (update.Type != UpdateType.Message)
            {
                return;
            }

            var message = update.Message;

            //_logger.LogInformation("Received Message from {0}", message.Chat.Id);

            if (message.Type == MessageType.Text)
            {
                // Echo each Message
                string text = $"{JsonConvert.SerializeObject(message.From, Formatting.Indented)}" +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"Said:" +
                              $"{Environment.NewLine}{Environment.NewLine}" +
                              $"{message.Text}";
                await client.SendTextMessageAsync(message.Chat.Id, text);
            }
            else if (message.Type == MessageType.Photo)
            {
                // Download Photo
                var fileId = message.Photo.LastOrDefault()?.FileId;
                var file = await client.GetFileAsync(fileId);

                var filename = file.FileId + "." + file.FilePath.Split('.').Last();

                using (var saveImageStream = System.IO.File.Open(filename, FileMode.Create))
                {
                    await client.DownloadFileAsync(file.FilePath, saveImageStream);
                }

                await client.SendTextMessageAsync(message.Chat.Id, "Thx for the Pics");
            }
        }
    }
}
