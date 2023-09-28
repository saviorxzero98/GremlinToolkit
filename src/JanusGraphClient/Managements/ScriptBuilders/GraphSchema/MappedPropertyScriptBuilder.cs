using GQL.JanusGraphClients.Configs;
using GQL.JanusGraphClients.Managements.Schema;

namespace GQL.JanusGraphClients.Managements.ScriptBuilders.GraphSchema
{
    public class MappedPropertyScriptBuilder : BaseScriptBuilder
    {
        private const string DefaultGraphName = JanusGraphDatabaseConfig.DefaultGraphName;

        public string Label { get; protected set; }

        private GraphElementType Type { get; set; }

        public List<string> Properties { get; protected set; } = new List<string>();



        private MappedPropertyScriptBuilder(GraphElementType type, string label, string graphName = DefaultGraphName)
        {
            Type = type;
            Label = label;
            Properties = new List<string>();
            SetGraphName(graphName);
        }

        /// <summary>
        /// 設定 Graph Name
        /// </summary>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public MappedPropertyScriptBuilder SetGraphName(string graphName)
        {
            GraphName = graphName;
            return this;
        }

        /// <summary>
        /// 設定 Label
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public MappedPropertyScriptBuilder SetLabel(string label)
        {
            Label = label;
            return this;
        }

        /// <summary>
        /// 加入 Property Key
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public MappedPropertyScriptBuilder AddPropertyKey(string property)
        {
            Properties.Add(property);
            return this;
        }

        /// <summary>
        /// 加入 Property Keys
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public MappedPropertyScriptBuilder AddPropertyKey(IEnumerable<string> properties)
        {
            Properties.AddRange(properties);
            return this;
        }

        /// <summary>
        /// 建立 Vertex Mapped Property
        /// </summary>
        /// <param name="label"></param>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public static MappedPropertyScriptBuilder CreateVertexMappedPropertyScriptBuilder(string label,
                                                                                          string graphName = DefaultGraphName)
        {
            return new MappedPropertyScriptBuilder(GraphElementType.Vertex, label, graphName);
        }

        /// <summary>
        /// 建立 Edge Mapped Property
        /// </summary>
        /// <param name="label"></param>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public static MappedPropertyScriptBuilder CreateEdgeMappedPropertyScriptBuilder(string label,
                                                                                        string graphName = DefaultGraphName)
        {
            return new MappedPropertyScriptBuilder(GraphElementType.Edge, label, graphName);
        }

        /// <summary>
        /// 建立 Script
        /// </summary>
        /// <returns></returns>
        protected override string BuildScript()
        {
            if (IsInvalid())
            {
                return string.Empty;
            }

            // 建立 Variable Name 對照表
            VariableNameMap.Add("${GraphName}", GraphName);

            // 建立 Groovy Script
            string script = string.Empty;

            switch (Type)
            {
                case GraphElementType.Vertex:
                    script = CreateVertexMappedProperty();
                    break;

                case GraphElementType.Edge:
                    script = CreateEdgeMappedProperty();
                    break;
            }


            // 建立 Script Name Binding
            NameBindings.Add("labelName", Label);
            NameBindings.Add("properties", Properties);

            return script;
        }

        /// <summary>
        /// 建立 重新命名 Vertex Label 的 Groovy Script
        /// </summary>
        /// <returns></returns>
        protected string CreateVertexMappedProperty()
        {
            string script = @"
mgmt = ${GraphName}.openManagement();
if (mgmt.getVertexLabel(labelName) != null) {
    vertexLabel = mgmt.getVertexLabel(labelName);
    for (int i = 0; i < properties.size(); i++) {
        if (mgmt.getPropertyKey(properties[i]) != null) {
            vertexLabel = mgmt.addProperties(vertexLabel, properties[i]);
        }
    }
}
mgmt.commit();
";
            return script;
        }

        /// <summary>
        /// 建立 重新命名 Edge Label Groovy Script
        /// </summary>
        /// <returns></returns>
        protected string CreateEdgeMappedProperty()
        {
            string script = @"
mgmt = ${GraphName}.openManagement();
if (mgmt.getEdgeLabel(labelName) != null) {
    edgeLabel = mgmt.getEdgeLabel(labelName);
    for (int i = 0; i < properties.size(); i++) {
        if (mgmt.getPropertyKey(properties[i]) != null) {
            edgeLabel = mgmt.addProperties(edgeLabel, properties[i]);
        }
    }
}
mgmt.commit();
";
            return script;
        }

        /// <summary>
        /// 檢查參數是否合法
        /// </summary>
        /// <returns></returns>
        protected bool IsInvalid()
        {
            if (string.IsNullOrEmpty(GraphName) ||
                string.IsNullOrEmpty(Label) ||
                !Properties.Any(p => !string.IsNullOrEmpty(p)))
            {
                return true;
            }
            return false;
        }
    }
}
