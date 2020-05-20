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

        private Vector2D _prevPos;
        private GameManager _gameManager;

        public override void OnCollision(IEnumerable<GameObject> collidedWith)
        {
            var collidedObjects = collidedWith.ToList();
            if (collidedObjects.Any(o => o is Stone))
            {
                Position = _prevPos;
            }

            if (collidedObjects.Any(o => o is Bonus))
            {
                _gameManager.OnBonus();
            }

            if (collidedObjects.Any(o => o is Enemy))
            {
                _gameManager.OnEnemy();
            }
        }

        public override void Start()
        {
            _gameManager = GameObjectPoolSingleton.Instance.GetObjectById("gameManager") as GameManager;
            Material = new Material('@', Color.DarkCyan);
        }

        public override void Step()
        {
            _prevPos = Position;
            switch (Input.ActiveKey)
            {
                case ConsoleKey.UpArrow:
                    Position += Vector2D.Up;
                    break;
                case ConsoleKey.DownArrow:
                    Position += Vector2D.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    Position += Vector2D.Left;
                    break;
                case ConsoleKey.RightArrow:
                    Position += Vector2D.Right;
                    break;
            }
        }
    }
}