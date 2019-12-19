using System;
using System.Collections.Generic;

namespace Dijkstra
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph();
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");


            graph.AddEdge("A","B",5);
            graph.AddEdge("B","C",5);
            graph.AddEdge("A","C",15);
            graph.AddEdge("C","D",1);

            graph.RunDijkstra("A", "D");                
        }        
    }


}
