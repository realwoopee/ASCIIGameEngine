using System.Linq;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.Core
{
    public static class Collisions
    {
        public static bool CanMoveTo(Vector2D position)
        {
            return !GameObjectPoolSingleton.Instance.GetObjectsAtPosition(position).Any(o => o.HasCollider);
        }
    }
}