using System.Collections.Generic;
using System.Linq;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.Core
{
    public class Renderer
    {
        public Vector2D Size { get; }
        private readonly IReadOnlyList<GameObject> _objects;

        public Renderer(Vector2D size)
        {
            Size = size;
            _objects = GameObjectPoolSingleton.Instance.Objects;
        }

        public Material[,] Render(Material[,] buffer)
        {
            var objectsToRender = _objects
                .Where(o => o.Position >= Vector2D.Zero)
                .Where(o => o.Position < Size)
                .GroupBy(o => o.Layer)
                .OrderBy(g => g.Key);

            var output = new Material[Size.X, Size.Y];
            for (var x = 0; x < output.GetLength(0); x++)
            {
                for (var y = 0; y < output.GetLength(1); y++)
                {
                    output[x, y] = new Material(' ', output[x, y].ForegroundColor, output[x, y].BackgroundColor);
                }
            }

            foreach (var go in objectsToRender)
            {
                foreach (var o in go)
                {
                    output[o.Position.X, o.Position.Y] = o.Material;
                }
            }

            return XorArrays(buffer, output);
        }

        private static Material[,] XorArrays(Material[,] buffer, Material[,] result)
        {
            for (var i = 0; i < buffer.GetLength(0); i++)
            {
                for (var j = 0; j < buffer.GetLength(1); j++)
                {
                    if (buffer[i, j] == result[i, j])
                    {
                        result[i, j] = new Material();
                    }
                }
            }

            return result;
        }
    }
}