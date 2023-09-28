using GQL.JanusGraphClients.Managements.Schema;

namespace GQL.JanusGraphClients.Managements.ScriptBuilders.GraphSchema
{
    public class EdgeMakeScriptBuilder : BaseScriptBuilder
    {
        public string Label { get; protected set; }
        public string Multiplicity { get; protected set; }

        public EdgeMakeScriptBuilder(string label)
        {
            Label = label;
            SetMultiplicity(EdgeMultiplicity.Multi);
        }
        public EdgeMakeScriptBuilder(string label, string multiplicity)
        {
            Label = label;
            SetMultiplicity(multiplicity);
        }
        public EdgeMakeScriptBuilder(string label, string multiplicity, string graphName)
        {
            Label = label;
            SetMultiplicity(multiplicity);
            SetGraphName(graphName);
        }


        /// <summary>
        /// 設定 Graph Name
        /// </summary>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public EdgeMakeScriptBuilder SetGraphName(string graphName)
        {
            GraphName = graphName;
            return this;
        }

        /// <summary>
        /// 設定 Edge Label
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public EdgeMakeScriptBuilder SetLabel(string label)
        {
            Label = label;
            return this;
        }

        /// <summary>
        /// 設定 Edge Multiplicity
        /// </summary>
        /// <param name="multiplicity"></param>
        /// <returns></returns>
        public EdgeMakeScriptBuilder SetMultiplicity(string multiplicity)
        {
            if (string.IsNullOrEmpty(multiplicity))
            {
                Multiplicity = EdgeMultiplicity.Multi;
            }
            else
            {
                switch (multiplicity.ToUpper())
                {
                    case EdgeMultiplicity.Multi:
                    case EdgeMultiplicity.Simple:
                    case EdgeMultiplicity.ManyToOne:
                    case EdgeMultiplicity.OneToMany:
                    case EdgeMultiplicity.OneToOne:
                        Multiplicity = multiplicity;
                        break;

                    default:
                        Multiplicity = EdgeMultiplicity.Multi;
                        break;
                }
            }
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
if (mgmt.getEdgeLabel(edgeLabel) == null) {
    switch (multiplicity) {
        case 'MULTI':
            mgmt.makeEdgeLabel(edgeLabel).multiplicity(MULTI).make();
            break;
        case 'SIMPLE':
            mgmt.makeEdgeLabel(edgeLabel).multiplicity(SIMPLE).make();
            break;
        case 'MANY2ONE':
            mgmt.makeEdgeLabel(edgeLabel).multiplicity(MANY2ONE).make();
            break;
        case 'ONE2MANY':
            mgmt.makeEdgeLabel(edgeLabel).multiplicity(ONE2MANY).make();
            break;
        case 'ONE2ONE':
            mgmt.makeEdgeLabel(edgeLabel).multiplicity(ONE2ONE).make();
            break;
    }
}
mgmt.commit();
";

            // 建立 Script Name Binding
            NameBindings.Add("edgeLabel", Label);
            NameBindings.Add("multiplicity", Multiplicity);

            return script;
        }

        /// <summary>
        /// 檢查參數是否合法
        /// </summary>
        /// <returns></returns>
        protected bool IsInvalid()
        {
            if (string.IsNullOrEmpty(GraphName) ||
                string.IsNullOrEmpty(Label) ||
                string.IsNullOrEmpty(Multiplicity))
            {
                return true;
            }
            return false;
        }
    }
}
