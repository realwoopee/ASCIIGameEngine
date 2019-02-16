using System;
using System.Collections.Generic;
using System.Text;
using ASCIIEngine.BasicClasses;

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
                ForegroundColor = ConsoleColor.Yellow
            };
        }

        public override void Step()
        {
            if(_counter)
            {
                this.Material = new Material
                {
                    Character = 'o',
                    ForegroundColor = ConsoleColor.DarkYellow
                };
            }
            else
            {
                this.Material = new Material
                {
                    Character = 'O',
                    ForegroundColor = ConsoleColor.Yellow
                };
            }
            _counter = !_counter;
        }
    }
}
