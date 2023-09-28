using GQL.JanusGraphClients.Configs;
using GQL.JanusGraphClients.Managements.IndexService;
using GQL.JanusGraphClients.Managements.Reports;
using GQL.JanusGraphClients.Managements.ScriptBuilders;
using GQL.JanusGraphClients.Managements.ScriptBuilders.GraphIndex;

namespace GQL.JanusGraphClients.Managements
{
    public class GraphIndexService
    {
        public const int DefaultCoolingTime = 3000;
        public const int DefaultAttempts = 3;

        private readonly JanusGraphGremlinClient _client;

        public string GraphName { get; set; } = JanusGraphDatabaseConfig.DefaultGraphName;

        public int CoolingTime { get; set; } = DefaultCoolingTime;
        public int Attempts { get; set; } = DefaultAttempts;


        public GraphIndexService(JanusGraphGremlinClient client)
        {
            _client = client;

            if (client == null || client.Config == null ||
                string.IsNullOrWhiteSpace(client.Config.GraphName))
            {
                GraphName = JanusGraphDatabaseConfig.DefaultGraphName;
            }
            else
            {
                GraphName = client.Config.GraphName;
            }
        }
        public GraphIndexService(JanusGraphDatabaseConfig config)
        {
            _client = new JanusGraphGremlinClient(config);

            if (config == null || string.IsNullOrWhiteSpace(config.GraphName))
            {
                GraphName = JanusGraphDatabaseConfig.DefaultGraphName;
            }
            else
            {
                GraphName = config.GraphName;
            }
        }


        #region Batch Actions (Composite Index)

        /// <summary>
        /// Create & Enable Composite Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="propertyName"></param>
        /// <param name="onlyVertexLabel"></param>
        /// <param name="isUnique"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> CreateAndEnableCompositeIndexAsync(string indexName, string propertyName,
                                                                                     string onlyVertexLabel = null,
                                                                                     bool isUnique = false)
        {
            if (string.IsNullOrWhiteSpace(indexName) ||
                string.IsNullOrWhiteSpace(propertyName))
            {
                return ManagementActionReport.Fail();
            }

            var property = GraphIndexProperty.Create(propertyName);
            return await CreateAndEnableCompositeIndexAsync(indexName, property, onlyVertexLabel, isUnique);
        }
        /// <summary>
        /// Create & Enable Composite Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="property"></param>
        /// <param name="onlyVertexLabel"></param>
        /// <param name="isUnique"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> CreateAndEnableCompositeIndexAsync(string indexName, GraphIndexProperty property,
                                                                                     string onlyVertexLabel = null,
                                                                                     bool isUnique = false)
        {
            if (string.IsNullOrWhiteSpace(indexName) ||
                property == null)
            {
                return ManagementActionReport.Fail();
            }

            List<GraphIndexProperty> properties = new List<GraphIndexProperty>() { property };
            return await CreateAndEnableCompositeIndexAsync(indexName, properties, onlyVertexLabel, isUnique);
        }
        /// <summary>
        ///  Create & Enable Composite Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="properties"></param>
        /// <param name="onlyVertexLabel"></param>
        /// <param name="isUnique"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> CreateAndEnableCompositeIndexAsync(string indexName,
                                                                                     List<GraphIndexProperty> properties,
                                                                                     string onlyVertexLabel = null,
                                                                                     bool isUnique = false)
        {
            // 查詢目前狀況
            var currentStatus = await GetStatusAsync(indexName);
            if (!currentStatus.IsSuccess)
            {
                return ManagementActionReport.Fail(currentStatus.Error);
            }

            // 建 Index 的前處理
            var preProcessReport = await ClearTransactionAndManagementAsync();
            if (!preProcessReport.IsSuccess)
            {
                return preProcessReport;
            }

            // 判斷 Index 是否存在
            if (currentStatus.IsNullIndex)
            {
                // 建立 Index
                var createReport = await CreateCompositeIndexAsync(indexName, properties, onlyVertexLabel, isUnique);

                if (!createReport.IsSuccess)
                {
                    return createReport;
                }

                Thread.Sleep(CoolingTime);
            }

            // 啟用 Index
            var report = await ReindexAsync(indexName);
            return report;
        }

        #endregion


        #region Batch Actions (Mixed Index)

        /// <summary>
        ///  Create & Enable Mixed Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="backingIndex"></param>
        /// <param name="propertyName"></param>
        /// <param name="mappingType"></param>
        /// <param name="onlyVertexLabel"></param>
        /// <param name="isUnique"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> CreateAndEnableMixedIndexAsync(string indexName, string backingIndex,
                                                                                 string propertyName, string mappingType = null,
                                                                                 string onlyVertexLabel = null, bool isUnique = false)
        {
            if (string.IsNullOrWhiteSpace(indexName) ||
                string.IsNullOrWhiteSpace(backingIndex) ||
                string.IsNullOrWhiteSpace(propertyName))
            {
                return ManagementActionReport.Fail();
            }

            var property = GraphIndexProperty.Create(propertyName, mappingType);
            return await CreateAndEnableMixedIndexAsync(indexName, backingIndex, property, onlyVertexLabel, isUnique);
        }
        /// <summary>
        /// Create & Enable Mixed Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="backingIndex"></param>
        /// <param name="property"></param>
        /// <param name="onlyVertexLabel"></param>
        /// <param name="isUnique"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> CreateAndEnableMixedIndexAsync(string indexName, string backingIndex,
                                                                                 GraphIndexProperty property,
                                                                                 string onlyVertexLabel = null, bool isUnique = false)
        {
            if (string.IsNullOrWhiteSpace(indexName) ||
                string.IsNullOrWhiteSpace(backingIndex) ||
                property == null)
            {
                return ManagementActionReport.Fail();
            }

            var properties = new List<GraphIndexProperty>() { property };
            return await CreateAndEnableMixedIndexAsync(indexName, backingIndex, properties, onlyVertexLabel, isUnique);
        }
        /// <summary>
        /// Create & Enable Mixed Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="backingIndex"></param>
        /// <param name="properties"></param>
        /// <param name="onlyVertexLabel"></param>
        /// <param name="isUnique"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> CreateAndEnableMixedIndexAsync(string indexName, string backingIndex,
                                                                                 List<GraphIndexProperty> properties,
                                                                                 string onlyVertexLabel = null, bool isUnique = false)
        {
            // 查詢目前狀況
            var currentStatus = await GetStatusAsync(indexName);
            if (!currentStatus.IsSuccess)
            {
                return ManagementActionReport.Fail(currentStatus.Error);
            }

            // 建 Index 的前處理
            var preProcessReport = await ClearTransactionAndManagementAsync();
            if (!preProcessReport.IsSuccess)
            {
                return preProcessReport;
            }

            // 判斷 Index 是否存在
            if (currentStatus.IsNullIndex)
            {
                // 建立 Index
                var createReport = await CreateMixedIndexAsync(indexName, backingIndex, properties, onlyVertexLabel);

                if (!createReport.IsSuccess)
                {
                    return createReport;
                }

                Thread.Sleep(CoolingTime);
            }

            // 啟用 Index
            var report = await ReindexAsync(indexName);
            return report;
        }


        #endregion


        #region Create Action (Composite Index)

        /// <summary>
        /// Create Composite Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="propertyName"></param>
        /// <param name="onlyVertexLabel"></param>
        /// <param name="isUnique"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> CreateCompositeIndexAsync(string indexName, string propertyName,
                                                                            string onlyVertexLabel = null,
                                                                            bool isUnique = false)
        {
            if (string.IsNullOrWhiteSpace(indexName) ||
                string.IsNullOrWhiteSpace(propertyName))
            {
                return ManagementActionReport.Fail();
            }
            var property = GraphIndexProperty.Create(propertyName);
            return await CreateCompositeIndexAsync(indexName, property, onlyVertexLabel, isUnique);
        }
        /// <summary>
        /// Create Composite Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="property"></param>
        /// <param name="onlyVertexLabel"></param>
        /// <param name="isUnique"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> CreateCompositeIndexAsync(string indexName, GraphIndexProperty property,
                                                                            string onlyVertexLabel = null,
                                                                            bool isUnique = false)
        {
            if (string.IsNullOrWhiteSpace(indexName) ||
                property == null)
            {
                return ManagementActionReport.Fail();
            }

            var properties = new List<GraphIndexProperty>() { property };
            return await CreateCompositeIndexAsync(indexName, properties, onlyVertexLabel, isUnique);
        }

        /// <summary>
        /// Create Composite Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="properties"></param>
        /// <param name="onlyVertexLabel"></param>
        /// <param name="isUnique"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> CreateCompositeIndexAsync(string indexName,
                                                                            List<GraphIndexProperty> properties,
                                                                            string onlyVertexLabel = null,
                                                                            bool isUnique = false)
        {
            if (string.IsNullOrWhiteSpace(indexName) ||
                !properties.Any())
            {
                return ManagementActionReport.Fail();
            }

            // 建立 Script
            ScriptResult scriptResult = GetCompositeIndexBuildScript(indexName, properties, onlyVertexLabel, isUnique);

            try
            {
                // 執行 Script
                await _client.SubmitAsync(scriptResult.Script, scriptResult.NamedBindings);

                return ManagementActionReport.Success();
            }
            catch (Exception e)
            {
                return ManagementActionReport.Fail(e);
            }
        }

        #endregion


        #region Create Action (Mixed Index)

        /// <summary>
        /// Create Mixed Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="backingIndex"></param>
        /// <param name="propertyName"></param>
        /// <param name="mappingType"></param>
        /// <param name="onlyVertexLabel"></param>
        /// <param name="isUnique"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> CreateMixedIndexAsync(string indexName, string backingIndex,
                                                                        string propertyName, string mappingType = null,
                                                                        string onlyVertexLabel = null)
        {
            if (string.IsNullOrWhiteSpace(indexName) ||
                string.IsNullOrWhiteSpace(backingIndex) ||
                string.IsNullOrWhiteSpace(propertyName))
            {
                return ManagementActionReport.Fail();
            }

            var property = GraphIndexProperty.Create(propertyName, mappingType);
            return await CreateMixedIndexAsync(indexName, backingIndex, property, onlyVertexLabel);
        }
        /// <summary>
        /// Create Mixed Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="backingIndex"></param>
        /// <param name="property"></param>
        /// <param name="onlyVertexLabel"></param>
        /// <param name="isUnique"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> CreateMixedIndexAsync(string indexName, string backingIndex,
                                                                        GraphIndexProperty property,
                                                                        string onlyVertexLabel = null)
        {
            if (string.IsNullOrWhiteSpace(indexName) ||
                string.IsNullOrWhiteSpace(backingIndex) ||
                property == null)
            {
                return ManagementActionReport.Fail();
            }
            var properties = new List<GraphIndexProperty>() { property };
            return await CreateMixedIndexAsync(indexName, backingIndex, properties, onlyVertexLabel);
        }
        /// <summary>
        /// Create Mixed Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="backingIndex"></param>
        /// <param name="properties"></param>
        /// <param name="onlyVertexLabel"></param>
        /// <param name="isUnique"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> CreateMixedIndexAsync(string indexName, string backingIndex,
                                                                        List<GraphIndexProperty> properties,
                                                                        string onlyVertexLabel = null)
        {
            if (string.IsNullOrWhiteSpace(indexName) ||
                string.IsNullOrWhiteSpace(backingIndex) ||
                !properties.Any())
            {
                return ManagementActionReport.Fail();
            }

            // 建立 Script
            ScriptResult scriptResult = GetMixedIndexBuildScript(indexName, backingIndex, properties, onlyVertexLabel);

            try
            {
                // 執行 Script
                await _client.SubmitAsync(scriptResult.Script, scriptResult.NamedBindings);

                return ManagementActionReport.Success();
            }
            catch (Exception e)
            {
                return ManagementActionReport.Fail(e);
            }
        }

        #endregion


        #region Common Action

        /// <summary>
        /// Close All Transaction & Management
        /// </summary>
        /// <returns></returns>
        public async Task<ManagementActionReport> ClearTransactionAndManagementAsync()
        {
            // 建立 Script
            var builder = new IndexPreprocessingScriptBuilder(GraphName);
            ScriptResult scriptResult = builder.Build();

            try
            {
                // 執行 Script
                await _client.SubmitAsync(scriptResult.Script, scriptResult.NamedBindings);

                return ManagementActionReport.Success();
            }
            catch (Exception e)
            {
                return ManagementActionReport.Fail(e);
            }
        }

        /// <summary>
        ///  Get Index Status
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<GraphIndexStatusReport> GetStatusAsync(string indexName)
        {
            if (string.IsNullOrWhiteSpace(indexName))
            {
                return GraphIndexStatusReport.Fail();
            }

            // 建立 Script
            var builder = new GraphIndexScriptBuilder(indexName, GraphName);
            ScriptResult scriptResult = builder.Build();

            try
            {
                // 執行 Script
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

        /// <summary>
        /// Register Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> RegisterAsync(string indexName)
        {
            var stateService = new GraphIndexStateService(_client)
            {
                CoolingTime = CoolingTime,
                Attempts = Attempts
            };
            return await stateService.RegisterIndexAsync(indexName);
        }

        /// <summary>
        /// Reindex
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> ReindexAsync(string indexName)
        {
            var stateService = new GraphIndexStateService(_client)
            {
                CoolingTime = CoolingTime,
                Attempts = Attempts
            };
            return await stateService.ReindexAsync(indexName);
        }

        /// <summary>
        /// Enable Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> EnableAsync(string indexName)
        {
            var stateService = new GraphIndexStateService(_client)
            {
                CoolingTime = CoolingTime,
                Attempts = Attempts
            };
            return await stateService.EnableIndexAsync(indexName);
        }

        /// <summary>
        /// Disable Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> DisableAsync(string indexName)
        {
            var stateService = new GraphIndexStateService(_client)
            {
                CoolingTime = CoolingTime,
                Attempts = Attempts
            };
            return await stateService.DisableIndexAsync(indexName);
        }

        /// <summary>
        /// Remove Index
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> RemoveAsync(string indexName)
        {
            var stateService = new GraphIndexStateService(_client)
            {
                CoolingTime = CoolingTime,
                Attempts = Attempts
            };
            return await stateService.RemoveIndexAsync(indexName);
        }

        #endregion


        #region Build Script

        /// <summary>
        /// 取得 Composite Index Build Script
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="properties"></param>
        /// <param name="onlyVertexLabel"></param>
        /// <param name="isUnique"></param>
        /// <returns></returns>
        protected ScriptResult GetCompositeIndexBuildScript(string indexName,
                                                            List<GraphIndexProperty> properties,
                                                            string onlyVertexLabel = null,
                                                            bool isUnique = false)
        {
            var builder = new CompositeIndexScriptBuilder(indexName, GraphName);

            // 設定 Property
            foreach (var property in properties)
            {
                builder.AddProperty(property);
            }

            // 設定 Unique
            if (isUnique)
            {
                builder.SetUnique();
            }

            // 設定 Index Only Index
            if (!string.IsNullOrEmpty(onlyVertexLabel))
            {
                builder.SetIndexOnlyVertex(onlyVertexLabel);
            }

            ScriptResult scriptResult = builder.Build();

            return scriptResult;
        }

        /// <summary>
        /// 取得 Mixed Index Build Script
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="backingIndex"></param>
        /// <param name="properties"></param>
        /// <param name="onlyVertexLabel"></param>
        /// <returns></returns>
        protected ScriptResult GetMixedIndexBuildScript(string indexName, string backingIndex,
                                                        List<GraphIndexProperty> properties,
                                                        string onlyVertexLabel = null)
        {
            var builder = new MixedIndexScriptBuilder(indexName, backingIndex).SetGraphName(GraphName);

            // 設定 Property
            foreach (var property in properties)
            {
                builder.AddProperty(property);
            }

            // 設定 Index Only Index
            if (!string.IsNullOrEmpty(onlyVertexLabel))
            {
                builder.SetIndexOnlyVertex(onlyVertexLabel);
            }

            ScriptResult scriptResult = builder.Build();

            return scriptResult;
        }

        #endregion
    }
}
