using System;

namespace Pokegraf.Infrastructure.Contract.Dto
{
    public class PokemonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Uri Image { get; set; }
        public Tuple<int, string> Before { get; set; }
        public Tuple<int, string> Next { get; set; }
    }
}