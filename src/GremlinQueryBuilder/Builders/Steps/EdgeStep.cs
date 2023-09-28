using GQL.QueryBuilders.Builders.Traversals;
using Newtonsoft.Json.Linq;
using System.Text;

namespace GQL.QueryBuilders.Builders.Steps
{
    /// <summary>
    /// https://tinkerpop.apache.org/docs/current/reference/#start-steps
    /// Add Edge  - https://tinkerpop.apache.org/docs/current/reference/#addedge-step
    /// Get Edge  - https://tinkerpop.apache.org/docs/current/reference/#vertex-steps
    /// Drop Edge - https://tinkerpop.apache.org/docs/current/reference/#drop-step
    /// From      - https://tinkerpop.apache.org/docs/current/reference/#from-step
    /// To        - https://tinkerpop.apache.org/docs/current/reference/#to-step
    /// </summary>
    public class EdgeStep : IQueryStep
    {
        public static EdgeStep Step
        {
            get
            {
                return new EdgeStep();
            }
        }

        /// <summary>
        /// Add Edge
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public string AddEdge(string label = null)
        {
            if (!string.IsNullOrEmpty(label))
            {
                return $".addE({GetParam(label)})";
            }
            else
            {
                return ".addE()";
            }
        }
        /// <summary>
        /// [Extend] Add Edge With Properties
        /// </summary>
        /// <param name="label"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string AddEdge(string label, object data)
        {
            StringBuilder query = new StringBuilder();
            query.Append(AddEdge(label));

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
        /// From Vertex
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string FromVertex(string name)
        {
            return $".from({name})";
        }
        /// <summary>
        /// From Vertex
        /// </summary>
        /// <param name="sourceVertex"></param>
        /// <returns></returns>
        public string FromVertex(VertexTraversal sourceVertex)
        {
            return $".from({sourceVertex ?? new VertexTraversal()})";
        }
        /// <summary>
        ///  From Vertex
        /// </summary>
        /// <param name="sourceTraversal"></param>
        /// <returns></returns>
        public string FromVertex(SubTraversal sourceTraversal)
        {
            return $".from({sourceTraversal})";
        }

        /// <summary>
        /// To Vertex
        /// </summary>
        /// <param name="targetName"></param>
        /// <returns></returns>
        public string ToVertex(string targetName)
        {
            return $".to({targetName})";
        }
        /// <summary>
        /// To Vertex
        /// </summary>
        /// <param name="targetVertex"></param>
        /// <returns></returns>
        public string ToVertex(VertexTraversal targetVertex)
        {
            return $".to({targetVertex ?? new VertexTraversal()})";
        }
        /// <summary>
        /// To Vertex
        /// </summary>
        /// <param name="targetTraversal"></param>
        /// <returns></returns>
        public string ToVertex(SubTraversal targetTraversal)
        {
            return $".to({targetTraversal})";
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
        /// Get Edges
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Edge(string id = null)
        {
            if (id != null)
            {
                return $".E({GetParam(id)})";
            }
            else
            {
                return ".E()";
            }
        }

        /// <summary>
        /// Get Edges
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Edge(long id)
        {
            return $".E({GetParam(id)})";
        }

        /// <summary>
        /// Get Out Edges
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public string Out(string label = null)
        {
            if (!string.IsNullOrEmpty(label))
            {
                return $".out({GetParam(label)})";
            }
            else
            {
                return ".out()";
            }
        }

        /// <summary>
        /// Get In Edges
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public string In(string label = null)
        {
            if (!string.IsNullOrEmpty(label))
            {
                return $".in({GetParam(label)})";
            }
            else
            {
                return ".in()";
            }
        }

        /// <summary>
        /// Get Out Edges
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public string OutEdge(string label = null)
        {
            if (!string.IsNullOrEmpty(label))
            {
                return $".outE({GetParam(label)})";
            }
            else
            {
                return ".outE()";
            }
        }

        /// <summary>
        /// Get In Edges
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public string InEdge(string label = null)
        {
            if (!string.IsNullOrEmpty(label))
            {
                return $".inE({GetParam(label)})";
            }
            else
            {
                return ".inE()";
            }
        }

        /// <summary>
        /// Get Both Edges
        /// </summary>
        /// <param name="labels"></param>
        /// <returns></returns>
        public string BothEdge(params string[] labels)
        {
            if (labels == null || !labels.Any(l => !string.IsNullOrEmpty(l)))
            {
                return ".bothE()";
            }
            else
            {
                return $".bothE({GetParams(labels)})";
            }
        }
    }
}
