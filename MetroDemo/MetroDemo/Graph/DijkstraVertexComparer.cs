using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroDemo.Graph
{
    class DijkstraVertexComparer<T> : IEqualityComparer<DijkstraVertex<T>>
    {
        public bool Equals(DijkstraVertex<T> x, DijkstraVertex<T> y)
        {
            return x.Value.Equals(y.Value);
        }

        public int GetHashCode(DijkstraVertex<T> obj)
        {
            return obj.Value.GetHashCode();
        }
    }
}
