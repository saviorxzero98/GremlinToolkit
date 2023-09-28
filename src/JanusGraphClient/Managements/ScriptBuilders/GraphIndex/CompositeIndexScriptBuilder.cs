namespace GQL.JanusGraphClients.Managements.ScriptBuilders.GraphIndex
{
    public class CompositeIndexScriptBuilder : BaseScriptBuilder
    {
        /// <summary>
        /// Index Name
        /// </summary>
        public string IndexName { get; protected set; } = string.Empty;

        /// <summary>
        /// Property
        /// </summary>
        public Dictionary<string, GraphIndexProperty> Properties { get; protected set; } = new Dictionary<string, GraphIndexProperty>();

        /// <summary>
        /// Property Keys
        /// </summary>
        protected List<string> PropertyKeys
        {
            get
            {
                List<string> propertyKeys = new List<string>();

                foreach (var property in Properties)
                {
                    string propertyKey = property.Value.PropertyKey;
                    if (!string.IsNullOrWhiteSpace(propertyKey))
                    {
                        propertyKeys.Add(propertyKey);
                    }
                }

                return propertyKeys;
            }
        }

        /// <summary>
        /// Is Unique
        /// </summary>
        public bool IsUnique { get; protected set; } = false;

        /// <summary>
        /// Index Only Vertex Label
        /// </summary>
        public string IndexOnlyVertexLabel { get; protected set; } = string.Empty;

        public CompositeIndexScriptBuilder(string indexName)
        {
            SetIndexName(indexName);
        }
        public CompositeIndexScriptBuilder(string indexName, string graphName)
        {
            SetIndexName(indexName);
            SetGraphName(graphName);
        }


        #region 屬性設定

        /// <summary>
        /// 設定 Graph Name
        /// </summary>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public CompositeIndexScriptBuilder SetGraphName(string graphName)
        {
            GraphName = graphName;
            return this;
        }

        /// <summary>
        /// 設定 Index Name
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public CompositeIndexScriptBuilder SetIndexName(string indexName)
        {
            IndexName = indexName;
            return this;
        }

        /// <summary>
        /// 加入 Property
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public CompositeIndexScriptBuilder AddProperty(string propertyName)
        {
            return AddProperty(GraphIndexProperty.Create(propertyName));
        }
        /// <summary>
        /// 加入 Property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public CompositeIndexScriptBuilder AddProperty(GraphIndexProperty property)
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
        /// 設定為 Unique
        /// </summary>
        /// <returns></returns>
        public CompositeIndexScriptBuilder SetUnique()
        {
            IsUnique = true;
            return this;
        }

        /// <summary>
        /// 設定為 Uniqueness
        /// </summary>
        /// <returns></returns>
        public CompositeIndexScriptBuilder SetUniqueness()
        {
            IsUnique = false;
            return this;
        }

        /// <summary>
        /// 設定 Index Only Vertex Label
        /// </summary>
        /// <param name="vertexLabel"></param>
        /// <returns></returns>
        public CompositeIndexScriptBuilder SetIndexOnlyVertex(string vertexLabel)
        {
            IndexOnlyVertexLabel = vertexLabel;
            return this;
        }

        #endregion


        /// <summary>
        /// 產生 Groovy Script
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
        indexBuilder = indexBuilder.addKey(mgmt.getPropertyKey(properties[i]));
    }
    if (isUnique) {
        indexBuilder = indexBuilder.unique();
    }
    if (indexOnlyVertexLabel != null && !indexOnlyVertexLabel.isEmpty()) {
        indexBuilder = indexBuilder.indexOnly(mgmt.getVertexLabel(indexOnlyVertexLabel));
    }
    indexBuilder.buildCompositeIndex();
}
mgmt.commit();
";

            // 建立 Script Name Binding
            NameBindings.Add("indexName", IndexName);
            NameBindings.Add("isUnique", IsUnique);
            NameBindings.Add("properties", PropertyKeys);
            NameBindings.Add("indexOnlyVertexLabel", IndexOnlyVertexLabel);

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
