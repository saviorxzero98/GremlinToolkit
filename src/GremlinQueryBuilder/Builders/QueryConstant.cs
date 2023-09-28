namespace GQL.QueryBuilders.Builders
{
    public static class QueryConstant
    {
        public const string DefaultTraversalSource = "g";

        public enum Scope
        {
            Global,
            Local
        }

        public enum OrderBy
        {
            Default = 0,
            Asc,
            Desc,
            Shuffle
        }
    }

    public enum PropertyCardinalityType { Signle, List, Set }
}
