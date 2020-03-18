using System.Collections.Generic;
using Navigator.Abstraction;
using Navigator.Extensions.Actions;
using Newtonsoft.Json;

namespace Pokegraf.Core.Domain.Actions.Callback.PokemonNext
{
    public class PokemonNextCallbackAction : CallbackQueryAction
    {
        public Dictionary<string, string> ParsedData { get; set; }

        public PokemonNextCallbackAction()
        {
            ParsedData = new Dictionary<string, string>();
        }

        public override IAction Init(INavigatorContext ctx)
        {
            base.Init(ctx);

            Dictionary<string, string> callbackData;
            try
            {
                callbackData = JsonConvert.DeserializeObject<Dictionary<string, string>>(Data);
                ParsedData = callbackData;
            }
            catch
            {
                // ignored
            }
            
            return this;
        }

        public override bool CanHandle(INavigatorContext ctx)
        {
            return ParsedData.TryGetValue("action", out var action) && action == "pokemon_next";
        }
    }
}