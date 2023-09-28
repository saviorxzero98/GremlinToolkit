using GQL.JanusGraphManagements.ScriptModels;
using Newtonsoft.Json;

namespace GQL.JanusGraphManagements.ConsoleApplication
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var script = GetSchemaScript("Sample.json");
            var runner = new SchemaScriptRunner();
            await runner.ExecuteAsync(script);
        }

        static SchemaScript GetSchemaScript(string fileName)
        {
            string filePath = $"SchemaScripts/{fileName}";


            if (File.Exists(filePath))
            {
                return ReadSchemaScriptFromJson(fileName);
            }
            return new SchemaScript();
        }

        static SchemaScript ReadSchemaScriptFromJson(string fileName)
        {
            string filePath = $"SchemaScripts/{fileName}";


            if (File.Exists(filePath))
            {
                using (var r = new StreamReader(filePath))
                {
                    try
                    {
                        var schemaJson = r.ReadToEnd();
                        var script = JsonConvert.DeserializeObject<SchemaScript>(schemaJson);
                        return script;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message, $"Read Graph Metadata Error (file: {fileName})");
                    }
                }
            }
            return new SchemaScript();
        }
    }
}