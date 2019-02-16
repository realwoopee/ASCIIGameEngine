using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ASCIIEngine.BasicClasses;

namespace ASCIIEngine
{
    public class GameObjectPoolSingleton
    {
        private static readonly Lazy<GameObjectPoolSingleton> _lazy =
            new Lazy<GameObjectPoolSingleton>(() => new GameObjectPoolSingleton());

        public static GameObjectPoolSingleton Instance { get => _lazy.Value; }

        internal List<GameObject> Objects;

        private GameObjectPoolSingleton()
        {
            Objects = new List<GameObject>();
        }

        internal void AddObject(GameObject gameObject)
        {
            if(gameObject.ID != null)
            if (Objects.Find(o => o.ID != null && o.ID.Equals(gameObject.ID)) != null)
            {
                throw new ArgumentException("There is already a gameObject with ID = " + gameObject.ID);
            }
            
            Objects.Add(gameObject);
            gameObject.Start();
        }

        public GameObject GetObjectById(string id) => 
            Objects.FirstOrDefault(o => o.ID != null && o.ID.Equals(id));

        internal void RemoveObject(string id)
        {
            var obj = Objects.FirstOrDefault(o => o.ID.Equals(id));
            if (obj != null)
            {
                Objects.Remove(obj);
            }
        }
    }
}
