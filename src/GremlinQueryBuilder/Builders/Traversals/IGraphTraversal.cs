using GQL.QueryBuilders.Extensions;
using System.Text;

namespace GQL.QueryBuilders.Builders.Traversals
{
    public abstract class IGraphTraversal
    {
        protected bool _isSubTraversal { get; set; } = true;
        protected string _traversalSource { get; set; } = QueryConstant.DefaultTraversalSource;
        protected StringBuilder _queryString { get; set; } = new StringBuilder();

        /// <summary>
        /// To Gremlin Query Statement
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (_isSubTraversal)
            {
                return _queryString.ToString()
                                   .TrimStart(_traversalSource)
                                   .TrimStart('.');
            }
            else
            {
                return $"{_traversalSource}{_queryString}";
            }
        }


        #region Cast Traversal

        /// <summary>
        /// AS Edge Traversal
        /// </summary>
        /// <returns></returns>
        public EdgeTraversal AsEdgeTraversal()
        {
            return new EdgeTraversal(_queryString, _traversalSource, _isSubTraversal);
        }
        /// <summary>
        /// AS Vertex Traversal
        /// </summary>
        /// <returns></returns>
        public VertexTraversal AsVertexTraversal()
        {
            return new VertexTraversal(_queryString, _traversalSource, _isSubTraversal);
        }
        /// <summary>
        /// AS Terminal Traversal
        /// </summary>
        /// <returns></returns>
        public TerminalTraversal AsTerminalTraversal()
        {
            return new TerminalTraversal(_queryString, _traversalSource, _isSubTraversal);
        }
        /// <summary>
        /// AS Property Traversal
        /// </summary>
        /// <returns></returns>
        public PropertyTraversal AsPropertyTraversal()
        {
            return new PropertyTraversal(_queryString, _traversalSource, _isSubTraversal);
        }
        /// <summary>
        /// AS Values Traversal
        /// </summary>
        /// <returns></returns>
        public ValuesTraversal AsValuesTraversal()
        {
            return new ValuesTraversal(_queryString, _traversalSource, _isSubTraversal);
        }
        /// <summary>
        /// AS Sub-Traversal
        /// </summary>
        /// <returns></returns>
        public SubTraversal AsSubTraversal()
        {
            return new SubTraversal(_queryString);
        }

        #endregion


        #region Query Parameters

        /// <summary>
        /// Get Query Parameter
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string GetParam(string value)
        {
            return GremlinParameter.Value(value);
        }
        /// <summary>
        /// Get Query Parameter
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string GetParam(long value)
        {
            return GremlinParameter.Value(value);
        }
        /// <summary>
        /// Get Query Parameter
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string GetParam(double value)
        {
            return GremlinParameter.Value(value);
        }
        /// <summary>
        /// Get Query Parameter
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string GetParam(decimal value)
        {
            return GremlinParameter.Value(value);
        }
        /// <summary>
        /// Get Query Parameter
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string GetParam(bool value)
        {
            return GremlinParameter.Value(value);
        }
        /// <summary>
        /// Get Query Parameter
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string GetParam(DateTime value)
        {
            return GremlinParameter.Value(value);
        }
        /// <summary>
        /// Get Query Parameter
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string GetParam(DateTimeOffset value)
        {
            return GremlinParameter.Value(value);
        }
        /// <summary>
        /// Get Query Parameter
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string GetParam(TimeSpan value)
        {
            return GremlinParameter.Value(value);
        }
        /// <summary>
        /// Get Query Parameter
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string GetParam(Enum value)
        {
            return GremlinParameter.Value(value);
        }
        /// <summary>
        /// Get Query Parameter
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        protected string GetParams(IEnumerable<string> values)
        {
            return GremlinParameter.Values(values);
        }
        /// <summary>
        /// Get Query Parameter
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        protected string GetParams(IEnumerable<long> values)
        {
            return GremlinParameter.Values(values);
        }
        /// <summary>
        /// Get Query Parameter
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        protected string GetParams(IEnumerable<double> values)
        {
            return GremlinParameter.Values(values);
        }
        /// <summary>
        /// Get Query Parameter
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        protected string GetParams(IEnumerable<decimal> values)
        {
            return GremlinParameter.Values(values);
        }
        /// <summary>
        /// Get Query Parameter
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        protected string GetParams(IEnumerable<IGraphTraversal> values)
        {
            return GremlinParameter.Values(values);
        }
        /// <summary>
        /// Get Query Parameter
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string GetParam<T>(T value)
        {
            return GremlinParameter.Value<T>(value);
        }

        #endregion
    }
}
