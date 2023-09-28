using Gremlin.Net.Structure;
using Newtonsoft.Json;

namespace GQL.OrientDBClients.Serializers.Models
{
    public class GraphSONToken : IGraphSONToken
    {
        [JsonProperty("@type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("@value")]
        public IGraphSONToken Value { get; set; }


        public GraphSONToken()
        {

        }
        public static GraphSONToken FromVertex(Vertex vertex)
        {
            var token = new GraphSONToken()
            {
                Type = OVertexRecord.TypeName,
                Value = new OVertexRecord(vertex)
            };
            return token;
        }
        public static GraphSONToken FromEdge(Edge edge)
        {
            var token = new GraphSONToken()
            {
                Type = OEdgeRecord.TypeName,
                Value = new OEdgeRecord(edge)
            };
            return token;
        }

        /// <summary>
        /// To Dictionary
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, dynamic> ToDictionary()
        {
            return new Dictionary<string, dynamic>()
            {
                { GetPropertyName(nameof(Type)), Type },
                { GetPropertyName(nameof(Value)), Value.ToDictionary() }
            };
        }
    }
}
