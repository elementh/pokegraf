using System;
using System.Collections.Generic;

namespace Pokegraf.Infrastructure.Contract.Dto.Pokemon
{
    public class BerryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan GrowthTime { get; set; }
        public TypeDto NaturalGiftType { get; set; }
        public List<string> Flavors { get; set; }
        public string Firmness { get; set; }
        public int MaxHarvest { get; set; }
        public int Smoothness { get; set; }
    }
}