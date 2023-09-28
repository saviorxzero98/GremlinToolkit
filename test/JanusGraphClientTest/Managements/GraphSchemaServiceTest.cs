using GQL.JanusGraphClients.Configs;
using GQL.JanusGraphClients.Managements;
using GQL.JanusGraphClients.Managements.Schema;
using Xunit;

namespace GQL.JanusGraphClients.Test.Managements
{
    public class GraphSchemaServiceTest
    {
        private JanusGraphDatabaseConfig Config;

        public GraphSchemaServiceTest()
        {
            Config = new JanusGraphDatabaseConfig().SetHost("localhost", 8182);
        }

        [Fact]
        public async Task TestMakeVertexAsync()
        {
            try
            {
                var service = new GraphSchemaService(Config);

                var report = await service.MakeVertexLabelAsync("person");

                Assert.True(report.IsSuccess);
            }
            catch (Exception e)
            {
                Assert.NotNull(e);
            }
        }

        [Fact]
        public async Task TestEdgeVertexAsync()
        {
            try
            {
                var service = new GraphSchemaService(Config);

                var report = await service.MakeEdgeLabelAsync("owned");

                Assert.True(report.IsSuccess);
            }
            catch (Exception e)
            {
                Assert.NotNull(e);
            }
        }

        [Fact]
        public async Task TestPropertyAsync()
        {
            try
            {
                var service = new GraphSchemaService(Config);

                var nameReport = await service.MakePropertyKeyAsync("name", PropertyDataType.String);
                Assert.True(nameReport.IsSuccess);

                var priceReport = await service.MakePropertyKeyAsync("price", PropertyDataType.Double);
                Assert.True(priceReport.IsSuccess);

                var countReport = await service.MakePropertyKeyAsync("count", PropertyDataType.Long);
                Assert.True(countReport.IsSuccess);

                var dateReport = await service.MakePropertyKeyAsync("date", PropertyDataType.Date);
                Assert.True(dateReport.IsSuccess);
            }
            catch (Exception e)
            {
                Assert.NotNull(e);
            }
        }
    }
}
