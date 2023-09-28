using Gremlin.Net.Structure.IO.GraphSON;
using System.Text.Json;

namespace GQL.OrientDBClients.Serializers
{
    public class OrientdbGraphSONDeserializer : IGraphSONDeserializer
    {
        public dynamic Objectify(JsonElement graphsonObject, GraphSONReader reader)
        {
            return graphsonObject.ToString();
        }
    }
}
