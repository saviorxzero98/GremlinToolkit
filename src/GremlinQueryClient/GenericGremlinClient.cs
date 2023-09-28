using GQL.GremlinClients.Configs;
using GQL.GremlinClients.Extensions;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Remote;
using Gremlin.Net.Process.Traversal;

namespace GQL.GremlinClients
{
    public class GenericGremlinClient : IGremlinQueryClient
    {
        public GenericGremlinConfig Config { get; set; }
        public GremlinClient Client { get; protected set; }
        protected CancellationTokenRegistration CancellationRegistration { get; set; }

        public GenericGremlinClient(GenericGremlinConfig config,
                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            Config = config;
            var options = new GremlinQueryClientOptions();

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

            Client = OpenConnection(options, cancellationToken);
        }
        public GenericGremlinClient(GenericGremlinConfig config, string sessionId,
                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            Config = config;
            var options = new GremlinQueryClientOptions();

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

            if (!string.IsNullOrEmpty(sessionId))
            {
                options.SessionId = sessionId;
            }

            Client = OpenConnection(options, cancellationToken);
        }
        public GenericGremlinClient(GenericGremlinConfig config, GremlinQueryClientOptions options,
                                  CancellationToken cancellationToken = default(CancellationToken))
        {
            Config = config;
            Client = OpenConnection(options, cancellationToken);
        }

        public void Dispose()
        {
            CloseConnection();
        }


        /// <summary>
        /// Open Gremlin Connection
        /// </summary>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public GremlinClient OpenConnection(GremlinQueryClientOptions options, 
                                            CancellationToken cancellationToken = default(CancellationToken))
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
        /// Close Gremlin Connection
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
        /// Get Graph Traversal Source
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


        /// <summary>
        /// Submit Script
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="script"></param>
        /// <param name="bindings"></param>
        /// <returns></returns>
        public async Task<List<T>> SubmitAsync<T>(string script, Dictionary<string, object> bindings = null)
        {
            ResultSet<T> resultSet = await Client.SubmitAsync<T>(script, bindings);
            List<T> results = resultSet.GetResults();
            return results;
        }

        /// <summary>
        /// Submit Script
        /// </summary>
        /// <param name="script"></param>
        /// <param name="bindings"></param>
        /// <returns></returns>
        public async Task SubmitAsync(string script, Dictionary<string, object> bindings = null)
        {
            await Client.SubmitAsync<dynamic>(script, bindings);
        }

        /// <summary>
        /// Submit Script (Single Result)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="script"></param>
        /// <param name="bindings"></param>
        /// <returns></returns>
        public async Task<T> SubmitWithSingleResultAsync<T>(string script, Dictionary<string, object> bindings = null)
        {
            var result = await Client.SubmitWithSingleResultAsync<T>(script, bindings);
            return result;
        }
    }
}
