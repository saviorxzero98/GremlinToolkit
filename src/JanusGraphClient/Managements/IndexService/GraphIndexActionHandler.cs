﻿using GQL.JanusGraphClients.Configs;
using GQL.JanusGraphClients.Managements.Reports;
using GQL.JanusGraphClients.Managements.ScriptBuilders;
using GQL.JanusGraphClients.Managements.ScriptBuilders.GraphIndex;

namespace GQL.JanusGraphClients.Managements.IndexService
{
    public class GraphIndexActionHandler
    {
        protected JanusGraphGremlinClient _client;
        protected string _graphName;

        public GraphIndexActionHandler(JanusGraphGremlinClient client)
        {
            _client = client;

            if (client == null || client.Config == null ||
                string.IsNullOrWhiteSpace(client.Config.GraphName))
            {
                _graphName = JanusGraphDatabaseConfig.DefaultGraphName;
            }
            else
            {
                _graphName = client.Config.GraphName;
            }
        }

        /// <summary>
        /// Close All Transaction & Management
        /// </summary>
        /// <returns></returns>
        public async Task<ManagementActionReport> ClearTransactionAndManagementAsync()
        {
            // 建立 Groovy Script
            var builder = new IndexPreprocessingScriptBuilder(_graphName);
            ScriptResult scriptResult = builder.Build();

            try
            {
                // 執行 Groovy Script
                await _client.SubmitAsync(scriptResult.Script, scriptResult.NamedBindings);

                return ManagementActionReport.Success();
            }
            catch (Exception e)
            {
                return ManagementActionReport.Fail(e);
            }
        }

        /// <summary>
        /// Get Index Status
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<GraphIndexStatusReport> GetStatusAsync(string indexName)
        {
            if (string.IsNullOrWhiteSpace(indexName))
            {
                return GraphIndexStatusReport.Fail();
            }

            // 建立 Groovy Script
            var builder = new GraphIndexScriptBuilder(indexName, _graphName);
            ScriptResult scriptResult = builder.Build();

            try
            {
                // 執行 Groovy Script
                var result = await _client.SubmitAsync<dynamic>(scriptResult.Script, scriptResult.NamedBindings);

                // 解析結果
                var report = GraphIndexStatusReport.From(result);
                return report;
            }
            catch (Exception e)
            {
                return new GraphIndexStatusReport()
                {
                    IsSuccess = false,
                    Error = e
                };
            }
        }

        public Task<ManagementActionReport> RegisterIndexAsync(string indexName)
        {
            // 建立 Groovy Script
            var builder = new UpdateIndexScriptBuilder(indexName, _graphName).SetRegisterIndex();
            return UpdateIndexAsync(builder);
        }

        /// <summary>
        /// Reindex
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public Task<ManagementActionReport> ReindexAsync(string indexName)
        {
            // 建立 Groovy Script
            var builder = new UpdateIndexScriptBuilder(indexName, _graphName).SetReindex();
            return UpdateIndexAsync(builder);
        }

        /// <summary>
        /// Enable Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public Task<ManagementActionReport> EnableIndexAsync(string indexName)
        {
            // 建立 Groovy Script
            var builder = new UpdateIndexScriptBuilder(indexName, _graphName).SetEnableIndex();
            return UpdateIndexAsync(builder);
        }

        /// <summary>
        /// Disable Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public Task<ManagementActionReport> DisableIndexAsync(string indexName)
        {
            // 建立 Groovy Script
            var builder = new UpdateIndexScriptBuilder(indexName, _graphName).SetDisableIndex();
            return UpdateIndexAsync(builder);
        }

        /// <summary>
        /// Remove Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public Task<ManagementActionReport> RemoveIndexAsync(string indexName)
        {
            // 建立 Groovy Script
            var builder = new UpdateIndexScriptBuilder(indexName, _graphName).SetRemoveIndex();
            return UpdateIndexAsync(builder);
        }

        /// <summary>
        /// Update Index
        /// </summary>
        /// <param name="scriptBuilder"></param>
        /// <returns></returns>
        protected async Task<ManagementActionReport> UpdateIndexAsync(UpdateIndexScriptBuilder scriptBuilder)
        {
            if (scriptBuilder == null)
            {
                return ManagementActionReport.Fail();
            }

            // 建立 Groovy Script
            ScriptResult scriptResult = scriptBuilder.Build();

            try
            {
                // 執行 Groovy Script
                await _client.SubmitAsync<dynamic>(scriptResult.Script, scriptResult.NamedBindings);

                return ManagementActionReport.Success();
            }
            catch (Exception e)
            {
                return ManagementActionReport.Fail(e);
            }
        }
    }
}
