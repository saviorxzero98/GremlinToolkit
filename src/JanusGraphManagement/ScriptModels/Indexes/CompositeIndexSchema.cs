namespace GQL.JanusGraphManagements.ScriptModels.Indexes
{
    public class CompositeIndexSchema
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
        /// Is Unique
        /// </summary>
        public bool IsUnique { get; set; } = false;
    }
}
