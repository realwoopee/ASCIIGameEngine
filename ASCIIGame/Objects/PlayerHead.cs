using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ASCIIEngine.Core;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIGame.Objects
{
    public class PlayerHead : GameObject
    {
        public override bool HasCollider => true;
        public override string Tag => "player";
        public override int Layer => 4;

        private Vector2D _prevPos;

        private GameManager gm;

        private Vector2D _direction;

        public PlayerTail Tail;

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
                ForegroundColor = Color.IndianRed
            };
        }

        public override void Step()
        {
            _prevPos = this.Position;

            switch (Input.ActiveKey)
            {
                case ConsoleKey.UpArrow:
                    _direction = Vector2D.Up;
                    break;
                case ConsoleKey.DownArrow:
                    _direction = Vector2D.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    _direction = Vector2D.Left;
                    break;
                case ConsoleKey.RightArrow:
                    _direction = Vector2D.Right;
                    break;
            }

            this.Position += _direction;
            if (Tail != null) Tail.Position = _prevPos;
        }

        public void AddTail()
        {
            if (Tail == null)
            {
                var newTail = new PlayerTail
                {
                    Position = _prevPos
                };
                GameObjectPoolSingleton.Instance.AddObject(newTail);
                Tail = newTail;
            }
            else
            {
                Tail.AddTail();
            }
        }
    }
}
