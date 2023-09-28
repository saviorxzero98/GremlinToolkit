using GQL.OrientDBClients.Configs;
using GQL.OrientDBClients.Models;
using GQL.OrientDBClients.Models.Requests;
using GQL.QueryBuilders.Builders.Traversals;
using Xunit;

namespace GQL.OrientDBClients.Test
{
    public class OrientDBRestClientTest
    {
        private OrientDBRestClient _client;

        public OrientDBRestClientTest()
        {
            var config = new OrientDBConfig().SetHost("localhost", 2480)
                                             .SetAccount("root", "<Your Password>")
                                             .SetDatabase("graphdb");

            _client = new OrientDBRestClient(config);
        }

        [Fact]
        public async Task TestPostGremlinCommand()
        {
            //string gremlinCommand = "g.V().hasLabel('department').toList()";
            string gremlinCommand = GraphTraversal.Graph()
                                              .Vertex()
                                              .HasLabel("department")
                                              .ToList()
                                              .ToString();
            var request = PostCommandRequest.Create(gremlinCommand);

            var startTime = DateTime.Now;
            var result = await _client.PostGremlinCommandAsync(request);
            var endTime = DateTime.Now;

            double elapsedMs = (endTime - startTime).TotalMilliseconds;
        }

        [Fact]
        public async Task TestPostBatchGremlinCommand()
        {
            List<string> commands = new List<string>()
            {
                GraphTraversal.Graph().Vertex().Drop().ToString(),
                GraphTraversal.Graph().AddVertex("test").Property("id", "0001").ToString(),
                GraphTraversal.Graph().Vertex().ToString()
            };
            
            var request = PostBatchRequest.CreateBatchGremlin(commands, transaction: true);
            var result = await _client.PostBatchAsync(request);
        }

        [Fact]
        public async Task TestPostSqlCommand()
        {
            string sqlCommand = "SELECT @rid FROM V WHERE SEARCH_INDEX(\"V.name\", \" *john*\") = true";
            var request = PostCommandRequest.Create(sqlCommand);

            var startTime = DateTime.Now;
            var result = await _client.PostSqlCommandAsync(request);
            var endTime = DateTime.Now;

            double elapsedMs = (endTime - startTime).TotalMilliseconds;
        }
    }
}
