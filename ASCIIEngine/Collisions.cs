using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.Core
{
    public static class Collisions
    {
        public static bool CanMoveTo(Vector2D position)
        {
            if(GameObjectPoolSingleton.Instance.GetObjectsAtPosition(position).Where(o => o.HasCollider).Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
