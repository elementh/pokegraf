using System;

namespace Pokegraf.Infrastructure.Contract.Dto
{
    public class PokemonFusionDto
    {
        public string Name { get; set; }
        public Uri Image { get; set; }

        public PokemonFusionDto(string name, Uri image)
        {
            Name = name;
            Image = image;
        }
    }
}