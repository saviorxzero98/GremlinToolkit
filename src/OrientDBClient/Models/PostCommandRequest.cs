using Newtonsoft.Json;

namespace GQL.OrientDBClients.Models
{
    public class PostCommandRequest
    {
        [JsonProperty("command")]
        public string Command { get; set; } = string.Empty;

        [JsonProperty("parameters")]
        public object Parameters { get; set; }

        public PostCommandRequest()
        {

        }
        public PostCommandRequest(string command)
        {
            Command = command;
        }
        public PostCommandRequest(string command, object parameters)
        {
            Command = command;
            Parameters = parameters;
        }


        /// <summary>
        /// 建立 Request
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static PostCommandRequest Create(string command)
        {
            return new PostCommandRequest(command);
        }

        /// <summary>
        /// 建立 Request
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static PostCommandRequest Create(string command, List<object> parameters)
        {
            return new PostCommandRequest()
            {
                Command = command,
                Parameters = parameters
            };
        }

        /// <summary>
        /// 建立 Request
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static PostCommandRequest Create<T>(string command, T parameters) where T : class
        {
            return new PostCommandRequest()
            {
                Command = command,
                Parameters = parameters
            };
        }
    }
}
