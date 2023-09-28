namespace GQL.QueryBuilders.Builders
{
    public class JanusGraphTextPredicateTraversal : TextPredicateTraversal
    {
        public JanusGraphTextPredicateTraversal()
        {
            Query = string.Empty;
        }

        // Text
        public static JanusGraphTextPredicateTraversal TextContains(string value)
        {
            var predicate = new JanusGraphTextPredicateTraversal
            {
                Query = $"textContains({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static JanusGraphTextPredicateTraversal TextNotContains(string value)
        {
            var predicate = new JanusGraphTextPredicateTraversal
            {
                Query = $"textNotContains({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static JanusGraphTextPredicateTraversal TextContainsPrefix(string value)
        {
            var predicate = new JanusGraphTextPredicateTraversal
            {
                Query = $"textContainsPrefix({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static JanusGraphTextPredicateTraversal TextNotContainsPrefix(string value)
        {
            var predicate = new JanusGraphTextPredicateTraversal
            {
                Query = $"textNotContainsPrefix({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static JanusGraphTextPredicateTraversal TextContainsRegex(string value)
        {
            var predicate = new JanusGraphTextPredicateTraversal
            {
                Query = $"textContainsRegex({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static JanusGraphTextPredicateTraversal TextNotContainsRegex(string value)
        {
            var predicate = new JanusGraphTextPredicateTraversal
            {
                Query = $"textNotContainsRegex({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static JanusGraphTextPredicateTraversal TextContainsFuzzy(string value)
        {
            var predicate = new JanusGraphTextPredicateTraversal
            {
                Query = $"textContainsFuzzy({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static JanusGraphTextPredicateTraversal TextNotContainsFuzzy(string value)
        {
            var predicate = new JanusGraphTextPredicateTraversal
            {
                Query = $"textNotContainsFuzzy({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static JanusGraphTextPredicateTraversal TextContainsPhrase(string value)
        {
            var predicate = new JanusGraphTextPredicateTraversal
            {
                Query = $"textContainsPhrase({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static JanusGraphTextPredicateTraversal TextNotContainsPhrase(string value)
        {
            var predicate = new JanusGraphTextPredicateTraversal
            {
                Query = $"textNotContainsPhrase({GremlinParameter.Value(value)})"
            };
            return predicate;
        }

        // String
        public static JanusGraphTextPredicateTraversal TextPrefix(string value)
        {
            var predicate = new JanusGraphTextPredicateTraversal
            {
                Query = $"textPrefix({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static JanusGraphTextPredicateTraversal TextNotPrefix(string value)
        {
            var predicate = new JanusGraphTextPredicateTraversal
            {
                Query = $"textNotPrefix({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static JanusGraphTextPredicateTraversal TextRegex(string value)
        {
            var predicate = new JanusGraphTextPredicateTraversal
            {
                Query = $"textRegex({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static JanusGraphTextPredicateTraversal TextNotRegex(string value)
        {
            var predicate = new JanusGraphTextPredicateTraversal
            {
                Query = $"textNotRegex({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static JanusGraphTextPredicateTraversal TextFuzzy(string value)
        {
            var predicate = new JanusGraphTextPredicateTraversal
            {
                Query = $"textFuzzy({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static JanusGraphTextPredicateTraversal TextNotFuzzy(string value)
        {
            var predicate = new JanusGraphTextPredicateTraversal
            {
                Query = $"textNotFuzzy({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
    }
}
