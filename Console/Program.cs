using System;
using System.Collections.Generic;

namespace Dijkstra
{
    class Program
    {
        static void Main(string[] args)
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

            Console.Out.WriteLine(graph.CalculateShortestPath("G", "J"));                
        }        
    }
}
