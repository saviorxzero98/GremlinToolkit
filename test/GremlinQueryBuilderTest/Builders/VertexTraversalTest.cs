using GQL.QueryBuilders.Builders.Traversals;
using Xunit;

namespace GQL.QueryBuilders.Test.Builders
{
    public class VertexTraversalTest
    {
        [Fact]
        public void TestAddVertex()
        {
            // Arrange
            string expected = "g.addV('person').property('name','stephen')";

            // Act
            string actual = GraphTraversal.Graph()
                                      .AddVertex("person")
                                      .Property("name", "stephen")
                                      .ToString();
            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestAddVertexWithOtherGraph()
        {
            // Arrange
            string expected = "tg.addV('person').property('name','stephen')";

            // Act
            string actual = GraphTraversal.Graph("tg")
                                      .AddVertex("person")
                                      .Property("name", "stephen")
                                      .ToString();
            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestAddVertexWithObject()
        {
            // Arrange
            string expected = "g.addV('person').property('id',1).property('name','stephen')";

            // Act
            string actual = GraphTraversal.Graph()
                                      .AddVertex("person", new { id = 1, name = "stephen" })
                                      .ToString();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestGetVertexValues()
        {
            // Arrange
            string expectedA = "g.V().has('person','name','stephen').values()";
            string expectedB = "g.V().has('person','name','stephen').values('ages','cname')";
            string expectedC = "g.V().has('person','name','stephen').valueMap()";
            string expectedD = "g.V().has('person','name','stephen').valueMap('ages','cname')";
            string expectedE = "g.V().has('person','name','stephen').elementMap()";
            string expectedF = "g.V().has('person','name','stephen').elementMap('ages','cname')";
            string expectedG = "g.V().not(hasLabel('person')).elementMap()";

            // Act
            string actualA = GraphTraversal.Graph().Vertex().Has("person", "name", "stephen").Values().ToString();
            string actualB = GraphTraversal.Graph().Vertex().Has("person", "name", "stephen").Values("ages", "cname").ToString();
            string actualC = GraphTraversal.Graph().Vertex().Has("person", "name", "stephen").ValueMap().ToString();
            string actualD = GraphTraversal.Graph().Vertex().Has("person", "name", "stephen").ValueMap("ages", "cname").ToString();
            string actualE = GraphTraversal.Graph().Vertex().Has("person", "name", "stephen").ElementMap().ToString();
            string actualF = GraphTraversal.Graph().Vertex().Has("person", "name", "stephen").ElementMap("ages", "cname").ToString();
            string actualG = GraphTraversal.Graph().Vertex().Not((v) => v.HasLabel("person")).ElementMap().ToString();

            // Assert
            Assert.Equal(expectedA, actualA);
            Assert.Equal(expectedB, actualB);
            Assert.Equal(expectedC, actualC);
            Assert.Equal(expectedD, actualD);
            Assert.Equal(expectedE, actualE);
            Assert.Equal(expectedF, actualF);
            Assert.Equal(expectedG, actualG);
        }

        [Fact]
        public void TextGqlInjection()
        {
            // Arrange
            string expected = "g.addV('person').property('name','jobs\\'stephen')";

            // Act
            string actual = GraphTraversal.Graph()
                                      .AddVertex("person")
                                      .Property("name", "jobs'stephen")
                                      .ToString();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
