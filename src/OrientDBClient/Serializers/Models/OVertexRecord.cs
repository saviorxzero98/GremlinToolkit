using Gremlin.Net.Structure;
using Newtonsoft.Json;

namespace GQL.OrientDBClients.Serializers.Models
{
    /// <summary>
    /// Vertex Record
    /// </summary>
    public class OVertexRecord : IGraphSONToken
    {
        public const string TypeName = "g:Vertex";

        [JsonProperty("id")]
        public ORecordId Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }


        public OVertexRecord()
        {

        }
        public OVertexRecord(Vertex vertex)
        {
            Id = new ORecordId(vertex.Id);
            Label = vertex.Label;
        }


        /// <summary>
        /// To Dictionary
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, dynamic> ToDictionary()
        {
            return new Dictionary<string, dynamic>()
            {
                { GetPropertyName(nameof(Id)), Id.ToDictionary() },
                { GetPropertyName(nameof(Label)), Label }
            };
        }
    }
}
