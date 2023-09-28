using GQL.GremlinClients.Configs;

namespace GQL.OrientDBClients.Configs
{
    public class OrientGremlinConfig : GenericGremlinConfig
    {
        /// <summary>
        /// 設定 Host
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="enableSSL"></param>
        /// <returns></returns>
        public OrientGremlinConfig SetHost(string host, int port, bool enableSSL = false)
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
        public OrientGremlinConfig SetHost(Uri host, int port, bool enableSSL = false)
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
        public OrientGremlinConfig SetAccount(string userName, string password)
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
        public OrientGremlinConfig SetTraversalSource(string traversalSource)
        {
            TraversalSource = traversalSource;
            return this;
        }
    }
}
