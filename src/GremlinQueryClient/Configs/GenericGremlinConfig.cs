using GQL.GremlinClients.Extensions;
using System.Security;

namespace GQL.GremlinClients.Configs
{
    public class GenericGremlinConfig
    {
        public const string DefaultTraversalSource = "g";
        public const int DefaultPort = 8182;

        /// <summary>
        /// Host Url
        /// </summary>
        public string Host { get; set; } = "localhost";

        /// <summary>
        /// Port
        /// </summary>
        public int Port { get; set; } = DefaultPort;

        /// <summary>
        /// Enable SSL
        /// </summary>
        public bool EnableSsl { get; set; } = false;

        /// <summary>
        /// User Name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        protected SecureString _password { get; set; }
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
        /// Traversal Source
        /// </summary>
        public string TraversalSource { get; set; } = DefaultTraversalSource;

        /// <summary>
        /// Connection Pool Options
        /// </summary>
        public ConnectionPoolOptions Options { get; set; }
    }

    public class ConnectionPoolOptions
    {
        public const int DefaultPoolSize = 4;
        public const int DefaultMaxInProcessPerConnection = 32;
        public const int DefaultReconnectionAttempts = 4;
        public const double DefaultReconnectionBaseDelay = 1000;

        private int _poolSize = DefaultPoolSize;
        /// <summary>
        /// Connection Pool Size
        /// </summary>
        public int PoolSize
        {
            get
            {
                return _poolSize;
            }
            set
            {
                if (value > 0)
                {
                    _poolSize = value;
                }
                else
                {
                    _poolSize = DefaultPoolSize;
                }
            }
        }

        private int _maxInProcessPerConnection = DefaultMaxInProcessPerConnection;
        /// <summary>
        /// Max In Process Per Connection
        /// </summary>
        public int MaxInProcessPerConnection
        {
            get
            {
                return _maxInProcessPerConnection;
            }
            set
            {
                if (value > 0)
                {
                    _maxInProcessPerConnection = value;
                }
                else
                {
                    _maxInProcessPerConnection = DefaultMaxInProcessPerConnection;
                }
            }
        }

        private int _reconnectionAttempts = DefaultReconnectionAttempts;
        /// <summary>
        /// Reconnection Attempts
        /// </summary>
        public int ReconnectionAttempts
        {
            get
            {
                return _reconnectionAttempts;
            }
            set
            {
                if (value > 0)
                {
                    _reconnectionAttempts = value;
                }
                else
                {
                    _reconnectionAttempts = DefaultReconnectionAttempts;
                }
            }
        }

        private TimeSpan _reconnectionBaseDelay = TimeSpan.FromMilliseconds(DefaultReconnectionBaseDelay);
        /// <summary>
        /// Reconnection Base Delay (MilliSeconds)
        /// </summary>
        public double ReconnectionBaseDelayMilliSeconds
        {
            get
            {
                return _reconnectionBaseDelay.TotalMilliseconds;
            }
            set
            {
                if (value > 0)
                {
                    _reconnectionBaseDelay = TimeSpan.FromMilliseconds(value);
                }
                else
                {
                    _reconnectionBaseDelay = TimeSpan.FromMilliseconds(DefaultReconnectionBaseDelay);
                }
            }
        }

        /// <summary>
        /// Get Reconnection Base Delay
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetReconnectionBaseDelay()
        {
            return _reconnectionBaseDelay;
        }
    }
}
