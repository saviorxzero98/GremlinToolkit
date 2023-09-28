using GQL.QueryBuilders.Builders.Traversals;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace GQL.QueryBuilders.Builders
{
    static class GremlinParameter
    {
        public static string DateFormat = "yyyy-MM-dd HH:mm:ss";
        public static string TimeFormat = "HH:mm:ss";

        public static string Value(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return $"''";
            }

            string outValue = value.Trim();

            if (outValue.StartsWith("'") && outValue.EndsWith("'"))
            {
                outValue = value.TrimStart('\'').TrimEnd('\'');
            }
            outValue = $"'{outValue.Replace("'", "\\'").Replace("\\\\'", "\\'")}'";
            return outValue;
        }

        public static string Value(long value)
        {
            string result = value.ToString();
            return result;
        }

        public static string Value(double value)
        {
            string result = value.ToString(CultureInfo.InvariantCulture);
            return result;
        }

        public static string Value(decimal value)
        {
            string result = value.ToString(CultureInfo.InvariantCulture);
            return result;
        }

        public static string Value(bool value)
        {
            string result = value.ToString().ToLower();
            return result;
        }

        public static string Value(IGraphTraversal value)
        {
            string result = value.ToString();
            return result;
        }

        public static string Value(DateTime value)
        {
            string result = value.ToString(DateFormat);
            return $"'{result}'";
        }

        public static string Value(DateTimeOffset value)
        {
            string result = value.ToString(DateFormat);
            return $"'{result}'";
        }

        public static string Value(TimeSpan value)
        {
            string result = value.ToString(TimeFormat);
            return $"'{result}'";
        }

        public static string Value(Enum value)
        {
            string result = value.ToString().ToLower();
            return result;
        }

        public static string Values<T>(IEnumerable<T> values)
        {
            if (values == null)
            {
                return string.Empty;
            }

            if (!values.Any())
            {
                return "[]";
            }

            if (typeof(T) == typeof(string))
            {

                var valueList = values.Cast<string>()
                                      .Where(v => !string.IsNullOrEmpty(v))
                                      .Select(v => Value(v));
                return string.Join(",", valueList);
            }
            else if (typeof(T) == typeof(DateTime))
            {

                var valueList = values.Cast<DateTime>()
                                      .Select(v => Value(v));
                return string.Join(",", valueList);
            }
            else if (typeof(T) == typeof(DateTimeOffset))
            {

                var valueList = values.Cast<DateTimeOffset>()
                                      .Select(v => Value(v));
                return string.Join(",", valueList);
            }
            else if (typeof(T) == typeof(TimeSpan))
            {

                var valueList = values.Cast<TimeSpan>()
                                      .Select(v => Value(v));
                return string.Join(",", valueList);
            }
            else if (typeof(T) == typeof(IGraphTraversal))
            {

                var valueList = values.Cast<IGraphTraversal>()
                                      .Where(v => v != null)
                                      .Select(v => v.ToString());
                return string.Join(",", valueList);
            }
            else
            {
                return string.Join(",", values);
            }
        }

        public static string Value<T>(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (IsIntegerNumeric<T>())
            {
                return Value(Convert.ToInt64(value));
            }
            else if (IsNumeric<T>())
            {
                return Value(Convert.ToDouble(value));
            }
            else if (typeof(T) == typeof(bool))
            {
                return Value(Convert.ToBoolean(value));
            }
            else if (typeof(T) == typeof(IGraphTraversal))
            {
                return Value((IGraphTraversal)value);
            }
            else if (typeof(T) == typeof(DateTime))
            {
                return Value((DateTime)value);
            }
            else if (typeof(T) == typeof(DateTimeOffset))
            {
                return Value((DateTimeOffset)value);
            }
            else if (typeof(T) == typeof(TimeSpan))
            {
                return Value((TimeSpan)value);
            }
            else if (typeof(T) == typeof(string))
            {
                return Value(Convert.ToString(value));
            }
            return Value(JToken.FromObject(value).ToString());
        }

        private static bool IsIntegerNumeric<T>()
        {
            if (typeof(T) == typeof(short) ||
                typeof(T) == typeof(int) ||
                typeof(T) == typeof(long))
            {
                return true;
            }
            return false;
        }

        private static bool IsNumeric<T>()
        {
            if (IsIntegerNumeric<T>())
            {
                return true;
            }

            if (typeof(T) == typeof(float) ||
                typeof(T) == typeof(double) ||
                typeof(T) == typeof(decimal))
            {
                return true;
            }
            return false;
        }
    }
}
