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

        public void AddEdge(string startVertexName, string endVertexName, int cost, bool biDirectional = true)
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

            if(biDirectional){
                _vertices[endVertexName].Edges.Add(new Edge
                {
                    ConnectsTo = _vertices[startVertexName], 
                    Cost = cost
                });
            }
        }

        public ShortestPathResult CalculateShortestPath(string initialVertexName, string destinationVertexName)
        {
            var allPaths = _vertices
                .ToDictionary(kv=>kv.Key, kv=>{
                    var v = kv.Value;
                    return new PathItem{Vertex = v, TotalCost = v.Name == initialVertexName ? 0 : int.MaxValue};
                });
            
            var unvisited = allPaths.Select(x=>x.Value).ToList();
            var destinationPath = allPaths[destinationVertexName];
            while(unvisited.Any()){
                var currentShortestPath = unvisited.OrderBy(v=>v.TotalCost).First();
                if(destinationPath.OptimalParent != null && currentShortestPath.TotalCost >= destinationPath.TotalCost)
                    break;

                unvisited.Remove(currentShortestPath);
                foreach(var edge in currentShortestPath.Vertex.Edges){
                    var childVertex = edge.ConnectsTo;
                    var computedCost = edge.Cost + currentShortestPath.TotalCost;
                    var childPathInfo = allPaths[childVertex.Name];
                    if(computedCost < childPathInfo.TotalCost){
                        childPathInfo.TotalCost = computedCost;
                        childPathInfo.OptimalParent = currentShortestPath.Vertex;
                        childPathInfo.OptimalEdge = edge;
                    }
                }
            }

            var result = new ShortestPathResult();
            var path = destinationPath;
            while(path.OptimalParent != null){
                result.Steps.AddFirst(path);
                path = allPaths[path.OptimalParent.Name];
            }
            result.Steps.AddFirst(path);

            return result;
        }
    }
}
