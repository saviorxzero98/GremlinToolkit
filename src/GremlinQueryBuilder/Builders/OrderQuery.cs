namespace GQL.QueryBuilders.Builders
{
    public class OrderQuery
    {
        public string Property { get; set; }

        public QueryConstant.OrderBy Order { get; set; }


        public OrderQuery()
        {

        }
        public OrderQuery(string property, QueryConstant.OrderBy order = QueryConstant.OrderBy.Asc)
        {
            Property = property;
            Order = order;
        }

        public static OrderQuery Default(string property)
        {
            return new OrderQuery(property, QueryConstant.OrderBy.Default);
        }

        public static OrderQuery Asc(string property)
        {
            return new OrderQuery(property, QueryConstant.OrderBy.Asc);
        }

        public static OrderQuery Desc(string property)
        {
            return new OrderQuery(property, QueryConstant.OrderBy.Desc);
        }

        public static OrderQuery Shuffle(string property)
        {
            return new OrderQuery(property, QueryConstant.OrderBy.Shuffle);
        }
    }
}
