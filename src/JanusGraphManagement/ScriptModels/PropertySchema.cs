using GQL.JanusGraphClients.Managements.Schema;

namespace GQL.JanusGraphManagements.ScriptModels
{
    public class PropertySchema
    {
        public string Key { get; set; } = string.Empty;

        public string DataType { get; set; } = PropertyDataType.Object;

        public string Cardinality { get; set; } = PropertyCardinality.Single;
    }
}
