using GQL.JanusGraphClients.Configs;
using System.Text.RegularExpressions;

namespace GQL.JanusGraphClients.Managements.ScriptBuilders
{
    public abstract class BaseScriptBuilder : IScriptBuilder
    {
        #region Binding 相關屬性

        /// <summary>
        /// 變數名稱的命名規則 (Regression Expression)
        /// </summary>
        public const string VariableNamePattern = "^[a-zA-Z_][a-zA-Z_0-9]*$";

        /// <summary>
        /// 保留關鍵字
        /// </summary>
        protected List<string> SpecialKeywords
        {
            get
            {
                var keywords = new List<string>()
                {
                    "vertex",
                    "element",
                    "edge",
                    "property",
                    "label",
                    "key"
                };
                return keywords;
            }
        }

        /// <summary>
        /// 變數名稱替換對照表
        /// </summary>
        protected Dictionary<string, string> VariableNameMap { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 腳本資料綁定參數
        /// </summary>
        protected Dictionary<string, object> NameBindings { get; set; } = new Dictionary<string, object>();

        #endregion


        #region Graph 變數

        /// <summary>
        /// Grpah 變數
        /// </summary>
        public string GraphName { get; set; } = JanusGraphDatabaseConfig.DefaultGraphName;

        /// <summary>
        /// 預設 Traversal Source 
        /// </summary>
        public const string DefaultTraversalSource = "g";

        /// <summary>
        /// Traversal Source 變數
        /// </summary>
        public string TraversalSource { get; set; } = DefaultTraversalSource;

        #endregion


        /// <summary>
        /// 建立 Script
        /// </summary>
        /// <returns></returns>
        public virtual ScriptResult Build()
        {
            string script = BuildScript();

            if (VariableNameMap.Any())
            {
                foreach (var variableNamePair in VariableNameMap)
                {
                    string variableKeyword = variableNamePair.Key;
                    string variableName = variableNamePair.Value;

                    // 檢查變數命名
                    if (IsVariableNamingValid(variableKeyword, variableName))
                    {
                        script = script.Replace(variableKeyword, variableName);
                    }
                }
            }

            return new ScriptResult()
            {
                Script = script,
                NamedBindings = NameBindings ?? new Dictionary<string, object>()
            };
        }

        /// <summary>
        /// 建立 Script
        /// </summary>
        /// <returns></returns>
        protected abstract string BuildScript();

        /// <summary>
        /// 檢查變數命名
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        protected bool IsVariableNamingValid(string variableKeyword, string variableName)
        {
            // 檢查變數名稱是否為空
            if (string.IsNullOrWhiteSpace(variableKeyword) ||
                string.IsNullOrWhiteSpace(variableName))
            {
                return false;
            }

            // 是否符合變數命名規則
            if (Regex.IsMatch(variableName, VariableNamePattern))
            {
                // 檢查變數名稱是否使用保留字
                if (SpecialKeywords.Any(k => k.Equals(variableName, StringComparison.InvariantCultureIgnoreCase)))
                {
                    return false;
                }
                return true;
            }

            return false;
        }
    }
}
