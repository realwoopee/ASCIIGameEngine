using System;
using System.Drawing;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.ExampleApp.Objects
{
    public class Bonus : GameObject
    {
        public override bool HasTrigger => true;
        public Vector2D SpawnBounds { get; set; }
        
        private bool _counter;
        private readonly Random _random = new Random();

        public override void OnTrigger(GameObject other)
        {
            Position = new Vector2D(_random.Next(SpawnBounds.X, SpawnBounds.Y), _random.Next(SpawnBounds.X, SpawnBounds.Y));
        }
        
        protected override void Start()
        {
            _counter = false;
            Material = new Material('O', Color.Yellow);
            
            Position = new Vector2D(_random.Next(SpawnBounds.X, SpawnBounds.Y), _random.Next(SpawnBounds.X, SpawnBounds.Y));
        }

        protected override void Update()
        {
            Material = _counter 
                ? new Material('o', Color.DarkOrange) 
                : new Material('O', Color.Yellow);

            _counter = !_counter;
        }
    }
}