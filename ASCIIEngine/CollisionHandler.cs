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
            // Exit without intersection because a dividing axis is found
            if (obj.Max.X < other.Min.X || obj.Min.X > other.Max.X)
                return false;

            if (obj.Max.Y < other.Min.Y || obj.Min.Y > other.Max.Y)
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
                        if (!obj.IsStatic || !other.IsStatic)
                        {
                            obj.OnCollision(new[] {other});
                            other.OnCollision(new[] {obj});

                            if (!obj.IsStatic)
                            {
                                var rBody = (RigidBody2D) obj.Components[typeof(RigidBody2D)];

                                if (rBody.Direction == Vector2D.Zero)
                                {
                                    obj.Position -= Vector2D.Down;
                                }

                                obj.Position -= rBody.Direction * rBody.Velocity;
                            }

                            if (!other.IsStatic)
                            {
                                var rBody = (RigidBody2D) other.Components[typeof(RigidBody2D)];

                                if (rBody.Direction == Vector2D.Zero)
                                {
                                    other.Position -= Vector2D.Up;
                                }

                                other.Position -= rBody.Direction * rBody.Velocity;
                            }

                            ResolveCollisions(colliders);
                        }
                    }

                    checkedObjs.Add(obj);
                    checkedObjs.Add(other);
                }
            }
        }
    }
}