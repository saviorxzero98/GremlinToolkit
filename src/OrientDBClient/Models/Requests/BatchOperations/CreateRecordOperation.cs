using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GQL.OrientDBClients.Models.Requests.BatchOperations
{
    public class CreateRecordOperation : IBatchOperation
    {
        public const string TypeName = "c";

        [JsonProperty("type")]
        public string Type { get => TypeName; }

        [JsonProperty("@class")]
        public string Class { get; set; }

        [JsonExtensionData(ReadData = true, WriteData = true)]
        public JObject Properties { get; set; }


        public CreateRecordOperation()
        {

        }
        public CreateRecordOperation(string className)
        {
            Class = className;
        }
        public CreateRecordOperation(string className, JObject data)
        {
            Class = className;

            if (data != null)
            {
                Properties = data;
            }
        }

        /// <summary>
        /// Create Data
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public static CreateRecordOperation Create(string className)
        {
            return new CreateRecordOperation(className);
        }


        /// <summary>
        /// Create Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="className"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static CreateRecordOperation Create<T>(string className, Dictionary<string, object> data)
        {
            if (data != null)
            {
                return new CreateRecordOperation(className, JObject.FromObject(data));
            }
            else
            {
                return Create(className);
            }
        }

        /// <summary>
        ///  Create Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="className"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static CreateRecordOperation Create<T>(string className, T data) where T : class
        {
            if (data != null)
            {
                return new CreateRecordOperation(className, JObject.FromObject(data));
            }
            else
            {
                return Create(className);
            }
        }
    }
}
