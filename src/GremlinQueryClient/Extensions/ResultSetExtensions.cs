using Gremlin.Net.Driver;

namespace GQL.GremlinClients.Extensions
{
    public static class ResultSetExtensions
    {
        /// <summary>
        /// Gremlin Result Set to Object List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resultSet"></param>
        /// <returns></returns>
        public static List<T> GetResults<T>(this ResultSet<T> resultSet)
        {
            return resultSet.GetEnumerator().ToList();
        }

        /// <summary>
        /// Enumerator to List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerator"></param>
        /// <returns></returns>
        private static List<T> ToList<T>(this IEnumerator<T> enumerator)
        {
            var list = new List<T>();
            while (enumerator.MoveNext())
            {
                list.Add(enumerator.Current);
            }
            return list;
        }
    }
}
