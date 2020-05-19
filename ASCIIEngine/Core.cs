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

            CheckForCollisions(objects
                .Where(o => o.HasChanged));
        }

        public void SetPressedKey(ConsoleKey key)
        {
            Input.SetPressedKey(key);
        }

        private void CheckForCollisions(IEnumerable<GameObject> objects)
        {
            var checkedObjs = new List<GameObject>();
            foreach (var obj in objects)
            {
                if (checkedObjs.Contains(obj)) continue;

                var collidedObjects = GameObjectPoolSingleton.Instance
                    .GetObjectsAtPosition(obj.Position)
                    .Where(o => o.HasCollider);

                if (collidedObjects.Count() > 1)
                {
                    foreach (var o in collidedObjects)
                        o.OnCollision(collidedObjects.Except(new[] {o}));
                }

                checkedObjs.AddRange(collidedObjects);
            }
        }

        public void AddObject(GameObject gameObject) => _objectsPool.AddObject(gameObject);
        public GameObject GetObjectById(string id) => _objectsPool.GetObjectById(id);
        public void RemoveObject(string id) => _objectsPool.RemoveObject(id);
    }
}