using AutoMapper;
using Pokegraf.Application.Implementation.BotActions.Commands.Fusion;
using Pokegraf.Application.Implementation.BotActions.Commands.Pokemon;
using Pokegraf.Application.Implementation.BotActions.Common;

namespace Pokegraf.Application.Implementation.Mapping.Extension
{
    public static class BotActionExtension
    {
        internal static IMapper Mapper { get; set; }

        static BotActionExtension()
        {
            Mapper = new MapperConfiguration(cfg => { }).CreateMapper();
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