using GQL.JanusGraphClients.Managements.Schema;

namespace GQL.JanusGraphClients.Managements.ScriptBuilders.GraphIndex
{
    /// <summary>
    /// 更新 Index 狀態
    /// </summary>
    public class UpdateIndexScriptBuilder : BaseScriptBuilder
    {
        /// <summary>
        /// Index Name
        /// </summary>
        public string IndexName { get; protected set; } = string.Empty;

        /// <summary>
        /// Schema Action
        /// </summary>
        public string Action { get; protected set; } = string.Empty;


        public UpdateIndexScriptBuilder(string indexName)
        {
            SetIndexName(indexName);
        }
        public UpdateIndexScriptBuilder(string indexName, string graphName)
        {
            SetIndexName(indexName);
            SetGraphName(graphName);
        }


        /// <summary>
        /// 設定 Graph Name
        /// </summary>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public UpdateIndexScriptBuilder SetGraphName(string graphName)
        {
            GraphName = graphName;
            return this;
        }

        /// <summary>
        /// 設定 Index Name
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public UpdateIndexScriptBuilder SetIndexName(string indexName)
        {
            IndexName = indexName;
            return this;
        }


        #region Schema Action

        /// <summary>
        /// 設定 RegisterIndex
        /// </summary>
        /// <returns></returns>
        public UpdateIndexScriptBuilder SetRegisterIndex()
        {
            Action = IndexAction.RegisterIndex;
            return this;
        }

        /// <summary>
        /// 設定 Reindex
        /// </summary>
        /// <returns></returns>
        public UpdateIndexScriptBuilder SetReindex()
        {
            Action = IndexAction.Reindex;
            return this;
        }

        /// <summary>
        /// 設定 EnableIndex
        /// </summary>
        /// <returns></returns>
        public UpdateIndexScriptBuilder SetEnableIndex()
        {
            Action = IndexAction.EnableIndex;
            return this;
        }

        /// <summary>
        /// 設定 DisableIndex
        /// </summary>
        /// <returns></returns>
        public UpdateIndexScriptBuilder SetDisableIndex()
        {
            Action = IndexAction.DisableIndex;
            return this;
        }

        /// <summary>
        /// 設定 Remove Index
        /// </summary>
        /// <returns></returns>
        public UpdateIndexScriptBuilder SetRemoveIndex()
        {
            Action = IndexAction.RemoveIndex;
            return this;
        }

        #endregion


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

            // 建立 Script
            string script = @"
${GraphName}.tx().rollback();
mgmt = ${GraphName}.openManagement();
if (mgmt.getGraphIndex(indexName) != null) {
    switch (schemaAction) {
        case 'REGISTER_INDEX':
            mgmt.updateIndex(mgmt.getGraphIndex(indexName), SchemaAction.REGISTER_INDEX).get();
            break;
        case 'REINDEX':
            mgmt.updateIndex(mgmt.getGraphIndex(indexName), SchemaAction.REINDEX).get();
            break;
        case 'ENABLE_INDEX':
            mgmt.updateIndex(mgmt.getGraphIndex(indexName), SchemaAction.ENABLE_INDEX).get();
            break;
        case 'DISABLE_INDEX':
            mgmt.updateIndex(mgmt.getGraphIndex(indexName), SchemaAction.DISABLE_INDEX).get();
            break;
        case 'REMOVE_INDEX':
            mgmt.updateIndex(mgmt.getGraphIndex(indexName), SchemaAction.REMOVE_INDEX).get();
            break;
    }
}
mgmt.commit();
";

            // 建立 Script Name Binding
            NameBindings.Add("indexName", IndexName);
            NameBindings.Add("schemaAction", Action);

            return script;
        }

        /// <summary>
        /// 檢查參數是否合法
        /// </summary>
        /// <returns></returns>
        protected bool IsInvalid()
        {
            if (string.IsNullOrEmpty(GraphName) ||
                string.IsNullOrEmpty(Action) ||
                string.IsNullOrEmpty(IndexName))
            {
                return true;
            }
            return false;
        }
    }
}
