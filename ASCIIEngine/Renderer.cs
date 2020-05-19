using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.Core
{
    public class Renderer
    {
        private IReadOnlyList<GameObject> _objects;

        public Renderer(Vector2D size)
        {
            Size = size;

            _objects = GameObjectPoolSingleton.Instance.Objects;
        }

        public Vector2D Size { get; set; }

        public Material[,] Render(Material[,] buffer)
        {
            var objsToRender = _objects
                .Where(o => o.Position >= new Vector2D(0, 0))
                .Where(o => o.Position < this.Size)
                .GroupBy(o => o.Layer)
                .OrderBy(g => g.Key);

            var output = new Material[Size.X, Size.Y];

            for (var x = 0; x < output.GetLength(0); x++)
            {
                for (var y = 0; y < output.GetLength(1); y++)
                {
                    output[x, y].Character = ' ';
                }
            }

            foreach (var g in objsToRender)
                foreach (var o in g)
                {
                    output[o.Position.X, o.Position.Y] = o.Material;
                }

            return XorArrays(buffer, output);
        }

        public Material[,] XorArrays(Material[,] buffer, Material[,] result)
        {
            for (var i = 0; i < buffer.GetLength(0); i++)
                for (var j = 0; j < buffer.GetLength(1); j++)
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
