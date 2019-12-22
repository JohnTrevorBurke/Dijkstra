using System.Collections.Generic;
using System.Linq;

namespace Dijkstra
{
    public class ShortestPathResult
    {
        public LinkedList<PathItem> Steps { get; private set; }

        public ShortestPathResult()
        {
            Steps = new LinkedList<PathItem>();
        }

        public int? TotalCost => Steps.Skip(1).LastOrDefault()?.TotalCost;

        public override string ToString() => 
            "Best Path: " + 
            string.Join("-", Steps.Select(s => $"{((s.OptimalEdge?.Cost) != null ? s.OptimalEdge?.Cost + "-" : string.Empty)}({s.Vertex.Name})")) + $". Total Cost: {TotalCost?.ToString() ?? "Infinite"}";
    }
}