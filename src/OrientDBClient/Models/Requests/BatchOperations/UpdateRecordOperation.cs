using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GQL.OrientDBClients.Models.Requests.BatchOperations
{
    public class UpdateRecordOperation : IBatchOperation
    {
        public const string TypeName = "u";

        [JsonProperty("type")]
        public string Type { get => TypeName; }

        [JsonProperty("@rid")]
        public string Rid { get; set; }

        [JsonExtensionData(ReadData = true, WriteData = true)]
        public JObject Properties { get; set; }

        public UpdateRecordOperation()
        {

        }
        public UpdateRecordOperation(string rid)
        {
            Rid = rid;
        }
        public UpdateRecordOperation(string rid, JObject data)
        {
            Rid = rid;

            if (data != null)
            {
                Properties = data;
            }
        }


        /// <summary>
        /// Update
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static UpdateRecordOperation Create(string rid, Dictionary<string, object> data)
        {
            return new UpdateRecordOperation(rid, JObject.FromObject(data));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static UpdateRecordOperation Create<T>(string rid, T data) where T : class
        {
            return new UpdateRecordOperation(rid, JObject.FromObject(data));
        }
    }
}
