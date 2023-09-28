using GQL.JanusGraphClients.Configs;
using GQL.JanusGraphClients.Managements.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GQL.JanusGraphClients.Managements.ScriptBuilders.GraphSchema
{
    public class NameChangeScriptBuilder : BaseScriptBuilder
    {
        private const string DefaultGraphName = JanusGraphDatabaseConfig.DefaultGraphName;

        public string OldName { get; protected set; }
        public string NewName { get; protected set; }

        private GraphElementType Type { get; set; }

        public bool IsVertex { get => Type == GraphElementType.Vertex; }
        public bool IsEdge { get => Type == GraphElementType.Edge; }
        public bool IsProperty { get => Type == GraphElementType.Property; }


        private NameChangeScriptBuilder(GraphElementType type, string oldName, string newName,
                                        string graphName = DefaultGraphName)
        {
            Type = type;
            OldName = oldName;
            NewName = newName;
            SetGraphName(graphName);
        }


        /// <summary>
        /// 設定 Graph Name
        /// </summary>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public NameChangeScriptBuilder SetGraphName(string graphName)
        {
            GraphName = graphName;
            return this;
        }

        /// <summary>
        /// 重新命名 Vertex Label
        /// </summary>
        /// <param name="label"></param>
        /// <param name="newLabel"></param>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public static NameChangeScriptBuilder CreateVertexNameChangeScriptBuilder(string label, string newLabel,
                                                                                  string graphName = DefaultGraphName)
        {
            return new NameChangeScriptBuilder(GraphElementType.Vertex, label, newLabel, graphName);
        }

        /// <summary>
        /// 重新命名 Edge Label
        /// </summary>
        /// <param name="label"></param>
        /// <param name="newLabel"></param>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public static NameChangeScriptBuilder CreateEdgeNameChangeScriptBuilder(string label, string newLabel,
                                                                                string graphName = DefaultGraphName)
        {
            return new NameChangeScriptBuilder(GraphElementType.Edge, label, newLabel, graphName);
        }

        /// <summary>
        /// 重新命名 Property Name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="newName"></param>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public static NameChangeScriptBuilder CreatePropertyNameChangeScriptBuilder(string name, string newName,
                                                                                    string graphName = DefaultGraphName)
        {
            return new NameChangeScriptBuilder(GraphElementType.Property, name, newName, graphName);
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

            // 建立 Script
            string script = string.Empty;

            switch (Type)
            {
                case GraphElementType.Vertex:
                    script = CreateChangeVertexLabel();
                    break;

                case GraphElementType.Edge:
                    script = CreateChangeEdgeLabel();
                    break;

                case GraphElementType.Property:
                    script = CreateChangePropertyLabel();
                    break;
            }

            // 建立 Script Name Binding
            NameBindings.Add("oldName", OldName);
            NameBindings.Add("newName", NewName);

            return script;
        }


        /// <summary>
        /// 建立 重新命名 Vertex Label 的 Script
        /// </summary>
        /// <returns></returns>
        protected string CreateChangeVertexLabel()
        {
            string script = @"
mgmt = ${GraphName}.openManagement();
if (mgmt.getVertexLabel(oldName) != null) {
    mgmt.changeName(mgmt.getVertexLabel(oldName), newName);
}
mgmt.commit();
";
            return script;
        }

        /// <summary>
        /// 建立 重新命名 Edge Label Script
        /// </summary>
        /// <returns></returns>
        protected string CreateChangeEdgeLabel()
        {
            string script = @"
mgmt = ${GraphName}.openManagement();
if (mgmt.getEdgeLabel(oldName) != null) {
    mgmt.changeName(mgmt.getEdgeLabel(oldName), newName);
}
mgmt.commit();
";
            return script;
        }

        /// <summary>
        /// 建立 重新命名 Property Name Script
        /// </summary>
        /// <returns></returns>
        protected string CreateChangePropertyLabel()
        {
            string script = @"
mgmt = ${GraphName}.openManagement();
if (mgmt.getPropertyKey(oldName) != null) {
    mgmt.changeName(mgmt.getPropertyKey(oldName), newName);
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
                string.IsNullOrEmpty(OldName) ||
                string.IsNullOrEmpty(NewName))
            {
                return true;
            }
            return false;
        }
    }
}
