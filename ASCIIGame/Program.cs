using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using ASCIIEngine.CLI;
using ASCIIEngine.Core;
using ASCIIEngine.Core.BasicClasses;
using ASCIIGame.Objects;
using Console = Colorful.Console;

namespace ASCIIGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "ASCIIGame";
            Console.SetWindowSize(76, 22);
            Console.SetBufferSize(76, 22);
            Console.BackgroundColor = Color.Black;
            var isPlaying = true;


            Vector2D worldSize = new Vector2D(20, 20);

            var renderer = new Renderer(worldSize);

            Logger.BasePoint = new Vector2D(42, 2);
            Logger.LineLengthLimit = 32;
            Logger.LineNumberLimit = 18;

            Base core = new Base();

            core.Initialize();

            var player = new Player
            {
                Position = new Vector2D(5, 5),
                Layer = 3
            };


            CLIHelper.DrawRect(Vector2D.Zero, new Vector2D(75, 21), new Material(Color.Empty, Color.Gray), new Material(Color.Empty, Color.Black));
            CLIHelper.DrawRect(Vector2D.Zero, new Vector2D(40, 21), new Material(Color.Empty, Color.Gray), new Material(Color.Empty, Color.Black));

            Logger.PrintLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

            var rand = new Random();

            //заполнение мира объектами
            for (int i = 0; i < worldSize.X; i++)
            for (int j = 0; j < worldSize.Y; j++)
            {
                if (rand.Next(100) < 10)
                {
                    core.AddObject(new Stone
                    {
                        Position = new Vector2D(i, j),
                        Layer = 2
                    });
                    continue;
                }
                else
                {
                    core.AddObject(new Grass
                    {
                        Position = new Vector2D(i, j),
                        Layer = 1
                    });
                }

                if (rand.Next(100) < 5)
                {
                    core.AddObject(new Enemy
                    {
                        target = player,
                        Position = new Vector2D(i, j),
                        Layer = 3
                    });
                }
            }

            var bonus = new Bonus
            {
                Layer = 4
            };

            core.AddObject(bonus);
            var gameManager = new GameManager(() => PlaceBonus(worldSize, core, bonus), worldSize);
            core.AddObject(gameManager);

            core.AddObject(player);

            PlaceBonus(worldSize, core, bonus);

            var buffer = new Material[renderer.Size.X, renderer.Size.Y];

            buffer = renderer.Render(buffer);
            CLIHelper.DrawArray(buffer, new Vector2D(1, 1));

            Input.GameInput = true;
            Input.TOGGLE_GAME_INPUT = ConsoleKey.Oem3;

            while (isPlaying)
            {
                Thread.Sleep(33);
                core.SetPressedKey(Console.ReadKey(true).Key);
                core.DoStep();
                buffer = renderer.Render(buffer);
                CLIHelper.DrawArray(buffer, new Vector2D(1, 1));
                PrintScore(gameManager.Score);
                while (Console.KeyAvailable)
                    Console.ReadKey(true);
            }
        }

        private static void PrintScore(int score)
        {
            Logger.PrintAt("Score: " + score, new Vector2D(42, 1));
        }

        private static void PlaceBonus(Vector2D worldSize, Base core, Bonus bonus)
        {
            var rand = new Random();
            var bonusPos = new Vector2D(rand.Next(worldSize.X), rand.Next(worldSize.Y));
            var objs = GameObjectsListSingleton.Instance.GetObjectsAtPosition(bonusPos);
            if (!objs.All(o => o is Stone))
            {
                bonus.Position = bonusPos;
            }
            else
            {
                PlaceBonus(worldSize, core, bonus);
            }
        }
    }
}