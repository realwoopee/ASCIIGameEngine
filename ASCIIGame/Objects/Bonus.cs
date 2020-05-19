using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIGame.Objects
{
    public class Bonus : GameObject
    {
        public override bool HasCollider => true;

        public bool _counter;

        public override void Start()
        {
            _counter = false;
            this.Material = new Material
            {
                Character = 'O',
                ForegroundColor = Color.Yellow
            };
        }

        public override void Step()
        {
            if(_counter)
            {
                this.Material = new Material
                {
                    Character = 'o',
                    ForegroundColor = Color.DarkOrange
                };
            }
            else
            {
                this.Material = new Material
                {
                    Character = 'O',
                    ForegroundColor = Color.Yellow
            };
            }
            _counter = !_counter;
        }
    }
}
