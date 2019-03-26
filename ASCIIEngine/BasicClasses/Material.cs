using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ASCIIEngine.Core.BasicClasses
{
    public struct Material
    {
        public Color BackgroundColor;
        public Color ForegroundColor;
        public char Character;

        public static bool operator ==(Material a, Material b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Material a, Material b)
        {
            return !a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Material))
            {
                return false;
            }

            var material = (Material)obj;
            return BackgroundColor == material.BackgroundColor &&
                   ForegroundColor == material.ForegroundColor &&
                   Character == material.Character;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BackgroundColor, ForegroundColor, Character);
        }
    }
}
