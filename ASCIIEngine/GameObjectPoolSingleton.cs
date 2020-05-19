using System;
using System.Collections.Generic;
using System.Linq;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.Core
{
    public class GameObjectPoolSingleton
    {
        private static readonly Lazy<GameObjectPoolSingleton> Lazy =
            new Lazy<GameObjectPoolSingleton>(() => new GameObjectPoolSingleton());

        public static GameObjectPoolSingleton Instance => Lazy.Value;

        private readonly List<GameObject> _objects;

        public IReadOnlyList<GameObject> Objects => _objects;

        private GameObjectPoolSingleton()
        {
            _objects = new List<GameObject>();
        }

        internal void AddObject(GameObject gameObject)
        {
            if (gameObject.Tag != null && _objects.Any(o => o.Tag != null && o.Tag.Equals(gameObject.Tag)))
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