using System;

namespace ASCIIEngine.Core
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
