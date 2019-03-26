using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ASCIIEngine.Core;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIGame.Objects
{
    class Grass : GameObject
    {
        public override string Tag => base.Tag;

        public override bool HasCollider => false;

        public override void Start()
        {
            this.Material = new Material()
            {
                Character = '·',
                ForegroundColor = Color.DarkGreen
            };
        }
    }
}
