using GQL.QueryBuilders.Builders.Steps;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GQL.QueryBuilders.Builders.Traversals
{
    public class VertexTraversal : IGraphTraversal
    {
        public delegate IGraphTraversal Vertex(VertexTraversal vertex);

        public VertexTraversal(string id = null)
        {
            if (!string.IsNullOrEmpty(id))
            {
                _queryString.Append($".V({GetParam(id)})");
            }
            else
            {
                _queryString.Append(".V()");
            }
        }
        public VertexTraversal(long id)
        {
            _queryString.Append($".V({GetParam(id)})");
        }

        internal VertexTraversal(StringBuilder queryString, bool isSubTraversal)
        {
            _queryString = queryString;
            _isSubTraversal = isSubTraversal;
        }
        internal VertexTraversal(StringBuilder queryString, string traversalSource, bool isSubTraversal)
        {
            _queryString = queryString;
            _isSubTraversal = isSubTraversal;

            if (!string.IsNullOrEmpty(traversalSource))
            {
                _traversalSource = traversalSource;
            }
        }

        /// <summary>
        /// Create Vertex Traversal
        /// </summary>
        /// <returns></returns>
        internal static VertexTraversal CreateTraversal()
        {
            return new VertexTraversal(new StringBuilder(), true);
        }


        #region Has

        /// <summary>
        /// Has Label
        /// </summary>
        /// <param name="label"></param>
        /// <param name="otherLabels"></param>
        /// <returns></returns>
        public VertexTraversal HasLabel(string label, params string[] otherLabels)
        {
            _queryString.Append(QueryStep.Step.HasLabel(label, otherLabels));
            return this;
        }

        /// <summary>
        /// Has Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VertexTraversal HasId(long id)
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
        public VertexTraversal Has(string label, string key, string value)
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
        public VertexTraversal Has(string label, string key, bool value)
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
        public VertexTraversal Has(string label, string key, long value)
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
        public VertexTraversal Has(string label, string key, double value)
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
        public VertexTraversal Has(string label, string key, JToken value)
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
        public VertexTraversal Has(string label, string key, PredicateTraversal predicate)
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
        public VertexTraversal Has(string label, string key, TextPredicateTraversal predicate)
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
        public VertexTraversal Has(string key, string value)
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
        public VertexTraversal Has(string key, bool value)
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
        public VertexTraversal Has(string key, long value)
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
        public VertexTraversal Has(string key, double value)
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
        public VertexTraversal Has(string key, PredicateTraversal predicate)
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
        public VertexTraversal Has(string key, TextPredicateTraversal predicate)
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
        public VertexTraversal Has(string key, SubTraversal traversal)
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
        public VertexTraversal Has(string key, SubTraversal.Traversal traversal)
        {
            _queryString.Append(QueryStep.Step.Has(key, traversal));
            return this;
        }

        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public VertexTraversal Has(string key)
        {
            _queryString.Append(QueryStep.Step.Has(key));
            return this;
        }

        /// <summary>
        /// Has Not
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public VertexTraversal HasNot(string key)
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
        public VertexTraversal Limit(int count)
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
        public VertexTraversal Limit(QueryConstant.Scope scope, int count)
        {
            _queryString.Append(LinqStep.Step.Limit(scope, count));
            return this;
        }

        /// <summary>
        /// Tail
        /// </summary>
        /// <returns></returns>
        public VertexTraversal Tail()
        {
            _queryString.Append(LinqStep.Step.Tail());
            return this;
        }
        /// <summary>
        /// Tail
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public VertexTraversal Tail(int count)
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
        public VertexTraversal Tail(QueryConstant.Scope scope, int count)
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
        public VertexTraversal Range(int min, int max)
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
        public VertexTraversal Range(QueryConstant.Scope scope, int min, int max)
        {
            _queryString.Append(LinqStep.Step.Range(scope, min, max));
            return this;
        }

        /// <summary>
        /// Skip
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public VertexTraversal Skip(int count)
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
        public VertexTraversal Skip(QueryConstant.Scope scope, int count)
        {
            _queryString.Append(LinqStep.Step.Skip(scope, count));
            return this;
        }

        /// <summary>
        /// Sample
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public VertexTraversal Sample(int count)
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
        public VertexTraversal Sample(QueryConstant.Scope scope, int count)
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
        public VertexTraversal SampleBy(int count, string property = null)
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
        public VertexTraversal SampleBy(QueryConstant.Scope scope, int count, string property = null)
        {
            _queryString.Append(LinqStep.Step.SampleBy(scope, count, property));
            return this;
        }


        /// <summary>
        /// Deduplify
        /// </summary>
        /// <param name="labels"></param>
        /// <returns></returns>
        public VertexTraversal Deduplify(params string[] labels)
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
        public VertexTraversal Deduplify(QueryConstant.Scope scope, params string[] labels)
        {
            _queryString.Append(LinqStep.Step.Deduplify(scope, labels));
            return this;
        }
        /// <summary>
        /// Debuplify By
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public VertexTraversal DebuplifyBy(string property = null)
        {
            _queryString.Append(LinqStep.Step.DebuplifyBy(property));
            return this;
        }


        /// <summary>
        /// Order By
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public VertexTraversal OrderBy(QueryConstant.OrderBy order = QueryConstant.OrderBy.Default)
        {
            _queryString.Append(LinqStep.Step.OrderBy(order));
            return this;
        }
        /// <summary>
        /// Order By
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public VertexTraversal OrderBy(QueryConstant.Scope scope, QueryConstant.OrderBy order = QueryConstant.OrderBy.Default)
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
        public VertexTraversal OrderBy(string property, QueryConstant.OrderBy order = QueryConstant.OrderBy.Default)
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
        public VertexTraversal OrderBy(QueryConstant.Scope scope, string property, QueryConstant.OrderBy order = QueryConstant.OrderBy.Default)
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
        public VertexTraversal OrderBy(OrderQuery firstOrder, params OrderQuery[] otherOrders)
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
        public VertexTraversal OrderBy(QueryConstant.Scope scope, OrderQuery firstOrder, params OrderQuery[] otherOrders)
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
        public VertexTraversal OrderBy(string property, params string[] otherProperties)
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
        public VertexTraversal OrderBy(QueryConstant.Scope scope, string property, params string[] otherProperties)
        {
            _queryString.Append(LinqStep.Step.OrderBy(scope, property, otherProperties));
            return this;
        }

        /// <summary>
        /// Group By
        /// </summary>
        /// <returns></returns>
        public VertexTraversal GroupBy()
        {
            _queryString.Append(LinqStep.Step.GroupBy());
            return this;
        }
        /// <summary>
        /// Group By Property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public VertexTraversal GroupBy(string property)
        {
            _queryString.Append(LinqStep.Step.GroupBy(property));
            return this;
        }
        /// <summary>
        /// Group By Count
        /// </summary>
        /// <returns></returns>
        public VertexTraversal GroupByCount()
        {
            _queryString.Append(LinqStep.Step.GroupByCount());
            return this;
        }

        /// <summary>
        /// Where
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public VertexTraversal Where(PredicateTraversal predicate)
        {
            _queryString.Append(LinqStep.Step.Where(predicate));
            return this;
        }
        /// <summary>
        /// Where
        /// </summary>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public VertexTraversal Where(SubTraversal traversal)
        {
            _queryString.Append(LinqStep.Step.Where(traversal));
            return this;
        }
        /// <summary>
        /// Where
        /// </summary>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public VertexTraversal Where(VertexTraversal.Vertex traversal)
        {
            var vertex = CreateTraversal();
            _queryString.Append(LinqStep.Step.Where(traversal(vertex).AsSubTraversal()));
            return this;
        }
        /// <summary>
        /// Where
        /// </summary>
        /// <param name="startKey"></param>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public VertexTraversal Where(string startKey, SubTraversal traversal)
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
        public VertexTraversal Where(string startKey, VertexTraversal.Vertex traversal)
        {
            var vertex = CreateTraversal();
            _queryString.Append(LinqStep.Step.Where(startKey, traversal(vertex).AsSubTraversal()));
            return this;
        }

        #endregion


        #region Vertex

        /// <summary>
        /// Add Vertex
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public VertexTraversal AddVertex(string label = null)
        {
            _queryString.Append("\n ");
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
            _queryString.Append("\n ");
            _queryString.Append(VertexStep.Step.AddVertex(label, data));
            return AsVertexTraversal();
        }


        #endregion


        #region Edge

        /// <summary>
        /// Get Out Edges
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public EdgeTraversal Out(string label = null)
        {
            _queryString.Append(EdgeStep.Step.Out(label));
            return AsEdgeTraversal();
        }

        /// <summary>
        /// Get In Edges
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public EdgeTraversal In(string label = null)
        {
            _queryString.Append(EdgeStep.Step.In(label));
            return AsEdgeTraversal();
        }

        /// <summary>
        /// Get Out Edges
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public EdgeTraversal OutEdge(string label = null)
        {
            _queryString.Append(EdgeStep.Step.OutEdge(label));
            return AsEdgeTraversal();
        }

        /// <summary>
        /// Get In Edges
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public EdgeTraversal InEdge(string label = null)
        {
            _queryString.Append(EdgeStep.Step.InEdge(label));
            return AsEdgeTraversal();
        }

        /// <summary>
        /// Get Both Edges
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public EdgeTraversal BothEdge(params string[] labels)
        {
            _queryString.Append(EdgeStep.Step.BothEdge(labels));
            return AsEdgeTraversal();
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


        #region Property

        /// <summary>
        /// Add/Set Property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public VertexTraversal Property(string name, string value, PropertyCardinalityType type = PropertyCardinalityType.Signle)
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
        public VertexTraversal Property(string name, int value, PropertyCardinalityType type = PropertyCardinalityType.Signle)
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
        public VertexTraversal Property(string name, double value, PropertyCardinalityType type = PropertyCardinalityType.Signle)
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
        public VertexTraversal Property(string name, bool value, PropertyCardinalityType type = PropertyCardinalityType.Signle)
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
        public VertexTraversal Property(string name, JToken value, PropertyCardinalityType type = PropertyCardinalityType.Signle)
        {
            _queryString.Append(PropertyStep.Step.Property(name, value, type));
            return this;
        }
        /// <summary>
        /// [Extend] Add/Set Property From Object
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public VertexTraversal Property(JObject data)
        {
            _queryString.Append(PropertyStep.Step.Property(data));
            return this;
        }
        /// <summary>
        /// [Extend] Add/Set Property From Dictionary
        /// </summary>
        /// <param name="properities"></param>
        /// <returns></returns>
        public VertexTraversal Property(Dictionary<string, object> properities)
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
        /// Drop Vertex
        /// </summary>
        /// <returns></returns>
        public TerminalTraversal Drop()
        {
            _queryString.Append(VertexStep.Step.Drop());
            return AsTerminalTraversal();
        }

        /// <summary>
        /// As Vertex
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public VertexTraversal As(params string[] alias)
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
        public VertexTraversal Not(SubTraversal traversal)
        {
            _queryString.Append(LogicStep.Step.Not(traversal));
            return this;
        }
        /// <summary>
        /// Not Sub-Traversal
        /// </summary>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public VertexTraversal Not(VertexTraversal.Vertex traversal)
        {
            var vertex = CreateTraversal();
            return Not(traversal(vertex).AsSubTraversal());
        }

        /// <summary>
        /// Not Sub-Traversal
        /// </summary>
        /// <param name="traversals"></param>
        /// <returns></returns>
        public VertexTraversal And(params SubTraversal[] traversals)
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
        public VertexTraversal And(params VertexTraversal.Vertex[] traversals)
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
        public VertexTraversal Or(params SubTraversal[] traversals)
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
        public VertexTraversal Or(params VertexTraversal.Vertex[] traversals)
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
