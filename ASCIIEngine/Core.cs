using System;
using System.Collections.Generic;
using System.Linq;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.Core
{
    public class Base
    {
        private readonly GameObjectPoolSingleton _objectsPool;

        public Base()
        {
            _objectsPool = GameObjectPoolSingleton.Instance;
        }

        public void Initialize()
        {
            foreach (var o in _objectsPool.Objects)
                o.Start();
        }

        public void DoStep()
        {
            var objects = _objectsPool.Objects;

            foreach (var o in objects)
            {
                o.HasChanged = false;
                o.Step();
            }

            ResolveCollisions(objects
                .Where(o => o.HasChanged));
        }

        public void SetPressedKey(ConsoleKey key)
        {
            Input.SetPressedKey(key);
        }

        private void ResolveCollisions(IEnumerable<GameObject> objects)
        {
            var checkedObjs = new List<GameObject>();
            var colliders = objects.ToList();
            foreach (var obj in colliders)
            {
                if (checkedObjs.Contains(obj))
                    continue;

                foreach (var other in colliders)
                {
                    if (CollisionHandler.IsCollisionDetected(obj, other))
                    {
                        obj.OnCollision(new[] { other });
                        other.OnCollision(new[] { obj });

                        checkedObjs.Add(obj);
                        checkedObjs.Add(other);
                    }
                }
            }
        }

        public void AddObject(GameObject gameObject) => _objectsPool.AddObject(gameObject);
        public GameObject GetObjectById(string id) => _objectsPool.GetObjectById(id);
        public void RemoveObject(string id) => _objectsPool.RemoveObject(id);
    }
}