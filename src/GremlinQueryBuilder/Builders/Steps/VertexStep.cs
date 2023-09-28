using Newtonsoft.Json.Linq;
using System.Text;

namespace GQL.QueryBuilders.Builders.Steps
{
    /// <summary>
    /// https://tinkerpop.apache.org/docs/current/reference/#start-steps
    /// Add Vertex  - https://tinkerpop.apache.org/docs/current/reference/#addvertex-step
    /// Get Vertex  - https://tinkerpop.apache.org/docs/current/reference/#vertex-steps
    /// Drop Vertex - https://tinkerpop.apache.org/docs/current/reference/#drop-step
    /// </summary>
    public class VertexStep : IQueryStep
    {
        public static VertexStep Step
        {
            get
            {
                return new VertexStep();
            }
        }

        /// <summary>
        /// Add Vertex
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public string AddVertex(string label = null)
        {
            if (label == null)
            {
                return ".addV()";
            }
            else
            {
                return $".addV({GetParam(label)})";
            }
        }
        /// <summary>
        /// [Extend] Add Vertex With Properties
        /// </summary>
        /// <param name="label"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public string AddVertex(string label, Dictionary<string, object> properties)
        {
            StringBuilder query = new StringBuilder();
            query.Append(AddVertex(label));

            if (properties == null)
            {
                return query.ToString();
            }

            query.Append(PropertyStep.Step.Property(JObject.FromObject(properties)));
            return query.ToString();
        }
        /// <summary>
        /// [Extend] Add Vertex With Properties
        /// </summary>
        /// <param name="label"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string AddVertex(string label, object data)
        {
            StringBuilder query = new StringBuilder();
            query.Append(AddVertex(label));

            if (data == null)
            {
                return query.ToString();
            }

            JToken jdata = JToken.FromObject(data);
            if (jdata.Type != JTokenType.Object)
            {
                return query.ToString();
            }

            query.Append(PropertyStep.Step.Property((JObject)jdata));
            return query.ToString();
        }

        /// <summary>
        /// Drop Vertex
        /// </summary>
        /// <returns></returns>
        public string Drop()
        {
            return ".drop()";
        }


        /// <summary>
        /// Get Vertices
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Vertex(string id = null)
        {

            if (id != null)
            {
                return $".V({GetParam(id)}";
            }
            else
            {
                return ".V()";
            }
        }

        /// <summary>
        /// Get Vertices
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Vertex(long id)
        {
            return $".V({GetParam(id)})";
        }

        /// <summary>
        /// Get Out Vertex
        /// </summary>
        /// <returns></returns>
        public string OutVertex()
        {
            return ".outV()";
        }

        /// <summary>
        /// Get In Vertex
        /// </summary>
        /// <returns></returns>
        public string InVertex()
        {
            return ".inV()";
        }

        /// <summary>
        /// Get Both Vertices
        /// </summary>
        /// <returns></returns>
        public string BothVertex()
        {
            return ".bothV()";
        }

        /// <summary>
        /// Get Other Vertices
        /// </summary>
        /// <returns></returns>
        public string OtherVertex()
        {
            return ".otherV()";
        }
    }
}
