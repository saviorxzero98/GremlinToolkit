using Newtonsoft.Json;
using System.Reflection;

namespace GQL.OrientDBClients.Serializers.Models
{
    public abstract class IGraphSONToken
    {
        /// <summary>
        /// To Dictionary
        /// </summary>
        /// <returns></returns>
        public abstract Dictionary<string, dynamic> ToDictionary();

        /// <summary>
        /// Get Property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        protected string GetPropertyName(string property)
        {
            var attribute = (JsonPropertyAttribute)GetType().GetProperty(property)
                                                            .GetCustomAttribute(typeof(JsonPropertyAttribute));
            return attribute.PropertyName;
        }
    }
}
