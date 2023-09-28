using GQL.QueryBuilders.Builders.Steps;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GQL.QueryBuilders.Builders.Traversals
{
    public class SubTraversal : IGraphTraversal
    {
        public delegate SubTraversal Traversal(SubTraversal traversal);

        public override string ToString()
        {
            return _queryString.ToString().TrimStart('g').TrimStart('.');
        }

        public SubTraversal()
        {
            _queryString = new StringBuilder();
        }
        internal SubTraversal(StringBuilder queryString)
        {
            _queryString = queryString;
            _isSubTraversal = true;
        }

        internal static SubTraversal CreateTraversal()
        {
            return new SubTraversal();
        }


        #region Has

        /// <summary>
        /// Has Label
        /// </summary>
        /// <param name="label"></param>
        /// <param name="otherLabels"></param>
        /// <returns></returns>
        public SubTraversal HasLabel(string label, params string[] otherLabels)
        {
            _queryString.Append(QueryStep.Step.HasLabel(label, otherLabels));
            return this;
        }

        /// <summary>
        /// Has Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SubTraversal HasId(string id)
        {
            _queryString.Append(QueryStep.Step.HasId(id));

            return this;
        }
        /// <summary>
        /// Has Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SubTraversal HasId(long id)
        {
            _queryString.Append(QueryStep.Step.HasId(id));
            return this;
        }

        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public SubTraversal Has(string key, string value)
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
        public SubTraversal Has(string key, long value)
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
        public SubTraversal Has(string key, double value)
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
        public SubTraversal Has(string key, bool value)
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
        public SubTraversal Has(string key, JToken value)
        {
            switch (value.Type)
            {
                case JTokenType.Integer:
                    return Has(key, value.ToObject<long>());

                case JTokenType.Float:
                    return Has(key, value.ToObject<double>());

                case JTokenType.Boolean:
                    return Has(key, value.ToObject<bool>());

                case JTokenType.String:
                default:
                    return Has(key, value.ToString());
            }
        }
        /// <summary>
        /// Has
        /// </summary>
        /// <param name="key"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public SubTraversal Has(string key, PredicateTraversal predicate)
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
        public SubTraversal Has(string key, TextPredicateTraversal predicate)
        {
            _queryString.Append(QueryStep.Step.Has(key, predicate));
            return this;
        }

        #endregion


        #region Traveral

        /// <summary>
        /// Get In Vertex
        /// </summary>
        /// <returns></returns>
        public SubTraversal InVertex()
        {
            _queryString.Append(VertexStep.Step.InVertex());
            return this;
        }

        /// <summary>
        /// Other Vertex
        /// </summary>
        /// <returns></returns>
        public SubTraversal OutVertex()
        {
            _queryString.Append(VertexStep.Step.OutVertex());
            return this;
        }

        /// <summary>
        /// Get Both Vertices
        /// </summary>
        /// <returns></returns>
        public SubTraversal BothVertex()
        {
            _queryString.Append(VertexStep.Step.BothVertex());
            return this;
        }

        /// <summary>
        /// Get Other Vertex
        /// </summary>
        /// <returns></returns>
        public SubTraversal OtherVertex()
        {
            _queryString.Append(VertexStep.Step.OtherVertex());
            return this;
        }


        /// <summary>
        /// Get Out Edge
        /// </summary>
        /// <returns></returns>
        public SubTraversal OutEdge()
        {
            _queryString.Append(EdgeStep.Step.OutEdge());
            return this;
        }

        /// <summary>
        /// Get In Edges
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public SubTraversal InEdge(string label = null)
        {
            _queryString.Append(EdgeStep.Step.InEdge(label));
            return this;
        }

        /// <summary>
        /// Get Both Edges
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public SubTraversal BothEdge(params string[] labels)
        {
            _queryString.Append(EdgeStep.Step.BothEdge(labels));
            return this;
        }

        /// <summary>
        /// Get Out Edge
        /// </summary>
        /// <returns></returns>
        public SubTraversal Out()
        {
            _queryString.Append(EdgeStep.Step.Out());
            return this;
        }

        /// <summary>
        /// Get In Edges
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public SubTraversal In(string label = null)
        {
            _queryString.Append(EdgeStep.Step.In(label));
            return this;
        }

        /// <summary>
        /// Get Simple Path
        /// </summary>
        /// <returns></returns>
        public SubTraversal SimplePath()
        {
            _queryString.Append(TraversalStep.Step.SimplePath());
            return this;
        }

        #endregion


        #region Logic

        /// <summary>
        /// Not Sub-Traversal
        /// </summary>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public SubTraversal Not(SubTraversal traversal)
        {
            _queryString.Append(LogicStep.Step.Not(traversal));
            return this;
        }
        /// <summary>
        /// Not Sub-Traversal
        /// </summary>
        /// <param name="traversal"></param>
        /// <returns></returns>
        public SubTraversal Not(SubTraversal.Traversal traversal)
        {
            var vertex = CreateTraversal();
            return Not(traversal(vertex));
        }

        /// <summary>
        /// Not Sub-Traversal
        /// </summary>
        /// <param name="traversals"></param>
        /// <returns></returns>
        public SubTraversal And(params SubTraversal[] traversals)
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
        public SubTraversal And(params SubTraversal.Traversal[] traversals)
        {
            if (traversals != null && traversals.Any())
            {
                SubTraversal[] traversalQueryList = traversals.Select(t => t(CreateTraversal())).ToArray();
                return And(traversalQueryList);
            }
            return this;
        }

        /// <summary>
        /// Not Sub-Traversal
        /// </summary>
        /// <param name="traversals"></param>
        /// <returns></returns>
        public SubTraversal Or(params SubTraversal[] traversals)
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
        public SubTraversal Or(params SubTraversal.Traversal[] traversals)
        {
            if (traversals != null && traversals.Any())
            {
                SubTraversal[] traversalQueryList = traversals.Select(t => t(CreateTraversal())).ToArray();
                return Or(traversalQueryList);
            }
            return this;
        }

        #endregion
    }
}
