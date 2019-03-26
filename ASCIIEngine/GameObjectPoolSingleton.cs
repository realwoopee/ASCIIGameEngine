using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.Core
{
    public class GameObjectPoolSingleton
    {
        private static readonly Lazy<GameObjectPoolSingleton> _lazy =
            new Lazy<GameObjectPoolSingleton>(() => new GameObjectPoolSingleton());

        public static GameObjectPoolSingleton Instance { get => _lazy.Value; }

        private List<GameObject> _objects;

        public IReadOnlyList<GameObject> Objects => _objects;

        private GameObjectPoolSingleton()
        {
            _objects = new List<GameObject>();
        }

        internal void AddObject(GameObject gameObject)
        {
            if(gameObject.Tag != null)
            if (_objects.Find(o => o.Tag != null && o.Tag.Equals(gameObject.Tag)) != null)
            {
                throw new ArgumentException("There is already a gameObject with ID = " + gameObject.Tag);
            }

            _objects.Add(gameObject);
            gameObject.Start();
        }

        public GameObject GetObjectById(string id) =>
            _objects.FirstOrDefault(o => o.Tag != null && o.Tag.Equals(id));

        public IEnumerable<GameObject> GetObjectsAtPosition(Vector2D position)
        {
            return _objects
                .Where(o => o.Position == position);
        }

        internal void RemoveObject(string id)
        {
            var obj = Objects.FirstOrDefault(o => o.Tag.Equals(id));
            if (obj != null)
            {
                _objects.Remove(obj);
            }
        }
    }
}
