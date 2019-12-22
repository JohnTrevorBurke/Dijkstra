using System;
using System.Collections.Generic;
using System.Linq;

namespace Dijkstra 
{
    public class PathPriorityQueue
    {
        private Dictionary<PathItem, int> _pathItemIndicies;
        private List<PathItem> _pathItems;

        public double Count => _pathItems.Count;

        public PathPriorityQueue()
        {
            _pathItemIndicies = new Dictionary<PathItem, int>();
            _pathItems = new List<PathItem>();
        }

        public PathPriorityQueue(int intialSize)
        {
            _pathItemIndicies = new Dictionary<PathItem, int>(intialSize);
            _pathItems = new List<PathItem>(intialSize);
        }

        public void Enqueue(PathItem item)
        {
             var insertIndex = _pathItems.Count;
            _pathItemIndicies.Add(item, insertIndex);     
            _pathItems.Insert(insertIndex, item);
            item.OnUpdateTotalCost((i)=>{
                if (!_pathItemIndicies.ContainsKey(i))
                    return;
                ReorderToTreeBase(i);
                ReorderToTreeRoot(i);
            });
            ReorderToTreeRoot(item);
        }

        public object Peek()
        {
            return _pathItems[0];
        }

        private void ReorderToTreeRoot(PathItem i)
        {
            var index = _pathItemIndicies[i];
            if(index == 0){
                return;
            }
            var parentIndex = (index - 1) / 2;
            var parentItem = _pathItems[parentIndex];
            if(i.CompareTo(parentItem)<0){
                SwapNodeIndicies(i, parentIndex);
                ReorderToTreeRoot(i);
                return;
            }
        }

        private void ReorderToTreeBase(PathItem i)
        {
            var index = _pathItemIndicies[i];
            var leftChildIndex = index * 2 + 1;
            var rightChildIndex = index * 2 + 2;
            if (_pathItems.Count -1 < leftChildIndex)
                return;
            var targetChildIndex = _pathItems.Count - 1 >= rightChildIndex && _pathItems[rightChildIndex].CompareTo(_pathItems[leftChildIndex]) < 0 ? rightChildIndex : leftChildIndex; 
            if(i.CompareTo(_pathItems[targetChildIndex])>0){
                SwapNodeIndicies(i, targetChildIndex);
                ReorderToTreeBase(i);
                return;
            }
        }

        private void SwapNodeIndicies(PathItem targetItem, int updatedIndex){
            var oldIndex = _pathItemIndicies[targetItem];
            var sourceItem = _pathItems[updatedIndex];
            _pathItemIndicies[sourceItem] = oldIndex;
            _pathItemIndicies[targetItem] = updatedIndex;
            _pathItems[oldIndex] = sourceItem;
            _pathItems[updatedIndex] = targetItem;            
        }

        public PathItem Pop(){
            if(!_pathItems.Any())
                throw new Exception($"Cannot remove item when queue is empty.");

            var highestPriorityItem = _pathItems[0];
            _pathItemIndicies.Remove(highestPriorityItem);
            var lastIndex = _pathItems.Count -1;
            var lastItem = _pathItems[lastIndex];
            _pathItems.RemoveAt(lastIndex);

            if(lastItem != highestPriorityItem)
            {
                _pathItemIndicies[lastItem] = 0;
                _pathItems[0] = lastItem;    
                ReorderToTreeBase(lastItem);
            }

            return highestPriorityItem;
        }
    }
}