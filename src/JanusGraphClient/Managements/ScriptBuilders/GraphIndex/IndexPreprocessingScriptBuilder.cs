namespace GQL.JanusGraphClients.Managements.ScriptBuilders.GraphIndex
{
    /// <summary>
    /// 建 Index 的前處理
    /// </summary>
    public class IndexPreprocessingScriptBuilder : BaseScriptBuilder
    {
        public IndexPreprocessingScriptBuilder()
        {
        }
        public IndexPreprocessingScriptBuilder(string graphName)
        {
            SetGraphName(graphName);
        }

        /// <summary>
        /// 設定 Graph Name
        /// </summary>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public IndexPreprocessingScriptBuilder SetGraphName(string graphName)
        {
            GraphName = graphName;
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
            // 1. 清空所有已開啟的 Transactions
            // 2. 清空所有已開啟的 Management 的 Instance
            string script = @"
${GraphName}.getOpenTransactions().forEach {  tx -> tx.rollback() }
mgmt = ${GraphName}.openManagement();
mgmt.getOpenInstances().forEach {
    if (it.reverse().take(1) != ')') { 
        mgmt.forceCloseInstance(it);
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
            if (string.IsNullOrEmpty(GraphName))
            {
                return true;
            }
            return false;
        }
    }
}
