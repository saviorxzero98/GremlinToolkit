using GQL.JanusGraphClients.Managements;
using GQL.JanusGraphClients.Managements.ScriptBuilders.GraphIndex;
using GQL.JanusGraphManagements.ScriptModels;
using GQL.JanusGraphManagements.ScriptModels.Indexes;

namespace GQL.JanusGraphManagements
{
    public class SchemaScriptRunner
    {
        public SchemaScriptRunner()
        {

        }


        public async Task<bool> ExecuteAsync(SchemaScript script)
        {
            if (!CheckScript(script))
            {
                return false;
            }

            // Build Properties
            await BuildPropertiesAsync(script);

            // Build Vertices
            await BuildVerticesAsync(script);

            // Build Edges
            await BuildEdgesAsync(script);

            // Build Composite Indexes
            await BuildCompositeIndexesAsync(script);

            // Build Composite Indexes
            await BuildMixedIndexesAsync(script);

            return true;
        }


        protected async Task<bool> BuildPropertiesAsync(SchemaScript script)
        {
            List<PropertySchema> properties = script.Properties;
            foreach (var property in properties)
            {
                try
                {
                    var service = new GraphSchemaService(script.Connection);
                    var result = await service.MakePropertyKeyAsync(property.Key, property.DataType, property.Cardinality);

                    if (result.IsSuccess)
                    {
                        LogSuccess($"Make Property \"{property.Key}\" Success");
                    }
                    else
                    {
                        LogFail($"Make Property \"{property.Key}\" Fail");
                        LogErrorMessage(result.Message);
                    }
                }
                catch
                {
                    LogFail($"Make Property \"{property.Key}\" Fail");
                }
            }
            return true;
        }

        protected async Task<bool> BuildVerticesAsync(SchemaScript script)
        {
            List<VertexSchema> vertices = script.Vertices;
            foreach (var vertex in vertices)
            {
                try
                {
                    var service = new GraphSchemaService(script.Connection);
                    var result = await service.MakeVertexLabelAsync(vertex.Label);

                    if (result.IsSuccess)
                    {
                        LogSuccess($"Make Vertex \"{vertex.Label}\" Success");
                    }
                    else
                    {
                        LogFail($"Make Vertex \"{vertex.Label}\" Fail");
                        LogErrorMessage(result.Message);
                    }
                }
                catch
                {
                    LogFail($"Make Vertex \"{vertex.Label}\" Fail");
                }
            }
            return true;
        }

        protected async Task<bool> BuildEdgesAsync(SchemaScript script)
        {
            List<EdgeSchema> edges = script.Edges;
            foreach (var edge in edges)
            {
                try
                {
                    var service = new GraphSchemaService(script.Connection);
                    var result = await service.MakeEdgeLabelAsync(edge.Label, edge.Multiplicity);

                    if (result.IsSuccess)
                    {
                        LogSuccess($"Make Edge \"{edge.Label}\" Success");
                    }
                    else
                    {
                        LogFail($"Make Edge \"{edge.Label}\" Fail");
                        LogErrorMessage(result.Message);
                    }
                }
                catch
                {
                    LogFail($"Make Edge \"{edge.Label}\" Fail");
                }
            }
            return true;
        }

        protected async Task<bool> BuildCompositeIndexesAsync(SchemaScript script)
        {
            List<CompositeIndexSchema> indexes = script.CompositeIndexes;
            foreach (var index in indexes)
            {
                try
                {
                    var service = new GraphIndexService(script.Connection);

                    string indexName = index.Name;
                    string propertyKey = index.PropertyKey;
                    var result = await service.CreateAndEnableCompositeIndexAsync(indexName, propertyKey, index.OnlyVertexLabel, index.IsUnique);

                    if (result.IsSuccess)
                    {
                        LogSuccess($"Build Composite Index \"{index.Name}\" on Property \"{index.PropertyKey}\" Success");
                    }
                    else
                    {
                        LogFail($"Build Composite Index \"{index.Name}\" on Property \"{index.PropertyKey}\" Fail");

                        if (!string.IsNullOrEmpty(result.Message))
                        {
                            LogErrorMessage(result.Message);
                        }
                    }
                }
                catch
                {
                    LogFail($"Build Composite Index \"{index.Name}\" on Property \"{index.PropertyKey}\" Fail");
                }
            }
            return true;
        }

        protected async Task<bool> BuildMixedIndexesAsync(SchemaScript script)
        {
            List<MixedIIndexSchema> indexes = script.MixedIndexes;
            foreach (var index in indexes)
            {
                try
                {
                    var service = new GraphIndexService(script.Connection);

                    string indexName = index.Name;
                    string backingIndex = index.BackingIndex;
                    var property = GraphIndexProperty.Create(index.PropertyKey, index.MappingType);
                    var result = await service.CreateAndEnableMixedIndexAsync(indexName, backingIndex, property, index.OnlyVertexLabel);

                    if (result.IsSuccess)
                    {
                        LogSuccess($"Build Mixed Index \"{index.Name}\" on Property \"{index.PropertyKey}\" Success");
                    }
                    else
                    {
                        LogFail($"Build Mixed Index \"{index.Name}\" on Property \"{index.PropertyKey}\" Fail");

                        if (!string.IsNullOrEmpty(result.Message))
                        {
                            LogErrorMessage(result.Message);
                        }
                    }
                }
                catch
                {
                    LogFail($"Build Mixed Index \"{index.Name}\" on Property \"{index.PropertyKey}\" Fail");
                }
            }
            return true;
        }


        public bool CheckScript(SchemaScript script)
        {
            return true;
        }

        public void LogSuccess(string message)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = currentColor;
        }

        public void LogFail(string message)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = currentColor;
        }

        public void LogErrorMessage(string message)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(message);
            Console.ForegroundColor = currentColor;
        }
    }
}
