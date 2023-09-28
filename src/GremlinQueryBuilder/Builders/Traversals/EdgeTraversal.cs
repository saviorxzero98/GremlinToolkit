using GQL.QueryBuilders.Builders.Steps;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GQL.QueryBuilders.Builders.Traversals
{
    public class EdgeTraversal : IGraphTraversal
    {
        public delegate IGraphTraversal Edge(EdgeTraversal vertex);

        public EdgeTraversal(string id = null)
        {
            if (id != null)
            {
                _queryString.Append($".E({GetParam(id)})");
            }
            else
            {
                _queryString.Append(".E()");
            }
        }
        public EdgeTraversal(long id)
        {
            _queryString.Append($".E({GetParam(id)})");
        }
        internal EdgeTraversal(StringBuilder queryString, bool isSubTraversal)
        {
            _queryString = queryString;
            _isSubTraversal = isSubTraversal;
        }
        internal EdgeTraversal(StringBuilder queryString, string traversalSource, bool isSubTraversal)
        {
            _queryString = queryString;
            _isSubTraversal = isSubTraversal;

            if (!string.IsNullOrEmpty(traversalSource))
            {
                _traversalSource = traversalSource;
            }
        }

        internal static EdgeTraversal CreateTraversal()
        {
            return new EdgeTraversal(new StringBuilder(), true);
        }


        #region Connect

        /// <summary>
        /// From Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public EdgeTraversal FromVertex(string name)
        {
            _queryString.Append(EdgeStep.Step.FromVertex(name));
            return this;
        }
        /// <summary>
        /// From Vertex
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public EdgeTraversal FromVertex(VertexTraversal vertex)
        {
            _queryString.Append(EdgeStep.Step.FromVertex(vertex));
            return this;
        }
        /// <summary>
        /// From Vertex
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public EdgeTraversal FromVertex(VertexTraversal.Vertex vertex)
        {
            return FromVertex(vertex(new VertexTraversal()).AsVertexTraversal());
        }
        /// <summary>
        /// From Vertex
        /// </summary>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public EdgeTraversal FromVertex(SubTraversal traversal)
        {
            _queryString.Append(EdgeStep.Step.FromVertex(traversal));
            return this;
        }
        /// <summary>
        /// [Extend] From Vertex
        /// </summary>
        /// <param name="label"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public EdgeTraversal FromVertex(string label, string property, object value)
        {
            return FromVertex((v) => v.Has(label, property, JToken.FromObject(value)));
        }
        /// <summary>
        /// [Extend] From Vertex
        /// </summary>
        /// <param name="vertexId"></param>
        /// <returns></returns>
        public EdgeTraversal FromVertex(long vertexId)
        {
            return FromVertex(new VertexTraversal(vertexId));
        }

        /// <summary>
        /// To Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public EdgeTraversal ToVertex(string name)
        {
            _queryString.Append(EdgeStep.Step.ToVertex(name));
            return this;
        }
        /// <summary>
        /// To Vertex
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public EdgeTraversal ToVertex(VertexTraversal vertex)
        {
            _queryString.Append(EdgeStep.Step.ToVertex(vertex));
            return this;
        }
        /// <summary>
        /// To Vertex
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public EdgeTraversal ToVertex(VertexTraversal.Vertex vertex)
        {
            return ToVertex(vertex(new VertexTraversal()).AsVertexTraversal());
        }
        /// <summary>
        /// To Vertex
        /// </summary>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public EdgeTraversal ToVertex(SubTraversal traversal)
        {
            _queryString.Append(EdgeStep.Step.ToVertex(traversal));
            return this;
        }
        /// <summary>
        /// [Extend] To Vertex
        /// </summary>
        /// <param name="label"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public EdgeTraversal ToVertex(string label, string property, object value)
        {
            return ToVertex((v) => v.Has(label, property, JToken.FromObject(value)));
        }
        /// <summary>
        /// [Extend] To Vertex
        /// </summary>
        /// <param name="vertexId"></param>
        /// <returns></returns>
        public EdgeTraversal ToVertex(long vertexId)
        {
            return ToVertex(new VertexTraversal(vertexId));
        }

        #endregion


        #region Vertex

        /// <summary>
        /// Get Out Vertex
        /// </summary>
        /// <returns></returns>
        public VertexTraversal OutVertex()
        {
            _queryString.Append(VertexStep.Step.OutVertex());
            return AsVertexTraversal();
        }

        /// <summary>
        /// Get In Vertex
        /// </summary>
        /// <returns></returns>
        public VertexTraversal InVertex()
        {
            _queryString.Append(VertexStep.Step.InVertex());
            return AsVertexTraversal();
        }

        /// <summary>
        /// Get Both Vertices
        /// </summary>
        /// <returns></returns>
        public VertexTraversal BothVertex()
        {
            _queryString.Append(VertexStep.Step.BothVertex());
            return AsVertexTraversal();
        }

        /// <summary>
        /// Get Other Vertices
        /// </summary>
        /// <returns></returns>
        public VertexTraversal OtherVertex()
        {
            _queryString.Append(VertexStep.Step.OtherVertex());
            return AsVertexTraversal();
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
        /// <param name="data"></param>
        /// <returns></returns>
        public VertexTraversal AddVertex(string label, object data)
        {
            _queryString.Append(VertexStep.Step.AddVertex(label, data));
            return AsVertexTraversal();
        }

        #endregion


        #region Edge

        /// <summary>
        /// Add Edge
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public EdgeTraversal AddEdge(string label = null)
        {
            _queryString.Append("\n ");
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
            _queryString.Append("\n ");
            _queryString.Append(EdgeStep.Step.AddEdge(label, data));
            return AsEdgeTraversal();
        }

        #endregion


        #region Has

        /// <summary>
        /// Has Label
        /// </summary>
        /// <param name="label"></param>
        /// <param name="otherLabels"></param>
        /// <returns></returns>
        public EdgeTraversal HasLabel(string label, params string[] otherLabels)
        {
            _queryString.Append(QueryStep.Step.HasLabel(label, otherLabels));
            return this;
        }

        /// <summary>
        /// Has Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EdgeTraversal HasId(long id)
        {
            _queryString.Append(QueryStep.Step.HasId(id));
            return this;
        }

        /// <summary>
        /// Has
        /// </summary>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public EdgeTraversal Has(string label, string key, string value)
        {
            _queryString.Append(QueryStep.Step.Has(label, key, value));
            return this;
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public EdgeTraversal Has(string label, string key, bool value)
        {
            _queryString.Append(QueryStep.Step.Has(label, key, value));
            return this;
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public EdgeTraversal Has(string label, string key, long value)
        {
            _queryString.Append(QueryStep.Step.Has(label, key, value));
            return this;
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public EdgeTraversal Has(string label, string key, double value)
        {
            _queryString.Append(QueryStep.Step.Has(label, key, value));
            return this;
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public EdgeTraversal Has(string label, string key, JToken value)
        {
            _queryString.Append(QueryStep.Step.Has(label, key, value));
            return this;
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public EdgeTraversal Has(string label, string key, PredicateTraversal predicate)
        {
            _queryString.Append(QueryStep.Step.Has(label, key, predicate));
            return this;
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="label"></param>
        /// <param name="key"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public EdgeTraversal Has(string label, string key, TextPredicateTraversal predicate)
        {
            _queryString.Append(QueryStep.Step.Has(label, key, predicate));
            return this;
        }

        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public EdgeTraversal Has(string key, string value)
        {
            _queryString.Append(QueryStep.Step.Has(key, value));
            return this;
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public EdgeTraversal Has(string key, bool value)
        {
            _queryString.Append(QueryStep.Step.Has(key, value));
            return this;
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public EdgeTraversal Has(string key, long value)
        {
            _queryString.Append(QueryStep.Step.Has(key, value));
            return this;
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public EdgeTraversal Has(string key, double value)
        {
            _queryString.Append(QueryStep.Step.Has(key, value));
            return this;
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public EdgeTraversal Has(string key, PredicateTraversal predicate)
        {
            _queryString.Append(QueryStep.Step.Has(key, predicate));
            return this;
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public EdgeTraversal Has(string key, TextPredicateTraversal predicate)
        {
            _queryString.Append(QueryStep.Step.Has(key, predicate));
            return this;
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public EdgeTraversal Has(string key, SubTraversal traversal)
        {
            _queryString.Append(QueryStep.Step.Has(key, traversal));
            return this;
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public EdgeTraversal Has(string key, SubTraversal.Traversal traversal)
        {
            _queryString.Append(QueryStep.Step.Has(key, traversal));
            return this;
        }

        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public EdgeTraversal Has(string key)
        {
            _queryString.Append(QueryStep.Step.Has(key));
            return this;
        }
        /// <summary>
        /// Has Not
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public EdgeTraversal HasNot(string key)
        {
            _queryString.Append(QueryStep.Step.HasNot(key));
            return this;
        }

        #endregion


        #region Get Value

        /// <summary>
        /// Get Id
        /// </summary>
        /// <returns></returns>
        public ValuesTraversal Id()
        {
            _queryString.Append(QueryStep.Step.Id());
            return AsValuesTraversal();
        }

        /// <summary>
        /// Get Label
        /// </summary>
        /// <returns></returns>
        public ValuesTraversal Label()
        {
            _queryString.Append(QueryStep.Step.Label());
            return AsValuesTraversal();
        }

        /// <summary>
        /// Get Values
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public ValuesTraversal Values(params string[] properties)
        {
            _queryString.Append(QueryStep.Step.Values(properties));
            return AsValuesTraversal();
        }

        /// <summary>
        /// Get Values (Mapping)
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public ValuesTraversal ValueMap(params string[] properties)
        {
            _queryString.Append(QueryStep.Step.ValueMap(properties));
            return AsValuesTraversal();
        }

        /// <summary>
        /// Get Values (Element Mapping)
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public ValuesTraversal ElementMap(params string[] properties)
        {
            _queryString.Append(QueryStep.Step.ElementMap(properties));
            return AsValuesTraversal();
        }

        #endregion


        #region Linq

        /// <summary>
        /// Count
        /// </summary>
        /// <returns></returns>
        public ValuesTraversal Count()
        {
            _queryString.Append(LinqStep.Step.Count());
            return AsValuesTraversal();
        }
        /// <summary>
        /// Count
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public ValuesTraversal Count(QueryConstant.Scope scope)
        {
            _queryString.Append(LinqStep.Step.Count(scope));
            return AsValuesTraversal();
        }


        /// <summary>
        /// Limit
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public EdgeTraversal Limit(int count)
        {
            _queryString.Append(LinqStep.Step.Limit(count));
            return this;
        }
        /// <summary>
        /// Limit
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EdgeTraversal Limit(QueryConstant.Scope scope, int count)
        {
            _queryString.Append(LinqStep.Step.Limit(scope, count));
            return this;
        }


        /// <summary>
        /// Tail
        /// </summary>
        /// <returns></returns>
        public EdgeTraversal Tail()
        {
            _queryString.Append(LinqStep.Step.Tail());
            return this;
        }
        /// <summary>
        /// Tail
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public EdgeTraversal Tail(int count)
        {
            _queryString.Append(LinqStep.Step.Tail(count));
            return this;
        }
        /// <summary>
        /// Tail
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EdgeTraversal Tail(QueryConstant.Scope scope, int count)
        {
            _queryString.Append(LinqStep.Step.Tail(scope, count));
            return this;
        }


        /// <summary>
        /// Range
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public EdgeTraversal Range(int min, int max)
        {
            _queryString.Append(LinqStep.Step.Range(min, max));
            return this;
        }
        /// <summary>
        /// Range
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public EdgeTraversal Range(QueryConstant.Scope scope, int min, int max)
        {
            _queryString.Append(LinqStep.Step.Range(scope, min, max));
            return this;
        }


        /// <summary>
        /// Skip
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public EdgeTraversal Skip(int count)
        {
            _queryString.Append(LinqStep.Step.Skip(count));
            return this;
        }
        /// <summary>
        /// Skip
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EdgeTraversal Skip(QueryConstant.Scope scope, int count)
        {
            _queryString.Append(LinqStep.Step.Skip(scope, count));
            return this;
        }


        /// <summary>
        /// Sample
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public EdgeTraversal Sample(int count)
        {
            _queryString.Append(LinqStep.Step.Sample(count));
            return this;
        }
        /// <summary>
        /// Sample
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EdgeTraversal Sample(QueryConstant.Scope scope, int count)
        {
            _queryString.Append(LinqStep.Step.Sample(scope, count));
            return this;
        }
        /// <summary>
        /// Sample By
        /// </summary>
        /// <param name="count"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public EdgeTraversal SampleBy(int count, string property = null)
        {
            _queryString.Append(LinqStep.Step.SampleBy(count, property));
            return this;
        }
        /// <summary>
        /// Sample By
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="count"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public EdgeTraversal SampleBy(QueryConstant.Scope scope, int count, string property = null)
        {
            _queryString.Append(LinqStep.Step.SampleBy(scope, count, property));
            return this;
        }


        /// <summary>
        /// Deduplify
        /// </summary>
        /// <param name="labels"></param>
        /// <returns></returns>
        public EdgeTraversal Deduplify(params string[] labels)
        {
            _queryString.Append(LinqStep.Step.Deduplify(labels));
            return this;
        }
        /// <summary>
        /// Deduplify
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="labels"></param>
        /// <returns></returns>
        public EdgeTraversal Deduplify(QueryConstant.Scope scope, params string[] labels)
        {
            _queryString.Append(LinqStep.Step.Deduplify(scope, labels));
            return this;
        }
        /// <summary>
        /// Debuplify By
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public EdgeTraversal DebuplifyBy(string property = null)
        {
            _queryString.Append(LinqStep.Step.DebuplifyBy(property));
            return this;
        }

        /// <summary>
        /// Order By
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public EdgeTraversal OrderBy(QueryConstant.OrderBy order = QueryConstant.OrderBy.Default)
        {
            _queryString.Append(LinqStep.Step.OrderBy(order));
            return this;
        }
        /// <summary>
        /// Order By
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public EdgeTraversal OrderBy(QueryConstant.Scope scope, QueryConstant.OrderBy order = QueryConstant.OrderBy.Default)
        {
            _queryString.Append(LinqStep.Step.OrderBy(scope, order));
            return this;
        }
        /// <summary>
        /// Order By
        /// </summary>
        /// <param name="property"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public EdgeTraversal OrderBy(string property, QueryConstant.OrderBy order = QueryConstant.OrderBy.Default)
        {
            _queryString.Append(LinqStep.Step.OrderBy(property, order));
            return this;
        }
        /// <summary>
        /// Order By
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="property"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public EdgeTraversal OrderBy(QueryConstant.Scope scope, string property, QueryConstant.OrderBy order = QueryConstant.OrderBy.Default)
        {
            _queryString.Append(LinqStep.Step.OrderBy(scope, property, order));
            return this;
        }
        /// <summary>
        /// Order By
        /// </summary>
        /// <param name="firstOrder"></param>
        /// <param name="otherOrders"></param>
        /// <returns></returns>
        public EdgeTraversal OrderBy(OrderQuery firstOrder, params OrderQuery[] otherOrders)
        {
            _queryString.Append(LinqStep.Step.OrderBy(firstOrder, otherOrders));
            return this;
        }
        /// <summary>
        /// Order By
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="firstOrder"></param>
        /// <param name="otherOrders"></param>
        /// <returns></returns>
        public EdgeTraversal OrderBy(QueryConstant.Scope scope, OrderQuery firstOrder, params OrderQuery[] otherOrders)
        {
            _queryString.Append(LinqStep.Step.OrderBy(scope, firstOrder, otherOrders));
            return this;
        }
        /// <summary>
        /// Order By
        /// </summary>
        /// <param name="property"></param>
        /// <param name="otherProperties"></param>
        /// <returns></returns>
        public EdgeTraversal OrderBy(string property, params string[] otherProperties)
        {
            _queryString.Append(LinqStep.Step.OrderBy(property, otherProperties));
            return this;
        }
        /// <summary>
        /// Order By
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="property"></param>
        /// <param name="otherProperties"></param>
        /// <returns></returns>
        public EdgeTraversal OrderBy(QueryConstant.Scope scope, string property, params string[] otherProperties)
        {
            _queryString.Append(LinqStep.Step.OrderBy(scope, property, otherProperties));
            return this;
        }

        /// <summary>
        /// Group By
        /// </summary>
        /// <returns></returns>
        public EdgeTraversal GroupBy()
        {
            _queryString.Append(LinqStep.Step.GroupBy());
            return this;
        }
        /// <summary>
        /// Group By Property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public EdgeTraversal GroupBy(string property)
        {
            _queryString.Append(LinqStep.Step.GroupBy(property));
            return this;
        }
        /// <summary>
        /// Group By Count
        /// </summary>
        /// <returns></returns>
        public EdgeTraversal GroupByCount()
        {
            _queryString.Append(LinqStep.Step.GroupByCount());
            return this;
        }

        /// <summary>
        /// Group Count
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public EdgeTraversal GroupCount(string name)
        {
            _queryString.Append(LinqStep.Step.GroupCount(name));
            return this;
        }

        /// <summary>
        /// Where
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public EdgeTraversal Where(PredicateTraversal predicate)
        {
            _queryString.Append(LinqStep.Step.Where(predicate));
            return this;
        }
        /// <summary>
        /// Where
        /// </summary>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public EdgeTraversal Where(SubTraversal traversal)
        {
            _queryString.Append(LinqStep.Step.Where(traversal));
            return this;
        }
        /// <summary>
        /// Where
        /// </summary>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public EdgeTraversal Where(EdgeTraversal.Edge traversal)
        {
            var edge = CreateTraversal();
            _queryString.Append(LinqStep.Step.Where(traversal(edge).AsSubTraversal()));
            return this;
        }
        /// <summary>
        /// Where
        /// </summary>
        /// <param name="startKey"></param>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public EdgeTraversal Where(string startKey, SubTraversal traversal)
        {
            _queryString.Append(LinqStep.Step.Where(startKey, traversal));
            return this;
        }
        /// <summary>
        /// Where
        /// </summary>
        /// <param name="startKey"></param>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public EdgeTraversal Where(string startKey, EdgeTraversal.Edge traversal)
        {
            var edge = CreateTraversal();
            _queryString.Append(LinqStep.Step.Where(startKey, traversal(edge).AsSubTraversal()));
            return this;
        }

        #endregion


        #region Property

        /// <summary>
        /// Add/Set Property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public EdgeTraversal Property(string name, string value, PropertyCardinalityType type = PropertyCardinalityType.Signle)
        {
            _queryString.Append(PropertyStep.Step.Property(name, value, type));
            return this;
        }
        /// <summary>
        ///  Add/Set Property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public EdgeTraversal Property(string name, long value, PropertyCardinalityType type = PropertyCardinalityType.Signle)
        {
            _queryString.Append(PropertyStep.Step.Property(name, value, type));
            return this;
        }
        /// <summary>
        /// Add/Set Property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public EdgeTraversal Property(string name, double value, PropertyCardinalityType type = PropertyCardinalityType.Signle)
        {
            _queryString.Append(PropertyStep.Step.Property(name, value, type));
            return this;
        }
        /// <summary>
        /// Add/Set Property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public EdgeTraversal Property(string name, decimal value, PropertyCardinalityType type = PropertyCardinalityType.Signle)
        {
            _queryString.Append(PropertyStep.Step.Property(name, value, type));
            return this;
        }
        /// <summary>
        /// Add/Set Property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public EdgeTraversal Property(string name, bool value, PropertyCardinalityType type = PropertyCardinalityType.Signle)
        {
            _queryString.Append(PropertyStep.Step.Property(name, value, type));
            return this;
        }
        /// <summary>
        /// Add/Set Property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public EdgeTraversal Property(string name, JToken value, PropertyCardinalityType type = PropertyCardinalityType.Signle)
        {
            _queryString.Append(PropertyStep.Step.Property(name, value, type));
            return this;
        }
        /// <summary>
        /// [Extend] Add/Set Property From Object
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public EdgeTraversal Property(JObject data)
        {
            _queryString.Append(PropertyStep.Step.Property(data));
            return this;
        }
        /// <summary>
        /// [Extend] Add/Set Property From Dictionary
        /// </summary>
        /// <param name="properities"></param>
        /// <returns></returns>
        public EdgeTraversal Property(Dictionary<string, object> properities)
        {
            _queryString.Append(PropertyStep.Step.Property(properities));
            return this;
        }

        /// <summary>
        /// Get Properties
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public PropertyTraversal Properties(params string[] properties)
        {
            _queryString.Append(PropertyStep.Step.Properties(properties));
            return AsPropertyTraversal();
        }

        /// <summary>
        /// Get Properties (Mapping)
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public PropertyTraversal PropertyMap(params string[] properties)
        {
            _queryString.Append(PropertyStep.Step.PropertyMap(properties));
            return AsPropertyTraversal();
        }

        #endregion


        #region Other

        /// <summary>
        /// Drop Edge
        /// </summary>
        /// <returns></returns>
        public TerminalTraversal Drop()
        {
            _queryString.Append(EdgeStep.Step.Drop());
            return AsTerminalTraversal();
        }

        /// <summary>
        /// As
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public EdgeTraversal As(params string[] alias)
        {
            _queryString.Append(QueryStep.Step.As(alias));
            return this;
        }

        #endregion


        #region Logic

        /// <summary>
        /// Not Sub-Traversal
        /// </summary>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public EdgeTraversal Not(SubTraversal traversal)
        {
            _queryString.Append(LogicStep.Step.Not(traversal));
            return this;
        }
        /// <summary>
        /// Not Sub-Traversal
        /// </summary>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public EdgeTraversal Not(EdgeTraversal.Edge traversal)
        {
            var vertex = CreateTraversal();
            return Not(traversal(vertex).AsSubTraversal());
        }

        /// <summary>
        /// Not Sub-Traversal
        /// </summary>
        /// <param name="traversals"></param>
        /// <returns></returns>
        public EdgeTraversal And(params SubTraversal[] traversals)
        {
            if (traversals != null && traversals.Any())
            {
                _queryString.Append(LogicStep.Step.And(traversals));
            }
            return this;
        }
        /// <summary>
        /// Not Sub-Traversal
        /// </summary>
        /// <param name="traversals"></param>
        /// <returns></returns>
        public EdgeTraversal And(params EdgeTraversal.Edge[] traversals)
        {
            if (traversals != null && traversals.Any())
            {
                SubTraversal[] traversalQueryList = traversals.Select(t => t(CreateTraversal()).AsSubTraversal()).ToArray();
                return And(traversalQueryList);
            }
            return this;
        }

        /// <summary>
        /// Not Sub-Traversal
        /// </summary>
        /// <param name="traversals"></param>
        /// <returns></returns>
        public EdgeTraversal Or(params SubTraversal[] traversals)
        {
            if (traversals != null && traversals.Any())
            {
                _queryString.Append(LogicStep.Step.Or(traversals));
            }
            return this;
        }
        /// <summary>
        /// Not Sub-Traversal
        /// </summary>
        /// <param name="traversals"></param>
        /// <returns></returns>
        public EdgeTraversal Or(params EdgeTraversal.Edge[] traversals)
        {
            if (traversals != null && traversals.Any())
            {
                SubTraversal[] traversalQueryList = traversals.Select(t => t(CreateTraversal()).AsSubTraversal()).ToArray();
                return Or(traversalQueryList);
            }
            return this;
        }

        #endregion


        #region Terminal

        /// <summary>
        /// Next
        /// </summary>
        /// <returns></returns>
        public TerminalTraversal Next()
        {
            _queryString.Append(TerminalStep.Step.Next());
            return AsTerminalTraversal();
        }

        /// <summary>
        /// To List
        /// </summary>
        /// <returns></returns>
        public TerminalTraversal ToList()
        {
            _queryString.Append(TerminalStep.Step.ToList());
            return AsTerminalTraversal();
        }

        /// <summary>
        /// Iterate
        /// </summary>
        /// <returns></returns>
        public TerminalTraversal Iterate()
        {
            _queryString.Append(TerminalStep.Step.Iterate());
            return AsTerminalTraversal();
        }

        #endregion
    }
}
