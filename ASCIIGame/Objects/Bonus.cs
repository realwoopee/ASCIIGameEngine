using System.Drawing;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIGame.Objects
{
    public class Bonus : GameObject
    {
        public override bool HasCollider => true;

        private bool _counter;

        public override void Start()
        {
            _counter = false;
            Material = new Material('O', Color.Yellow);
        }

        public override void Step()
        {
            Material = _counter 
                ? new Material('o', Color.DarkOrange) 
                : new Material('O', Color.Yellow);

            _counter = !_counter;
        }
    }
}
