using Dijkstra;
using NUnit.Framework;

namespace Library.Tests
{
    public class Tests
    {
        [Test]
        public void ShouldGetPathBetweenTwoNodes()
        {
            var graph = new Graph();
            graph.AddVertex("A");
            graph.AddVertex("B");

            graph.AddEdge("A", "B", 5);

            var shortestPathInfo = graph.CalculateShortestPath("A", "B");
        }
    }
}