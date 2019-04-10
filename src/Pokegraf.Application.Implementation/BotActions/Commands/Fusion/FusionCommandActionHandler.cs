using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Pokegraf.Application.Implementation.BotActions.Commands.Pokemon;
using Pokegraf.Application.Implementation.BotActions.Responses.Photo;
using Pokegraf.Common.Result;

namespace Pokegraf.Application.Implementation.BotActions.Commands.Fusion
{
    public class FusionCommandActionHandler : Pokegraf.Common.Request.RequestHandler<FusionCommandAction, Result>
    {
        private string[] FirstHalf = {"Bulb", "Ivy", "Venu", "Char", "Char", "Char", "Squirt", "War", "Blast", "Cater", "Meta", "Butter", 
                "Wee", "Kak", "Bee", "Pid", "Pidg", "Pidg", "Rat", "Rat", "Spear", "Fear", "Ek", "Arb", "Pika", "Rai", "Sand", "Sand", "Nido", 
                "Nido", "Nido", "Nido", "Nido", "Nido", "Clef", "Clef", "Vul", "Nine", "Jiggly", "Wiggly", "Zu", "Gol", "Odd", "Gloo", "Vile", 
                "Pa", "Para", "Veno", "Veno", "Dig", "Dug", "Meow", "Per", "Psy", "Gol", "Man", "Prime", "Grow", "Arca", "Poli", "Poli", "Poli", 
                "Ab", "Kada", "Ala", "Ma", "Ma", "Ma", "Bell", "Weepin", "Victree", "Tenta", "Tenta", "Geo", "Grav", "Gol", "Pony", "Rapi", 
                "Slow", "Slow", "Magne", "Magne", "Far", "Do", "Do", "See", "Dew", "Gri", "Mu", "Shell", "Cloy", "Gas", "Haunt", "Gen", "On", 
                "Drow", "Hyp", "Krab", "King", "Volt", "Electr", "Exegg", "Exegg", "Cu", "Maro", "Hitmon", "Hitmon", "Licki", "Koff", "Wee", 
                "Rhy", "Rhy", "Chan", "Tang", "Kangas", "Hors", "Sea", "Gold", "Sea", "Star", "Star", "Mr.", "Scy", "Jyn", "Electa", "Mag", "Pin",
                "Tau", "Magi", "Gyara", "Lap", "Dit", "Ee", "Vapor", "Jolt", "Flare", "Pory", "Oma", "Oma", "Kabu", "Kabu", "Aero", "Snor", 
                "Artic", "Zap", "Molt", "Dra", "Dragon", "Dragon", "Mew", "Mew"};
            
        private string[] SecondHalf = {"basaur", "ysaur", "usaur", "mander", "meleon", "izard", "tle", "tortle", "toise", "pie", "pod",
                "free", "dle", "una", "drill", "gey", "eotto", "eot", "tata", "icate", "row", "row", "kans", "bok", "chu", "chu", "shrew",
                "slash", "oran", "rina", "queen", "ran", "rino", "king", "fairy", "fable", "pix", "tales", "puff", "tuff", "bat", "bat", "ish",
                "oom", "plume", "ras", "sect", "nat", "moth", "lett", "trio", "th", "sian", "duck", "duck", "key", "ape", "lithe", "nine", "wag",
                "whirl", "wrath", "ra", "bra", "kazam", "chop", "choke", "champ", "sprout", "bell", "bell", "cool", "cruel", "dude", "eler", "em", 
                "ta", "dash", "poke", "bro", "mite", "ton", "fetchd", "duo", "drio", "eel", "gong", "mer", "uk", "der", "ster", "tly", "ter",
                "gar", "ix", "zee", "no", "by", "ler", "orb", "ode", "cute", "utor", "bone", "wak", "lee", "chan", "tung", "fing", "zing", "horn",
                "don", "sey", "gela", "khan", "sea", "dra", "deen", "king", "yu", "mie", "mime", "ther", "nx", "buzz", "mar", "sir", "ros", 
                "karp", "dos", "ras", "to", "vee", "eon", "eon", "eon", "gon", "nyte", "star", "to", "tops", "dactyl", "lax", "cuno", "dos", 
                "tres", "tini", "nair", "nite", "two", "ew"};
        
        public FusionCommandActionHandler(ILogger<Pokegraf.Common.Request.RequestHandler<FusionCommandAction, Result>> logger, IMediator mediatR) : base(logger, mediatR)
        {
        }

        public override async Task<Result> Handle(FusionCommandAction request, CancellationToken cancellationToken)
        {
            var firstPokemon = new Random().Next(1, 151);
            var secondPokemon = new Random().Next(1, 151);
            
            while (firstPokemon == secondPokemon) {
                secondPokemon = new Random().Next(1, 151);
            }

            var photoUrl = $"http://images.alexonsager.net/pokemon/fused/{firstPokemon}/{firstPokemon}.{secondPokemon}.png";
            var caption = $"{FirstHalf[secondPokemon - 1]}{SecondHalf[firstPokemon - 1]}";
            
            return await MediatR.Send(new PhotoWithCaptionResponse(request.Chat.Id,photoUrl,caption));
        }
    }
}