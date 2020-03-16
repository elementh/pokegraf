using System;
using System.Threading;

namespace Pokegraf.Common.Helper
{
    /// <summary>
    /// Random provider thanks to https://csharpindepth.com/Articles/Random
    /// </summary>
    public static class RandomProvider
    {    
        private static int _seed = Environment.TickCount;

        private static readonly ThreadLocal<Random> RandomWrapper = new ThreadLocal<Random>
            (() => new Random(Interlocked.Increment(ref _seed)));

        public static Random? GetThreadRandom()
        {
            return RandomWrapper.Value;
        }
    }
}