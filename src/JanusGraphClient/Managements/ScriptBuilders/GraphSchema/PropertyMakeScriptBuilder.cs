using GQL.JanusGraphClients.Configs;
using GQL.JanusGraphClients.Managements.Schema;

namespace GQL.JanusGraphClients.Managements.ScriptBuilders.GraphSchema
{
    public class PropertyMakeScriptBuilder : BaseScriptBuilder
    {
        private const string DefaultGraphName = JanusGraphDatabaseConfig.DefaultGraphName;

        public string Name { get; protected set; }

        public string DataType { get; protected set; }

        public string Cardinality { get; protected set; }

        public PropertyMakeScriptBuilder(string name, string dataType,
                                         string cardinality = PropertyCardinality.Single,
                                         string graphName = DefaultGraphName)
        {
            Name = name;
            SetDataType(dataType);
            SetCardinality(cardinality);
            SetGraphName(graphName);
        }


        /// <summary>
        /// 設定 Graph Name
        /// </summary>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public PropertyMakeScriptBuilder SetGraphName(string graphName)
        {
            GraphName = graphName;
            return this;
        }

        /// <summary>
        /// 設定 Property Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PropertyMakeScriptBuilder SetName(string name)
        {
            Name = name;
            return this;
        }

        /// <summary>
        /// 設定 Property Data Type
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public PropertyMakeScriptBuilder SetDataType(string dataType)
        {
            if (string.IsNullOrEmpty(dataType))
            {
                DataType = PropertyDataType.String;
            }
            else
            {
                switch (dataType.ToUpper())
                {
                    case PropertyDataType.String:
                    case PropertyDataType.Character:
                    case PropertyDataType.Boolean:
                    case PropertyDataType.Short:
                    case PropertyDataType.Integer:
                    case PropertyDataType.Long:
                    case PropertyDataType.Float:
                    case PropertyDataType.Double:
                    case PropertyDataType.Date:
                    case PropertyDataType.Uuid:
                        DataType = dataType;
                        break;

                    default:
                        DataType = PropertyDataType.String;
                        break;
                }
            }
            return this;
        }

        /// <summary>
        /// 設定 Property Cardinality
        /// </summary>
        /// <param name="cardinality"></param>
        /// <returns></returns>
        public PropertyMakeScriptBuilder SetCardinality(string cardinality)
        {
            if (string.IsNullOrEmpty(cardinality))
            {
                Cardinality = PropertyCardinality.Single;
            }
            else
            {
                switch (cardinality.ToUpper())
                {
                    case PropertyCardinality.Single:
                    case PropertyCardinality.List:
                    case PropertyCardinality.Set:
                        Cardinality = cardinality;
                        break;

                    default:
                        Cardinality = PropertyCardinality.Single;
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
if (mgmt.getPropertyKey(propertyName) == null) {
    maker = mgmt.makePropertyKey(propertyName);
    switch(propertyDataType) {
        case 'CHARACTER':
            maker = maker.dataType(Character.class);
            break;
        case 'BOOLEAN':
            maker = maker.dataType(Boolean.class);
            break;
        case 'SHORT':
            maker = maker.dataType(Short.class);
            break;
        case 'INTEGER':
            maker = maker.dataType(Integer.class);
            break;
        case 'LONG':
            maker = maker.dataType(Long.class);
            break;
        case 'FLOAT':
            maker = maker.dataType(Float.class);
            break;
        case 'DOUBLE':
            maker = maker.dataType(Double.class);
            break;
        case 'DATE':
            maker = maker.dataType(Date.class);
            break;
        case 'UUID':
            maker = maker.dataType(UUID.class);
            break;
        case 'OBJECT':
            maker = maker.dataType(Object.class);
            break;
        case 'STRING':
        default:
            maker = maker.dataType(String.class);
            break;
    }
    switch(propertyCardinality) {
        case 'LIST':
            maker = maker.cardinality(org.janusgraph.core.Cardinality.LIST);
            break;
        case 'SET':
            maker = maker.cardinality(org.janusgraph.core.Cardinality.SET);
            break;
        case 'SINGLE':
        default:
            maker = maker.cardinality(org.janusgraph.core.Cardinality.SINGLE);
            break;
    }
    maker.make();
}
mgmt.commit();
";

            // 建立 Script Name Binding
            NameBindings.Add("propertyName", Name);
            NameBindings.Add("propertyDataType", DataType.ToUpper());
            NameBindings.Add("propertyCardinality", Cardinality);

            return script;
        }

        /// <summary>
        /// 檢查參數是否合法
        /// </summary>
        /// <returns></returns>
        protected bool IsInvalid()
        {
            if (string.IsNullOrEmpty(GraphName) ||
                string.IsNullOrEmpty(Name) ||
                string.IsNullOrEmpty(DataType) ||
                string.IsNullOrEmpty(Cardinality))
            {
                return true;
            }
            return false;
        }
    }
}
