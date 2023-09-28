using Gremlin.Net.Driver;
using Gremlin.Net.Process.Traversal;
using System.Net.WebSockets;

namespace GQL.GremlinClients
{
    public interface IGremlinQueryClient : IDisposable
    {
        #region Open / Close Gremlin Client

        /// <summary>
        /// 開啟 Gremlin
        /// </summary>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        GremlinClient OpenConnection(GremlinQueryClientOptions options, CancellationToken cancellationToken);

        /// <summary>
        /// 關閉 Gremlin
        /// </summary>
        void CloseConnection();

        /// <summary>
        /// 取得 Traversal Source
        /// </summary>
        /// <returns></returns>
        GraphTraversalSource GetTraversalSource();

        #endregion


        #region Submit Groovy Script

        /// <summary>
        /// 執行 Gremlin Groovy Script
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="script"></param>
        /// <param name="bindings"></param>
        /// <returns></returns>
        Task<List<T>> SubmitAsync<T>(string script, Dictionary<string, object> bindings = null);

        /// <summary>
        /// 執行 Gremlin Groovy Script
        /// </summary>
        /// <param name="script"></param>
        /// <param name="bindings"></param>
        /// <returns></returns>
        Task SubmitAsync(string script, Dictionary<string, object> bindings = null);

        /// <summary>
        /// 執行 Gremlin Groovy Script
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="script"></param>
        /// <param name="bindings"></param>
        /// <returns></returns>
        Task<T> SubmitWithSingleResultAsync<T>(string script, Dictionary<string, object> bindings = null);

        #endregion
    }

    public class GremlinQueryClientOptions
    {
        public IMessageSerializer MessageSerializer { get; set; } = null;
        public ConnectionPoolSettings ConnectionPoolSettings { get; set; } = null;
        public Action<ClientWebSocketOptions> WebSocketConfiguration { get; set; } = null;
        public string SessionId { get; set; } = null;

        /// <summary>
        /// 設定 Message Serializer
        /// </summary>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public GremlinQueryClientOptions SetMessageSerializer(IMessageSerializer serializer)
        {
            MessageSerializer = serializer;
            return this;
        }

        /// <summary>
        /// 設定 Connection Pool Settings
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public GremlinQueryClientOptions SetConnectionPoolSettings(ConnectionPoolSettings settings)
        {
            ConnectionPoolSettings = settings;
            return this;
        }

        /// <summary>
        /// 設定 Web Socket Configuration
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public GremlinQueryClientOptions SetWebSocketConfiguration(Action<ClientWebSocketOptions> config)
        {
            WebSocketConfiguration = config;
            return this;
        }

        /// <summary>
        /// 設定 Session ID
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public GremlinQueryClientOptions SetSessionId(string sessionId)
        {
            SessionId = sessionId;
            return this;
        }
    }
}
