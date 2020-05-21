using System.Collections.Generic;
using System.Linq;
using ASCIIEngine.Core.BasicClasses;
using ASCIIEngine.Core.Components;

namespace ASCIIEngine.Core
{
    public static class CollisionHandler
    {
        public static bool IsCollisionDetected(GameObject a, GameObject b)
        {
            // Exit without intersection because a dividing axis is found
            if (a.Max.X < b.Min.X || a.Min.X > b.Max.X)
                return false;

            if (a.Max.Y < b.Min.Y || a.Min.Y > b.Max.Y)
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
                    if(obj.Equals(other))
                        continue;

                    if (IsCollisionDetected(obj, other))
                    {
                        obj.OnCollision(new[] { other });
                        other.OnCollision(new[] { obj });

                        if (!obj.IsStatic)
                        {
                            var rBody = (RigidBody2D) obj.Components[typeof(RigidBody2D)];
                            obj.Position -= rBody.Direction * rBody.Velocity;
                        }

                        if (!other.IsStatic)
                        {
                            var rBody = (RigidBody2D) other.Components[typeof(RigidBody2D)];
                            other.Position -= rBody.Direction * rBody.Velocity;
                        }

                        checkedObjs.Add(obj);
                        checkedObjs.Add(other);
                    }
                }
            }
        }
    }
}