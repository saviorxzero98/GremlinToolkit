using GQL.GremlinClients;
using GQL.GremlinClients.Extensions;
using GQL.OrientDBClients.Configs;
using GQL.OrientDBClients.Serializers;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Remote;
using Gremlin.Net.Process.Traversal;

namespace GQL.OrientDBClients
{
    public class OrientDBGremlinClient : IGremlinQueryClient
    {
        public OrientGremlinConfig Config { get; set; }
        public GremlinClient Client { get; protected set; }
        protected CancellationTokenRegistration CancellationRegistration { get; set; }

        public OrientDBGremlinClient(OrientGremlinConfig config, CancellationToken cancellationToken = default(CancellationToken))
        {
            Config = config;
            var options = new GremlinQueryClientOptions();

            // 設定 OrientDB Message Serializer
            options = options.SetMessageSerializer(OrientdbMessageSerializer.GetSerializer());

            // 設定 Connection Pool
            if (config.Options != null)
            {
                options.ConnectionPoolSettings = new ConnectionPoolSettings()
                {
                    PoolSize = config.Options.PoolSize,
                    MaxInProcessPerConnection = config.Options.MaxInProcessPerConnection,
                    ReconnectionAttempts = config.Options.ReconnectionAttempts,
                    ReconnectionBaseDelay = config.Options.GetReconnectionBaseDelay()
                };
            }

            OpenConnection(options, cancellationToken);
        }
        public OrientDBGremlinClient(OrientGremlinConfig config, GremlinQueryClientOptions options,
                                     CancellationToken cancellationToken = default(CancellationToken))
        {
            Config = config;
            options = options.SetMessageSerializer(OrientdbMessageSerializer.GetSerializer());
            OpenConnection(options, cancellationToken);
        }

        public void Dispose()
        {
            CloseConnection();
        }

        /// <summary>
        /// 開啟 Gremlin Client 連線
        /// </summary>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public GremlinClient OpenConnection(GremlinQueryClientOptions options, CancellationToken cancellationToken)
        {
            // Open Gremlin Client
            var server = new GremlinServer(Config.Host, Config.Port, Config.EnableSsl, Config.UserName, Config.Password);
            Client = new GremlinClient(server, options.MessageSerializer, options.ConnectionPoolSettings,
                                       options.WebSocketConfiguration, options.SessionId);


            // 註冊 Close Gremlin Client Callback
            CancellationRegistration = default(CancellationTokenRegistration);
            if (cancellationToken != null)
            {
                // 註冊 Close Gremlin Client Callback
                CancellationRegistration = cancellationToken.Register(() =>
                {
                    if (Client != null)
                    {
                        Client.Dispose();
                    }
                });
            }
            return Client;
        }

        /// <summary>
        /// 關閉 Gremlin Client 連線
        /// </summary>
        public void CloseConnection()
        {
            if (CancellationRegistration != null)
            {
                CancellationRegistration.Dispose();
            }

            if (Client != null)
            {
                Client.Dispose();
            }
        }

        /// <summary>
        /// 取得 Traversal Source
        /// </summary>
        /// <returns></returns>
        public GraphTraversalSource GetTraversalSource()
        {
            DriverRemoteConnection connection;

            if (string.IsNullOrEmpty(Config.TraversalSource))
            {
                connection = new DriverRemoteConnection(Client);
            }
            else
            {
                connection = new DriverRemoteConnection(Client, Config.TraversalSource);
            }

            var source = AnonymousTraversalSource.Traversal()
                                                 .WithRemote(connection);
            return source;
        }


        #region Common Traversal Step

        /// <summary>
        /// 清空所有 Vertex
        /// </summary>
        public void ClearAllVertex()
        {
            var source = GetTraversalSource();
            source.V().Drop().Iterate();
        }

        /// <summary>
        /// 清空所有 Edge
        /// </summary>
        public void ClearAllEdge()
        {
            var source = GetTraversalSource();
            source.E().Drop().Iterate();
        }

        #endregion


        #region 執行 Gremlin Groovy Script

        /// <summary>
        /// 執行 Gremlin Groovy Script
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="script"></param>
        /// <returns></returns>
        public async Task<List<T>> SubmitAsync<T>(string script, Dictionary<string, object> bindings = null)
        {
            ResultSet<T> resultSet = await Client.SubmitAsync<T>(script, bindings);
            List<T> results = resultSet.GetResults();
            return results;
        }

        /// <summary>
        /// 執行 Gremlin Groovy Script
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public async Task SubmitAsync(string script, Dictionary<string, object> bindings = null)
        {
            await Client.SubmitAsync<dynamic>(script, bindings);
        }

        /// <summary>
        /// 執行 Gremlin Groovy Script
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="script"></param>
        /// <returns></returns>
        public async Task<T> SubmitWithSingleResultAsync<T>(string script, Dictionary<string, object> bindings = null)
        {
            var result = await Client.SubmitWithSingleResultAsync<T>(script, bindings);
            return result;
        }

        #endregion
    }
}
