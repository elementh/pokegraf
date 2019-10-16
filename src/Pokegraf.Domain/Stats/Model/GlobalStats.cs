using System;

namespace Pokegraf.Domain.Stats.Model
{
    public class GlobalStats
    {
        public DateTime Timestamp { get; set; }
        public int PokemonRequests { get; set; }
        public int FusionRequests { get; set; }
        public int Users { get; set; }
        public int Chats { get; set; }
    }
}