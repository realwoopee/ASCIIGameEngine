using System;
using System.Collections.Generic;
using ASCIIEngine;
using ASCIIEngine.BasicClasses;

using System.Linq;

using ASCIIGame.Objects;

namespace ASCIIGame
{
    class Program
    {
        private static Random rand;

        static void Main(string[] args)
        {
            var exitProgram = false;

            var isPlaying = true;

            Vector2D worldSize = new Vector2D(20, 20);

            rand = new Random();

            var camera = new Camera(
                    new Vector2D(0, 0),
                    worldSize);

            ASCIIEngine.Core core = new Core(
                camera);

            core.Initialize();

            var player = new Player()
            {
                Position = new Vector2D(5, 5),
                Layer = 3
            };

            

            for(int i = -1; i < worldSize.X+1; i++)
            {
                core.AddObject(new Stone
                {
                    Position = new Vector2D(i, 0),
                    Layer = 2
                });
                core.AddObject(new Stone
                {
                    Position = new Vector2D(0, i),
                    Layer = 2
                });
                core.AddObject(new Stone
                {
                    Position = new Vector2D(worldSize.X-1, i),
                    Layer = 2
                });
                core.AddObject(new Stone
                {
                    Position = new Vector2D(i, worldSize.Y-1),
                    Layer = 2
                });
            }

            for (int i = 1; i < worldSize.X-1; i++)
                for (int j = 1; j < worldSize.Y-1; j++)
                {
                    if(rand.Next(100) < 10)
                    {
                        core.AddObject(new Stone()
                        {
                            Position = new Vector2D(i, j),
                            Layer = 2
                        });
                    }
                    else
                    {
                        core.AddObject(new Grass()
                        {
                            Position = new Vector2D(i, j),
                            Layer = 1
                        });
                    }

                    if (rand.Next(100) < 5)
                    {
                        core.AddObject(new Enemy()
                        {
                            target = player,
                            Position = new Vector2D(i, j),
                            Layer = 3
                        });
                    }
                }


            var bonus = new Bonus()
            {
                Layer = 4
            };
            core.AddObject(bonus);
            var gm = new GameManager(() => PlaceBonus(worldSize, core, bonus));
            core.AddObject(gm);
            
            core.AddObject(player);

            PlaceBonus(worldSize, core, bonus);

            var buffer = new Material[camera.Size.X, camera.Size.Y];

            Console.CursorVisible = false;

            buffer = core.Render(buffer);
            WriteBuffer(buffer);
            while (isPlaying)
            {
                var input = Console.ReadKey(true);
                core.SetPressedKey(input.Key);
                core.DoStep();
                buffer = core.Render(buffer);
                WriteBuffer(buffer);
                PrintScore(gm.Score);
            }
        }

        private static void PrintScore(int score)
        {
            Console.SetCursorPosition(41, 1);
            Console.Write("Score: " + score + "     ");
        }

        private static void WriteBuffer(Material[,] buffer)
        {
            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                for (int j = 0; j < buffer.GetLength(1); j++)
                {
                    if (buffer[i, j].Character == '\0')
                        continue;
                    var obj = buffer[i, j];
                    Console.SetCursorPosition(j*2, i);
                    Console.ForegroundColor = obj.ForegroundColor;
                    Console.BackgroundColor = obj.BackgroundColor;
                    Console.Write(obj.Character);
                }
            }
        }

        private static void PlaceBonus(Vector2D worldSize, Core core, Bonus bonus)
        {
            Vector2D bonusPos = new Vector2D(rand.Next(worldSize.X), rand.Next(worldSize.Y));
            var objs = core.GetObjectsAtPosition(bonusPos);
            if (objs.FirstOrDefault(o => o is Stone) == null)
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