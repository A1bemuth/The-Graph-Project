using NUnit.Framework;

namespace Tests.DataLayerTests
{
    public static class TestExtensions
    {
        public static void AssertDimensions(this int[,] range, int expectedHeight, int expectedWidth)
        {
            Assert.That(range.GetLength(0), Is.EqualTo(expectedHeight));
            Assert.That(range.GetLength(1), Is.EqualTo(expectedWidth));
        }
    }
}
