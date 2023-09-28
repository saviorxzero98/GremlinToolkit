namespace GQL.QueryBuilders.Builders.Steps
{
    /// <summary>
    /// Terminal - https://tinkerpop.apache.org/docs/current/reference/#terminal-steps
    /// </summary>
    public class TerminalStep : IQueryStep
    {
        public static TerminalStep Step
        {
            get
            {
                return new TerminalStep();
            }
        }

        /// <summary>
        /// Next
        /// </summary>
        /// <returns></returns>
        public string Next()
        {
            return ".next()";
        }

        /// <summary>
        /// To List
        /// </summary>
        /// <returns></returns>
        public string ToList()
        {
            return ".toList()";
        }

        /// <summary>
        /// Iterate
        /// </summary>
        /// <returns></returns>
        public string Iterate()
        {
            return ".iterate()";
        }
    }
}
