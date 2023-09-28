using Newtonsoft.Json;

namespace GQL.OrientDBClients.Models.Requests.BatchOperations
{
    public class DeleteRecordOperation : IBatchOperation
    {
        public const string TypeName = "d";

        [JsonProperty("type")]
        public string Type { get => TypeName; }

        [JsonProperty("@rid")]
        public string Rid { get; set; }

        public DeleteRecordOperation()
        {

        }
        public DeleteRecordOperation(string rid)
        {
            Rid = rid;
        }

        /// <summary>
        /// Delete Data
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public static DeleteRecordOperation Create(string rid)
        {
            return new DeleteRecordOperation(rid);
        }
    }
}
