using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIGame.Objects
{
    public class Enemy : GameObject
    {
        public override bool HasCollider => true;

        private int _counter;

        private const int MaxCounterValue = 3;

        public GameObject target; //GameObject that this entity will chase.

        private Vector2D _prevPos;

        public override void OnCollision(IEnumerable<GameObject> collidedWith)
        {
            var newBackground = Color.White;

            if (collidedWith.Any(o => o is Enemy))
            {
                Position = _prevPos;
                newBackground = Color.Green;
            }

            Material = new Material(Material.Character, Material.ForegroundColor, newBackground);
        }

        public override void Start()
        {
            Material = new Material('X', Color.Red);
            _counter = 0;
        }

        public override void Step()
        {
            var tempPos = Position;

            switch (_counter)
            {
                case 0:
                case 2:
                {
                    var relativePos = target.Position - Position;
                    Position += relativePos / (relativePos.Length == 0 ? 1 : relativePos.Length);
                    _prevPos = tempPos;

                    break;
                }
            }
            if (_counter == MaxCounterValue)
            {
                _counter = 0;
            }
            else
            {
                _counter++;
            }
        }

        public override string ToString() => $"Enemy at X:{Position.X} Y:{Position.Y}";
    }
}