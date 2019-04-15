using AutoMapper;
using Pokegraf.Application.Implementation.BotActions.Commands.About;
using Pokegraf.Application.Implementation.BotActions.Commands.Fusion;
using Pokegraf.Application.Implementation.BotActions.Commands.Pokemon;
using Pokegraf.Application.Implementation.BotActions.Commands.Start;
using Pokegraf.Application.Implementation.BotActions.Common;
using Pokegraf.Application.Implementation.Common.Actions;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Implementation.Mapping.Extension
{
    public static class CommandActionExtension
    {
        internal static IMapper Mapper { get; set; }

        static CommandActionExtension()
        {
            Mapper = new MapperConfiguration(cfg => { }).CreateMapper();
        }

        public static CommandAction ToCommandAction(this Message message)
        {
            return new CommandAction()
            {
                MessageId = message.MessageId,
                Chat = message.Chat,
                From = message.From,
                Text = message.Text
            };
        }
        
        public static AboutCommandAction ToAboutCommandAction(this CommandAction botAction)
        {
            return botAction != null
                ? Mapper.Map<AboutCommandAction>(botAction)
                : null;
        }

        public static StartCommandAction ToStartCommandAction(this CommandAction botAction)
        {
            return botAction != null
                ? Mapper.Map<StartCommandAction>(botAction)
                : null;
        }
        
        public static PokemonCommandAction ToPokemonCommandAction(this CommandAction botAction)
        {
            return botAction != null
                ? Mapper.Map<PokemonCommandAction>(botAction)
                : null;
        }
        
        public static FusionCommandAction ToFusionCommandAction(this CommandAction botAction)
        {
            return botAction != null
                ? Mapper.Map<FusionCommandAction>(botAction)
                : null;
        }
    }
}