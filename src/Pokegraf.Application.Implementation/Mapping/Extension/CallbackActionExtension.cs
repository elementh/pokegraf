using System.Collections.Specialized;
using AutoMapper;
using Newtonsoft.Json;
using Pokegraf.Application.Implementation.BotActions.Callbacks.PokemonNext;
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
            OrderedDictionary callbackData;
            try
            {
                callbackData = JsonConvert.DeserializeObject<OrderedDictionary>(callbackQuery.Data);
            }
            catch
            {
                callbackData = new OrderedDictionary();
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
    }
}