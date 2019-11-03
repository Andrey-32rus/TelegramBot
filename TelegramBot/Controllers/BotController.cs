using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace TelegramBot.Controllers
{
    [Route("")]
    [ApiController]
    public class BotController : ControllerBase
    {
        [HttpPost("update")]
        public void Update(Update request)
        {
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadToEndAsync().Result;
                var obj = JsonConvert.DeserializeObject<Update>(body);
            }
        }
    }
}
