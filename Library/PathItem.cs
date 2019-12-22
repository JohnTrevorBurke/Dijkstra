using System;
using System.Collections.Generic;

namespace Dijkstra
{
    public class PathItem : IComparable
    {
        public int TotalCost { 
            get
            {
                return _totalCost;
            } 
            set
            {
                _totalCost = value;
                _totalCostUpdateRegistrations.ForEach(r=>r(this));                
            }
        } 
        public Vertex Vertex {get; set; }
        public Edge OptimalEdge {get; set;}
        private List<Action<PathItem>> _totalCostUpdateRegistrations;
        private int _totalCost;
        public PathItem()
        {
            _totalCostUpdateRegistrations = new List<Action<PathItem>>(1);
        }

        public int CompareTo(object obj)
        {
            if(obj == null)
                throw new ArgumentNullException(nameof(obj));

            if(!(obj is PathItem))
                throw new ArgumentException($"{obj.GetType().ToString()} is an invalid type. Can only compare a PathItem to another PathItem");


            return TotalCost.CompareTo(((PathItem)obj).TotalCost);
        }

        internal void OnUpdateTotalCost(Action<PathItem> r)
        {
            _totalCostUpdateRegistrations.Add(r);
        }

        public override string ToString(){
            return OptimalEdge != null ? $"Path: ({OptimalEdge.ConnectsFrom.Name})-{OptimalEdge?.Cost}-({OptimalEdge?.ConnectsTo.Name})" : $"Point: {Vertex?.Name}";
        }
    }

}