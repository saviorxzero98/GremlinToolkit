using GQL.GremlinClients.Extensions;
using System.Security;

namespace GQL.OrientDBClients.Configs
{
    public class OrientDBConfig
    {
        public const int DefaultPort = 2480;

        /// <summary>
        /// Host Url
        /// </summary>
        public string Host { get; set; } = "localhost";

        /// <summary>
        /// Port
        /// </summary>
        public int Port { get; set; } = DefaultPort;

        /// <summary>
        /// Database
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        private SecureString _password { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password
        {
            get
            {
                if (!SecureStringExtensions.IsNullOrEmpty(_password))
                {
                    return _password.ToPlainString();
                }
                return null;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _password = value.ToSecureString();
                }
            }
        }


        /// <summary>
        /// 設定 Host
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public OrientDBConfig SetHost(string host, int port)
        {
            Host = host;
            Port = port;
            return this;
        }
        /// <summary>
        /// 設定 Host
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public OrientDBConfig SetHost(Uri host, int port)
        {
            Host = host.ToString();
            Port = port;
            return this;
        }

        /// <summary>
        /// 設定帳號/密碼
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public OrientDBConfig SetAccount(string userName, string password)
        {
            UserName = userName;
            Password = password;
            return this;
        }

        /// <summary>
        /// 設定資料庫
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        public OrientDBConfig SetDatabase(string database)
        {
            Database = database;
            return this;
        }

        /// <summary>
        /// 從連線字串
        /// </summary>
        /// <param name="connectionStirng"></param>
        /// <returns></returns>
        public static OrientDBConfig FromConnectionString(string connectionStirng)
        {
            return new OrientDBConfig();
        }
    }
}
