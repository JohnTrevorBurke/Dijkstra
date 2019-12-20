using System.Collections;
using System.Collections.Generic;

namespace Dijkstra
{
    public class Vertex
    {
        public string Name { get; set; }
        public List<Edge> Edges {get; set; }

        public Vertex()
        {
            Edges = new List<Edge>();
        }
    }
}