using Gremlin.Net.Structure;
using Newtonsoft.Json;

namespace GQL.OrientDBClients.Serializers.Models
{
    /// <summary>
    /// Edge Record
    /// </summary>
    public class OEdgeRecord : IGraphSONToken
    {
        public const string TypeName = "g:Edge";

        [JsonProperty("id")]
        public ORecordId Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("inV")]
        public ORecordId InV { get; set; }

        [JsonProperty("inVLabel")]
        public string InVLabel { get; set; }

        [JsonProperty("outV")]
        public ORecordId OutV { get; set; }

        [JsonProperty("outVLabel")]
        public string OutVLabel { get; set; }


        public OEdgeRecord()
        {

        }
        public OEdgeRecord(Edge edge)
        {
            Id = new ORecordId(edge.Id);
            Label = edge.Label;
            InV = new ORecordId(edge.InV.Id);
            InVLabel = edge.InV.Label;
            OutV = new ORecordId(edge.OutV.Id);
            OutVLabel = edge.OutV.Label;
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
                { GetPropertyName(nameof(Label)), Label },
                { GetPropertyName(nameof(InV)), InV.ToDictionary() },
                { GetPropertyName(nameof(InVLabel)), Label },
                { GetPropertyName(nameof(OutV)), OutV.ToDictionary() },
                { GetPropertyName(nameof(OutVLabel)), OutVLabel }
            };
        }
    }
}
