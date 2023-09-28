using Newtonsoft.Json.Linq;
using System.Text;

namespace GQL.QueryBuilders.Builders.Steps
{
    /// <summary>
    /// Add Property  - https://tinkerpop.apache.org/docs/current/reference/#addproperty-step
    /// Properties    - https://tinkerpop.apache.org/docs/current/reference/#properties-step
    /// PropertyMap   - https://tinkerpop.apache.org/docs/current/reference/#propertymap-step
    /// Drop Property - https://tinkerpop.apache.org/docs/current/reference/#drop-step
    /// </summary>
    public class PropertyStep : IQueryStep
    {
        public static PropertyStep Step
        {
            get
            {
                return new PropertyStep();
            }
        }

        /// <summary>
        /// Add/Set Property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string Property(string name, string value, PropertyCardinalityType type = PropertyCardinalityType.Signle)
        {
            switch (type)
            {
                case PropertyCardinalityType.List:
                    return $".property(list,{GetParam(name)},{GetParam(value)})";

                case PropertyCardinalityType.Set:
                    return $".property(set,{GetParam(name)},{GetParam(value)})";
            }

            return $".property({GetParam(name)},{GetParam(value)})";
        }
        /// <summary>
        /// Add/Set Property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string Property(string name, long value, PropertyCardinalityType type = PropertyCardinalityType.Signle)
        {
            switch (type)
            {
                case PropertyCardinalityType.List:
                    return $".property(list,{GetParam(name)},{GetParam(value)})";

                case PropertyCardinalityType.Set:
                    return $".property(set,{GetParam(name)},{GetParam(value)})";
            }
            return $".property({GetParam(name)},{GetParam(value)})";
        }
        /// <summary>
        /// Add/Set Property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string Property(string name, double value, PropertyCardinalityType type = PropertyCardinalityType.Signle)
        {
            switch (type)
            {
                case PropertyCardinalityType.List:
                    return $".property(list,{GetParam(name)},{GetParam(value)})";

                case PropertyCardinalityType.Set:
                    return $".property(set,{GetParam(name)},{GetParam(value)})";
            }
            return $".property({GetParam(name)},{GetParam(value)})";
        }
        /// <summary>
        /// Add/Set Property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string Property(string name, decimal value, PropertyCardinalityType type = PropertyCardinalityType.Signle)
        {
            switch (type)
            {
                case PropertyCardinalityType.List:
                    return $".property(list,{GetParam(name)},{GetParam(value)})";

                case PropertyCardinalityType.Set:
                    return $".property(set,{GetParam(name)},{GetParam(value)})";
            }
            return $".property({GetParam(name)},{GetParam(value)})";
        }
        /// <summary>
        /// Add/Set Property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string Property(string name, bool value, PropertyCardinalityType type = PropertyCardinalityType.Signle)
        {
            switch (type)
            {
                case PropertyCardinalityType.List:
                    return $".property(list,{GetParam(name)},{GetParam(value)})";

                case PropertyCardinalityType.Set:
                    return $".property(set,{GetParam(name)},{GetParam(value)})";
            }
            return $".property({GetParam(name)},{GetParam(value)})";
        }
        /// <summary>
        /// Add/Set Property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string Property(string name, DateTime value, PropertyCardinalityType type = PropertyCardinalityType.Signle)
        {
            switch (type)
            {
                case PropertyCardinalityType.List:
                    return $".property(list,{GetParam(name)},{GetParam(value)})";

                case PropertyCardinalityType.Set:
                    return $".property(set,{GetParam(name)},{GetParam(value)})";
            }
            return $".property({GetParam(name)},{GetParam(value)})";
        }

        /// A/// <summary>dd/Set Property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string Property(string name, JToken value, PropertyCardinalityType type = PropertyCardinalityType.Signle)
        {
            switch (value.Type)
            {
                case JTokenType.Integer:
                    return Property(name, value.ToObject<long>(), type);

                case JTokenType.Float:
                    return Property(name, value.ToObject<double>(), type);

                case JTokenType.Boolean:
                    return Property(name, value.ToObject<bool>(), type);

                case JTokenType.Date:
                    return Property(name, value.ToObject<DateTime>(), type);

                case JTokenType.String:
                default:
                    return Property(name, value.ToString(), type);
            }
        }
        /// <summary>
        /// Add/Set Property
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Property(JObject data)
        {
            StringBuilder query = new StringBuilder();
            foreach (var item in data)
            {
                string name = item.Key;
                JToken value = item.Value;

                query.Append(Property(name, value));
            }
            return query.ToString();
        }
        /// <summary>
        /// [Extend] Add/Set Property From Object
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public string Property(Dictionary<string, object> properties)
        {
            StringBuilder query = new StringBuilder();
            foreach (var property in properties)
            {
                string name = property.Key;
                JToken value = JToken.FromObject(property.Value ?? string.Empty);
                query.Append(Property(name, value));
            }
            return query.ToString();
        }

        /// <summary>
        /// Drop Vertex
        /// </summary>
        /// <returns></returns>
        public string Drop()
        {
            return ".drop()";
        }


        /// <summary>
        /// Get Properties
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public string Properties(params string[] properties)
        {
            if (properties == null || !properties.Any())
            {
                return ".properties()";
            }
            else
            {
                return $".properties({GetParams(properties)})";
            }
        }

        /// <summary>
        /// Get Properties (Mapping)
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public string PropertyMap(params string[] properties)
        {
            if (properties == null || !properties.Any())
            {
                return ".propertyMap()";
            }
            else
            {
                return $".propertyMap({GetParams(properties)})";
            }
        }
    }
}
