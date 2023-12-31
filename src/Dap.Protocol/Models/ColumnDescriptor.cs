﻿using OmniSharp.Extensions.DebugAdapter.Protocol.Serialization;

namespace OmniSharp.Extensions.DebugAdapter.Protocol.Models
{
    /// <summary>
    /// A ColumnDescriptor specifies what module attribute to show in a column of the ModulesView, how to format it, and what the column’s label should be.
    /// It is only used if the underlying UI actually supports this level of customization.
    /// </summary>
    public record ColumnDescriptor
    {
        /// <summary>
        /// Name of the attribute rendered in this column.
        /// </summary>
        public string AttributeName { get; init; } = null!;

        /// <summary>
        /// Header UI label of column.
        /// </summary>
        public string Label { get; init; } = null!;

        /// <summary>
        /// Format to use for the rendered values in this column. TBD how the format strings looks like.
        /// </summary>
        [Optional]
        public string? Format { get; init; }

        /// <summary>
        /// Datatype of values in this column.  Defaults to 'string' if not specified.
        /// </summary>
        [Optional]
        public ColumnDescriptorType? Type { get; init; }

        /// <summary>
        /// Width of this column in characters (hint only).
        /// </summary>
        [Optional]
        public long? Width { get; init; }
    }
}
