using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ASCIIEngine.BasicClasses;

namespace ASCIIEngine
{
    public class Core
    {

        private Camera _camera;
        private GameObjectPoolSingleton _objectsPool;

        public Core(Camera camera)
        {
            _camera = camera;
            _objectsPool = GameObjectPoolSingleton.Instance;
        }

        public void Initialize()
        {
            _camera.Objects = _objectsPool.Objects;
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

        public Material[,] Render(Material[,] buffer)
        {
            return _camera.Render(buffer);
        }

        public void SetPressedKey(ConsoleKey key)
        {
            Input.SetPressedKey(key);
        }

        public IEnumerable<GameObject> GetObjectsAtPosition(Vector2D position)
        {
            return _objectsPool.Objects
                .Where(o => o.Position == position);
        }

        private void CheckForCollisions(IEnumerable<GameObject> objects)
        {
            foreach(var obj in objects)
            {
                var list = GetObjectsAtPosition(obj.Position).ToList();
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