namespace GQL.JanusGraphClients.Managements.ScriptBuilders
{
    public class ScriptResult
    {
        /// <summary>
        /// 腳本內容
        /// </summary>
        public string Script { get; set; } = string.Empty;

        /// <summary>
        /// 綁定的資料
        /// </summary>
        public Dictionary<string, object> NamedBindings { get; set; } = new Dictionary<string, object>();
    }
}
