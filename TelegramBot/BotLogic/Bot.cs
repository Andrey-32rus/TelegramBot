using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MihaZupan;
using Telegram.Bot;
using UtilsLib;

namespace TelegramBot.BotLogic
{
    public static class Bot
    {
        private static TelegramBotClient Client;

        public static void Initialize()
        {
            // use proxy if configured in appsettings.*.json
            string token = AppConfig.GetValue<string>("BotConfiguration:BotToken");
            string socks5Host = AppConfig.GetValue<string>("BotConfiguration:Socks5Host");
            int socks5Port = AppConfig.GetValue<int>("BotConfiguration:Socks5Port");
            string webHooksUrl = AppConfig.GetValue<string>("BotConfiguration:WebHooksUrl");
            Client = new TelegramBotClient(token, new HttpToSocks5Proxy(socks5Host, socks5Port));
            Client.SetWebhookAsync(webHooksUrl).Wait();
            //var str = UtilsLib.HttpUtils.Get("http://b8216a98.ngrok.io/ping");
        }
    }
}
