namespace GQL.JanusGraphClients.Managements.ScriptBuilders.GraphSchema
{
    public class MappedConnectionScriptBuilder : BaseScriptBuilder
    {
        public string EdgeLabel { get; protected set; }

        public string SourceVertexLabel { get; protected set; }

        public string TargetVertexLabel { get; protected set; }


        public MappedConnectionScriptBuilder(string edgeLabel, string sourceVertexLabel, string targetVertexLabel)
        {
            EdgeLabel = edgeLabel;
            SourceVertexLabel = sourceVertexLabel;
            TargetVertexLabel = targetVertexLabel;
        }
        public MappedConnectionScriptBuilder(string edgeLabel, string sourceVertexLabel, string targetVertexLabel,
                                             string graphName)
        {
            EdgeLabel = edgeLabel;
            SourceVertexLabel = sourceVertexLabel;
            TargetVertexLabel = targetVertexLabel;
            SetGraphName(graphName);
        }

        /// <summary>
        /// 設定 Graph Name
        /// </summary>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public MappedConnectionScriptBuilder SetGraphName(string graphName)
        {
            GraphName = graphName;
            return this;
        }

        /// <summary>
        ///  設定 Vertex Label
        /// </summary>
        /// <param name="edgeLabel"></param>
        /// <param name="sourceVertexLabel"></param>
        /// <param name="targetVertexLabel"></param>
        /// <returns></returns>
        public MappedConnectionScriptBuilder SetConnection(string edgeLabel, string sourceVertexLabel, string targetVertexLabel)
        {
            EdgeLabel = edgeLabel;
            SourceVertexLabel = sourceVertexLabel;
            TargetVertexLabel = targetVertexLabel;
            return this;
        }

        /// <summary>
        /// 建立 Groovy Script
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
            string script = @"
mgmt = ${GraphName}.openManagement();
sourceV = mgmt.getVertexLabel(sourceVertexLabel);
targetV = mgmt.getVertexLabel(targetVertexLabel);
relationE = mgmt.getEdgeLabel(edgeLabel);
if (sourceV != null && targetV != null && relationE != null) {
    mgmt.addConnection(relation, targetV, relation);
}
mgmt.commit();
";

            // 建立 Script Name Binding
            NameBindings.Add("edgeLabel", EdgeLabel);
            NameBindings.Add("sourceVertexLabel", SourceVertexLabel);
            NameBindings.Add("targetVertexLabel", TargetVertexLabel);

            return script;
        }

        /// <summary>
        /// 檢查參數是否合法
        /// </summary>
        /// <returns></returns>
        protected bool IsInvalid()
        {
            if (string.IsNullOrEmpty(GraphName) ||
                string.IsNullOrEmpty(EdgeLabel) ||
                string.IsNullOrEmpty(SourceVertexLabel) ||
                string.IsNullOrEmpty(TargetVertexLabel))
            {
                return true;
            }
            return false;
        }
    }
}
