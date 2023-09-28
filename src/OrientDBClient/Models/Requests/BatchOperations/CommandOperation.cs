using Newtonsoft.Json;

namespace GQL.OrientDBClients.Models.Requests.BatchOperations
{
    public class CommandOperation : IBatchOperation
    {
        public const string TypeName = "cmd";

        [JsonProperty("type")]
        public string Type { get => TypeName; }

        [JsonProperty("language")]
        public string Language { get; set; } = CommandLanguage.SQL;

        [JsonProperty("command")]
        public string Command { get; set; } = string.Empty;

        [JsonProperty("parameters")]
        public object Parameters { get; set; }

        public CommandOperation()
        {

        }
        public CommandOperation(string language, string command)
        {
            Language = language;
            Command = command;
        }
        public CommandOperation(string language, string command, object parameters)
        {
            Language = language;
            Command = command;
            Parameters = parameters;
        }

        /// <summary>
        /// SQL Command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static CommandOperation Sql(string command)
        {
            return new CommandOperation(CommandLanguage.SQL, command);
        }
        /// <summary>
        /// SQL Command
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static CommandOperation Sql(string command, IEnumerable<string> parameters)
        {
            return new CommandOperation(CommandLanguage.SQL, command, parameters);
        }

        /// <summary>
        /// Gremlin Command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static CommandOperation Gremlin(string command)
        {
            return new CommandOperation(CommandLanguage.Gremlin, command);
        }
        /// <summary>
        /// Gremlin Command
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static CommandOperation Gremlin(string command, IEnumerable<string> parameters)
        {
            return new CommandOperation(CommandLanguage.Gremlin, command, parameters);
        }
    }
}
