using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.Core.Components
{
    public class RigidBody2D : Component
    {
        public Vector2D Direction
        {
            get => _direction.Normalize;
            set => _direction = value;
        }
 
        private Vector2D _direction;
    }
}