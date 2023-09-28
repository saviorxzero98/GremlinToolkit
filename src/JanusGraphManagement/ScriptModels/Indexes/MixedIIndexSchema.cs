using GQL.JanusGraphClients.Managements.Schema;

namespace GQL.JanusGraphManagements.ScriptModels.Indexes
{
    public class MixedIIndexSchema
    {
        /// <summary>
        /// Index Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Property Key
        /// </summary>
        public string PropertyKey { get; set; }

        /// <summary>
        /// Index Only Vertex Label
        /// </summary>
        public string OnlyVertexLabel { get; set; } = string.Empty;

        /// <summary>
        /// Backing Index
        /// </summary>
        public string BackingIndex { get; set; } = BackingIndexName.Default;

        /// <summary>
        /// Mapping Type
        /// </summary>
        public string MappingType { get; set; } = MixedIndexMapping.Default;
    }
}
