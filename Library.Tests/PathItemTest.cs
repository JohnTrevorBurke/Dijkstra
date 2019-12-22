using Dijkstra;
using NUnit.Framework;

[TestFixture]
public class PathItemTest
{
    [TestCase(null, 0, ExpectedResult = 1)]
    [TestCase(100, 0, ExpectedResult = 1)]
    [TestCase(0, 1, ExpectedResult = -1)]
    [TestCase(0, 0, ExpectedResult = 0)]
    [TestCase(null, null, ExpectedResult = 0)]
    [TestCase(0, null, ExpectedResult = -1)]
    [TestCase(0, 100, ExpectedResult = -1)]
    public int ShouldCompare(int? a, int? b){
        return new PathItem(){TotalCost = a}.CompareTo(new PathItem{TotalCost = b});
    }
}