using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ASCIIEngine.Core.BasicClasses;
using ASCIIEngine.Core.Components;

namespace ASCIIEngine.ExampleApp.Objects
{
    public class Entity : GameObject
    {
        public override bool HasCollider => true;

        private RigidBody2D _rigidBody;

        protected override void Start()
        {
            _rigidBody = AddComponent<RigidBody2D>();
            _rigidBody.Velocity = Vector2D.Right;
            
            Material = new Material('@', Color.Red);
        }
        
        public override void OnCollision(IEnumerable<GameObject> collidedWith)
        {
            if (collidedWith.FirstOrDefault(go => go.GetType() == typeof(Entity)) != null)
                return;
            
            if (_rigidBody.Velocity == Vector2D.Right)
            {
                _rigidBody.Velocity = Vector2D.Down;
                return;
            }
            
            if (_rigidBody.Velocity == Vector2D.Down)
            {
                _rigidBody.Velocity = Vector2D.Left;
                return;
            }
            
            if (_rigidBody.Velocity == Vector2D.Left)
            {
                _rigidBody.Velocity = Vector2D.Up;
                return;
            }
            
            if (_rigidBody.Velocity == Vector2D.Up)
            {
                _rigidBody.Velocity = Vector2D.Right;
            }
        }
    }
}