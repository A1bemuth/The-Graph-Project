using NUnit.Framework;

namespace GraphDataLayer
{
    [TestFixture]
    class GraphTests
    {
        [Test]
        public void SomeTest()
        {
            var graph = new GraphMock();
            graph.HasArrow(1, 5);
        }
    }
}
