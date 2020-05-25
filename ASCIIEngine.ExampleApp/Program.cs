using System;
using System.Threading;
using ASCIIEngine.CLI;
using ASCIIEngine.Core;
using ASCIIEngine.Core.BasicClasses;
using ASCIIEngine.ExampleApp.Objects;

namespace ASCIIEngine.ExampleApp
{
    internal static class Program
    {
        private static void Main()
        {
            FirstExample();
        }

        private static void FirstExample()
        {
            var worldSize = new Vector2D(25, 25);

            var renderer = new Renderer(worldSize);

            Logger.BasePoint = new Vector2D(42, 2);
            Logger.LineLengthLimit = 32;
            Logger.LineNumberLimit = 18;

            var core = new Base();

            core.Initialize();

            for (var x = 2; x <= 20; x++)
            {
                core.AddObject(new Wall
                {
                    Position = new Vector2D(1, x),
                    Layer = 3
                });

                core.AddObject(new Wall
                {
                    Position = new Vector2D(x, 2),
                    Layer = 3
                });
                
                core.AddObject(new Wall
                {
                    Position = new Vector2D(x, 21),
                    Layer = 3
                });
                
                core.AddObject(new Wall
                {
                    Position = new Vector2D(21, x),
                    Layer = 3
                });
            }

            var buffer = new Material[renderer.Size.X, renderer.Size.Y];

            Console.CursorVisible = false;

            core.AddObject(new Entity
            {
                Position = new Vector2D(2, 3),
                Layer = 3
            });
            
            core.AddObject(new Entity
            {
                Position = new Vector2D(2, 4),
                Layer = 3
            });

            buffer = renderer.Render(buffer);
            CLIHelper.DrawArray(buffer, new Vector2D(1, 1));
            
            while (true)
            {
                Thread.Sleep(33);
                core.DoStep();
                buffer = renderer.Render(buffer);
                CLIHelper.DrawArray(buffer, new Vector2D(1, 1));
            }
        }
    }
}