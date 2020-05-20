using System.Linq;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.Core
{
    public static class CollisionHandler
    {
        public static bool CanMoveTo(Vector2D position)
        {
            return !GameObjectPoolSingleton.Instance.GetObjectsAtPosition(position).Any(o => o.HasCollider);
        }

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
    }
}