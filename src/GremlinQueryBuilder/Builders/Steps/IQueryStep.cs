using GQL.QueryBuilders.Builders.Traversals;

namespace GQL.QueryBuilders.Builders.Steps
{
    public abstract class IQueryStep
    {
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
