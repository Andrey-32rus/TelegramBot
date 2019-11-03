using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MihaZupan;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UtilsLib;

namespace TelegramBot.BotLogic
{
    public static class Bot
    {
        private static TelegramBotClient Client;

        public static void Initialize()
        {
            // use proxy if configured in appsettings.*.json
            var config = AppConfig.GetSection<BotConfiguration>("BotConfiguration");
            //string token = AppConfig.GetValue<string>("BotConfiguration:BotToken");
            //string socks5Host = AppConfig.GetValue<string>("BotConfiguration:Socks5Host");
            //int socks5Port = AppConfig.GetValue<int>("BotConfiguration:Socks5Port");
            //string webHooksUrl = AppConfig.GetValue<string>("BotConfiguration:WebHooksUrl");
            Client = new TelegramBotClient(config.BotToken, new HttpToSocks5Proxy(config.Socks5Host, config.Socks5Port));
            Client.SetWebhookAsync(config.WebHooksUrl).Wait();
            //var str = UtilsLib.HttpUtils.Get("http://b8216a98.ngrok.io/ping");
        }

        public static async Task Update(Update update)
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
                await Client.SendTextMessageAsync(message.Chat.Id, message.Text);
            }
            else if (message.Type == MessageType.Photo)
            {
                // Download Photo
                var fileId = message.Photo.LastOrDefault()?.FileId;
                var file = await Client.GetFileAsync(fileId);

                var filename = file.FileId + "." + file.FilePath.Split('.').Last();

                using (var saveImageStream = System.IO.File.Open(filename, FileMode.Create))
                {
                    await Client.DownloadFileAsync(file.FilePath, saveImageStream);
                }

                await Client.SendTextMessageAsync(message.Chat.Id, "Thx for the Pics");
            }
        }
    }
}
