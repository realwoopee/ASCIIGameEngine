using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.Core.Components
{
    public class RigidBody2D : Component
    {
        public Vector2D Velocity { get; set; }

        public RigidBody2D(GameObject parent) : base(parent)
        {
        }

        public RigidBody2D()
        {
            
        }

        internal override void Update()
        { 
            Parent.Position += Velocity;
        }

        internal void OnCollision(Vector2D freePosition)
        {
            Parent.Position = freePosition;
        }
        
        internal void OnCollision()
        {
            Parent.Position -= Velocity.Normalize();
        }
    }
}