﻿namespace OmniSharp.Extensions.DebugAdapter.Protocol.Models
{
    /// <summary>
    /// A Thread
    /// </summary>
    public record Thread
    {
        /// <summary>
        /// Unique identifier for the thread.
        /// </summary>
        public long Id { get; init; }

        /// <summary>
        /// A name of the thread.
        /// </summary>
        public string Name { get; init; } = null!;
    }
}
