using Newtonsoft.Json.Linq;

namespace GQL.JanusGraphClients.Managements.Reports
{
    public class GraphIndexStatusReport
    {
        public bool IsSuccess { get; set; } = false;

        public bool IsNullIndex { get; set; } = true;

        public string IndexName { get; set; } = string.Empty;
        public List<string> Statuses { get; set; } = new List<string>();

        public Exception Error { get; set; }


        /// <summary>
        /// 建立 Report
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GraphIndexStatusReport From(object data)
        {
            try
            {
                var tempData = JToken.FromObject(data)
                                     .ToObject<List<Dictionary<string, object>>>()
                                     .SelectMany(d => d)
                                     .ToDictionary(e => e.Key, e => e.Value);

                var report = JToken.FromObject(tempData)
                                   .ToObject<GraphIndexStatusReport>();
                report.IsSuccess = true;
                return report;
            }
            catch
            {
                return new GraphIndexStatusReport();
            }
        }

        /// <summary>
        /// Fail
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static GraphIndexStatusReport Fail(Exception error = null)
        {
            return new GraphIndexStatusReport()
            {
                IsSuccess = false,
                Error = error
            };
        }
    }
}
