using System;
using System.Collections.Generic;
using System.Text;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.Core
{
    public static class Logger
    {
        private static int _lastLine;
        private static Vector2D _basePoint;
        private static int _lineNumberLimit;
        private static int _lineLengthLimit;

        public static int LineNumberLimit
        {
            get => _lineNumberLimit;
            set
            {
                if (value >= 1)
                    _lineNumberLimit = value;
                else
                    _lineNumberLimit = 1;
            }
        }

        public static int LineLengthLimit
        {
            get => _lineLengthLimit;
            set
            {
                if (value >= 1)
                    _lineLengthLimit = value;
                else
                    _lineLengthLimit = 1;
            }
        }

        public static int LastLine
        {
            get => _lastLine;
            set
            {
                if (value >= 0)
                    _lastLine = value;
                else
                    _lastLine = 0;
            }
        }

        public static Vector2D BasePoint
        {
            get => _basePoint;
            set
            {
                if (value >= Vector2D.Zero)
                    _basePoint = value;
                else
                    _basePoint = Vector2D.Zero;
            }

        }

        public static void PrintLine(string message, 
            ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            if (_lastLine >= _lineNumberLimit)
            {
                Clear(_basePoint, _basePoint + new Vector2D(LineLengthLimit, LineNumberLimit));
                _lastLine = 0;
            }

            Vector2D lineStart = _basePoint + new Vector2D(0, 1) * _lastLine;

            if (message.Length > LineLengthLimit)
            {
                var stringA = message.Substring(0, LineLengthLimit);
                var stringB = message.Substring(LineLengthLimit, message.Length - LineLengthLimit);
                PrintLine(stringA, foregroundColor, backgroundColor);
                PrintLine(stringB, foregroundColor, backgroundColor);
            }
            else
            {
                Print(message, lineStart, foregroundColor, backgroundColor);
                _lastLine++;
            }
        }

        public static void PrintAt(string message, 
            Vector2D position, 
            ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Print(message, position, foregroundColor, backgroundColor);
        }

        private static void Print(string message, 
            Vector2D position, 
            ConsoleColor foregroundColor = ConsoleColor.White, 
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.SetCursorPosition(position.X, position.Y);
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(message);
        }

        private static void Clear(Vector2D startPos, Vector2D endPos)
        {
            Console.BackgroundColor = ConsoleColor.Black;

            for (int y = startPos.Y; y <= endPos.Y; y++)
            {
                Console.SetCursorPosition(startPos.X, y);

                for (int x = startPos.X; x <= endPos.X; x++)
                {
                    Console.Write('\0');
                }
            }
        }
    }
}
