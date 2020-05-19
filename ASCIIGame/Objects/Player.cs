using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ASCIIEngine.Core;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIGame.Objects
{
    public class Player : GameObject
    {
        public override bool HasCollider => true;

        private Vector2D _prevPos;

        private GameManager gm;

        public override void OnCollision(IEnumerable<GameObject> collidedWith)
        {
            if (collidedWith.Any(o => o is Stone))
            {
                this.Position = _prevPos;
            }

            if (collidedWith.Any(o => o is Bonus))
            {
                gm.OnBonus();
            }

            if (collidedWith.Any(o => o is Enemy))
            {
                gm.OnEnemy();
            }
        }

        public override void Start()
        {
            gm = GameObjectPoolSingleton.Instance.GetObjectById("gameManager") as GameManager;

            this.Material = new Material
            {
                Character = '@',
                ForegroundColor = Color.DarkCyan
            };
        }

        public override void Step()
        {
            _prevPos = this.Position;
            switch (Input.ActiveKey)
            {
                case ConsoleKey.UpArrow:
                    this.Position += Vector2D.Up;
                    break;
                case ConsoleKey.DownArrow:
                    this.Position += Vector2D.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    this.Position += Vector2D.Left;
                    break;
                case ConsoleKey.RightArrow:
                    this.Position += Vector2D.Right;
                    break;
            }
        }
    }
}
