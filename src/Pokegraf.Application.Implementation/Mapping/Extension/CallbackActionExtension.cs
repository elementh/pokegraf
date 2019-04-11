using System.Collections.Generic;
using System.Collections.Specialized;
using AutoMapper;
using Newtonsoft.Json;
using Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonBefore;
using Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonDescription;
using Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonNext;
using Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonStats;
using Pokegraf.Application.Implementation.BotActions.Common;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Implementation.Mapping.Extension
{
    public static class CallbackActionExtension
    {
        internal static IMapper Mapper { get; set; }

        static CallbackActionExtension()
        {
            Mapper = new MapperConfiguration(cfg => { }).CreateMapper();
        }
        
        public static CallbackAction ToCallbackAction(this CallbackQuery callbackQuery)
        {
            Dictionary<string, string> callbackData;
            try
            {
                callbackData = JsonConvert.DeserializeObject<Dictionary<string, string>>(callbackQuery.Data);
            }
            catch
            {
                callbackData = new Dictionary<string, string>();
            }
            
            return new CallbackAction()
            {
                MessageId = callbackQuery.Message.MessageId,
                Chat = callbackQuery.Message.Chat,
                From = callbackQuery.From,
                Text = callbackQuery.Data,
                Data = callbackData
            };
        }
        
        public static PokemonNextCallbackAction ToPokemonNextCallbackAction(this CallbackAction callbackAction)
        {
            return callbackAction != null
                ? Mapper.Map<PokemonNextCallbackAction>(callbackAction)
                : null;
        }        
        
        public static PokemonBeforeCallbackAction ToPokemonBeforeCallbackAction(this CallbackAction callbackAction)
        {
            return callbackAction != null
                ? Mapper.Map<PokemonBeforeCallbackAction>(callbackAction)
                : null;
        }   
        
        public static PokemonStatsCallbackAction ToPokemonStatsCallbackAction(this CallbackAction callbackAction)
        {
            return callbackAction != null
                ? Mapper.Map<PokemonStatsCallbackAction>(callbackAction)
                : null;
        }   
        
        public static PokemonDescriptionCallbackAction ToPokemonDescriptionCallbackAction(this CallbackAction callbackAction)
        {
            return callbackAction != null
                ? Mapper.Map<PokemonDescriptionCallbackAction>(callbackAction)
                : null;
        }
    }
}