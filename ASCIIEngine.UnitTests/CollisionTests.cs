using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ASCIIEngine.Core;
using ASCIIEngine.Core.BasicClasses;
using ASCIIEngine.Core.Components;
using FluentAssertions;
using Xunit;

namespace ASCIIEngine.UnitTests
{
    public class CollisionTests
    {
        [Fact]
        public void Collision_ShouldNotRegister()
        {
            var collided = false;

            var baseCore = new Base();

            baseCore.AddObject(new GameObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(1, 1)
            });

            baseCore.AddObject(new TestObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(1, 1),
                FireOnCollision = () => collided = true
            });


            baseCore.AddObject(new TestObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(1, 2),
                FireOnCollision = () => collided = true
            });

            baseCore.DoStep();

            collided.Should().BeFalse();
        }

        [Fact]
        public void IsCollisionDetected_ShouldReturnTrue()
        {
            var firstObject = new GameObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(1, 1)
            };

            var secondObject = new TestObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(1, 1)
            };

            var collided = CollisionHandler.IsCollisionDetected(firstObject, secondObject);

            collided.Should().BeTrue();
        }

        [Fact]
        public void IsCollisionDetected_ShouldReturnFalse()
        {
            var firstObject = new GameObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(0, 0)
            };

            var secondObject = new TestObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(1, 1)
            };

            var collided = CollisionHandler.IsCollisionDetected(firstObject, secondObject);

            collided.Should().BeFalse();
        }

        [Fact]
        public void ResolveCollisionsWithTwoRigidBodies_ShouldMoveObjectsToCorrectPositions()
        {
            var firstObject = new GameObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(0, -1)
            };

            firstObject.Components.Add(typeof(RigidBody2D), new RigidBody2D(firstObject)
            {
                Direction = Vector2D.Right,
                Velocity = 1
            });

            var secondObject = new GameObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(0, 1)
            };

            secondObject.Components.Add(typeof(RigidBody2D), new RigidBody2D(secondObject)
            {
                Direction = Vector2D.Left,
                Velocity = 1
            });

            firstObject.Step();
            secondObject.Step();

            CollisionHandler.ResolveCollisions(new[] {firstObject, secondObject});

            firstObject.Position.Should().BeEquivalentTo(new Vector2D(0, -1));
            secondObject.Position.Should().BeEquivalentTo(new Vector2D(0, 1));
        }

        [Fact]
        public void ResolveCollisionsWithTwoRigidBodies_ShouldDoNothing()
        {
            var firstObject = new GameObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(0, 0)
            };

            firstObject.Components.Add(typeof(RigidBody2D), new RigidBody2D(firstObject)
            {
                Direction = Vector2D.Left,
                Velocity = 1
            });

            var secondObject = new GameObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(0, 1)
            };

            secondObject.Components.Add(typeof(RigidBody2D), new RigidBody2D(secondObject)
            {
                Direction = Vector2D.Right,
                Velocity = 1
            });

            firstObject.Step();
            secondObject.Step();

            CollisionHandler.ResolveCollisions(new[] {firstObject, secondObject});

            firstObject.Position.Should().BeEquivalentTo(new Vector2D(0, -1));
            secondObject.Position.Should().BeEquivalentTo(new Vector2D(0, 2));
        }

        [Fact]
        public void ResolveCollisionsWithRigidBodyAndStaticObject_ShouldMoveRigidBodyToCorrectPosition()
        {
            var firstObject = new GameObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(0, 0)
            };

            firstObject.Components.Add(typeof(RigidBody2D), new RigidBody2D(firstObject)
            {
                Direction = Vector2D.Right,
                Velocity = 1
            });

            var secondObject = new GameObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(0, 1)
            };

            firstObject.Step();
            secondObject.Step();

            CollisionHandler.ResolveCollisions(new[] {firstObject, secondObject});

            firstObject.Position.Should().BeEquivalentTo(new Vector2D(0, 0));
            secondObject.Position.Should().BeEquivalentTo(new Vector2D(0, 1));
        }

        [Fact]
        public void ResolveCollisionsWithRigidBodyAndStaticObject_ShouldDoNothing()
        {
            var firstObject = new GameObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(0, 0)
            };

            firstObject.Components.Add(typeof(RigidBody2D), new RigidBody2D(firstObject)
            {
                Direction = Vector2D.Down,
                Velocity = 1
            });

            var secondObject = new GameObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(0, 1)
            };

            firstObject.Step();
            secondObject.Step();

            CollisionHandler.ResolveCollisions(new[] { firstObject, secondObject });

            firstObject.Position.Should().BeEquivalentTo(new Vector2D(1, 0));
            secondObject.Position.Should().BeEquivalentTo(new Vector2D(0, 1));
        }

        [Fact]
        public void ResolveConflictedCollidersWithoutDirection_ShouldMoveCollidersToDifferentPositions()
        {
            var firstObject = new GameObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(0, 0)
            };

            firstObject.Components.Add(typeof(RigidBody2D), new RigidBody2D(firstObject)
            {
                Direction = Vector2D.Zero,
                Velocity = 0
            });

            var secondObject = new GameObject
            {
                Layer = 1,
                HasCollider = true,
                Position = new Vector2D(0, 0)
            };

            secondObject.Components.Add(typeof(RigidBody2D), new RigidBody2D(secondObject)
            {
                Direction = Vector2D.Zero,
                Velocity = 0
            });

            CollisionHandler.ResolveCollisions(new[] { firstObject, secondObject });

            firstObject.Position.Should().BeEquivalentTo(new Vector2D(-1, 0));
            secondObject.Position.Should().BeEquivalentTo(new Vector2D(1, 0));
        }

        private class TestObject : GameObject
        {
            public Action FireOnCollision;

            public override void OnCollision(IEnumerable<GameObject> collidedWith) => FireOnCollision();

            protected override void Update() => HasChanged = true;
        }
    }
}