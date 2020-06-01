using ASCIIEngine.Core.BasicClasses;
using ASCIIEngine.Core.Components;
using FluentAssertions;
using Xunit;

namespace ASCIIEngine.UnitTests.Components
{
    public class RigidBody2DTests
    {
        [Fact]
        public void Update_ShouldChangePositionByVelocity()
        {
            var gameObject = new GameObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(0, 0)
            };

            var rBody = gameObject.AddComponent<RigidBody2D>();
            rBody.Velocity = new Vector2D(0, 5);
            
            gameObject.Step();
            gameObject.Position.Should().BeEquivalentTo(new Vector2D(0, 5));
            
            rBody.Update();
            gameObject.Position.Should().BeEquivalentTo(new Vector2D(0, 10));
            
            rBody.Velocity = new Vector2D(-10, -5);
            gameObject.Step();
            rBody.Update();
            
            gameObject.Position.Should().BeEquivalentTo(new Vector2D(-20, 0));
            
            rBody.Velocity = Vector2D.Up + Vector2D.Right;
            rBody.Update();
            
            gameObject.Position.Should().BeEquivalentTo(new Vector2D(-20, 0) + Vector2D.Up + Vector2D.Right);
        }

        [Fact]
        public void OnCollision_ShouldSubtractPositionCorrectly()
        {
            var gameObject = new GameObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(0, 0)
            };

            var rBody = gameObject.AddComponent<RigidBody2D>();
            rBody.Velocity = new Vector2D(0, 5);
            
            rBody.OnCollision();
            
            gameObject.Position.Should().BeEquivalentTo(new Vector2D(0, -1));
        }
        
        [Fact]
        public void OnCollisionWithFreePosition_ShouldMoveToFreePosition()
        {
            var gameObject = new GameObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(0, 0)
            };

            var rBody = gameObject.AddComponent<RigidBody2D>();
            rBody.Velocity = new Vector2D(0, 5);

            rBody.OnCollision(new Vector2D(7, 7));
            
            gameObject.Position.Should().BeEquivalentTo(new Vector2D(7, 7));
            
            rBody.OnCollision(new Vector2D(2, 9));
            
            gameObject.Position.Should().BeEquivalentTo(new Vector2D(2, 9));
        }
    }
}