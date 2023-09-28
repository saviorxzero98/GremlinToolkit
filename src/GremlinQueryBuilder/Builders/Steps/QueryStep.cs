using GQL.QueryBuilders.Builders.Traversals;
using Newtonsoft.Json.Linq;

namespace GQL.QueryBuilders.Builders.Steps
{
    /// <summary>
    /// Has        - https://tinkerpop.apache.org/docs/current/reference/#has-step
    /// Id         - https://tinkerpop.apache.org/docs/current/reference/#id-step
    /// Label      - https://tinkerpop.apache.org/docs/current/reference/#label-step
    /// Values     - https://tinkerpop.apache.org/docs/current/reference/#values-step
    /// ValueMap   - https://tinkerpop.apache.org/docs/current/reference/#valuemap-step
    /// ElementMap - https://tinkerpop.apache.org/docs/current/reference/#elementmap-step
    /// </summary>
    public class QueryStep : IQueryStep
    {
        public static QueryStep Step
        {
            get
            {
                return new QueryStep();
            }
        }


        #region Has

        /// <summary>
        /// Has Label
        /// </summary>
        /// <param name="label"></param>
        /// <param name="otherLabels"></param>
        /// <returns></returns>
        public string HasLabel(string label, params string[] otherLabels)
        {
            if (otherLabels == null || !otherLabels.Any())
            {
                return $".hasLabel({GetParam(label)})";
            }
            else
            {
                return $".hasLabel({GetParam(label)},{GetParams(otherLabels)})";
            }
        }

        /// <summary>
        /// Has Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string HasId(string id)
        {
            return $".hasId({GetParam(id)})";
        }
        /// <summary>
        /// Has Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string HasId(long id)
        {
            return $".hasId({GetParam(id)})";
        }


        /// <summary>
        /// Has Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string HasKey(string key)
        {
            return $".hasKey({GetParam(key)})";
        }

        /// <summary>
        /// Has Value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string HasValue(string key)
        {
            return $".hasValue({GetParam(key)})";
        }

        /// <summary>
        /// Has
        /// </summary>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Has(string label, string key, string value)
        {
            return $".has({GetParam(label)},{GetParam(key)},{GetParam(value)})";
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Has(string label, string key, bool value)
        {
            return $".has({GetParam(label)},{GetParam(key)},{GetParam(value)})";
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Has(string label, string key, long value)
        {
            return $".has({GetParam(label)},{GetParam(key)},{GetParam(value)})";
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Has(string label, string key, double value)
        {
            return $".has({GetParam(label)},{GetParam(key)},{GetParam(value)})";
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Has(string label, string key, JToken value)
        {
            switch (value.Type)
            {
                case JTokenType.Integer:
                    return Has(label, key, value.ToObject<long>());

                case JTokenType.Float:
                    return Has(label, key, value.ToObject<double>());

                case JTokenType.Boolean:
                    return Has(label, key, value.ToObject<bool>());

                case JTokenType.String:
                default:
                    return Has(label, key, value.ToString());
            }
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public string Has(string label, string key, PredicateTraversal predicate)
        {
            return $".has({GetParam(label)},{GetParam(key)},{predicate})";
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public string Has(string label, string key, TextPredicateTraversal predicate)
        {
            return $".has({GetParam(label)},{GetParam(key)},{predicate})";
        }

        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Has(string key, string value)
        {
            return $".has({GetParam(key)},{GetParam(value)})";
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Has(string key, bool value)
        {
            return $".has({GetParam(key)},{GetParam(value)})";
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Has(string key, long value)
        {
            return $".has({GetParam(key)},{GetParam(value)})";
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Has(string key, double value)
        {
            return $".has({GetParam(key)},{GetParam(value)})";
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public string Has(string key, PredicateTraversal predicate)
        {
            return $".has({GetParam(key)},{predicate})";
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public string Has(string key, TextPredicateTraversal predicate)
        {
            return $".has({GetParam(key)},{predicate})";
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public string Has(string key, SubTraversal traversal)
        {
            return $".has({GetParam(key)},{traversal})";
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public string Has(string key, SubTraversal.Traversal traversal)
        {
            return Has(key, traversal(new SubTraversal()));
        }

        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Has(string key)
        {
            return $".has({GetParam(key)})";
        }
        /// <summary>
        /// Has Not
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string HasNot(string key)
        {
            return $".hasNot({GetParam(key)})";
        }

        /// <summary>
        /// As
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public string As(params string[] alias)
        {
            return $".as({GetParams(alias)})";
        }

        #endregion


        #region Get Data

        /// <summary>
        /// Get Id
        /// </summary>
        /// <returns></returns>
        public string Id()
        {
            return ".id()";
        }

        /// <summary>
        /// Get Value
        /// </summary>
        /// <returns></returns>
        public string Label()
        {
            return ".label()";
        }

        /// <summary>
        /// Get Key
        /// </summary>
        /// <returns></returns>
        public string Key()
        {
            return $".key()";
        }

        /// <summary>
        /// Get Value
        /// </summary>
        /// <returns></returns>
        public string Value()
        {
            return $".value()";
        }

        /// <summary>
        /// Get Values
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public string Values(params string[] properties)
        {
            if (properties == null || !properties.Any())
            {
                return ".values()";
            }
            else
            {
                return $".values({GetParams(properties)})";
            }
        }

        /// <summary>
        /// Get Values (Mappings)
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public string ValueMap(params string[] properties)
        {
            if (properties == null || !properties.Any())
            {
                return ".valueMap()";
            }
            else
            {
                return $".valueMap({GetParams(properties)})";
            }
        }

        /// <summary>
        /// Get Values (Element Mapping)
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public string ElementMap(params string[] properties)
        {
            if (properties == null || !properties.Any())
            {
                return ".elementMap()";
            }
            else
            {
                return $".elementMap({GetParams(properties)})";
            }
        }

        #endregion
    }
}
