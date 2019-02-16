using System;
using System.Collections.Generic;
using System.Text;
using ASCIIEngine.BasicClasses;

namespace ASCIIGame.Objects
{
    public class Enemy : GameObject
    {
        public override bool HasCollider => true;

        private int _counter;

        private const int MaxCounterValue = 3;

        public GameObject target; //GameObject that this entity will chase.

        private Vector2D _prevPos;

        public override void OnCollision(List<GameObject> collidedWith)
        {
            if (collidedWith.Find(o => o is Stone || o is Bonus || (o is Enemy && o != this)) != null)
            {
                this.Position = _prevPos;
            }

            //if(collidedWith)
        }

        public override void Start()
        {
            this.Material = new Material
            {
                Character = 'X',
                ForegroundColor = ConsoleColor.Red
            };

            _counter = 0;
        }

        public override void Step()
        {
            _prevPos = this.Position;

            Vector2D relativePos;

            switch (_counter)
            {
                case 0:
                case 2:
                {
                    relativePos = target.Position - this.Position;
                    
                    this.Position += relativePos / (relativePos.Length == 0 ? 1 : relativePos.Length);
                    break;
                }
            }

            if(_counter == MaxCounterValue)
            {
                _counter = 0;
            }
            else
            {
                _counter++;
            }
        }
    }
}
