using System.Drawing;
using ASCIIEngine.Core.BasicClasses;
using ASCIIEngine.Core.Components;

namespace ASCIIEngine.ExampleApp.Objects
{
    public class Hunter : GameObject
    {
        public override bool HasCollider => true;
        public GameObject Target { get; set; }

        private RigidBody2D _rigidBody;

        protected override void Start()
        {
            _rigidBody = AddComponent<RigidBody2D>();
            _rigidBody.Velocity = (Target.Position - Position).Normalize();
            
            Material = new Material('@', Color.Red);
        }

        protected override void Update()
        {
            _rigidBody.Velocity = (Target.Position - Position).Normalize();
        }
    }
}