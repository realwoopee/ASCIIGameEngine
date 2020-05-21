using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ASCIIEngine.Core.Components;
[assembly: InternalsVisibleTo("ASCIIEngine.UnitTests")]
namespace ASCIIEngine.Core.BasicClasses
{
    public class GameObject
    {
        public Vector2D Position { get; set; }

        public Material Material { get; set; }

        public int Layer { get; set; }

        public virtual bool HasCollider { get; set; }

        public virtual string Tag { get; set; }

        // At now we have only dots, so max is equivalent to position
        public Vector2D Size => Vector2D.Zero;

        private readonly Dictionary<Type, Component> _components = new Dictionary<Type, Component>();

        public virtual void OnCollision(IEnumerable<GameObject> collidedWith)
        {
        }

        public T GetComponent<T>() where T : Component
        {
            if (ContainsComponent<T>())
            {
                return (T) _components[typeof(T)];
            }

            return null;
        }

        public void AddComponent<T>(T component) where T : Component
        {
            _components.Add(typeof(T), component);
        }

        public bool ContainsComponent<T>()
        {
            return _components.ContainsKey(typeof(T));
        }

        internal void Initialize()
        {
            Start();
        }

        internal void Step()
        {
            foreach (var component in _components.Values)
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