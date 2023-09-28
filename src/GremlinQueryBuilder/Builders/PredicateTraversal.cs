namespace GQL.QueryBuilders.Builders
{
    public class PredicateTraversal
    {
        public string Query { get; private set; }

        public PredicateTraversal()
        {
            Query = string.Empty;
        }

        public override string ToString()
        {
            return Query;
        }

        public static PredicateTraversal Equals<T>(T value)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"eq({GremlinParameter.Value<T>(value)})"
            };
            return predicate;
        }

        public static PredicateTraversal NotEquals<T>(T value)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"neq({GremlinParameter.Value<T>(value)}"
            };
            return predicate;
        }

        public static PredicateTraversal LessThen(long value)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"lt({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static PredicateTraversal LessThen(double value)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"lt({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static PredicateTraversal LessThen(decimal value)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"lt({GremlinParameter.Value(value)})"
            };
            return predicate;
        }

        public static PredicateTraversal LessThenOrEquals(long value)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"lte({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static PredicateTraversal LessThenOrEquals(double value)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"lte({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static PredicateTraversal LessThenOrEquals(decimal value)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"lte({GremlinParameter.Value(value)})"
            };
            return predicate;
        }

        public static PredicateTraversal GreaterThan(long value)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"gt({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static PredicateTraversal GreaterThan(double value)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"gt({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static PredicateTraversal GreaterThan(decimal value)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"gt({GremlinParameter.Value(value)})"
            };
            return predicate;
        }

        public static PredicateTraversal GreaterThanOrEquals(long value)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"gte({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static PredicateTraversal GreaterThanOrEquals(double value)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"gte({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static PredicateTraversal GreaterThanOrEquals(decimal value)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"gte({GremlinParameter.Value(value)})"
            };
            return predicate;
        }

        public static PredicateTraversal Inside(long value1, long value2)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"inside({GremlinParameter.Value(value1)},{GremlinParameter.Value(value2)})"
            };
            return predicate;
        }
        public static PredicateTraversal Inside(double value1, double value2)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"inside({GremlinParameter.Value(value1)},{GremlinParameter.Value(value2)})"
            };
            return predicate;
        }
        public static PredicateTraversal Inside(decimal value1, decimal value2)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"inside({GremlinParameter.Value(value1)},{GremlinParameter.Value(value2)})"
            };
            return predicate;
        }

        public static PredicateTraversal Outside(long value1, long value2)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"outside({GremlinParameter.Value(value1)},{GremlinParameter.Value(value2)})"
            };
            return predicate;
        }
        public static PredicateTraversal Outside(double value1, double value2)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"outside({GremlinParameter.Value(value1)},{GremlinParameter.Value(value2)})"
            };
            return predicate;
        }
        public static PredicateTraversal Outside(decimal value1, decimal value2)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"outside({GremlinParameter.Value(value1)},{GremlinParameter.Value(value2)})"
            };
            return predicate;
        }

        public static PredicateTraversal Between(long value1, long value2)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"between({GremlinParameter.Value(value1)},{GremlinParameter.Value(value2)})"
            };
            return predicate;
        }
        public static PredicateTraversal Between(double value1, double value2)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"between({GremlinParameter.Value(value1)},{GremlinParameter.Value(value2)})"
            };
            return predicate;
        }
        public static PredicateTraversal Between(decimal value1, decimal value2)
        {
            var predicate = new PredicateTraversal
            {
                Query = $"between({GremlinParameter.Value(value1)},{GremlinParameter.Value(value2)})"
            };
            return predicate;
        }

        public static PredicateTraversal Within(params string[] values)
        {
            if (values == null || !values.Any())
            {
                return new PredicateTraversal();
            }
            else
            {
                var predicate = new PredicateTraversal
                {
                    Query = $"within({GremlinParameter.Values(values)})"
                };
                return predicate;
            }
        }
        public static PredicateTraversal Within(params long[] values)
        {
            if (values == null || !values.Any())
            {
                return new PredicateTraversal();
            }
            else
            {
                var predicate = new PredicateTraversal
                {
                    Query = $"within({GremlinParameter.Values(values)})"
                };
                return predicate;
            }
        }
        public static PredicateTraversal Within(params double[] values)
        {
            if (values == null || !values.Any())
            {
                return new PredicateTraversal();
            }
            else
            {
                var predicate = new PredicateTraversal
                {
                    Query = $"within({GremlinParameter.Values(values)})"
                };
                return predicate;
            }
        }
        public static PredicateTraversal Within(params decimal[] values)
        {
            if (values == null || !values.Any())
            {
                return new PredicateTraversal();
            }
            else
            {
                var predicate = new PredicateTraversal
                {
                    Query = $"within({GremlinParameter.Values(values)})"
                };
                return predicate;
            }
        }

        public static PredicateTraversal Without(params string[] values)
        {
            if (values == null || !values.Any())
            {
                return new PredicateTraversal();
            }
            else
            {
                var predicate = new PredicateTraversal
                {
                    Query = $"without({GremlinParameter.Values(values)})"
                };
                return predicate;
            }
        }
        public static PredicateTraversal Without(params long[] values)
        {
            if (values == null || !values.Any())
            {
                return new PredicateTraversal();
            }
            else
            {
                var predicate = new PredicateTraversal
                {
                    Query = $"without({GremlinParameter.Values(values)})"
                };
                return predicate;
            }
        }
        public static PredicateTraversal Without(params double[] values)
        {
            if (values == null || !values.Any())
            {
                return new PredicateTraversal();
            }
            else
            {
                var predicate = new PredicateTraversal
                {
                    Query = $"without({GremlinParameter.Values(values)})"
                };
                return predicate;
            }
        }
        public static PredicateTraversal Without(params decimal[] values)
        {
            if (values == null || !values.Any())
            {
                return new PredicateTraversal();
            }
            else
            {
                var predicate = new PredicateTraversal
                {
                    Query = $"without({GremlinParameter.Values(values)})"
                };
                return predicate;
            }
        }
    }

    public class TextPredicateTraversal
    {
        public string Query { get; protected set; }

        public TextPredicateTraversal()
        {
            Query = string.Empty;
        }

        public override string ToString()
        {
            return Query;
        }


        public static TextPredicateTraversal StartingWith(string value)
        {
            var predicate = new TextPredicateTraversal
            {
                Query = $"startingWith({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static TextPredicateTraversal EndingWith(string value)
        {
            var predicate = new TextPredicateTraversal
            {
                Query = $"endingWith({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static TextPredicateTraversal Containing(string value)
        {
            var predicate = new TextPredicateTraversal
            {
                Query = $"containing({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static TextPredicateTraversal NotStartingWith(string value)
        {
            var predicate = new TextPredicateTraversal
            {
                Query = $"notStartingWith({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static TextPredicateTraversal NotEndingWith(string value)
        {
            var predicate = new TextPredicateTraversal
            {
                Query = $"notEndingWith({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
        public static TextPredicateTraversal NotContaining(string value)
        {
            var predicate = new TextPredicateTraversal
            {
                Query = $"notContaining({GremlinParameter.Value(value)})"
            };
            return predicate;
        }
    }
}
