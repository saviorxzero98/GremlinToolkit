using System.Text;

namespace GQL.QueryBuilders.Builders.Traversals
{
    public class TerminalTraversal : IGraphTraversal
    {
        public TerminalTraversal()
        {

        }
        internal TerminalTraversal(StringBuilder queryString, bool isSubTraversal)
        {
            _queryString = queryString;
            _isSubTraversal = isSubTraversal;
        }
        internal TerminalTraversal(StringBuilder queryString, string traversalSource, bool isSubTraversal)
        {
            _queryString = queryString;
            _isSubTraversal = isSubTraversal;

            if (!string.IsNullOrEmpty(traversalSource))
            {
                _traversalSource = traversalSource;
            }
        }
    }
}
