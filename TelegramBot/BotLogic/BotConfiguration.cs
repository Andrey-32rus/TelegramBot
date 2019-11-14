using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBot.BotLogic
{
    public class BotConfiguration
    {
        public string BotToken { get; set; }
        public string ProxyHost { get; set; }
        public int ProxyPort { get; set; }
        public string WebHooksUrl { get; set; }
    }
}
