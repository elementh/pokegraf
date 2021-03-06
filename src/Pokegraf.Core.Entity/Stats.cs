﻿using System;

namespace Pokegraf.Core.Entity
{
    public class Stats
    {
        public Stats()
        {
            Requests = new RequestStats();
        }

        /// <summary>
        /// Id of the stats.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Id of the trainer.
        /// </summary>
        public int TrainerId { get; set; }
        /// <summary>
        /// Trainer
        /// </summary>
        public Trainer Trainer { get; set; }
        /// <summary>
        /// Request related stats.
        /// </summary>
        public RequestStats Requests { get; set; }

        public class RequestStats
        {
            /// <summary>
            /// Id of the request stats.
            /// </summary>
            public Guid Id { get; set; } = Guid.NewGuid();
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