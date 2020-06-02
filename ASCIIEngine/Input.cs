using System;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.Core
{
    public static class Input
    {
        public static ConsoleKey ActiveKey { get; private set; }

        public static Vector2D ActiveDirection =>
            GameInput ? ActiveKey switch
            {
                ConsoleKey.UpArrow => Vector2D.Up,
                ConsoleKey.DownArrow => Vector2D.Down,
                ConsoleKey.LeftArrow => Vector2D.Left,
                ConsoleKey.RightArrow => Vector2D.Right,
                _ => new Vector2D()
            } : new Vector2D();

        public static bool GameInput { get; set; }

        public static void SetPressedKey(ConsoleKey key)
        {
            ActiveKey = key;

            if (key == TOGGLE_GAME_INPUT)
            {
                GameInput = !GameInput;
            }
        }

        public static ConsoleKey TOGGLE_GAME_INPUT;
    }
}