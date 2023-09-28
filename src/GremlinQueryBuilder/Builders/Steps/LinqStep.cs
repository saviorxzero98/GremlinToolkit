using GQL.QueryBuilders.Builders.Traversals;
using System.Text;

namespace GQL.QueryBuilders.Builders.Steps
{
    /// <summary>
    /// Count       - https://tinkerpop.apache.org/docs/current/reference/#count-step
    /// Limit       - https://tinkerpop.apache.org/docs/current/reference/#limit-step
    /// Tail        - https://tinkerpop.apache.org/docs/current/reference/#tail-step
    /// Range       - https://tinkerpop.apache.org/docs/current/reference/#range-step
    /// Skip        - https://tinkerpop.apache.org/docs/current/reference/#skip-step
    /// Sample      - https://tinkerpop.apache.org/docs/current/reference/#sample-step
    /// Deduplify   - https://tinkerpop.apache.org/docs/current/reference/#dedup-step
    /// Order       - https://tinkerpop.apache.org/docs/current/reference/#order-step
    /// Group       - https://tinkerpop.apache.org/docs/current/reference/#group-step
    /// Group Count - https://tinkerpop.apache.org/docs/current/reference/#groupcount-step
    /// Where       - https://tinkerpop.apache.org/docs/current/reference/#where-step
    /// Select      - 
    /// </summary>
    public class LinqStep : IQueryStep
    {
        public static LinqStep Step
        {
            get
            {
                return new LinqStep();
            }
        }

        /// <summary>
        /// Count
        /// </summary>
        /// <returns></returns>
        public string Count()
        {
            return ".count()";
        }
        /// <summary>
        /// Count
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public string Count(QueryConstant.Scope scope)
        {
            return $".count({scope.ToString().ToLower()})";
        }


        /// <summary>
        /// Limit
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public string Limit(int count)
        {
            return $".limit({GetParam(count)})";
        }
        /// <summary>
        /// Limit
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public string Limit(QueryConstant.Scope scope, int count)
        {
            return $".limit({scope.ToString().ToLower()},{GetParam(count)})";
        }


        /// <summary>
        /// Tail
        /// </summary>
        /// <returns></returns>
        public string Tail()
        {
            return $".tail()";
        }
        /// <summary>
        /// Tail
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public string Tail(int count)
        {
            return $".tail({GetParam(count)})";
        }
        /// <summary>
        /// Tail
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public string Tail(QueryConstant.Scope scope, int count)
        {
            return $".limit({scope.ToString().ToLower()},{GetParam(count)})";
        }


        /// <summary>
        /// Range
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public string Range(int min, int max)
        {
            return $".range({GetParam(min)},{GetParam(max)})";
        }
        /// <summary>
        /// Range
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public string Range(QueryConstant.Scope scope, int min, int max)
        {
            return $".range({scope.ToString().ToLower()},{GetParam(min)},{GetParam(max)})";
        }


        /// <summary>
        /// Skip
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public string Skip(int count)
        {
            return $".skip({GetParam(count)})";
        }
        /// <summary>
        /// Skip
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public string Skip(QueryConstant.Scope scope, int count)
        {
            return $".skip({scope.ToString().ToLower()},{GetParam(count)})";
        }


        /// <summary>
        /// Sample
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public string Sample(int count)
        {
            return $".sample({GetParam(count)})";
        }
        /// <summary>
        /// Sample
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public string Sample(QueryConstant.Scope scope, int count)
        {
            return $".sample({scope.ToString().ToLower()},{GetParam(count)})";
        }
        /// <summary>
        /// Sample By
        /// </summary>
        /// <param name="count"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public string SampleBy(int count, string property = null)
        {
            if (string.IsNullOrEmpty(GetParam(property)))
            {
                return Sample(count);
            }
            else
            {
                return $".sample({count}).by({GetParam(property)})";
            }
        }
        /// <summary>
        /// Sample By
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="count"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public string SampleBy(QueryConstant.Scope scope, int count, string property = null)
        {
            if (string.IsNullOrEmpty(GetParam(property)))
            {
                return Sample(scope, count);
            }
            else
            {
                return $".sample({scope.ToString().ToLower()},{count}).by({GetParam(property)})";
            }
        }

        /// <summary>
        /// Deduplify
        /// </summary>
        /// <returns></returns>
        public string Deduplify(params string[] labels)
        {
            if (labels == null || !labels.Any())
            {
                return ".dedup()";
            }
            else
            {
                return $".dedup({GetParams(labels)})";
            }
        }
        /// <summary>
        /// Deduplify
        /// </summary>
        /// <returns></returns>
        public string Deduplify(QueryConstant.Scope scope, params string[] labels)
        {
            if (labels == null || !labels.Any())
            {
                return $".dedup({scope.ToString().ToLower()})";
            }
            else
            {
                return $".dedup({scope.ToString().ToLower()},{GetParams(labels)})";
            }
        }
        /// <summary>
        /// Deduplify By
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public string DebuplifyBy(string property = null)
        {
            if (string.IsNullOrEmpty(GetParam(property)))
            {
                return Deduplify();
            }
            else
            {
                return $".dedup().by({GetParam(property)})";
            }
        }

        /// <summary>
        /// Order By
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public string OrderBy(QueryConstant.OrderBy order = QueryConstant.OrderBy.Default)
        {
            StringBuilder query = new StringBuilder();
            query.Append($".order()");
            query.Append(ByPropertyOrder(order));
            return query.ToString();
        }
        /// <summary>
        /// Order By
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public string OrderBy(QueryConstant.Scope scope, QueryConstant.OrderBy order = QueryConstant.OrderBy.Default)
        {
            StringBuilder query = new StringBuilder();
            query.Append($".order({scope.ToString().ToLower()})");
            query.Append(ByPropertyOrder(order));
            return query.ToString();
        }
        /// <summary>
        /// Order By
        /// </summary>
        /// <param name="property"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public string OrderBy(string property, QueryConstant.OrderBy order = QueryConstant.OrderBy.Default)
        {
            StringBuilder query = new StringBuilder();
            query.Append($".order()");
            query.Append(ByPropertyOrder(property, order));
            return query.ToString();
        }
        /// <summary>
        /// Order By
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="property"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public string OrderBy(QueryConstant.Scope scope, string property, QueryConstant.OrderBy order = QueryConstant.OrderBy.Default)
        {
            StringBuilder query = new StringBuilder();
            query.Append($".order({scope.ToString().ToLower()})");
            query.Append(ByPropertyOrder(property, order));
            return query.ToString();
        }
        /// <summary>
        /// [Extend] Order By Many
        /// </summary>
        /// <param name="otherOrders"></param>
        /// <returns></returns>
        public string OrderBy(OrderQuery firstOrder, params OrderQuery[] otherOrders)
        {
            StringBuilder query = new StringBuilder();

            // Order By
            if (otherOrders != null && otherOrders.Any())
            {
                query.Append(".order()");
                query.Append(ByPropertyOrder(firstOrder.Property, firstOrder.Order));

                foreach (var order in otherOrders)
                {
                    query.Append(ByPropertyOrder(order.Property, order.Order));
                }
            }
            return query.ToString();
        }
        /// <summary>
        /// [Extend] Order By Many
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="firstOrder"></param>
        /// <param name="otherOrders"></param>
        /// <returns></returns>
        public string OrderBy(QueryConstant.Scope scope, OrderQuery firstOrder, params OrderQuery[] otherOrders)
        {
            StringBuilder query = new StringBuilder();

            // Order By
            if (otherOrders != null && otherOrders.Any())
            {
                query.Append($".order({scope.ToString().ToLower()})");
                query.Append(ByPropertyOrder(firstOrder.Property, firstOrder.Order));

                foreach (var order in otherOrders)
                {
                    query.Append(ByPropertyOrder(order.Property, order.Order));
                }
            }
            return query.ToString();
        }
        /// <summary>
        /// [Extend] Order By Many
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public string OrderBy(string property, params string[] otherProperties)
        {
            StringBuilder query = new StringBuilder();
            query.Append(".order()");
            query.Append(ByPropertyOrder(property));

            foreach (var otherProperty in otherProperties)
            {
                query.Append(ByPropertyOrder(otherProperty));
            }
            return query.ToString();
        }
        /// <summary>
        /// [Extend] Order By Many
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public string OrderBy(QueryConstant.Scope scope, string property, params string[] otherProperties)
        {
            StringBuilder query = new StringBuilder();
            query.Append($".order({scope.ToString().ToLower()})");
            query.Append(ByPropertyOrder(property));

            foreach (var otherProperty in otherProperties)
            {
                query.Append(ByPropertyOrder(otherProperty));
            }
            return query.ToString();
        }


        /// <summary>
        /// Group By
        /// </summary>
        /// <returns></returns>
        public string GroupBy()
        {
            return ".group().by(label)";
        }
        /// <summary>
        /// Group By property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public string GroupBy(string property)
        {
            return $".group().by(label).by({GetParam(property)})";
        }
        /// <summary>
        /// Group By Count
        /// </summary>
        /// <returns></returns>
        public string GroupByCount()
        {
            return ".group().by(label).by(count())";
        }

        /// <summary>
        /// Group Count
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GroupCount(string name)
        {
            return $".groupCount({GetParam(name)})";
        }


        /// <summary>
        /// Where
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public string Where(PredicateTraversal predicate)
        {
            return $".where({predicate})";
        }
        /// <summary>
        /// Where
        /// </summary>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public string Where(SubTraversal traversal)
        {
            return $".where({traversal})";
        }
        /// <summary>
        /// Where
        /// </summary>
        /// <param name="startKey"></param>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public string Where(string startKey, SubTraversal traversal)
        {
            return $".where({startKey},{traversal})";
        }


        #region Private Method

        /// <summary>
        /// By Order Property
        /// </summary>
        /// <param name="property"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private string ByPropertyOrder(QueryConstant.OrderBy order = QueryConstant.OrderBy.Default)
        {
            if (order == QueryConstant.OrderBy.Default)
            {
                return $".by()";
            }
            else
            {
                return $".by({GetParam(order)})";
            }
        }
        /// <summary>
        /// By Order Property
        /// </summary>
        /// <param name="property"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private string ByPropertyOrder(string property, QueryConstant.OrderBy order = QueryConstant.OrderBy.Default)
        {
            if (!string.IsNullOrEmpty(GetParam(property)))
            {
                if (order == QueryConstant.OrderBy.Default)
                {
                    return $".by({GetParam(property)})";
                }
                else
                {
                    return $".by({GetParam(property)},{GetParam(order)})";
                }
            }
            else
            {
                return ByPropertyOrder(order);
            }
        }

        #endregion
    }
}
