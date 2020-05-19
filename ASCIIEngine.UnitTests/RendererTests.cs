using System.Drawing;
using ASCIIEngine.Core;
using ASCIIEngine.Core.BasicClasses;
using FluentAssertions;
using Xunit;

namespace ASCIIEngine.UnitTests
{
    public class RendererTests
    {
        [Fact]
        public void Render_ShouldReturnRenderedOutput()
        {
            var baseCore = new Base();
            baseCore.AddObject(new GameObject
            {
                Layer = 1,
                Material = new Material('@', Color.Red, Color.Blue),
                Position = new Vector2D(2, 2),
                Tag = "test_object",
                HasCollider = true
            });
            
            var renderer = new Renderer(new Vector2D(5, 5));
            
            var buffer = new Material[renderer.Size.X, renderer.Size.Y];
            var output = renderer.Render(buffer);

            var testObject = output[2, 2];
            testObject.Character.Should().Be('@');
            testObject.ForegroundColor.Should().Be(Color.Red);
            testObject.BackgroundColor.Should().Be(Color.Blue);
            
            baseCore.AddObject(new GameObject
            {
                Layer = 2,
                Material = new Material('#', Color.Blue, Color.Red),
                Position = new Vector2D(2, 2),
                Tag = "test_object_2",
                HasCollider = true
            });
            
            var newOutput = renderer.Render(buffer);
            var secondTestObject = newOutput[2, 2];
            secondTestObject.Character.Should().Be('#');
            secondTestObject.ForegroundColor.Should().Be(Color.Blue);
            secondTestObject.BackgroundColor.Should().Be(Color.Red);
        }
    }
}