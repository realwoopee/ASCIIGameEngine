using System;
using System.Collections.Generic;
using System.Text;

namespace ASCIIEngine.Core.BasicClasses
{
    public struct Vector2D
    {
        

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Length => (int)Math.Sqrt(X * X + Y * Y);

        public Vector2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        #region operators
        public static Vector2D operator +(Vector2D a, Vector2D b) => new Vector2D(a.X + b.X, a.Y + b.Y);
        public static Vector2D operator -(Vector2D a, Vector2D b) => new Vector2D(a.X - b.X, a.Y - b.Y);

        public static Vector2D operator -(Vector2D a) => a * -1;

        public static Vector2D operator *(Vector2D a, int num) => new Vector2D(a.X * num, a.Y * num);
        public static Vector2D operator /(Vector2D a, int num) => new Vector2D(a.X / num, a.Y / num);

        public static bool operator >(Vector2D a, Vector2D b) => a.X > b.X && a.Y > b.Y;
        public static bool operator <(Vector2D a, Vector2D b) => a.X < b.X && a.Y < b.Y;

        public static bool operator >=(Vector2D a, Vector2D b) => a.X >= b.X && a.Y >= b.Y;
        public static bool operator <=(Vector2D a, Vector2D b) => a.X <= b.X && a.Y <= b.Y;

        public static bool operator ==(Vector2D a, Vector2D b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Vector2D a, Vector2D b) => a.X != b.X && a.Y != b.Y;

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + X.GetHashCode();
                hash = hash * 31 + Y.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return "{x:" + X + ", y:" + Y + "}";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector2D))
            {
                return false;
            }

            var d = (Vector2D)obj;
            return X == d.X &&
                   Y == d.Y;
        }

        public static Vector2D Up => new Vector2D(-1, 0);
        public static Vector2D Down => new Vector2D(1, 0);
        public static Vector2D Left => new Vector2D(0, -1);
        public static Vector2D Right => new Vector2D(0, 1);
        public static Vector2D Zero => new Vector2D(0, 0);
        #endregion
    }
}
