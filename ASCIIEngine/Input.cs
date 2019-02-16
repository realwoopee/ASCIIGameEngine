using System;

namespace ASCIIEngine
{
    public static class Input
    {
        public static ConsoleKey ActiveKey { get; private set; }

        public static void SetPressedKey(ConsoleKey key)
        {
            ActiveKey = key;
        }
    }
}
