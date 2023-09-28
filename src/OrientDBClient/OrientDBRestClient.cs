using GQL.OrientDBClients.Configs;
using GQL.OrientDBClients.Models;
using GQL.OrientDBClients.Models.Requests;
using GQL.OrientDBClients.Models.Responses;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;
using System.Text;

namespace GQL.OrientDBClients
{
    public class OrientDBRestClient
    {
        public OrientDBConfig Config { get; protected set; }

        public OrientDBRestClient(OrientDBConfig config)
        {
            Config = config;
        }

        /// <summary>
        /// 發送 Batch
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PostBatchResponse> PostBatchAsync(PostBatchRequest request)
        {
            if (TryCreateClient(out IRestClient restClient) &&
                !string.IsNullOrEmpty(Config.Database))
            {
                var restRequest = new RestRequest("batch/{database}")
                                        .AddUrlSegment("database", Config.Database)
                                        .AddJsonBody(request);

                var response = await restClient.PostAsync<PostBatchResponse>(restRequest);
                return response;
            }
            return null;
        }

        /// <summary>
        /// 發送 SQL Command
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PostCommandResponse> PostSqlCommandAsync(PostCommandRequest request)
        {
            if (TryCreateClient(out IRestClient restClient) &&
                !string.IsNullOrEmpty(Config.Database))
            {
                var restRequest = new RestRequest("command/{database}/{language}")
                                        .AddUrlSegment("database", Config.Database)
                                        .AddUrlSegment("language", CommandLanguage.SQL)
                                        .AddJsonBody(request);

                var response = await restClient.PostAsync<PostCommandResponse>(restRequest);
                return response;
            }
            return null;
        }

        /// <summary>
        /// 發送 Gremlin Command
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PostCommandResponse> PostGremlinCommandAsync(PostCommandRequest request)
        {
            if (TryCreateClient(out IRestClient restClient) &&
                !string.IsNullOrEmpty(Config.Database))
            {
                var restRequest = new RestRequest("command/{database}/{language}")
                                        .AddUrlSegment("database", Config.Database)
                                        .AddUrlSegment("language", CommandLanguage.Gremlin)
                                        .AddJsonBody(request);

                var response = await restClient.PostAsync<PostCommandResponse>(restRequest);
                return response;
            }
            return null;
        }


        /// <summary>
        /// 建立 REST Client
        /// </summary>
        /// <param name="restClient"></param>
        /// <returns></returns>
        private bool TryCreateClient(out IRestClient restClient)
        {
            if (Config == null ||
                string.IsNullOrWhiteSpace(Config.Host) ||
                string.IsNullOrWhiteSpace(Config.UserName) ||
                string.IsNullOrWhiteSpace(Config.Password))
            {
                restClient = null;
                return false;
            }
            else
            {
                var uriBuilder = new UriBuilder()
                {
                    Host = Config.Host,
                    Port = Config.Port
                };
                restClient = new RestClient(uriBuilder.Uri)
                                    .UseSerializer<JsonNetSerializer>();
                restClient.Authenticator = new HttpBasicAuthenticator(Config.UserName, Config.Password, Encoding.UTF8);
                return true;
            }
        }
    }
}
