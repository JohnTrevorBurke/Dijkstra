using System;
using System.Collections.Generic;

namespace Dijkstra
{
    public class PathItem : IComparable
    {
        public int? TotalCost { 
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
        private int? _totalCost;
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

            var otherCost = ((PathItem)obj).TotalCost;
            if (TotalCost == null)
                return otherCost == null ? 0 : 1;
            if (otherCost == null)
                return -1;

            return TotalCost.Value.CompareTo(otherCost.Value);
        }

        internal void OnUpdateTotalCost(Action<PathItem> r)
        {
            _totalCostUpdateRegistrations.Add(r);
        }

        internal void DeleteOnUpdateTotalCost(Action<PathItem> r)
        {
            _totalCostUpdateRegistrations.Remove(r);
        }

        public override string ToString(){
            return OptimalEdge != null ? $"Path: ({OptimalEdge.ConnectsFrom.Name})-{OptimalEdge?.Cost}-({OptimalEdge?.ConnectsTo.Name})" : $"Point: {Vertex?.Name}";
        }
    }

}