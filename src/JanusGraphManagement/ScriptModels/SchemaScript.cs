using GQL.JanusGraphClients.Configs;
using GQL.JanusGraphManagements.ScriptModels.Indexes;

namespace GQL.JanusGraphManagements.ScriptModels
{
    public class SchemaScript
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int SchemaVersion { get; set; } = 1;



        public JanusGraphDatabaseConfig Connection { get; set; } = new JanusGraphDatabaseConfig();



        public List<PropertySchema> Properties { get; set; } = new List<PropertySchema>();

        public List<VertexSchema> Vertices { get; set; } = new List<VertexSchema>();

        public List<EdgeSchema> Edges { get; set; } = new List<EdgeSchema>();

        public List<CompositeIndexSchema> CompositeIndexes { get; set; } = new List<CompositeIndexSchema>();

        public List<MixedIIndexSchema> MixedIndexes { get; set; } = new List<MixedIIndexSchema>();
    }
}
