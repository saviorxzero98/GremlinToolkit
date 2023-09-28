using GQL.JanusGraphClients.Managements.Reports;

namespace GQL.JanusGraphClients.Managements.ScriptBuilders.GraphIndex
{
    /// <summary>
    /// 取得 Index Status
    /// </summary>
    public class GraphIndexScriptBuilder : BaseScriptBuilder
    {
        /// <summary>
        /// Index Name
        /// </summary>
        public string IndexName { get; protected set; } = string.Empty;


        public GraphIndexScriptBuilder(string indexName)
        {
            SetIndexName(indexName);
        }
        public GraphIndexScriptBuilder(string indexName, string graphName)
        {
            SetIndexName(indexName);
            SetGraphName(graphName);
        }

        /// <summary>
        /// 設定 Graph Name
        /// </summary>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public GraphIndexScriptBuilder SetGraphName(string graphName)
        {
            GraphName = graphName;
            return this;
        }

        /// <summary>
        /// 設定 Index Name
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public GraphIndexScriptBuilder SetIndexName(string indexName)
        {
            IndexName = indexName;
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
index = mgmt.getGraphIndex(indexName);
result = new HashMap<String, Object>();
statuses = [];
if (index != null) {
    result.put(indexNameKey, index.name());
    result.put(isNullIndexKey, false);
    fieldKeys = index.getFieldKeys();
    for (int i = 0; i < fieldKeys.length; i++) {
        statuses.push(index.getIndexStatus(fieldKeys[i]).toString());
    }
}
else {
    result.put(indexNameKey, indexName);
    result.put(isNullIndexKey, true);
}
result.put(indexStatusKey, statuses);
mgmt.commit();
return result;
";

            // 建立 Script Name Binding
            NameBindings.Add("indexName", IndexName);
            NameBindings.Add("indexNameKey", nameof(GraphIndexStatusReport.IndexName));
            NameBindings.Add("indexStatusKey", nameof(GraphIndexStatusReport.Statuses));
            NameBindings.Add("isNullIndexKey", nameof(GraphIndexStatusReport.IsNullIndex));

            return script;
        }

        /// <summary>
        /// 檢查參數是否合法
        /// </summary>
        /// <returns></returns>
        protected bool IsInvalid()
        {
            if (string.IsNullOrEmpty(GraphName) ||
                string.IsNullOrEmpty(IndexName))
            {
                return true;
            }
            return false;
        }
    }
}
