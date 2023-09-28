using GQL.OrientDBClients.Serializers.Models;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure;
using Gremlin.Net.Structure.IO.GraphSON;

namespace GQL.OrientDBClients.Serializers
{
    public class OrientdbMessageSerializer
    {
        public const string ORecordIdKey = "orient:ORecordId";

        /// <summary>
        /// Get Message Serializer
        /// </summary>
        /// <returns></returns>
        public static IMessageSerializer GetSerializer()
        {
            // GraphSON 3 Reader
            var readerDict = new Dictionary<string, IGraphSONDeserializer>
            {
                { ORecordId.TypeName, new OrientdbGraphSONDeserializer() },
            };
            var reader = new GraphSON3Reader(readerDict);

            // GraphSON 3 Writer
            var writerDict = new Dictionary<Type, IGraphSONSerializer>
            {
                { typeof(Vertex), new OrientdbGraphSONSerializer() },
                { typeof(Edge), new OrientdbGraphSONSerializer() }
            };
            var writer = new GraphSON3Writer(writerDict);

            // GraphSON 3 Message Serializer
            var messageSerializer = new GraphSON3MessageSerializer(reader, writer);
            return messageSerializer;
        }
    }
}
