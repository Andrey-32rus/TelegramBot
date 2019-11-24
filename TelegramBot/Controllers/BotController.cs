using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.BotLogic;
using UtilsLib;

namespace TelegramBot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly IBot botLogic;

        public BotController(IBot bot)
        {
            botLogic = bot;
        }

        [HttpPost]
        public ActionResult Post(Update request)
        {
            try
            {
                var task = botLogic.Update(request);
                return Ok();
            }
            catch (Exception e)
            {
                MyLogger.Log(e.ToString(), LogLevel.Error, "BotController.Update");
                return BadRequest();
            }
        }
    }
}
