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
        public override bool HasCollider => false;
        private Random rand;
        public override int Layer => 1;

        public override void Start()
        {
            rand = new Random();
            this.Material = new Material()
            {
                Character = '·',
                ForegroundColor = Color.DarkGreen
            };
        }

        public override void Step()
        {
            var mat = this.Material;
            mat.Character = rand.Next(0, 2) == 0 ? '·' : ',';
            mat.ForegroundColor = rand.Next(0, 2) == 0 ? Color.ForestGreen : Color.DarkGreen;
            this.Material = mat;
        }
    }
}
