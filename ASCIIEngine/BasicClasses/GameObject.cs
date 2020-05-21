using System;
using System.Collections.Generic;
using ASCIIEngine.Core.Components;

namespace ASCIIEngine.Core.BasicClasses
{
    public class GameObject
    {
        private Vector2D _position;

        public Vector2D Position
        {
            get => _position;
            set
            {
                _position = value;
                HasChanged = true;
            }
        }

        private Material _material;

        public Material Material
        {
            get => _material;
            set
            {
                _material = value;
                HasChanged = true;
            }
        }

        public int Layer { get; set; }

        public bool HasChanged { get; set; }

        public virtual bool HasCollider { get; set; }

        public virtual string Tag { get; set; }
        
        public Vector2D Min => Position;
        
        public Vector2D Max => Position;
        
        public Dictionary<Type, Component> Components { get; } = new Dictionary<Type, Component>();

        public bool IsStatic => !Components.ContainsKey(typeof(RigidBody2D));

        public virtual void OnCollision(IEnumerable<GameObject> collidedWith)
        {
        }

        internal void Initialize()
        {
            Start();
        }

        internal void Step()
        {
            HasChanged = false;
            Update();
        }
        
        /// <summary>
        /// Can be overriden with additional functional
        /// </summary>
        protected virtual void Start()
        {
        }
        
        /// <summary>
        /// Can be overriden with additional functional
        /// </summary>
        protected virtual void Update()
        {
        }
    }
}