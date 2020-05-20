using System;
using ASCIIEngine.Core.BasicClasses;
using FluentAssertions;
using Xunit;

namespace ASCIIEngine.UnitTests.BasicClasses
{
    public class Vector2DTests
    {
        [Fact]
        public void VectorLength_ShouldReturnCoordinatesSquareRoot()
        {
            var vector = new Vector2D(3, 4);
            vector.Length.Should().Be(5);
            vector.Length.Should().Be((int)Math.Sqrt(3 * 3 + 4 * 4));
            vector.Length.Should().Be((int) Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y));
        }
    }
}