using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ASCIIEngine.Core;
using ASCIIEngine.Core.BasicClasses;

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
                ForegroundColor = Color.DarkGray
            };
        }
    }
}
