using GQL.JanusGraphClients.Managements.Schema;

namespace GQL.JanusGraphClients.Managements.ScriptBuilders.GraphIndex
{
    public class GraphIndexProperty
    {
        public string PropertyKey { get; set; }

        public string MixedIndexMappingType { get; set; }

        private GraphIndexProperty()
        {

        }

        /// <summary>
        /// 建立 Property
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static GraphIndexProperty Create(string propertyName)
        {
            return new GraphIndexProperty()
            {
                PropertyKey = propertyName
            };
        }
        /// <summary>
        /// 建立 Property
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="mappingType"></param>
        /// <returns></returns>
        public static GraphIndexProperty Create(string propertyName, string mappingType)
        {
            var property = new GraphIndexProperty()
            {
                PropertyKey = propertyName,
            };

            if (string.IsNullOrEmpty(mappingType))
            {
                return property;
            }

            switch (mappingType.ToUpper())
            {
                case MixedIndexMapping.String:
                case MixedIndexMapping.Text:
                case MixedIndexMapping.TextString:
                case MixedIndexMapping.Geo:
                case MixedIndexMapping.Default:
                    property.MixedIndexMappingType = mappingType.ToUpper();
                    break;
            }

            return property;
        }

        /// <summary>
        /// 檢查 Property Key
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return (!string.IsNullOrEmpty(PropertyKey));
        }
    }
}
