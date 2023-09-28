using Newtonsoft.Json;

namespace GQL.OrientDBClients.Models.Responses
{
    public class PostCommandResponse
    {
        [JsonProperty("result")]
        public object Result { get; set; }

        [JsonProperty("elapsedMs")]
        public int ElapsedMilliseconds { get; set; }
    }
}
