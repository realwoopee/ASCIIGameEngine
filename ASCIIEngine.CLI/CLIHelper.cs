using System;
using ASCIIEngine.Core.BasicClasses;

using Console = Colorful.Console;

namespace ASCIIEngine.CLI
{
    public static class CLIHelper
    {
        public static void DrawRect(Vector2D start, Vector2D end, Material material)
        {
            Console.ForegroundColor = material.ForegroundColor;
            Console.BackgroundColor = material.BackgroundColor;

            for (int y = start.Y; y <= end.Y; y++)
            {
                Console.SetCursorPosition(start.X, y);

                for (int x = start.X; x <= end.X; x++)
                {
                    Console.Write(material.Character);
                }
            }
        }

        public static void DrawRect(Vector2D start, Vector2D end, Material outerMaterial, Material innerMaterial)
        {
            Console.ForegroundColor = innerMaterial.ForegroundColor;
            Console.BackgroundColor = innerMaterial.BackgroundColor;

            for (int y = start.Y+1; y <= end.Y-1; y++)
            {
                Console.SetCursorPosition(start.X+1, y);

                for (int x = start.X+1; x <= end.X-1; x++)
                {
                    Console.Write(innerMaterial.Character);
                }
            }

            //рамки
            DrawRect(start, new Vector2D(end.X, start.Y), outerMaterial);
            DrawRect(new Vector2D(end.X, start.Y), end, outerMaterial);
            DrawRect(new Vector2D(start.X, end.Y), end, outerMaterial);
            DrawRect(start, new Vector2D(start.X, end.Y), outerMaterial);
        }

        public static void DrawArray(Material[,] buffer, Vector2D basePoint)
        {
            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                for (int j = 0; j < buffer.GetLength(1); j++)
                {
                    if (buffer[i, j].Character == '\0')
                        continue;
                    var obj = buffer[i, j];
                    Console.SetCursorPosition(j * 2 + basePoint.X, i + basePoint.Y);
                    Console.ForegroundColor = obj.ForegroundColor;
                    Console.BackgroundColor = obj.BackgroundColor;
                    Console.Write(obj.Character);
                }
            }
        }
    }
}
