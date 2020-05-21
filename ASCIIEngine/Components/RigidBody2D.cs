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

        public int Velocity { get; set; } = 0;
 
        private Vector2D _direction;
    }
}