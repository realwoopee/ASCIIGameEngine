using System;
using System.Collections.Generic;
using System.Text;
using ASCIIEngine;
using ASCIIEngine.BasicClasses;

namespace ASCIIGame.Objects
{
    public class Stone : GameObject
    {
        public override bool HasCollider => true;

        public override void Start()
        {
            this.Material = new Material
            {
                Character = '█',
                ForegroundColor = ConsoleColor.DarkGray
            };
        }
    }
}
