namespace GQL.QueryBuilders.Builders.Steps
{
    /// <summary>
    /// SimplePath - https://tinkerpop.apache.org/docs/current/reference/#simplepath-step
    /// Path       - https://tinkerpop.apache.org/docs/current/reference/#path-step
    /// </summary>
    public class TraversalStep : IQueryStep
    {
        public static TraversalStep Step
        {
            get
            {
                return new TraversalStep();
            }
        }

        /// <summary>
        /// Get Simple Path
        /// </summary>
        /// <returns></returns>
        public string SimplePath()
        {
            return ".simplePath()";
        }

        /// <summary>
        /// Get Path
        /// </summary>
        /// <returns></returns>
        public string Path()
        {
            return ".path()";
        }
    }
}
