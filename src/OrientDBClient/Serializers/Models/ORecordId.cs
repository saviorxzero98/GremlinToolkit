using Newtonsoft.Json;

namespace GQL.OrientDBClients.Serializers.Models
{
    /// <summary>
    /// Orient Record Id
    /// </summary>
    public class ORecordId : IGraphSONToken
    {
        public const string TypeName = "orient:ORecordId";

        [JsonProperty("@type")]
        public string Type { get; set; } = TypeName;

        [JsonProperty("@value")]
        public string Value { get; set; }

        public ORecordId()
        {
            Type = TypeName;
        }
        public ORecordId(object id)
        {
            Type = TypeName;
            Value = Convert.ToString(id);
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
                { GetPropertyName(nameof(Value)), Value }
            };
        }
    }
}
