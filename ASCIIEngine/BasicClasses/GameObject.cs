using System;
using System.Collections.Generic;
using System.Text;

namespace ASCIIEngine.Core.BasicClasses
{
    public class GameObject
    {
        private Vector2D _position;
        public virtual Vector2D Position
        {
            get => _position;
            set
            {
                _position = value;
                HasChanged = true;
            }
        }

        private Material _material;
        public virtual Material Material
        {
            get => _material;
            set
            {
                _material = value;
                HasChanged = true;
            }
        }

        public virtual int Layer { get; set; }

        public virtual bool HasChanged { get; set; }

        public virtual bool HasCollider { get; set; }

        public virtual string Tag { get; set; }

        public virtual void Start() { }

        public virtual void Step() {}

        public virtual void OnCollision(IEnumerable<GameObject> collidedWith) { }
    }
}
