namespace Dijkstra{
    public class PathItem{
        public int TotalCost { get; set; }
        public Vertex Vertex {get; set; }
        public Vertex OptimalParent { get; set; }
        public Edge OptimalEdge {get; set;}
    }

}