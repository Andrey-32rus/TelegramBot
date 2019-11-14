using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot.BotLogic
{
    public interface IBot
    {
        Task Update(Update update);
    }
}