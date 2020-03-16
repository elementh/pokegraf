using System;

namespace Pokegraf.Core.Domain.Stats.Model
{
    public class GlobalStats
    {
        public GlobalStats(int pokemonRequests, int fusionRequests, int users, int chats)
        {
            Timestamp = DateTime.UtcNow;
            PokemonRequests = pokemonRequests;
            FusionRequests = fusionRequests;
            Users = users;
            Chats = chats;
        }

        public DateTime Timestamp { get; }
        public int PokemonRequests { get; }
        public int FusionRequests { get; }
        public int Users { get; }
        public int Chats { get; }
    }
}