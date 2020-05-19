using System.Drawing;
using ASCIIEngine.Core;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIGame.Objects
{
    public class PlayerTail : GameObject
    {
        public PlayerTail Next { get; set; }
        public PlayerTail Tail => Next?.Tail ?? Next ?? this;

        private Vector2D _prevPos;

        public override int Layer => 3;

        public override void Start()
        {
            this.Material = new Material
            {
                Character = '@',
                ForegroundColor = Color.DarkCyan
            };
        }

        public override void Step()
        {
            if (Next != null) Next.Position = _prevPos;
            _prevPos = this.Position;
        }

        public void AddTail()
        {
            if (Tail == this)
            {
                var newTail = new PlayerTail
                {
                    Position = _prevPos
                };
                GameObjectPoolSingleton.Instance.AddObject(newTail);
                this.Next = newTail;
            }
            else
            {
                Tail.AddTail();
            }
        }
    }
}