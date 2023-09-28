using GQL.JanusGraphClients.Managements.Schema;

namespace GQL.JanusGraphManagements.ScriptModels
{
    public class EdgeSchema
    {
        public string Label { get; set; } = string.Empty;

        public string Multiplicity { get; set; } = EdgeMultiplicity.Multi;
    }
}
