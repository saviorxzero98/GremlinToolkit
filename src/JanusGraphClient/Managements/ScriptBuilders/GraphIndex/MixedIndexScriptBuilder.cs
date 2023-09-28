using GQL.JanusGraphClients.Managements.Schema;

namespace GQL.JanusGraphClients.Managements.ScriptBuilders.GraphIndex
{
    public class MixedIndexScriptBuilder : BaseScriptBuilder
    {
        /// <summary>
        /// Index Name
        /// </summary>
        public string IndexName { get; protected set; } = string.Empty;

        /// <summary>
        /// Backing Index
        /// </summary>
        public string BackingIndex { get; protected set; } = BackingIndexName.Default;

        /// <summary>
        /// Property Key
        /// </summary>
        public Dictionary<string, GraphIndexProperty> Properties { get; protected set; } = new Dictionary<string, GraphIndexProperty>();

        /// <summary>
        /// Property Keys
        /// </summary>
        protected List<Dictionary<string, object>> PropertyKeys
        {
            get
            {
                List<Dictionary<string, object>> propertyKeys = new List<Dictionary<string, object>>();

                foreach (var property in Properties)
                {
                    string propertyKey = property.Value.PropertyKey;
                    string mappingType = property.Value.MixedIndexMappingType;
                    if (!string.IsNullOrWhiteSpace(propertyKey))
                    {
                        propertyKeys.Add(new Dictionary<string, object>()
                        {
                            { nameof(GraphIndexProperty.PropertyKey), propertyKey },
                            { nameof(GraphIndexProperty.MixedIndexMappingType), mappingType }
                        });
                    }
                }

                return propertyKeys;
            }
        }


        /// <summary>
        /// Index Only Vertex Label
        /// </summary>
        public string IndexOnlyVertexLabel { get; protected set; }

        public MixedIndexScriptBuilder(string indexName, string backingIndex = BackingIndexName.Default)
        {
            SetIndexName(indexName, backingIndex);
        }


        #region 屬性設定

        /// <summary>
        /// 設定 Graph Name
        /// </summary>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public MixedIndexScriptBuilder SetGraphName(string graphName)
        {
            GraphName = graphName;
            return this;
        }

        /// <summary>
        /// 設定 Index Name
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="backingIndex"></param>
        /// <returns></returns>
        public MixedIndexScriptBuilder SetIndexName(string indexName, string backingIndex = BackingIndexName.Default)
        {
            IndexName = indexName;
            BackingIndex = backingIndex;
            return this;
        }

        /// <summary>
        /// 加入 Property
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public MixedIndexScriptBuilder AddProperty(string propertyName)
        {
            return AddProperty(GraphIndexProperty.Create(propertyName));
        }
        /// <summary>
        /// 加入 Property
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="mappingType"></param>
        /// <returns></returns>
        public MixedIndexScriptBuilder AddProperty(string propertyName, string mappingType)
        {
            return AddProperty(GraphIndexProperty.Create(propertyName, mappingType));
        }
        /// <summary>
        /// 加入 Property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public MixedIndexScriptBuilder AddProperty(GraphIndexProperty property)
        {
            if (property != null && property.IsValid())
            {
                string propertyKey = property.PropertyKey;
                if (Properties.ContainsKey(propertyKey))
                {
                    Properties[propertyKey] = property;
                }
                else
                {
                    Properties.Add(propertyKey, property);
                }
            }
            return this;
        }

        /// <summary>
        /// 設定 Index Only Vertex Label
        /// </summary>
        /// <param name="vertexLabel"></param>
        /// <returns></returns>
        public MixedIndexScriptBuilder SetIndexOnlyVertex(string vertexLabel)
        {
            IndexOnlyVertexLabel = vertexLabel;
            return this;
        }

        #endregion

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
${GraphName}.tx().rollback();
mgmt = ${GraphName}.openManagement();
if (mgmt.getGraphIndex(indexName) == null) {
    indexBuilder = mgmt.buildIndex(indexName, Vertex.class);
    for (int i = 0; i < properties.size(); i++) {
        property = properties[i];
        switch (property.get(mappingTypeName)) {
             case 'TEXT':
                indexBuilder.addKey(mgmt.getPropertyKey(property.get(propertyKeyName)), Mapping.TEXT.asParameter());
                break;
             case 'STRING':
                indexBuilder.addKey(mgmt.getPropertyKey(property.get(propertyKeyName)), Mapping.STRING.asParameter());
                break;
             case 'TEXTSTRING':
                indexBuilder.addKey(mgmt.getPropertyKey(property.get(propertyKeyName)), Mapping.TEXTSTRING.asParameter());
                break;
             case 'PREFIX_TREE':
                indexBuilder.addKey(mgmt.getPropertyKey(property.get(propertyKeyName)), Mapping.PREFIX_TREE.asParameter());
                break;
             default:
                indexBuilder.addKey(mgmt.getPropertyKey(property.get(propertyKeyName)));
                break;
        }
    }
    if (indexOnlyVertexLabel != null && !indexOnlyVertexLabel.isEmpty()) {
        indexBuilder.indexOnly(mgmt.getVertexLabel(indexOnlyVertexLabel));
    }
    indexBuilder.buildMixedIndex(backingIndex);
}
mgmt.commit();
";

            // 建立 Script Name Binding
            NameBindings.Add("indexName", IndexName);
            NameBindings.Add("properties", PropertyKeys);
            NameBindings.Add("indexOnlyVertexLabel", IndexOnlyVertexLabel);
            NameBindings.Add("backingIndex", BackingIndex);
            NameBindings.Add("propertyKeyName", nameof(GraphIndexProperty.PropertyKey));
            NameBindings.Add("mappingTypeName", nameof(GraphIndexProperty.MixedIndexMappingType));

            return script;
        }

        /// <summary>
        /// 檢查參數是否合法
        /// </summary>
        /// <returns></returns>
        protected bool IsInvalid()
        {
            if (string.IsNullOrEmpty(GraphName) ||
                Properties == null ||
                !Properties.Any(p => p.Value != null && p.Value.IsValid()) ||
                string.IsNullOrEmpty(IndexName))
            {
                return true;
            }
            return false;
        }
    }
}
