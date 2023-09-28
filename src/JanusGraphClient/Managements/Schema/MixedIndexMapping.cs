namespace GQL.JanusGraphClients.Managements.Schema
{
    public class MixedIndexMapping
    {
        /// <summary>
        /// Default
        /// </summary>
        public const string Default = "DEFAULT";

        /// <summary>
        /// Full-Text Search
        /// </summary>
        public const string Text = "TEXT";

        /// <summary>
        /// String Search
        /// </summary>
        public const string String = "STRING";

        /// <summary>
        /// Full text and string search
        /// </summary>
        public const string TextString = "TEXTSTRING";

        /// <summary>
        /// Geo Search
        /// </summary>
        public const string Geo = "PREFIX_TREE";
    }
}
