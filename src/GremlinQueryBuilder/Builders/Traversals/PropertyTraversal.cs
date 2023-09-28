using GQL.QueryBuilders.Builders.Steps;
using System.Text;

namespace GQL.QueryBuilders.Builders.Traversals
{
    public class PropertyTraversal : IGraphTraversal
    {
        public PropertyTraversal()
        {

        }
        internal PropertyTraversal(StringBuilder queryString, bool isSubTraversal)
        {
            _queryString = queryString;
            _isSubTraversal = isSubTraversal;
        }
        internal PropertyTraversal(StringBuilder queryString, string traversalSource, bool isSubTraversal)
        {
            _queryString = queryString;
            _isSubTraversal = isSubTraversal;

            if (!string.IsNullOrEmpty(traversalSource))
            {
                _traversalSource = traversalSource;
            }
        }


        #region Has

        /// <summary>
        /// Has Label
        /// </summary>
        /// <param name="label"></param>
        /// <param name="otherLabels"></param>
        /// <returns></returns>
        public PropertyTraversal HasLabel(string label, params string[] otherLabels)
        {
            _queryString.Append(QueryStep.Step.HasLabel(label, otherLabels));
            return this;
        }

        /// <summary>
        /// Has Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PropertyTraversal HasId(long id)
        {
            _queryString.Append(QueryStep.Step.HasId(id));
            return this;
        }

        /// <summary>
        /// Has Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public PropertyTraversal HasKey(string key)
        {
            _queryString.Append(QueryStep.Step.HasKey(key));
            return this;
        }

        /// <summary>
        /// Has Value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public PropertyTraversal HasValue(string key)
        {
            _queryString.Append(QueryStep.Step.HasValue(key));
            return this;
        }

        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public PropertyTraversal Has(string key)
        {
            _queryString.Append(QueryStep.Step.Has(key));
            return this;
        }

        /// <summary>
        /// Has Not
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public PropertyTraversal HasNot(string key)
        {
            _queryString.Append(QueryStep.Step.HasNot(key));
            return this;
        }

        #endregion


        #region Get Value

        /// <summary>
        /// Get Label
        /// </summary>
        /// <returns></returns>
        public ValuesTraversal Label()
        {
            _queryString.Append(QueryStep.Step.Label());
            return AsValuesTraversal();
        }

        /// <summary>
        /// Get Id
        /// </summary>
        /// <returns></returns>
        public ValuesTraversal Id()
        {
            _queryString.Append(QueryStep.Step.Id());
            return AsValuesTraversal();
        }

        /// <summary>
        /// Get Key
        /// </summary>
        /// <returns></returns>
        public ValuesTraversal Key()
        {
            _queryString.Append(QueryStep.Step.Key());
            return AsValuesTraversal();
        }

        /// <summary>
        /// Get Value
        /// </summary>
        /// <returns></returns>
        public ValuesTraversal Value()
        {
            _queryString.Append(QueryStep.Step.Value());
            return AsValuesTraversal();
        }

        #endregion


        #region Linq

        /// <summary>
        /// Get Count
        /// </summary>
        /// <returns></returns>
        public ValuesTraversal Count()
        {
            _queryString.Append(LinqStep.Step.Count());
            return AsValuesTraversal();
        }

        #endregion


        #region Other

        /// <summary>
        /// Drop Property
        /// </summary>
        /// <returns></returns>
        public TerminalTraversal Drop()
        {
            _queryString.Append(PropertyStep.Step.Drop());
            return AsTerminalTraversal();
        }

        #endregion
    }
}
