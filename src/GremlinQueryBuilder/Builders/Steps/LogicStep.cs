using GQL.QueryBuilders.Builders.Traversals;

namespace GQL.QueryBuilders.Builders.Steps
{
    /// <summary>
    /// Not - https://tinkerpop.apache.org/docs/current/reference/#not-step
    /// And - https://tinkerpop.apache.org/docs/current/reference/#and-step
    /// Or  - https://tinkerpop.apache.org/docs/current/reference/#or-step
    /// </summary>
    public class LogicStep : IQueryStep
    {
        public static LogicStep Step
        {
            get
            {
                return new LogicStep();
            }
        }

        /// <summary>
        /// Not Sub-Traversal
        /// </summary>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public string Not(SubTraversal traversal)
        {
            return $".not({traversal})";
        }

        /// <summary>
        /// And Sub-Traversal
        /// </summary>
        /// <param name="traversals"></param>
        /// <returns></returns>
        public string And(params SubTraversal[] traversals)
        {
            if (traversals != null && traversals.Any())
            {
                return $".and({GetParams(traversals)})";
            }
            return string.Empty;
        }

        /// <summary>
        /// Or Sub-Traversal
        /// </summary>
        /// <param name="traversals"></param>
        /// <returns></returns>
        public string Or(params SubTraversal[] traversals)
        {
            if (traversals != null && traversals.Any())
            {
                return $".or({GetParams(traversals)})";
            }
            return string.Empty;
        }
    }
}
