using GQL.GremlinClients.Configs;

namespace GQL.JanusGraphClients.Configs
{
    public class JanusGraphDatabaseConfig : GenericGremlinConfig
    {
        public const string DefaultGraphName = "graph";

        /// <summary>
        /// Graph Name
        /// </summary>
        public string GraphName { get; set; } = DefaultGraphName;


        /// <summary>
        /// 設定 Host
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="enableSSL"></param>
        /// <returns></returns>
        public JanusGraphDatabaseConfig SetHost(string host, int port, bool enableSSL = false)
        {
            Host = host;
            Port = port;
            EnableSsl = enableSSL;
            return this;
        }
        /// <summary>
        /// 設定 Host
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="enableSSL"></param>
        /// <returns></returns>
        public JanusGraphDatabaseConfig SetHost(Uri host, int port, bool enableSSL = false)
        {
            Host = host.ToString();
            Port = port;
            EnableSsl = enableSSL;
            return this;
        }

        /// <summary>
        /// 設定帳號/密碼
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JanusGraphDatabaseConfig SetAccount(string userName, string password)
        {
            UserName = userName;
            Password = password;
            return this;
        }

        /// <summary>
        /// 設定 Traversal Source
        /// </summary>
        /// <param name="traversalSource"></param>
        /// <returns></returns>
        public JanusGraphDatabaseConfig SetTraversalSource(string traversalSource)
        {
            TraversalSource = traversalSource;
            return this;
        }

        /// <summary>
        /// 設定 JanusGraph Name
        /// </summary>
        /// <param name="graphName"></param>
        /// <returns></returns>
        public JanusGraphDatabaseConfig SetGraphName(string graphName)
        {
            GraphName = graphName;
            return this;
        }
    }
}
