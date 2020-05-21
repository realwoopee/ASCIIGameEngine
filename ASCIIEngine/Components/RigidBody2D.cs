using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.Core.Components
{
    public class RigidBody2D : Component
    {
        public Vector2D Direction
        {
            get => _direction.Normalize();
            set => _direction = value;
        }

        public int Velocity { get; set; }
 
        private Vector2D _direction;
        private GameObject _parent;

        public RigidBody2D(GameObject parent)
        {
            _parent = parent;
        }

        internal override void Update()
        {
            _parent.Position += _direction * Velocity;
        }

        internal void OnCollision(Vector2D freePosition)
        {
            _parent.Position = freePosition;
        }
        
        internal void OnCollision()
        {
            _parent.Position -= Direction * Velocity;
        }
    }
}