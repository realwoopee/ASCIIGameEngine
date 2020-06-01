using System;
using System.Drawing;
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
            Console.CursorVisible = false;
            Console.Title = "ASCIIGame";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetWindowSize(25*2, 26);
            Console.SetBufferSize(25*2, 26);
            SecondExample();
        }

        private static void FirstExample()
        {
            var worldSize = new Vector2D(25, 25);

            var renderer = new Renderer(worldSize);

            Logger.BasePoint = new Vector2D(0, 0);
            Logger.LineLengthLimit = 32;
            Logger.LineNumberLimit = 18;

            var core = new Base();

            core.Initialize();

            for (var n = 0; n < worldSize.X; n++)
            {
                core.AddObject(new Wall
                {
                    Position = new Vector2D(0, n),
                    Layer = 3
                });

                core.AddObject(new Wall
                {
                    Position = new Vector2D(n, 0),
                    Layer = 3
                });
                
                core.AddObject(new Wall
                {
                    Position = new Vector2D(n, worldSize.Y-1),
                    Layer = 3
                });
                
                core.AddObject(new Wall
                {
                    Position = new Vector2D(worldSize.X-1, n),
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
            
            core.AddObject(new Entity
            {
                Position = new Vector2D(2, 5),
                Layer = 3
            });
            
            core.AddObject(new Entity
            {
                Position = new Vector2D(2, 6),
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

        private static void SecondExample()
        {
            var worldSize = new Vector2D(25, 25);

            var renderer = new Renderer(worldSize);

            Logger.BasePoint = new Vector2D(0, 0);
            Logger.LineLengthLimit = 32;
            Logger.LineNumberLimit = 18;

            var core = new Base();

            core.Initialize();

            for (var n = 0; n < worldSize.X; n++)
            {
                core.AddObject(new Wall
                {
                    Position = new Vector2D(0, n),
                    Layer = 3
                });

                core.AddObject(new Wall
                {
                    Position = new Vector2D(n, 0),
                    Layer = 3
                });

                core.AddObject(new Wall
                {
                    Position = new Vector2D(n, worldSize.Y - 1),
                    Layer = 3
                });

                core.AddObject(new Wall
                {
                    Position = new Vector2D(worldSize.X - 1, n),
                    Layer = 3
                });
            }

            var buffer = new Material[renderer.Size.X, renderer.Size.Y];

            Console.CursorVisible = false;

            var bonus = new Bonus
            {
                SpawnBounds = new Vector2D(3, 20),
                Layer = 2
            };
            
            var hunter = new Hunter
            {
                Position = new Vector2D(2, 3),
                Layer = 3,
                Target = bonus
            };
            
            core.AddObject(hunter);
            core.AddObject(bonus);

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