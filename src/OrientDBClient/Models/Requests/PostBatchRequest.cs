using GQL.OrientDBClients.Models.Requests.BatchOperations;
using Newtonsoft.Json;

namespace GQL.OrientDBClients.Models.Requests
{
    public class PostBatchRequest
    {
        [JsonProperty("transaction")]
        public bool Transaction { get; set; } = true;

        [JsonProperty("operations")]
        public List<IBatchOperation> Operations { get; set; } = new List<IBatchOperation>();


        public PostBatchRequest()
        {

        }
        public PostBatchRequest(List<IBatchOperation> operations, bool transaction = true)
        {
            Transaction = transaction;
            Operations = operations;
        }

        /// <summary>
        /// 建立 Request (Gremlin Command)
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static PostBatchRequest CreateBatchGremlin(IEnumerable<string> commands, bool transaction = true)
        {
            if (commands == null || !commands.Any())
            {
                return null;
            }

            var operations = commands.Select(c => CommandOperation.Gremlin(c))
                                     .Cast<IBatchOperation>()
                                     .ToList();
            return new PostBatchRequest(operations, transaction);
        }

        /// <summary>
        /// 建立 Request (SQL Command)
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static PostBatchRequest CreateBatchSqlCommands(IEnumerable<string> commands, bool transaction = true)
        {
            if (commands == null || !commands.Any())
            {
                return null;
            }

            var operations = commands.Select(c => CommandOperation.Sql(c))
                                     .Cast<IBatchOperation>()
                                     .ToList();
            return new PostBatchRequest(operations, transaction);
        }


        /// <summary>
        /// 建立 Request (JavaScript)
        /// </summary>
        /// <param name="scripts"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static PostBatchRequest CreateBatchJavaScripts(IEnumerable<string> scripts, bool transaction = true)
        {
            if (scripts == null || !scripts.Any())
            {
                return null;
            }

            var operations = scripts.Select(s => ScriptOperation.JavaScript(s))
                                    .Cast<IBatchOperation>()
                                    .ToList();
            return new PostBatchRequest(operations, transaction);
        }

        /// <summary>
        /// 建立 Request (SQL Script)
        /// </summary>
        /// <param name="scripts"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static PostBatchRequest CreateBatchSqlScripts(IEnumerable<string> scripts, bool transaction = true)
        {
            if (scripts == null || !scripts.Any())
            {
                return null;
            }

            var operations = scripts.Select(s => ScriptOperation.JavaScript(s))
                                    .Cast<IBatchOperation>()
                                    .ToList();
            return new PostBatchRequest(operations, transaction);
        }
    }
}
