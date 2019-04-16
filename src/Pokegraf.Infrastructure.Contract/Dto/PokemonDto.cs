using System;

namespace Pokegraf.Infrastructure.Contract.Dto
{
    public class PokemonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StatsDto Stats { get; set; }
        public Uri Image { get; set; }
        public Uri Sprite { get; set; }
        public Tuple<int, string> Before { get; set; }
        public Tuple<int, string> Next { get; set; }

        public class StatsDto
        {
            public int Health { get; set; }
            public int Attack { get; set; }
            public int Defense  { get; set; }
            public int SpecialAttack { get; set; }
            public int SpecialDefense { get; set; }
            public int Speed  { get; set; }
        }
    }
}