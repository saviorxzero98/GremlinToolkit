using GQL.JanusGraphClients.Configs;
using Gremlin.Net.Process.Traversal;
using Xunit;

namespace GQL.JanusGraphClients.Test
{
    public class JanusGraphGremlinClientTest : IDisposable
    {
        private JanusGraphGremlinClient _service;

        public JanusGraphGremlinClientTest()
        {
            var config = new JanusGraphDatabaseConfig().SetHost("localhost", 8182);

            _service = new JanusGraphGremlinClient(config);
        }

        public void Dispose()
        {
            // Clear Edge & Vertex
            _service.ClearAllEdge();
            _service.ClearAllVertex();

            // Close Gremlin Client
            _service.CloseConnection();
        }

        [Fact]
        public void TestGraphTraversal()
        {
            var startTime = DateTime.Now;

            GraphTraversalSource source = _service.GetTraversalSource();

            var result = source.V()
                               .Has("名稱", TextP.Containing("Alice"))
                               .Next();

            var endTime = DateTime.Now;

            double elapsedMs = (endTime - startTime).TotalMilliseconds;
        }
    }
}
