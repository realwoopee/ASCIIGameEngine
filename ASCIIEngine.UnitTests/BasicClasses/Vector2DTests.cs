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

        [Fact]
        public void VectorNormalized_ShouldBeRight()
        {
            new Vector2D(0, 0).Normalize().Should().Be(new Vector2D(0, 0));

            new Vector2D(1, 0).Normalize().Should().Be(new Vector2D(1, 0));
            new Vector2D(0, 1).Normalize().Should().Be(new Vector2D(0, 1));
            new Vector2D(-1, 0).Normalize().Should().Be(new Vector2D(-1, 0));
            new Vector2D(0, -1).Normalize().Should().Be(new Vector2D(0, -1));

            new Vector2D(1, 1).Normalize().Should().Be(new Vector2D(1, 1));
            new Vector2D(-1, 1).Normalize().Should().Be(new Vector2D(-1, 1));
            new Vector2D(1, -1).Normalize().Should().Be(new Vector2D(1, -1));
            new Vector2D(-1, -1).Normalize().Should().Be(new Vector2D(-1, -1));

            new Vector2D(3, 1).Normalize().Should().Be(new Vector2D(1, 0));
            new Vector2D(-3, 1).Normalize().Should().Be(new Vector2D(-1, 0));
            new Vector2D(1, -3).Normalize().Should().Be(new Vector2D(0, -1));
            new Vector2D(-1, -3).Normalize().Should().Be(new Vector2D(0, -1));
            new Vector2D(-3, -1).Normalize().Should().Be(new Vector2D(-1, 0));
            new Vector2D(3, -1).Normalize().Should().Be(new Vector2D(1, 0));
            new Vector2D(-1, 3).Normalize().Should().Be(new Vector2D(0, 1));
            new Vector2D(1, 3).Normalize().Should().Be(new Vector2D(0, 1));

            new Vector2D(2, 3).Normalize().Should().Be(new Vector2D(1, 1));
            new Vector2D(-2, -3).Normalize().Should().Be(new Vector2D(-1, -1));
            new Vector2D(2, -3).Normalize().Should().Be(new Vector2D(1, -1));
            new Vector2D(-2, 3).Normalize().Should().Be(new Vector2D(-1, 1));

            new Vector2D(2, 4).Normalize().Should().Be(new Vector2D(0, 1));
            new Vector2D(-2, -4).Normalize().Should().Be(new Vector2D(-0, -1));
            new Vector2D(2, -4).Normalize().Should().Be(new Vector2D(0, -1));
            new Vector2D(-2, 4).Normalize().Should().Be(new Vector2D(-0, 1));
        }

        [Fact]
        public void VectorsSubtraction_ShouldReturnDirectionBetweenVectors()
        {
            var firstVector = new Vector2D(1, 2);
            var secondVector = new Vector2D(4, 3);

            var direction = secondVector - firstVector;
            direction.Should().BeEquivalentTo(new Vector2D(3, 1));
        }
    }
}