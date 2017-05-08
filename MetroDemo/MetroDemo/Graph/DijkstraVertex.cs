using System;

namespace MetroDemo.Graph
{
    /// <summary>
    /// Vertex used in Dijkstra algorithm
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class DijkstraVertex<T> : IEquatable<DijkstraVertex<T>>
    {
        /// <summary>
        /// Value of vertex
        /// </summary>
        public T Value;

        /// <summary>
        /// Last vertex in minimumPath
        /// </summary>
        public DijkstraVertex<T> LastVertexInMinimumPath { get; set; }

        /// <summary>
        /// Vertex was visited
        /// </summary>
        public bool IsVisited { get; set; }

        /// <summary>
        /// Minimum path weight to vertex
        /// </summary>
        public double? MinWeight { get; set; }

        public DijkstraVertex(T value)
        {
            Value = value;
        }

        public void SetVisited()
        {
            IsVisited = true;
        }

        public bool Equals(DijkstraVertex<T> other)
        {
            return Value.Equals(other.Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
