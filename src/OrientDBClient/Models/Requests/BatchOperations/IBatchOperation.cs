using Newtonsoft.Json;

namespace GQL.OrientDBClients.Models.Requests.BatchOperations
{
    public interface IBatchOperation
    {
        [JsonProperty("type")]
        string Type { get; }
    }
}
