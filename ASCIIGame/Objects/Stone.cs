using System.Drawing;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIGame.Objects
{
    public class Stone : GameObject
    {
        public override bool HasCollider => true;

        protected override void Start()
        {
            Material = new Material('█', Color.DarkGray);
        }
    }
}
