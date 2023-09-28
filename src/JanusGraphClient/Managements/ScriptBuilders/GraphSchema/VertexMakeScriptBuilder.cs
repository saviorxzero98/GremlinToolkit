namespace GQL.JanusGraphClients.Managements.ScriptBuilders.GraphSchema
{
    public class VertexMakeScriptBuilder : BaseScriptBuilder
    {
        public string Label { get; protected set; }

        public VertexMakeScriptBuilder(string label)
        {
            Label = label;
        }
        public VertexMakeScriptBuilder(string label, string graphName)
        {
            Label = label;
            SetGraphName(graphName);
        }


        /// <summary>
        /// 設定 Graph Name
        /// </summary>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public VertexMakeScriptBuilder SetGraphName(string graphName)
        {
            GraphName = graphName;
            return this;
        }

        /// <summary>
        /// 設定 Vertex Label
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public VertexMakeScriptBuilder SetLabel(string label)
        {
            Label = label;
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
if (mgmt.getVertexLabel(vertexLabel) == null) {
    mgmt.makeVertexLabel(vertexLabel).make();
}
mgmt.commit();
";

            // 建立 Script Name Binding
            NameBindings.Add("vertexLabel", Label);

            return script;
        }

        /// <summary>
        /// 檢查參數是否合法
        /// </summary>
        /// <returns></returns>
        protected bool IsInvalid()
        {
            if (string.IsNullOrEmpty(GraphName) ||
                string.IsNullOrEmpty(Label))
            {
                return true;
            }
            return false;
        }
    }
}
