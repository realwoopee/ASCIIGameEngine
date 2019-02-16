using System;
using System.Collections.Generic;
using System.Text;
using ASCIIEngine;
using ASCIIEngine.BasicClasses;

namespace ASCIIGame.Objects
{
    class Grass : GameObject
    {
        public override string ID => base.ID;

        public override bool HasCollider => false;

        public override void Start()
        {
            this.Material = new Material()
            {
                Character = '·',
                ForegroundColor = ConsoleColor.DarkGreen
            };
        }
    }
}
