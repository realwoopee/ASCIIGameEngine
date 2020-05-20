using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ASCIIEngine.Core;
using ASCIIEngine.Core.BasicClasses;
using FluentAssertions;
using Xunit;

namespace ASCIIEngine.UnitTests
{
    public class CollisionTests
    {
        [Fact]
        public void Collision_ShouldRegister()
        {
            var collided = false;

            var baseCore = new Base();

            baseCore.AddObject(new GameObject
            {
                Layer = 1,
                Tag = "test_object1",
                HasCollider = true,
                Position = new Vector2D(1, 1)
            });

            baseCore.AddObject(new TestObject
            {
                Layer = 1,
                Tag = "test_object2",
                HasCollider = true,
                Position = new Vector2D(1,1),
                FireOnCollision = () => collided = true
            });

            baseCore.DoStep();

            collided.Should().BeTrue();
        }

        [Fact]
        public void Collision_ShouldNotRegister()
        {
            var collided = false;

            var baseCore = new Base();

            baseCore.AddObject(new GameObject
            {
                Layer = 1,
                Tag = "test_object1",
                HasCollider = true,
                Position = new Vector2D(1, 1)
            });

            baseCore.AddObject(new TestObject
            {
                Layer = 1,
                Tag = "test_object2",
                HasCollider = true,
                Position = new Vector2D(1, 2),
                FireOnCollision = () => collided = true
            });

            baseCore.DoStep();

            collided.Should().BeFalse();
        }

        private class TestObject : GameObject
        {
            public Action FireOnCollision;

            public override void OnCollision(IEnumerable<GameObject> collidedWith) => FireOnCollision();

            public override void Step() => HasChanged = true;
        }
    }
}
