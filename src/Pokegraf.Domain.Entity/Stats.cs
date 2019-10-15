using System;

namespace Pokegraf.Domain.Entity
{
    public class Stats
    {
        /// <summary>
        /// Id of the stats.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Id of the user.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Request related stats.
        /// </summary>
        public RequestStats Requests { get; set; }
        
        public User User { get; set; }

        public class RequestStats
        {
            /// <summary>
            /// Id of the request stats.
            /// </summary>
            public Guid Id { get; set; }
            /// <summary>
            /// Number of times a pokemon request has been made.
            /// </summary>
            public int Pokemon { get; set; }
            /// <summary>
            /// Number of times a fusion request has been made.
            /// </summary>
            public int Fusion { get; set; }
        }
    }
}