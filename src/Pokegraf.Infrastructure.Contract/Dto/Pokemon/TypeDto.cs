using System.Collections.Generic;

namespace Pokegraf.Infrastructure.Contract.Dto.Pokemon
{
    public class TypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Generation { get; set; }
        public string MoveDamageClass { get; set; }
        public List<string> DoubleDamageFrom { get; set; }
        public List<string> DoubleDamageTo { get; set; }
        public List<string> HalfDamageFrom { get; set; }
        public List<string> HalfDamageTo { get; set; }
        public List<string> NoDamageFrom { get; set; }
        public List<string> NoDamageTo { get; set; }
        public List<string> Pokemon { get; set; }
    }
}