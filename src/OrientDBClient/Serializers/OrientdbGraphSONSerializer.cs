using GQL.OrientDBClients.Serializers.Models;
using Gremlin.Net.Structure;
using Gremlin.Net.Structure.IO.GraphSON;

namespace GQL.OrientDBClients.Serializers
{
    public class OrientdbGraphSONSerializer : IGraphSONSerializer
    {
        protected const string ORecordIdKey = OrientdbMessageSerializer.ORecordIdKey;

        public Dictionary<string, dynamic> Dictify(dynamic objectData, GraphSONWriter writer)
        {
            if (objectData != null)
            {
                if (objectData is Vertex ||
                    objectData is Edge)
                {
                    return Serialize(objectData);
                }
            }
            return new Dictionary<string, dynamic>();
        }

        protected Dictionary<string, dynamic> Serialize(Vertex vertex)
        {
            var graphSON = GraphSONToken.FromVertex(vertex);
            var dict = graphSON.ToDictionary();
            return dict;
        }

        protected Dictionary<string, dynamic> Serialize(Edge edge)
        {
            var graphSON = GraphSONToken.FromEdge(edge);
            var dict = graphSON.ToDictionary();
            return dict;
        }
    }
}
