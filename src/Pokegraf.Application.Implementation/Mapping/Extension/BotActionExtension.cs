using AutoMapper;
using Pokegraf.Application.Implementation.BotActions.Commands.About;
using Pokegraf.Application.Implementation.BotActions.Commands.Fusion;
using Pokegraf.Application.Implementation.BotActions.Commands.Pokemon;
using Pokegraf.Application.Implementation.BotActions.Commands.Start;
using Pokegraf.Application.Implementation.BotActions.Common;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Implementation.Mapping.Extension
{
    public static class BotActionExtension
    {
        internal static IMapper Mapper { get; set; }

        static BotActionExtension()
        {
            Mapper = new MapperConfiguration(cfg => { }).CreateMapper();
        }

        public static BotAction ToBotAction(this Message message)
        {
            return new BotAction()
            {
                MessageId = message.MessageId,
                Chat = message.Chat,
                From = message.From,
                Text = message.Text
            };
        }
        
        public static AboutCommandAction ToAboutCommandAction(this BotAction botAction)
        {
            return botAction != null
                ? Mapper.Map<AboutCommandAction>(botAction)
                : null;
        }

        public static StartCommandAction ToStartCommandAction(this BotAction botAction)
        {
            return botAction != null
                ? Mapper.Map<StartCommandAction>(botAction)
                : null;
        }
        
        public static PokemonCommandAction ToPokemonCommandAction(this BotAction botAction)
        {
            return botAction != null
                ? Mapper.Map<PokemonCommandAction>(botAction)
                : null;
        }
        
        public static FusionCommandAction ToFusionCommandAction(this BotAction botAction)
        {
            return botAction != null
                ? Mapper.Map<FusionCommandAction>(botAction)
                : null;
        }
    }
}