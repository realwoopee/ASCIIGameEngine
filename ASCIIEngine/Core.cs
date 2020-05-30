using System;
using System.Linq;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.Core
{
    public class Base
    {
        private readonly GameObjectsListSingleton _objectsPool;

        public Base()
        {
            _objectsPool = GameObjectsListSingleton.Instance;
            _objectsPool.Initialize();
        }

        public void Initialize()
        {
            foreach (var o in _objectsPool.Objects)
                o.Initialize();
        }

        public void DoStep()
        {
            var objects = _objectsPool.Objects;

            foreach (var o in objects)
            {
                o.Step();
            }

            CollisionHandler.ResolveCollisions(objects.Where(o => o.HasCollider || o.HasTrigger));
        }

        public void SetPressedKey(ConsoleKey key)
        {
            Input.SetPressedKey(key);
        }

        public void AddObject(GameObject gameObject) => _objectsPool.AddObject(gameObject);

        public GameObject GetObjectById(string id) => _objectsPool.GetObjectById(id);

        public void RemoveObject(string id) => _objectsPool.RemoveObject(id);
    }
}