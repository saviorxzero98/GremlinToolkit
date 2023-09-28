using GQL.OrientDBClients.Configs;
using GQL.QueryBuilders.Builders.Traversals;
using Gremlin.Net.Process.Traversal;
using Xunit;

namespace GQL.OrientDBClients.Test
{
    public class OrientDBGremlinClientTest : IDisposable
    {
        private OrientDBGremlinClient _service;

        public OrientDBGremlinClientTest()
        {
            var config = new OrientGremlinConfig().SetHost("localhost", 8182)
                                                  .SetAccount("root", "<Your Password>");

            _service = new OrientDBGremlinClient(config);

            GraphTraversalSource source = _service.GetTraversalSource();
            source.AddV("person").Property("name", "john").Iterate();
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
                               .Has("name", TextP.Containing("john"))
                               .Next();

            var endTime = DateTime.Now;

            double elapsedMs = (endTime - startTime).TotalMilliseconds;
        }

        [Fact]
        public async Task TestSubmitAsync()
        {
            var startTime = DateTime.Now;

            //string gremlinCommand = "g.V().has('name', containing('john'))";
            string gremlinCommand = GraphTraversal.Graph()
                                              .Vertex()
                                              .Has("name", "john")
                                              .ToString();

            var result = await _service.SubmitAsync<dynamic>(gremlinCommand);
            var endTime = DateTime.Now;

            double elapsedMs = (endTime - startTime).TotalMilliseconds;
        }
    }
}
