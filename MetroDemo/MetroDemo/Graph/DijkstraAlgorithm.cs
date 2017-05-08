using System.Collections.Generic;

namespace MetroDemo.Graph
{
    /// <summary>
    /// Implementation of Dijkstra algorithm
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class DijkstraAlgorithm <T>
    {
        /// <summary>
        /// Vertexes using in algorithm
        /// </summary>
        private HashSet<DijkstraVertex<T>> Vertexes;

        /// <summary>
        /// Edges using in algorithm
        /// </summary>
        private HashSet<DijkstraEdge<T>> Edges;

        /// <summary>
        /// Is there Unvisited vertexes
        /// </summary>
        private bool IsUnvisitedVertexes
        {
            get
            {
                foreach (var vertex in Vertexes)
                {
                    if (!vertex.IsVisited)
                        return true;
                }

                return false;
            }
        }

        public DijkstraAlgorithm()
        {
            Vertexes = new HashSet<DijkstraVertex<T>>(new DijkstraVertexComparer<T>());
            Edges = new HashSet<DijkstraEdge<T>>();
        }

        /// <summary>
        /// Adds edge between vertex1 and vertex2 with weight
        /// </summary>
        public void AddEdge(T vertex1, T vertex2, double weight)
        {
            DijkstraVertex<T> dijkstraVertex1 = new DijkstraVertex<T>(vertex1);
            DijkstraVertex<T> dijkstraVertex2 = new DijkstraVertex<T>(vertex2);

            if (!Vertexes.Add(dijkstraVertex1))
                dijkstraVertex1 = GetDijkstraVertexByValue(vertex1);

            if (!Vertexes.Add(dijkstraVertex2))
                dijkstraVertex2 = GetDijkstraVertexByValue(vertex2);

            Edges.Add(new DijkstraEdge<T>(dijkstraVertex1, dijkstraVertex2, weight));
        }

        /// <summary>
        /// Return minimum path and its weight between fromVertex and toVertex
        /// </summary>
        public IEnumerable<T> GetMinimumPath(T fromVertex, T toVertex, out double weight)
        {
            DijkstraVertex<T> fromDijkstraVertex = GetDijkstraVertexByValue(fromVertex);
            DijkstraVertex<T> toDijkstraVertex = GetDijkstraVertexByValue(toVertex);

            fromDijkstraVertex.SetVisited();
            fromDijkstraVertex.MinWeight = 0;
            DijkstraVertex<T> currentVertex = fromDijkstraVertex;
            do
            {
                foreach (var edge in GetEdgesToUnvisitedNeighboringVertexes(currentVertex))
                {
                    ChangeMinWeight(currentVertex, edge.GetNeighborVertex(currentVertex), edge.Weight);
                }
                currentVertex.SetVisited();
                currentVertex = GetNextVertex();

                if (currentVertex == null)
                    break;
            }
            while (IsUnvisitedVertexes);

            weight = 0;
            if (!toDijkstraVertex.IsVisited)
                return null;

            weight = toDijkstraVertex.MinWeight.Value;

            List<T> minPath = new List<T>();
            minPath.Add(toVertex);
            while (toDijkstraVertex.LastVertexInMinimumPath != null)
            {
                minPath.Add(toDijkstraVertex.LastVertexInMinimumPath.Value);
                toDijkstraVertex = toDijkstraVertex.LastVertexInMinimumPath;
            }

            minPath.Reverse();
            return minPath;
        }

        private DijkstraVertex<T> GetDijkstraVertexByValue(T value)
        {
            foreach (var vertex in Vertexes)
            {
                if (vertex.Value.Equals(value))
                    return vertex;
            }

            return null;
        }
        private IEnumerable<DijkstraEdge<T>> GetEdgesToUnvisitedNeighboringVertexes(DijkstraVertex<T> vertex)
        {
            List<DijkstraEdge<T>> unvisitedNeighboringVertexes = new List<DijkstraEdge<T>>();
            foreach (var edge in Edges)
            {
                if ((edge.Vertex1.Equals(vertex) && !edge.Vertex2.IsVisited) || (edge.Vertex2.Equals(vertex) && !edge.Vertex1.IsVisited))
                    unvisitedNeighboringVertexes.Add(edge);
            }

            return unvisitedNeighboringVertexes;
        }
        private void ChangeMinWeight(DijkstraVertex<T> fromVertex, DijkstraVertex<T> toVertex, double pathWeight)
        {
            if (!toVertex.MinWeight.HasValue || (toVertex.MinWeight.HasValue && (fromVertex.MinWeight + pathWeight) < toVertex.MinWeight))
            {
                toVertex.MinWeight = fromVertex.MinWeight + pathWeight;
                toVertex.LastVertexInMinimumPath = fromVertex;
            }
        }
        private DijkstraVertex<T> GetNextVertex()
        {
            double? minWeight = null;
            DijkstraVertex<T> nextWertex = null;

            foreach (var vertex in Vertexes)
            {
                if ((!minWeight.HasValue && !vertex.IsVisited && vertex.MinWeight.HasValue) || 
                    (vertex.MinWeight.HasValue && !vertex.IsVisited && vertex.MinWeight < minWeight))
                {
                    nextWertex = vertex;
                    minWeight = nextWertex.MinWeight;
                }
            }

            return nextWertex;
        }
    }
}