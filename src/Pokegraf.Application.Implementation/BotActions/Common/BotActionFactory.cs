using System.Linq;
using Pokegraf.Application.Contract.BotAction.Common;
using Pokegraf.Application.Implementation.BotActions.Commands.Fusion;
using Pokegraf.Application.Implementation.BotActions.Commands.Pokemon;
using Pokegraf.Application.Implementation.Mapping.Extension;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Pokegraf.Application.Implementation.BotActions.Common
{
    public class BotActionFactory : IBotActionFactory
    {
        public IBotAction GetBotAction(Message message)
        {
            var command = GetCommand(message);

            var botAction = ToBotAction(message);

            if (command == null) return botAction;
            
            switch (command)
            {
                case "/pokemon":
                case "pkm":
                    return botAction.ToPokemonCommandAction();
                case "/fusion":
                    return botAction.ToFusionCommandAction();
                default:
                    return botAction;
            }
        }
        
        private string GetCommand(Message message)
        {
            if (message.Entities?.First()?.Type != MessageEntityType.BotCommand) return null;
            
            var command = message.EntityValues.First();

            if (!command.Contains('@')) return command;
                
            if (!command.ToLower().Contains("@pokegraf")) return null;

            command = command.Substring(0, command.IndexOf('@'));

            return command;
        }
        
        private BotAction ToBotAction(Message message)
        {
            return new BotAction()
            {
                MessageId = message.MessageId,
                Chat = message.Chat,
                From = message.From,
                Text = message.Text
            };
        }
    }
}