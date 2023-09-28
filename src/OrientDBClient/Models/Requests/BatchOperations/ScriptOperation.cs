using Newtonsoft.Json;

namespace GQL.OrientDBClients.Models.Requests.BatchOperations
{
    public class ScriptOperation : IBatchOperation
    {
        public const string TypeName = "script";

        [JsonProperty("type")]
        public string Type { get => TypeName; }

        [JsonProperty("language")]
        public string Language { get; set; } = ScriptLanguage.SQL;

        [JsonProperty("script")]
        public List<string> Script { get; set; } = new List<string>();

        [JsonProperty("parameters")]
        public object Parameters { get; set; }

        public ScriptOperation()
        {

        }
        public ScriptOperation(string language, string script)
        {
            Language = language;
            Script = new List<string>() { script };
        }
        public ScriptOperation(string language, string script, object parameters)
        {
            Language = language;
            Script = new List<string>() { script };
            Parameters = parameters;
        }


        /// <summary>
        /// Create SQL Script
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public static ScriptOperation Sql(string script)
        {
            return new ScriptOperation(ScriptLanguage.SQL, script);
        }
        /// <summary>
        /// Create SQL Script
        /// </summary>
        /// <param name="script"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static ScriptOperation Sql(string script, IEnumerable<string> parameters)
        {
            return new ScriptOperation(ScriptLanguage.SQL, script, parameters);
        }

        /// <summary>
        /// Create JavaScript
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public static ScriptOperation JavaScript(string script)
        {
            return new ScriptOperation(ScriptLanguage.JavaScript, script);
        }

        /// <summary>
        /// Create Groovy Script
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public static ScriptOperation Groovy(string script)
        {
            return new ScriptOperation(ScriptLanguage.Groovy, script);
        }

        /// <summary>
        /// Create Gremlin Groovy Script 
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public static ScriptOperation GremlinGroovy(string script)
        {
            return new ScriptOperation(ScriptLanguage.GremlinGroovy, script);
        }
    }
}
