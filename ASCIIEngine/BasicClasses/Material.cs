using System;
using System.Drawing;

namespace ASCIIEngine.Core.BasicClasses
{
    public struct Material
    {
        public readonly Color BackgroundColor;
        public readonly Color ForegroundColor;
        public readonly char Character;

        public Material(char character, Color foregroundColor, Color backgroundColor)
        {
            Character = character;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }
        
        public Material(char character, Color foregroundColor)
        {
            Character = character;
            ForegroundColor = foregroundColor;
            BackgroundColor = Color.Empty;
        }
        
        public Material(Color foregroundColor, Color backgroundColor)
        {
            Character = default;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

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

            var material = (Material) obj;
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