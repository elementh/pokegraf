namespace Pokegraf.Common.Helper
{
    public static class CacheKeys
    {
        public static string GlobalStats => "_GlobalStats";
        public static string Pokemon(object pokeNumber) => $"pokemon:{pokeNumber}";
    }
}