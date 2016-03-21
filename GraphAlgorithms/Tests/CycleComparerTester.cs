using System;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Compatibility;

namespace GraphAlgorithms.Tests
{
    [TestFixture]
    public class CycleComparerTester
    {
        private readonly CycleComparer comparer = new CycleComparer();

        [Test]
        public void CompareNullReferencesTest()
        {
            int[] first = null;
            int[] second = null;

            var result = comparer.Equals(first, second);

            Assert.False(result);
        }

        [Test]
        public void CompareWithSelfTest()
        {
            int[] collection = {1, 2, 3};

            var result = comparer.Equals(collection, collection);

            Assert.True(result);
        }

        [Test]
        public void CompareCollectionDifferentLengthTest()
        {
            int[] first = {1, 2, 3};
            int[] second = {1, 2, 3, 4};

            var result = comparer.Equals(first, second);

            Assert.False(result);
        }

        [Test]
        public void CompareEqualCollectionTest()
        {
            int[] first = {1, 2, 3};
            int[] second = {1, 2, 3};

            var result = comparer.Equals(first, second);

            Assert.True(result);
        }

        [Test]
        public void CompareCollectionWithOffset()
        {
            int[] first = {1, 2, 3};
            int[] second = {2, 3, 1};

            var result = comparer.Equals(first, second);

            Assert.True(result);
        }

        [Test]
        public void SecondCollectionNotContainFirstStartIndexTest()
        {
            int[] first = {1, 2, 3};
            int[] second = {2, 3, 4};

            var result = comparer.Equals(first, second);

            Assert.False(result);
        }

        [Test]
        public void CyclesDifferinSecondVertexTest()
        {
            int[] first = {1, 2, 3};
            int[] second = {3, 1, 4};

            var result = comparer.Equals(first, second);

            Assert.False(result);
        }

        [Test]
        public void CompareEqualCollectionWithOffset()
        {
            int[] first = {1, 2, 3, 4, 5, 6};
            int[] second = {4, 5, 6, 1, 2, 3};

            var result = comparer.Equals(first, second);

            Assert.True(result);
        }

        [Test]
        public void CompareBigCollectionWithBadSituationTest()
        {
            var first = Enumerable.Repeat(2, 100000000).ToArray();
            first[99999999] = 1;
            var second = Enumerable.Repeat(2, 100000000).ToArray();
            second[0] = 1;
            var timer = new Stopwatch();

            timer.Start();
            var result = comparer.Equals(first, second);
            timer.Stop();

            Assert.True(result);
            Console.WriteLine(timer.Elapsed);
            //Быстро :)
        }
    }
}