using GQL.QueryBuilders.Builders.Traversals;
using Xunit;

namespace GQL.QueryBuilders.Test.Builders
{
    public class EdgeTraversalTest
    {
        [Fact]
        public void TestAddEdge()
        {
            // Arrange
            string expectedA = "g.addE('create').property('weight',0.99).from(V().has('person','name','jobs steve')).to(V().has('product','name','iphone'))";
            string expectedB = "g.V().has('person','name','jobs steve').addE('create').property('weight',0.99).to(V().has('product','name','iphone'))";

            // Act
            string actualA = GraphTraversal.Graph()
                                       .AddEdge("create").Property("weight", 0.99)
                                       .FromVertex((v) => v.Has("person", "name", "jobs steve"))
                                       .ToVertex((v) => v.Has("product", "name", "iphone")).ToString();
            string actualB = GraphTraversal.Graph()
                                       .Vertex().Has("person", "name", "jobs steve")
                                       .AddEdge("create").Property("weight", 0.99)
                                       .ToVertex((v) => v.Has("product", "name", "iphone")).ToString();

            // Assert
            Assert.Equal(expectedA, actualA);
            Assert.Equal(expectedB, actualB);
        }

        [Fact]
        public void TestAddEdgeWithOtherGraph()
        {
            // Arrange
            string expectedA = "tg.addE('create').property('weight',0.99).from(V().has('person','name','jobs steve')).to(V().has('product','name','iphone'))";
            string expectedB = "tg.V().has('person','name','jobs steve').addE('create').property('weight',0.99).to(V().has('product','name','iphone'))";

            // Act
            string actualA = GraphTraversal.Graph("tg")
                                       .AddEdge("create").Property("weight", 0.99)
                                       .FromVertex((v) => v.Has("person", "name", "jobs steve"))
                                       .ToVertex((v) => v.Has("product", "name", "iphone")).ToString();
            string actualB = GraphTraversal.Graph("tg")
                                       .Vertex().Has("person", "name", "jobs steve")
                                       .AddEdge("create").Property("weight", 0.99)
                                       .ToVertex((v) => v.Has("product", "name", "iphone")).ToString();

            // Assert
            Assert.Equal(expectedA, actualA);
            Assert.Equal(expectedB, actualB);
        }


        [Fact]
        public void TestAddEdgeWithObject()
        {
            // Arrange
            string expectedA = "g.addE('create').property('weight',0.99).from(V().has('person','name','jobs steve')).to(V().has('product','name','iphone'))";
            string expectedB = "g.V().has('person','name','jobs steve').addE('create').property('weight',0.99).to(V().has('product','name','iphone'))";

            // Act
            string actualA = GraphTraversal.Graph()
                                       .AddEdge("create", new { weight = 0.99 })
                                       .FromVertex("person", "name", "jobs steve")
                                       .ToVertex("product", "name", "iphone").ToString();
            string actualB = GraphTraversal.Graph()
                                       .Vertex().Has("person", "name", "jobs steve")
                                       .AddEdge("create", new { weight = 0.99 })
                                       .ToVertex("product", "name", "iphone").ToString();

            // Assert
            Assert.Equal(expectedA, actualA);
            Assert.Equal(expectedB, actualB);
        }
    }
}
