using Newtonsoft.Json;

namespace GQL.OrientDBClients.Models.Responses
{
    public class PostBatchResponse
    {
        [JsonProperty("result")]
        public List<object> Result { get; set; } = new List<object>();

        [JsonProperty("errors")]
        public List<object> Errors { get; set; } = new List<object>();
    }
}
