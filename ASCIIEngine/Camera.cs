using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.Core
{
    public class Camera
    {
        private IReadOnlyList<GameObject> _objects;

        public Camera(Vector2D position, Vector2D size)
        {
            Position = position;
            Size = size;

            _objects = GameObjectPoolSingleton.Instance.Objects;
        }

        public Vector2D Position { get; set; }

        public Vector2D Size { get; set; }

        public Material[,] Render(Material[,] buffer)
        {
            var objsToRender = _objects
                //.Where(o => o.HasChanged)
                .Where(o => o.Position >= this.Position)
                .Where(o => o.Position < this.Position + this.Size)
                .GroupBy(o => o.Layer)
                .OrderBy(g => g.Key);

            var output = new Material[Size.X, Size.Y];

            foreach (var g in objsToRender)
                foreach (var o in g)
                {
                    var relativePos = o.Position - this.Position;
                    output[relativePos.X, relativePos.Y] = o.Material;
                }

            return XorArrays(buffer, output);
        }

        public Material[,] XorArrays(Material[,] buffer, Material[,] result)
        {
            for (int i = 0; i < buffer.GetLength(0); i++)
                for (int j = 0; j < buffer.GetLength(1); j++)
                {
                    if(buffer[i,j] == result[i,j])
                    {
                        result[i, j] = new Material();
                    }
                }
            return result;
        }
    }
}
