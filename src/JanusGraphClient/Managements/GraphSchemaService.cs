using GQL.JanusGraphClients.Configs;
using GQL.JanusGraphClients.Managements.Reports;
using GQL.JanusGraphClients.Managements.Schema;
using GQL.JanusGraphClients.Managements.ScriptBuilders;
using GQL.JanusGraphClients.Managements.ScriptBuilders.GraphSchema;

namespace GQL.JanusGraphClients.Managements
{
    public class GraphSchemaService
    {
        private readonly JanusGraphGremlinClient _client;

        public string GraphName { get; set; } = JanusGraphDatabaseConfig.DefaultGraphName;

        public GraphSchemaService(JanusGraphGremlinClient client)
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
        public GraphSchemaService(JanusGraphDatabaseConfig config)
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


        #region Make Schema

        /// <summary>
        /// Create Vertex Label Schema
        /// </summary>
        /// <param name="vertexLabel"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> MakeVertexLabelAsync(string vertexLabel)
        {
            // 建立 Script
            var builder = new VertexMakeScriptBuilder(vertexLabel, GraphName);
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
        /// Create Edge Label Schema
        /// </summary>
        /// <param name="edgeLabel"></param>
        /// <param name="multiplicity"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> MakeEdgeLabelAsync(string edgeLabel,
                                                                     string multiplicity = EdgeMultiplicity.Multi)
        {
            // 建立 Script
            var builder = new EdgeMakeScriptBuilder(edgeLabel, multiplicity, GraphName);
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
        /// Create Property Key Schema
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="dataType"></param>
        /// <param name="cardinality"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> MakePropertyKeyAsync(string propertyName,
                                                                       string dataType = PropertyDataType.String,
                                                                       string cardinality = PropertyCardinality.Single)
        {
            // 建立 Script
            var builder = new PropertyMakeScriptBuilder(propertyName, dataType, cardinality, GraphName);
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

        #endregion


        #region Change Schema Name

        /// <summary>
        /// 修改 Vertex Lable Name
        /// </summary>
        /// <param name="vertexLabel"></param>
        /// <param name="newLabel"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> ChangeVertexLabelAsync(string vertexLabel, string newLabel)
        {
            // 建立 Script
            var builder = NameChangeScriptBuilder.CreateVertexNameChangeScriptBuilder(vertexLabel, newLabel)
                                                 .SetGraphName(GraphName);
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
        /// 修改 Edge Lable Name
        /// </summary>
        /// <param name="edgeLabel"></param>
        /// <param name="newLabel"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> ChangeEdgeLabelAsync(string edgeLabel, string newLabel)
        {
            // 建立 Script
            var builder = NameChangeScriptBuilder.CreateEdgeNameChangeScriptBuilder(edgeLabel, newLabel)
                                                 .SetGraphName(GraphName);
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
        /// 修改 Property Name
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        public async Task<ManagementActionReport> ChangePropertyKeyAsync(string propertyName, string newName)
        {
            // 建立 Script
            var builder = NameChangeScriptBuilder.CreatePropertyNameChangeScriptBuilder(propertyName, newName)
                                                 .SetGraphName(GraphName);
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

        #endregion
    }
}
