using Dijkstra;
using NUnit.Framework;
using System.Linq;

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
            CollectionAssert.AreEqual(new []{"A", "B"}, shortestPathInfo.Steps.Select(s=>s.Vertex.Name));
            Assert.AreEqual(shortestPathInfo.TotalCost, 5);
        }

        [Test]
        public void ShouldGetPathBetweenFourNodes()
        {
            var graph = new Graph();
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");

            graph.AddEdge("A", "B", 5);
            graph.AddEdge("A", "C", 10);
            graph.AddEdge("A", "D", 20);
            graph.AddEdge("B", "D", 20);
            graph.AddEdge("B", "C", 2);
            graph.AddEdge("C", "D", 2);

            var shortestPathInfo = graph.CalculateShortestPath("A", "D");
            CollectionAssert.AreEqual(new []{"A", "B", "C", "D"}, shortestPathInfo.Steps.Select(s=>s.Vertex.Name));
            Assert.AreEqual(shortestPathInfo.TotalCost, 9);
            Assert.AreEqual("Best Path: (A)-5-(B)-2-(C)-2-(D). Total Cost: 9", shortestPathInfo.ToString());
        }

        [Test]
        public void ShouldGetPathBetweenLotsOfNodes()
        {
            var graph = new Graph();
            graph.AddVertex("T");
            graph.AddVertex("D");
            graph.AddVertex("B");
            graph.AddVertex("A");
            graph.AddVertex("G");
            graph.AddVertex("E");
            graph.AddVertex("F");
            graph.AddVertex("K");
            graph.AddVertex("L");
            graph.AddVertex("J");
            graph.AddVertex("H");
            graph.AddVertex("I");


            graph.AddEdge("T","B",5);
            graph.AddEdge("T","D",3);
            graph.AddEdge("D","A",6);
            graph.AddEdge("A","G",3);
            graph.AddEdge("A","F",9);
            graph.AddEdge("D","E",11);
            graph.AddEdge("E","F",10);
            graph.AddEdge("F","H",3);
            graph.AddEdge("F","K",8);
            graph.AddEdge("H","I",20);
            graph.AddEdge("I","J",2);
            graph.AddEdge("B","J",9);
            graph.AddEdge("K","L",4);
            graph.AddEdge("L","J",1);

            var shortestPathInfo = graph.CalculateShortestPath("G", "J");
            CollectionAssert.AreEqual(new []{"G", "A", "F", "K", "L", "J"}, shortestPathInfo.Steps.Select(s=>s.Vertex.Name));
            Assert.AreEqual(25, shortestPathInfo.TotalCost);
        }

        [Test]
        public void ShouldHandleNoRoute()
        {
            var graph = new Graph();
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");

            graph.AddEdge("A", "B", 5);
            graph.AddEdge("C", "B", 10, false);

            var shortestPath = graph.CalculateShortestPath("A", "C");
            CollectionAssert.AreEqual(new []{"A"}, shortestPath.Steps.Select(s=>s.Vertex.Name));
            Assert.AreEqual(null, shortestPath.TotalCost);
            Assert.AreEqual("Best Path: (A). Total Cost: Infinite", shortestPath.ToString());
        }
    }
}