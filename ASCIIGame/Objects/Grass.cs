using System.Drawing;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIGame.Objects
{
    internal class Grass : GameObject
    {
        public override bool HasCollider => false;

        protected override void Start()
        {
            Material = new Material('·', Color.DarkGreen);
        }
    }
}
