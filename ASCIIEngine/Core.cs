using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.Core
{
    public class Base
    {
        private GameObjectPoolSingleton _objectsPool;

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
            //Logic
            foreach (var o in objects)
            {
                o.HasChanged = false;
                o.Step();
            }

            CheckForCollisions(objects
                .Where(o => o.HasCollider)
                .Where(o => o.HasChanged));
        }

        public void SetPressedKey(ConsoleKey key)
        {
            Input.SetPressedKey(key);
        }

        

        private void CheckForCollisions(IEnumerable<GameObject> objects)
        {
            foreach(var obj in objects)
            {
                var list = GameObjectPoolSingleton.Instance.GetObjectsAtPosition(obj.Position).Where(o => o.HasCollider).ToList();
                if(list.Count > 1)
                {
                    foreach (var o in list)
                        o.OnCollision(list);
                }
            }
        }

        public void AddObject(GameObject gameObject) => _objectsPool.AddObject(gameObject);
        public GameObject GetObjectById(string id) => _objectsPool.GetObjectById(id);
        public void RemoveObject(string id) => _objectsPool.RemoveObject(id);
    }
}