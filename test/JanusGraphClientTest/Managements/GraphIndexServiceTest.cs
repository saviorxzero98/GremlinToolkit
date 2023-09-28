using GQL.JanusGraphClients.Configs;
using GQL.JanusGraphClients.Managements;
using Xunit;

namespace GQL.JanusGraphClients.Test.Managements
{
    public class GraphIndexServiceTest
    {
        private JanusGraphDatabaseConfig Config;

        public GraphIndexServiceTest()
        {
            Config = new JanusGraphDatabaseConfig().SetHost("localhost", 8182);
        }

        [Fact]
        public async Task TestGetGraphIndexStatusAsync()
        {
            try
            {
                var service = new GraphIndexService(Config);

                string indexName = "PrimaryKeyIndex";
                var report = await service.GetStatusAsync(indexName);

                Assert.True(report.IsSuccess);
            }
            catch (Exception e)
            {
                Assert.NotNull(e);
            }
        }

        [Fact]
        public async Task TestIndexPreprocessingAsync()
        {
            try
            {
                var service = new GraphIndexService(Config);

                var result = await service.ClearTransactionAndManagementAsync();
            }
            catch (Exception e)
            {
                Assert.NotNull(e);
            }
        }

        [Fact]
        public async Task TestBuildIndexAsync()
        {
            try
            {
                var service = new GraphIndexService(Config);

                string indexName = "PrimaryKeyIndex";
                string propertyName = "PrimaryKey";
                var result = await service.CreateAndEnableCompositeIndexAsync(indexName, propertyName);
            }
            catch (Exception e)
            {
                Assert.NotNull(e);
            }
        }

        [Fact]
        public async Task TestReindexAsync()
        {
            try
            {
                var service = new GraphIndexService(Config);

                string indexName = "PrimaryKeyIndex";
                var result = await service.ReindexAsync(indexName);
            }
            catch (Exception e)
            {
                Assert.NotNull(e);
            }
        }
    }
}
