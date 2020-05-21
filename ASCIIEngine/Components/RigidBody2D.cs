using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.Core.Components
{
    public class RigidBody2D : Component
    {
        public Vector2D Velocity { get; set; }

        private GameObject _parent;

        public RigidBody2D(GameObject parent)
        {
            _parent = parent;
        }

        internal override void Update()
        {
            _parent.Position += Velocity;
        }

        internal void OnCollision(Vector2D freePosition)
        {
            _parent.Position = freePosition;
        }
        
        internal void OnCollision()
        {
            _parent.Position -= Velocity.Normalize();
        }
    }
}