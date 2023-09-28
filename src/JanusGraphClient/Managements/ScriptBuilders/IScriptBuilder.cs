namespace GQL.JanusGraphClients.Managements.ScriptBuilders
{
    public interface IScriptBuilder
    {
        /// <summary>
        /// Grpah 變數
        /// </summary>
        string GraphName { get; set; }

        /// <summary>
        /// Traversal Source 變數
        /// </summary>
        string TraversalSource { get; set; }

        /// <summary>
        /// 建立 Groovy Script
        /// </summary>
        /// <returns></returns>
        ScriptResult Build();
    }
}
