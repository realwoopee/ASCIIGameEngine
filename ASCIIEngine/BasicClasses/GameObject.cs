using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ASCIIEngine.Core.Components;
[assembly: InternalsVisibleTo("ASCIIEngine.UnitTests")]
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

        public bool HasChanged { get; protected set; }

        public virtual bool HasCollider { get; set; }

        public virtual string Tag { get; set; }

        // At now we have only dots, so max is equivalent to position
        public Vector2D Size => Vector2D.Zero;

        public Dictionary<Type, Component> Components { get; } = new Dictionary<Type, Component>();

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
            foreach (var component in Components.Values)
            {
                component.Update();
            }

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