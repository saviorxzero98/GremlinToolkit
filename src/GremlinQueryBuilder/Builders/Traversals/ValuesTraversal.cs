using GQL.QueryBuilders.Builders.Steps;
using Newtonsoft.Json.Linq;
using System.Text;

namespace GQL.QueryBuilders.Builders.Traversals
{
    public class ValuesTraversal : IGraphTraversal
    {
        public ValuesTraversal()
        {

        }
        internal ValuesTraversal(StringBuilder queryString, bool isSubTraversal)
        {
            _queryString = queryString;
            _isSubTraversal = isSubTraversal;
        }
        internal ValuesTraversal(StringBuilder queryString, string traversalSource, bool isSubTraversal)
        {
            _queryString = queryString;
            _isSubTraversal = isSubTraversal;

            if (!string.IsNullOrEmpty(traversalSource))
            {
                _traversalSource = traversalSource;
            }
        }

        #region Check Value

        /// <summary>
        /// Is Value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ValuesTraversal Is(string value)
        {
            _queryString.Append(ValueStep.Step.Is(value));
            return this;
        }
        /// <summary>
        /// Is Value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ValuesTraversal Is(long value)
        {
            _queryString.Append(ValueStep.Step.Is(value));
            return this;
        }
        /// <summary>
        /// Is Value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ValuesTraversal Is(double value)
        {
            _queryString.Append(ValueStep.Step.Is(value));
            return this;
        }
        /// <summary>
        /// Is Value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ValuesTraversal Is(decimal value)
        {
            _queryString.Append(ValueStep.Step.Is(value));
            return this;
        }
        /// <summary>
        /// Is Value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ValuesTraversal Is(bool value)
        {
            _queryString.Append(ValueStep.Step.Is(value));
            return this;
        }
        /// <summary>
        /// Is Value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ValuesTraversal Is(JToken value)
        {
            _queryString.Append(ValueStep.Step.Is(value));
            return this;
        }
        /// <summary>
        /// Is Predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public ValuesTraversal Is(PredicateTraversal predicate)
        {
            _queryString.Append(ValueStep.Step.Is(predicate));
            return this;
        }
        /// <summary>
        /// Is Predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public ValuesTraversal Is(TextPredicateTraversal predicate)
        {
            _queryString.Append(ValueStep.Step.Is(predicate));
            return this;
        }

        #endregion
    }
}
