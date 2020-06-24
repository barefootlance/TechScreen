using System;
using Xunit;
using Library;
using FluentAssertions;

namespace Test
{
    public class IncrementAndDecrement
    {
        [Theory]
        [InlineData(0, 2, 0, 2)]
        [InlineData(0, 1, 0, 2)]
        [InlineData(0, 2, 0, 1)]
        [InlineData(0, 2, -42, 1)]
        public void IncrementAndDecrementTest(int a, int b, int x, int y)
        {
            Int64 expectedALoops = Math.Abs((Int64)a - (Int64)x); // avoid overflow
            Int64 expectedBLoops = Math.Abs((Int64)b - (Int64)y);
            Int64 expectedLoops = Math.Max(expectedALoops, expectedBLoops);
            Int64 loops = Questions.MakeNumbersMatchBetter(a, b, x, y);
            loops.Should().Be(expectedLoops);
        }
    }
}
