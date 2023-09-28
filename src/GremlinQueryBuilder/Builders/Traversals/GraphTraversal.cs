using GQL.QueryBuilders.Builders.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GQL.QueryBuilders.Builders.Traversals
{
    public class GraphTraversal : IGraphTraversal
    {
        public GraphTraversal(string traversalSource = QueryConstant.DefaultTraversalSource)
        {
            _isSubTraversal = false;
            _traversalSource = traversalSource;
        }


        public static GraphTraversal Graph()
        {
            return new GraphTraversal();
        }
        public static GraphTraversal Graph(string traversalSource = QueryConstant.DefaultTraversalSource)
        {
            if (string.IsNullOrEmpty(traversalSource))
            {
                return new GraphTraversal();
            }
            else
            {
                return new GraphTraversal(traversalSource);
            }
        }


        #region Private Method

        /// <summary>
        /// Get Vertices
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VertexTraversal Vertex(string id = null)
        {
            _queryString.Append(VertexStep.Step.Vertex(id));
            return AsVertexTraversal();
        }
        /// <summary>
        /// Get Vertices
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VertexTraversal Vertex(long id)
        {
            _queryString.Append(VertexStep.Step.Vertex(id));
            return AsVertexTraversal();
        }

        /// <summary>
        /// Get Edges
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EdgeTraversal Edge(string id = null)
        {
            _queryString.Append(EdgeStep.Step.Edge(id));
            return AsEdgeTraversal();
        }
        /// <summary>
        /// Get Edges
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EdgeTraversal Edge(long id)
        {
            _queryString.Append(EdgeStep.Step.Edge(id));
            return AsEdgeTraversal();
        }


        /// <summary>
        /// Add Vertex
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public VertexTraversal AddVertex(string label = null)
        {
            _queryString.Append(VertexStep.Step.AddVertex(label));
            return AsVertexTraversal();
        }
        /// <summary>
        /// [Extend] Add Vertex With Properties
        /// </summary>
        /// <param name="label"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public VertexTraversal AddVertex(string label, Dictionary<string, object> properties)
        {
            _queryString.Append(VertexStep.Step.AddVertex(label, properties));
            return AsVertexTraversal();
        }
        /// <summary>
        /// [Extend] Add Vertex With Properties
        /// </summary>
        /// <param name="label"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public VertexTraversal AddVertex(string label, object data)
        {
            _queryString.Append(VertexStep.Step.AddVertex(label, data));
            return AsVertexTraversal();
        }

        /// <summary>
        /// Add Edge
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public EdgeTraversal AddEdge(string label = null)
        {
            _queryString.Append(EdgeStep.Step.AddEdge(label));
            return AsEdgeTraversal();
        }
        /// <summary>
        /// [Extend] Add Edge With Properties
        /// </summary>
        /// <param name="label"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public EdgeTraversal AddEdge(string label, object data)
        {
            _queryString.Append(EdgeStep.Step.AddEdge(label, data));
            return AsEdgeTraversal();
        }

        #endregion
    }
}
