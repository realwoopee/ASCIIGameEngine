using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASCIIEngine.BasicClasses;

namespace ASCIIEngine
{
    public class Camera
    {
        public List<GameObject> Objects { get; set; }

        public Camera(Vector2D position, Vector2D size)
        {
            Position = position;
            Size = size;
        }

        public Vector2D Position { get; set; }

        public Vector2D Size { get; set; }

        public Material[,] Render(Material[,] buffer)
        {
            var objsToRender = Objects?
                .Where(o => o.Position >= this.Position)
                .Where(o => o.Position < this.Position + this.Size)
                .GroupBy(o => o.Layer)
                .OrderBy(g => g.Key);

            var output = new Material[Size.X, Size.Y];

            foreach(var g in objsToRender)
            foreach(var o in g)
            {
                var relativePos = o.Position - this.Position;
                output[relativePos.X, relativePos.Y] = o.Material;
            }

            return output;
        }
    }
}
