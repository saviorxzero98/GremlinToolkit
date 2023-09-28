using Newtonsoft.Json.Linq;

namespace GQL.QueryBuilders.Builders.Steps
{
    /// <summary>
    /// Is - https://tinkerpop.apache.org/docs/current/reference/#is-step
    /// </summary>
    public class ValueStep : IQueryStep
    {
        public static ValueStep Step
        {
            get
            {
                return new ValueStep();
            }
        }


        /// <summary>
        /// Is
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Is(string value)
        {
            return $".is({GetParam(value)})";
        }
        /// <summary>
        /// Is
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Is(long value)
        {
            return $".is({GetParam(value)})";
        }
        /// <summary>
        /// Is
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Is(double value)
        {
            return $".is({GetParam(value)})";
        }
        /// <summary>
        /// Is
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Is(bool value)
        {
            return $".is({GetParam(value)})";
        }
        /// <summary>
        /// Is
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Is(JToken value)
        {
            switch (value.Type)
            {
                case JTokenType.Integer:
                    return Is(value.ToObject<long>());

                case JTokenType.Float:
                    return Is(value.ToObject<double>());

                case JTokenType.Boolean:
                    return Is(value.ToObject<bool>());

                case JTokenType.String:
                default:
                    return Is(value.ToString());
            }
        }
        /// <summary>
        /// Is
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public string Is(PredicateTraversal predicate)
        {
            return $".is({predicate})";
        }
        /// <summary>
        /// Is
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public string Is(TextPredicateTraversal predicate)
        {
            return $".is({predicate})";
        }
    }
}
