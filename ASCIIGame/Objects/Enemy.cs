﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
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

        public override void OnCollision(List<GameObject> collidedWith)
        {
            var mat = this.Material;
            mat.BackgroundColor = Color.White;

            if (collidedWith.Find(o => (o is Enemy && o != this)) != null)
            {
                this.Position = _prevPos;
                mat.BackgroundColor = Color.Green;
            }

            if (collidedWith.Find(o => o is Bonus) != null)
            {
                this.Position = _prevPos;
                mat.BackgroundColor = Color.Yellow;
            }

            if (collidedWith.Find(o => o is Stone) != null)
            {
                this.Position = _prevPos;
                mat.BackgroundColor = Color.DarkGray;
            }

            this.Material = mat;
        }

        public override void Start()
        {
            this.Material = new Material
            {
                Character = 'X',
                ForegroundColor = Color.Red
            };

            _counter = 0;
        }

        public override void Step()
        {
            var tempPos = this.Position;

            switch (_counter)
            {
                case 0:
                case 2:
                {
                    Vector2D relativePos = target.Position - this.Position;
                    
                    this.Position += relativePos / (relativePos.Length == 0 ? 1 : relativePos.Length);

                    _prevPos = tempPos;
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

        public override string ToString() => $"Enemy at X:{this.Position.X} Y:{this.Position.Y}";
    }
}
