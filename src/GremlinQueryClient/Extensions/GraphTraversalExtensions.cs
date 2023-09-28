using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure;
using Newtonsoft.Json.Linq;

namespace GQL.GremlinClients.Extensions
{
    public enum PropertyCardinalityType
    {
        Signle,
        List,
        Set
    }

    public static class GraphTraversalExtensions
    {
        #region Get Vertex

        /// <summary>
        ///  Get Vertex By Id
        /// </summary>
        /// <param name="source"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static GraphTraversal<Vertex, Vertex> GetVertex(this GraphTraversalSource source, object id)
        {
            return source.V(id);
        }

        /// <summary>
        /// Get Vertices By Label
        /// </summary>
        /// <param name="source"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static GraphTraversal<Vertex, Vertex> GetVerticesByLabel(this GraphTraversalSource source, string label)
        {
            if (string.IsNullOrEmpty(label))
            {
                return source.V();
            }

            return source.V().HasLabel(label);
        }

        /// <summary>
        /// Get Vertices By Label & Property
        /// </summary>
        /// <param name="source"></param>
        /// <param name="label"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static GraphTraversal<Vertex, Vertex> GetVerticesByProperty(this GraphTraversalSource source, string label,
                                                                           string name, object value)
        {
            if (string.IsNullOrEmpty(name) || IsNullPropertyValue(value))
            {
                return source.V().HasLabel(label);
            }
            return source.V().Has(label, name, value);
        }

        /// <summary>
        /// Get Vertices By Property
        /// </summary>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static GraphTraversal<Vertex, Vertex> GetVerticesByProperty(this GraphTraversalSource source, string name, object value)
        {
            if (string.IsNullOrEmpty(name) || IsNullPropertyValue(value))
            {
                return source.V();
            }
            return source.V().Has(name, value);
        }

        /// <summary>
        /// Get Vertices By Label & Properties
        /// </summary>
        /// <param name="source"></param>
        /// <param name="label"></param>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        public static GraphTraversal<Vertex, Vertex> GetVerticesByProperty(this GraphTraversalSource source, string label,
                                                                           List<KeyValuePair<string, object>> whereConditions)
        {
            if (string.IsNullOrEmpty(label) || whereConditions == null || !whereConditions.Any())
            {
                return source.V().HasLabel(label);
            }

            GraphTraversal<Vertex, Vertex> traversal = source.V().HasLabel(label);
            for (int i = 0; i < whereConditions.Count; i++)
            {
                string name = whereConditions[i].Key;
                object value = whereConditions[i].Value;

                if (!string.IsNullOrEmpty(name) && !IsNullPropertyValue(value))
                {
                    traversal = traversal.Has(name, value);
                }
            }
            return traversal;
        }

        /// <summary>
        /// Get Vertices By Properties
        /// </summary>
        /// <param name="source"></param>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        public static GraphTraversal<Vertex, Vertex> GetVerticesByProperty(this GraphTraversalSource source,
                                                                           List<KeyValuePair<string, object>> whereConditions)
        {
            if (whereConditions == null || !whereConditions.Any())
            {
                return source.V();
            }

            GraphTraversal<Vertex, Vertex> traversal = source.V();
            for (int i = 0; i < whereConditions.Count; i++)
            {
                string name = whereConditions[i].Key;
                object value = whereConditions[i].Value;

                if (!string.IsNullOrEmpty(name) && !IsNullPropertyValue(value))
                {
                    traversal = traversal.Has(name, value);
                }
            }
            return traversal;
        }


        #endregion


        #region Add Vertex

        /// <summary>
        /// Add Vertex (Graph.AddVertex())
        /// </summary>
        /// <param name="source"></param>
        /// <param name="label"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static GraphTraversal<Vertex, Vertex> AddVertex(this GraphTraversalSource source,
                                                               string label, Dictionary<string, object> properties)
        {
            var traversal = source.AddV(label);
            foreach (var property in properties)
            {
                string propertyName = property.Key;
                object propertyValue = property.Value;
                traversal = traversal.SetProperty(propertyName, propertyValue);
            }
            return traversal;
        }
        /// <summary>
        /// Add Vertex (Edge.AddVertex())
        /// </summary> 
        /// <param name="edge"></param>
        /// <param name="label"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static GraphTraversal<S, Vertex> AddVertex<S, E>(this GraphTraversal<S, E> edge,
                                                               string label,
                                                               Dictionary<string, object> properties)
        {
            var traversal = edge.AddV(label);
            foreach (var property in properties)
            {
                string propertyName = property.Key;
                object propertyValue = property.Value;
                traversal = traversal.SetProperty(propertyName, propertyValue);
            }
            return traversal;
        }
        /// <summary>
        /// Add Vertex (Graph.AddVertex())
        /// </summary>
        /// <param name="source"></param>
        /// <param name="vertexLabel"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GraphTraversal<Vertex, Vertex> AddVertex(this GraphTraversalSource source,
                                                               string vertexLabel,
                                                               object data = null)
        {
            var traversal = source.AddV(vertexLabel);

            if (TryGetProperties(data, out Dictionary<string, object> properties))
            {
                foreach (var property in properties)
                {
                    string propertyName = property.Key;
                    object propertyValue = property.Value;
                    traversal = traversal.SetProperty(propertyName, propertyValue);
                }
            }
            return traversal;
        }
        /// <summary>
        /// Add Vertex (Edge.AddVertex())
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="vertexLabel"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GraphTraversal<S, Vertex> AddVertex<S, E>(this GraphTraversal<S, E> edge,
                                                               string vertexLabel,
                                                               object data = null)
        {
            var traversal = edge.AddV(vertexLabel);

            if (TryGetProperties(data, out Dictionary<string, object> properties))
            {
                foreach (var property in properties)
                {
                    string propertyName = property.Key;
                    object propertyValue = property.Value;
                    traversal = traversal.SetProperty(propertyName, propertyValue);
                }
            }
            return traversal;
        }


        #endregion


        #region Update Vertex

        /// <summary>
        /// Update Vertex
        /// </summary>
        /// <param name="source"></param>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static GraphTraversal<Vertex, Vertex> UpdateVertex(this GraphTraversalSource source,
                                                                  string label, string key, object value,
                                                                  Dictionary<string, object> properties)
        {
            var traversal = source.GetVerticesByProperty(label, key, value);

            foreach (var property in properties)
            {
                string propertyName = property.Key;
                object propertyValue = property.Value;
                traversal = traversal.SetProperty(propertyName, propertyValue);
            }
            return traversal;
        }
        /// <summary>
        /// Update Vertex
        /// </summary>
        /// <param name="source"></param>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GraphTraversal<Vertex, Vertex> UpdateVertex(this GraphTraversalSource source,
                                                                  string label, string key, object value,
                                                                  object data = null)
        {
            var traversal = source.GetVerticesByProperty(label, key, value);

            if (TryGetProperties(data, out Dictionary<string, object> properties))
            {
                foreach (var property in properties)
                {
                    string propertyName = property.Key;
                    object propertyValue = property.Value;
                    traversal = traversal.SetProperty(propertyName, propertyValue);
                }
            }
            return traversal;
        }

        /// <summary>
        /// Update Vertex
        /// </summary>
        /// <param name="source"></param>
        /// <param name="id"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static GraphTraversal<Vertex, Vertex> UpdateVertex(this GraphTraversalSource source,
                                                                  object id,
                                                                  Dictionary<string, object> properties)
        {
            var traversal = source.GetVertex(id);

            foreach (var property in properties)
            {
                string propertyName = property.Key;
                object propertyValue = property.Value;
                traversal = traversal.SetProperty(propertyName, propertyValue);
            }
            return traversal;
        }

        /// <summary>
        /// Update Vertex
        /// </summary>
        /// <param name="source"></param>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GraphTraversal<Vertex, Vertex> UpdateVertex(this GraphTraversalSource source,
                                                                  object id, object data = null)
        {
            var traversal = source.GetVertex(id);

            if (TryGetProperties(data, out Dictionary<string, object> properties))
            {
                foreach (var property in properties)
                {
                    string propertyName = property.Key;
                    object propertyValue = property.Value;
                    traversal = traversal.SetProperty(propertyName, propertyValue);
                }
            }
            return traversal;
        }

        #endregion


        #region Get Edge

        /// <summary>
        /// Get Edge By Id
        /// </summary>
        /// <param name="source"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static GraphTraversal<Edge, Edge> GetEdge(this GraphTraversalSource source, object id)
        {
            return source.E(id);
        }

        /// <summary>
        /// Get Edges By Label
        /// </summary>
        /// <param name="source"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static GraphTraversal<Edge, Edge> GetEdgesByLabel(this GraphTraversalSource source, string label)
        {
            if (string.IsNullOrEmpty(label))
            {
                return source.E();
            }

            return source.E().HasLabel(label);
        }

        /// <summary>
        /// Get Edge By Label & Property
        /// </summary>
        /// <param name="source"></param>
        /// <param name="label"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static GraphTraversal<Edge, Edge> GetEdgesByProperty(this GraphTraversalSource source, string label,
                                                                    string name, object value)
        {
            if (string.IsNullOrEmpty(name) || IsNullPropertyValue(value))
            {
                return source.E().HasLabel(label);
            }
            return source.E().Has(label, name, value);
        }

        /// <summary>
        /// Get Edge By Property
        /// </summary>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static GraphTraversal<Edge, Edge> GetEdgesByProperty(this GraphTraversalSource source,
                                                                    string name, object value)
        {
            if (string.IsNullOrEmpty(name) || IsNullPropertyValue(value))
            {
                return source.E();
            }
            return source.E().Has(name, value);
        }

        /// <summary>
        /// Get Edges By Label & Properties
        /// </summary>
        /// <param name="source"></param>
        /// <param name="label"></param>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        public static GraphTraversal<Edge, Edge> GetEdgesByProperty(this GraphTraversalSource source, string label,
                                                                    List<KeyValuePair<string, object>> whereConditions)
        {
            if (string.IsNullOrEmpty(label) || whereConditions == null || !whereConditions.Any())
            {
                return source.E().HasLabel(label);
            }

            GraphTraversal<Edge, Edge> traversal = source.E().HasLabel(label);
            for (int i = 0; i < whereConditions.Count; i++)
            {
                string name = whereConditions[i].Key;
                object value = whereConditions[i].Value;

                if (!string.IsNullOrEmpty(name) && !IsNullPropertyValue(value))
                {
                    traversal = traversal.Has(name, value);
                }
            }
            return traversal;
        }

        /// <summary>
        /// Get Edges By Properties
        /// </summary>
        /// <param name="source"></param>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        public static GraphTraversal<Edge, Edge> GetEdgesByProperty(this GraphTraversalSource source,
                                                                    List<KeyValuePair<string, object>> whereConditions)
        {
            if (whereConditions == null || !whereConditions.Any())
            {
                return source.E();
            }

            GraphTraversal<Edge, Edge> traversal = source.E();
            for (int i = 0; i < whereConditions.Count; i++)
            {
                string name = whereConditions[i].Key;
                object value = whereConditions[i].Value;

                if (!string.IsNullOrEmpty(name) && !IsNullPropertyValue(value))
                {
                    traversal = traversal.Has(name, value);
                }
            }
            return traversal;
        }

        #endregion


        #region Add Edge


        /// <summary>
        /// Add Edge (Graph.AddEdge())
        /// </summary>
        /// <param name="source"></param>
        /// <param name="edgeLabel"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static GraphTraversal<Edge, Edge> AddEdge(this GraphTraversalSource source,
                                                         string edgeLabel, Dictionary<string, object> properties)
        {
            var traversal = source.AddE(edgeLabel);

            foreach (var property in properties)
            {
                string propertyName = property.Key;
                object propertyValue = property.Value;
                traversal = traversal.SetProperty(propertyName, propertyValue);
            }
            return traversal;
        }

        /// <summary>
        /// Add Edge (Vertex.AddEdge())
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="edgeLabel"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static GraphTraversal<S, Edge> AddEdge<S, E>(this GraphTraversal<S, E> vertex,
                                                           string edgeLabel, Dictionary<string, object> properties)
        {
            var traversal = vertex.AddE(edgeLabel);

            foreach (var property in properties)
            {
                string propertyName = property.Key;
                object propertyValue = property.Value;
                traversal = traversal.SetProperty(propertyName, propertyValue);
            }
            return traversal;
        }

        /// <summary>
        /// Add Edge (Graph.AddEdge())
        /// </summary>
        /// <param name="source"></param>
        /// <param name="edgeLabel"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GraphTraversal<Edge, Edge> AddEdge(this GraphTraversalSource source,
                                                         string edgeLabel, object data = null)
        {
            var traversal = source.AddE(edgeLabel);

            if (TryGetProperties(data, out Dictionary<string, object> properties))
            {
                foreach (var property in properties)
                {
                    string propertyName = property.Key;
                    object propertyValue = property.Value;
                    traversal = traversal.SetProperty(propertyName, propertyValue);
                }
            }
            return traversal;
        }
        /// <summary>
        /// Add Edge (Vertex.AddEdge())
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="edgeLabel"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GraphTraversal<S, Edge> AddEdge<S, E>(this GraphTraversal<S, E> vertex,
                                                           string edgeLabel, object data = null)
        {
            var traversal = vertex.AddE(edgeLabel);

            if (TryGetProperties(data, out Dictionary<string, object> properties))
            {
                foreach (var property in properties)
                {
                    string propertyName = property.Key;
                    object propertyValue = property.Value;
                    traversal = traversal.SetProperty(propertyName, propertyValue);
                }
            }
            return traversal;
        }

        #endregion


        #region Add Edge To Vertex

        /// <summary>
        /// Add Edge To Vertex
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public static GraphTraversal<S, E> ToVertex<S, E>(this GraphTraversal<S, E> edge, ITraversal vertex)
        {
            var traversal = edge.To(vertex);
            return traversal;
        }

        /// <summary>
        /// Add Edge To Vertex
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public static GraphTraversal<S, E> ToVertex<S, E>(this GraphTraversal<S, E> edge, Vertex vertex)
        {
            var traversal = edge.To(vertex);
            return traversal;
        }

        /// <summary>
        /// Add Edge To Vertex
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static GraphTraversal<S, E> ToVertex<S, E>(this GraphTraversal<S, E> edge, string label, string key, object value)
        {
            var traversal = edge.To(__.V().Has(label, key, value));
            return traversal;
        }

        /// <summary>
        /// Add Edge To Vertex
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="edge"></param>
        /// <param name="label"></param>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        public static GraphTraversal<S, E> ToVertex<S, E>(this GraphTraversal<S, E> edge, string label,
                                                          List<KeyValuePair<string, object>> whereConditions)
        {
            var toTraversal = __.V().HasLabel(label);
            for (int i = 0; i < whereConditions.Count; i++)
            {
                string name = whereConditions[i].Key;
                object value = whereConditions[i].Value;

                if (!string.IsNullOrEmpty(name) && !IsNullPropertyValue(value))
                {
                    toTraversal = toTraversal.Has(name, value);
                }
            }
            var traversal = edge.To(toTraversal);
            return traversal;
        }

        /// <summary>
        /// Add Edge To Vertex
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static GraphTraversal<S, E> ToVertex<S, E>(this GraphTraversal<S, E> edge, object id)
        {
            var traversal = edge.To(__.V(id));
            return traversal;
        }

        /// <summary>
        /// Add Edge To Vertex
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="edge"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static GraphTraversal<S, E> ToVertex<S, E>(this GraphTraversal<S, E> edge,
                                                          Func<GraphTraversal<object, Vertex>, ITraversal> predicate)
        {
            if (predicate != null)
            {
                var traversal = edge.To(predicate(__.V()));
                return traversal;
            }
            return edge;
        }

        #endregion


        #region Add Edge From Vertex


        /// <summary>
        /// Add Edge From Vertex
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public static GraphTraversal<S, E> FromVertex<S, E>(this GraphTraversal<S, E> edge, ITraversal vertex)
        {
            var traversal = edge.From(vertex);
            return traversal;
        }

        /// <summary>
        /// Add Edge From Vertex
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public static GraphTraversal<S, E> FromVertex<S, E>(this GraphTraversal<S, E> edge, Vertex vertex)
        {
            var traversal = edge.From(vertex);
            return traversal;
        }

        /// <summary>
        /// Add Edge From Vertex
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static GraphTraversal<S, E> FromVertex<S, E>(this GraphTraversal<S, E> edge, string label, string key, object value)
        {
            var traversal = edge.From(__.V().Has(label, key, value));
            return traversal;
        }

        /// <summary>
        /// Add Edge From Vertex
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="edge"></param>
        /// <param name="label"></param>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        public static GraphTraversal<S, E> FromVertex<S, E>(this GraphTraversal<S, E> edge, string label,
                                                            List<KeyValuePair<string, object>> whereConditions)
        {
            var fromTraversal = __.V().HasLabel(label);
            for (int i = 0; i < whereConditions.Count; i++)
            {
                string name = whereConditions[i].Key;
                object value = whereConditions[i].Value;

                if (!string.IsNullOrEmpty(name) && !IsNullPropertyValue(value))
                {
                    fromTraversal = fromTraversal.Has(name, value);
                }
            }
            var traversal = edge.From(fromTraversal);
            return traversal;
        }


        /// <summary>
        /// Add Edge From Vertex
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static GraphTraversal<S, E> FromVertex<S, E>(this GraphTraversal<S, E> edge, object id)
        {
            var traversal = edge.From(__.V(id));
            return traversal;
        }

        /// <summary>
        /// Add Edge From Vertex
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="edge"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static GraphTraversal<S, E> FromVertex<S, E>(this GraphTraversal<S, E> edge,
                                                            Func<GraphTraversal<object, Vertex>, ITraversal> predicate)
        {
            if (predicate != null)
            {
                var traversal = edge.From(predicate(__.V()));
                return traversal;
            }
            return edge;
        }

        #endregion


        #region Update Edge

        /// <summary>
        /// Update Edge
        /// </summary>
        /// <param name="source"></param>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static void UpdateEdge(this GraphTraversalSource source,
                                      string label, string key, string value,
                                      List<KeyValuePair<string, object>> properties)
        {
            var traversal = source.GetEdgesByProperty(label, key, value);

            foreach (var property in properties)
            {
                string propertyName = property.Key;
                object propertyValue = property.Value;
                traversal = traversal.Property(propertyName, propertyValue);
            }
            traversal.Iterate();
        }

        /// <summary>
        /// Update Edge
        /// </summary>
        /// <param name="source"></param>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static void UpdateEdge(this GraphTraversalSource source,
                                      string label, string key, string value,
                                      object data = null)
        {
            var traversal = source.GetEdgesByProperty(label, key, value);

            if (TryGetProperties(data, out Dictionary<string, object> properties))
            {
                foreach (var property in properties)
                {
                    string propertyName = property.Key;
                    object propertyValue = property.Value;
                    traversal = traversal.Property(propertyName, propertyValue);
                }
            }
            traversal.Iterate();
        }

        #endregion


        #region Where

        /// <summary>
        /// Where
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="traversal"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static GraphTraversal<S, E> Where<S, E>(this GraphTraversal<S, E> traversal, Func<ITraversal> predicate)
        {
            if (predicate != null)
            {
                return traversal.Where(predicate());
            }
            return traversal;
        }

        #endregion


        #region Set Property

        /// <summary>
        /// Set Property
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="traversal"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static GraphTraversal<S, E> SetProperty<S, E>(this GraphTraversal<S, E> traversal,
                                                             string name, object value)
        {
            // 檢查 Property Name
            if (string.IsNullOrEmpty(name))
            {
                return traversal;
            }

            // 檢查 Property Value
            if (IsNullPropertyValue(value))
            {
                return traversal;
            }

            traversal = traversal.Property(name, value);
            return traversal;
        }

        /// <summary>
        /// Set Property
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="traversal"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="cardinality"></param>
        /// <returns></returns>
        public static GraphTraversal<S, E> SetProperty<S, E>(this GraphTraversal<S, E> traversal,
                                                             string name, object value,
                                                             PropertyCardinalityType cardinality)
        {
            // 檢查 Property Name
            if (string.IsNullOrEmpty(name))
            {
                return traversal;
            }

            // 檢查 Property Value
            if (IsNullPropertyValue(value))
            {
                return traversal;
            }

            switch (cardinality)
            {
                case PropertyCardinalityType.List:
                    traversal = traversal.Property(Cardinality.List, name, value);
                    break;

                case PropertyCardinalityType.Set:
                    traversal = traversal.Property(Cardinality.Set, name, value);
                    break;

                case PropertyCardinalityType.Signle:
                default:
                    traversal = traversal.Property(name, value);
                    break;
            }
            return traversal;
        }

        /// <summary>
        /// Push List/Set Property
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="traversal"></param>
        /// <param name="name"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static GraphTraversal<S, E> PushProperty<S, E, T>(this GraphTraversal<S, E> traversal,
                                                                 string name, IEnumerable<T> values)
        {
            // 檢查 Property Name
            if (string.IsNullOrEmpty(name))
            {
                return traversal;
            }

            // 檢查 Property Value
            if (IsNullPropertyValue(values))
            {
                return traversal;
            }

            if (values.GetType() == typeof(HashSet<T>))
            {
                // SET
                foreach (T value in values)
                {
                    traversal = traversal.Property(Cardinality.Set, name, value);
                }
            }
            else
            {
                // List
                foreach (T value in values)
                {
                    traversal = traversal.Property(Cardinality.List, name, value);
                }
            }
            return traversal;
        }

        /// <summary>
        /// 檢查 Property Value 是否為 Null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsNullPropertyValue(object value)
        {
            return (value == null || value is System.DBNull);
        }


        /// <summary>
        /// Get Properties
        /// </summary>
        /// <param name="data"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        private static bool TryGetProperties(object data, out Dictionary<string, object> properties)
        {
            try
            {
                if (data == null)
                {
                    properties = new Dictionary<string, object>();
                    return false;
                }

                properties = JObject.FromObject(data).ToObject<Dictionary<string, object>>();
                return (properties != null && properties.Any());
            }
            catch
            {
                properties = new Dictionary<string, object>();
                return false;
            }
        }

        #endregion
    }
}
