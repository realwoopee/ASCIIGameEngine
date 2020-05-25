using System.Drawing;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.ExampleApp.Objects
{
    public class Wall : GameObject
    {
        public override bool HasCollider => true;

        protected override void Start()
        {
            Material = new Material('█', Color.DarkGray);
        }
    }
}