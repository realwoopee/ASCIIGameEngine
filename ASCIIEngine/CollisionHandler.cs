using System.Collections.Generic;
using System.Linq;
using ASCIIEngine.Core.BasicClasses;
using ASCIIEngine.Core.Components;

namespace ASCIIEngine.Core
{
    public static class CollisionHandler
    {
        public static bool IsCollisionDetected(GameObject obj, GameObject other)
        {
            var max = obj.Size + obj.Position;
            var otherMax = other.Size + other.Position;

            // Exit without intersection because a dividing axis is found
            if (max.X < other.Position.X || obj.Position.X > otherMax.X)
                return false;

            if (max.Y < other.Position.Y || obj.Position.Y > otherMax.Y)
                return false;

            // No separation axis found, so at least one intersecting axis exists
            return true;
        }

        public static void ResolveCollisions(IEnumerable<GameObject> objects)
        {
            var checkedObjs = new List<GameObject>();
            var colliders = objects.ToList();
            foreach (var obj in colliders)
            {
                if (checkedObjs.Contains(obj))
                    continue;

                foreach (var other in colliders)
                {
                    if (obj.Equals(other))
                        continue;

                    if (IsCollisionDetected(obj, other))
                    {
                        // If both are static - ignore collision
                        if (obj.Components.ContainsKey(typeof(RigidBody2D)) || other.Components.ContainsKey(typeof(RigidBody2D)))
                        {
                            obj.OnCollision(new[] {other});
                            other.OnCollision(new[] {obj});

                            ProcessRigidBody(obj, Vector2D.Up);
                            ProcessRigidBody(other, Vector2D.Down);
                            ResolveCollisions(colliders);
                        }
                    }

                    checkedObjs.Add(obj);
                    checkedObjs.Add(other);
                }
            }
        }

        private static void ProcessRigidBody(GameObject obj, Vector2D direction)
        {
            if (obj.Components.ContainsKey(typeof(RigidBody2D)))
            {
                var rBody = (RigidBody2D) obj.Components[typeof(RigidBody2D)];
                if (rBody.Direction == Vector2D.Zero)
                {
                    rBody.OnCollision(direction);
                }
                else
                {
                    rBody.OnCollision();
                }
            }
        }
    }
}