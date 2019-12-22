using Dijkstra;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Tests
{
    [TestFixture]
    public class PathPriortyQueueTest
    {
        [Test]
        public void ShouldAddRemoveItem()
        {
            var queue = new PathPriorityQueue();
            var item = new PathItem();
            queue.Enqueue(item);
            Assert.AreEqual(1, queue.Count);
            Assert.AreSame(queue.Pop(), item);
            Assert.AreEqual(0, queue.Count);            
        }

        [Test]
        public void ShouldAddRemoveTwoItemsOfEqualPriorty()
        {
            var queue = new PathPriorityQueue();
            var item = new PathItem();
            var item2 = new PathItem();
            queue.Enqueue(item);
            queue.Enqueue(item2);
            Assert.AreEqual(2, queue.Count);
            Assert.AreSame(queue.Pop(), item);
            Assert.AreSame(queue.Pop(), item2);
            Assert.AreEqual(0, queue.Count);   
        }

        [Test]
        public void ShouldAddRemoveTwoItemsWithDifferentPriorty()
        {
            var queue = new PathPriorityQueue();
            var item = new PathItem{TotalCost=1};
            var item2 = new PathItem{TotalCost=0};
            queue.Enqueue(item);
            queue.Enqueue(item2);
            Assert.AreSame(queue.Pop(), item2);
            Assert.AreSame(queue.Pop(), item);
        }
               
        [Test]
        public void ShouldAddRemoveFiveItemsWithDifferentPriortyMoveToRoot()
        {
            var queue = new PathPriorityQueue();
            queue.Enqueue(new PathItem { TotalCost = 500 });
            queue.Enqueue(new PathItem { TotalCost = 500 });
            queue.Enqueue(new PathItem { TotalCost = 500 });
            queue.Enqueue(new PathItem { TotalCost = 500 });
            queue.Enqueue(new PathItem { TotalCost = 500 });
            queue.Enqueue(new PathItem { TotalCost = 0 });
            queue.Enqueue(new PathItem { TotalCost = 500 });
            queue.Enqueue(new PathItem { TotalCost = 500 });

            Assert.AreEqual(0, queue.Pop().TotalCost);
        }


        [Test]
        public void ShouldAddRemoveFiveItemsWithDifferentPriortyMoveToBase()
        {
            var queue = new PathPriorityQueue();
            queue.Enqueue(new PathItem { TotalCost = 0 });
            queue.Enqueue(new PathItem { TotalCost = 0 });
            queue.Enqueue(new PathItem { TotalCost = 0 });
            var middleItem = new PathItem { TotalCost = 0 };
            queue.Enqueue(middleItem);
            queue.Enqueue(new PathItem { TotalCost = 0 });
            queue.Enqueue(new PathItem { TotalCost = 0 });
            queue.Enqueue(new PathItem { TotalCost = 0 });
            queue.Enqueue(new PathItem { TotalCost = 0 });

            middleItem.TotalCost = 500;

            while(queue.Count > 1)
                Assert.AreEqual(0, queue.Pop().TotalCost);

            Assert.AreEqual(500, queue.Pop().TotalCost);
        }

        [Test]
        public void ShouldUpdateToLowerPriorty()
        {
            var queue = new PathPriorityQueue();
            var item = new PathItem{TotalCost=1};
            var item2 = new PathItem{TotalCost=0};
            queue.Enqueue(item);
            queue.Enqueue(item2);
            
            Assert.AreSame(queue.Peek(), item2);

            item2.TotalCost = 1;

            Assert.AreSame(queue.Peek(), item2);

            item2.TotalCost = 2;
            
            Assert.AreSame(queue.Pop(), item);
            Assert.AreSame(queue.Pop(), item2);
        }

        [Test]
        public void ShouldBeAbleToUpdateCostAfterPop()
        {
            var queue = new PathPriorityQueue();
            var item = new PathItem();
            queue.Enqueue(item);
            item.TotalCost = 2;
            queue.Pop();
            item.TotalCost = 5;
        }

        [Test]
        public void ShouldAlwaysGetMin()
        {
            var queue = new PathPriorityQueue();
            var items = new List<PathItem>();
            var random = new Random();
            for(var i = 0; i<5000; i++)
            {
                var item = new PathItem { TotalCost = random.Next(0, 100000) };
                items.Add(item);
                queue.Enqueue(item);
            }

            for(var i = 0; i<50; i++)
            {
                var min = items.OrderBy(x => x.TotalCost).First();
                Assert.AreEqual(min.TotalCost, queue.Pop().TotalCost, $"Failed at index {i}.");
                items.Remove(min);
            }

            for(var i = 0; i<50; i++)
            {
                var item = items[i];
                item.TotalCost = random.Next(0,100000);
            }

            for(var i = 0; i<20; i++)
            {
                var item = new PathItem { TotalCost = random.Next(0, 100) };
                items.Add(item);
                queue.Enqueue(item);
            }

            for(var i = 0; i<4500; i++)
            {
                var min = items.OrderBy(x => x.TotalCost).First();
                Assert.AreEqual(min.TotalCost, queue.Pop().TotalCost, $"Failed at index {i}.");
                items.Remove(min);
            }

            for(var i = 0; i<20; i++)
            {
                var item = items[i];
                item.TotalCost = random.Next(0,1000);
            }

            for(var i = 0; i<20; i++)
            {
                var min = items.OrderBy(x => x.TotalCost).First();
                Assert.AreEqual(min.TotalCost, queue.Pop().TotalCost, $"Failed at index {i}.");
                items.Remove(min);
            }

            for(var i = 0; i<400; i++)
            {
                var item = items[i];
                item.TotalCost = random.Next(0,100000);
            }

            for(var i = 0; i<450; i++)
            {
                var min = items.OrderBy(x => x.TotalCost).First();
                Assert.AreEqual(min.TotalCost, queue.Pop().TotalCost, $"Failed at index {i}.");
                items.Remove(min);
            }

            Assert.AreEqual(0, queue.Count);
        }
    }
}