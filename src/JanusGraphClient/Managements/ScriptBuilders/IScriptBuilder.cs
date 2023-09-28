using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// 建立 Script
        /// </summary>
        /// <returns></returns>
        ScriptResult Build();
    }
}
