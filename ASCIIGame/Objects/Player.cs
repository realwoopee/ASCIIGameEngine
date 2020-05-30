using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ASCIIEngine.Core;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIGame.Objects
{
    public class Player : GameObject
    {
        public override bool HasCollider => true;

        private GameManager _gameManager;

        public override void OnCollision(IEnumerable<GameObject> collidedWith)
        {
            var collidedObjects = collidedWith.ToList();

            if (collidedObjects.Any(o => o is Bonus))
            {
                _gameManager.OnBonus();
            }

            if (collidedObjects.Any(o => o is Enemy))
            {
                _gameManager.OnEnemy();
            }
        }

        protected override void Start()
        {
            _gameManager = GameObjectsListSingleton.Instance.GetObjectById("gameManager") as GameManager;
            Material = new Material('@', Color.DarkCyan);
        }

        protected override void Update()
        {
            Position += Input.ActiveDirection;
        }
    }
}