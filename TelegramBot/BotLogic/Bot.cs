using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
            string sock5Host = AppConfig.GetValue<string>("BotConfiguration:Socks5Host");
            int sock5Port = AppConfig.GetValue<int>("BotConfiguration:Socks5Port");
            //Client = new TelegramBotClient(token);
            //Client.SetWebhookAsync("http://b8216a98.ngrok.io/update").Wait();
            //var str = UtilsLib.HttpUtils.Get("http://b8216a98.ngrok.io/ping");
        }
    }
}
