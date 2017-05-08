namespace MetroDemo.Graph
{
    /// <summary>
    /// Edge used in Dijkstra algorithm
    /// </summary>
    class DijkstraEdge<T>
    {
        /// <summary>
        /// First vertex of edge 
        /// </summary>
        public DijkstraVertex<T> Vertex1 { get; private set; }

        /// <summary>
        /// Second vertex of edge 
        /// </summary>
        public DijkstraVertex<T> Vertex2 { get; private set; }

        /// <summary>
        /// Weight of edge
        /// </summary>
        public double Weight { get; private set; }

        public DijkstraEdge(DijkstraVertex<T> vertex1, DijkstraVertex<T> vertex2, double weight)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Weight = weight;
        }

        public DijkstraVertex<T> GetNeighborVertex(DijkstraVertex<T> vertex)
        {
            if (Vertex1.Equals(vertex))
                return Vertex2;
            else if (Vertex2.Equals(vertex))
                return Vertex1;

            return null;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}: {2}", Vertex1, Vertex2, Weight);
        }
    }
}
