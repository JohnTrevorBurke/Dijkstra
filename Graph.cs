using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dijkstra
{
    public class Graph
    {
        private Dictionary<string, Vertex> _vertices;

        public Graph()
        {
            _vertices = new Dictionary<string, Vertex>();
        }

        public void AddVertex(string v)
        {
            _vertices.Add(v, new Vertex{Name = v});
        }

        public void AddEdge(string startVertexName, string endVertexName, int cost)
        {
            if(!_vertices.ContainsKey(startVertexName))
                throw new ArgumentOutOfRangeException(nameof(startVertexName), $"Vertex {startVertexName} not found.");

            if(!_vertices.ContainsKey(endVertexName))
                throw new ArgumentOutOfRangeException(nameof(endVertexName), $"Vertex {startVertexName} not found.");

            _vertices[startVertexName].Edges.Add(new Edge
            {
                ConnectsTo = _vertices[endVertexName], 
                Cost = cost
            });
        }

        public void RunDijkstra(string v1, string v2)
        {
            _vertices[v1].ShortestPathCost = 0;
            var unvisited = _vertices.Select(x=>x.Value).ToList();
            while(unvisited.Any()){
                var minCostVertex = unvisited.OrderBy(v=>v.ShortestPathCost.GetValueOrDefault(int.MaxValue)).First();
                unvisited.Remove(minCostVertex);
                foreach(var edge in minCostVertex.Edges){
                    var childVertex = edge.ConnectsTo;
                    var computedCost = edge.Cost + minCostVertex.ShortestPathCost.Value;
                    if(computedCost < childVertex.ShortestPathCost.GetValueOrDefault(int.MaxValue)){
                        childVertex.ShortestPathCost = computedCost;
                        childVertex.ShortestPathParent = minCostVertex;
                    }
                }
            }

            Console.Out.WriteLine(_vertices[v2].ShortestPathCost);

            var names = new Stack<string>();
            var vertex = _vertices[v2];
            while(vertex.ShortestPathParent != null){
                names.Push(vertex.ShortestPathCost.ToString());
                names.Push(vertex.ShortestPathParent.Name);

                vertex = vertex.ShortestPathParent;
            }
            Console.Out.WriteLine(string.Join(' ', names));
        }
    }
}
