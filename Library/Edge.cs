namespace Dijkstra
{
    public class Edge
    {
        public int Cost { get; set; }
        public Vertex ConnectsTo { get; set; }
        public Vertex ConnectsFrom { get; internal set; }
    }
}