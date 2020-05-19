using System.Drawing;
using ASCIIEngine.Core.BasicClasses;
using FluentAssertions;
using Xunit;

namespace ASCIIEngine.UnitTests.BasicClasses
{
    public class MaterialTests
    {
        [Fact]
        public void EqualsOperatorWithEqualMaterial_ShouldReturnTrue()
        {
            var firstMaterial = new Material('@', Color.Aquamarine, Color.Chocolate);
            var secondMaterial = new Material('@', Color.Aquamarine, Color.Chocolate);
            (firstMaterial == secondMaterial).Should().BeTrue();
        }
        
        [Fact]
        public void EqualsOperatorWithNotEqualMaterials_ShouldReturnFalse()
        {
            var firstMaterial = new Material('@', Color.Aquamarine, Color.Chocolate);
            var secondMaterial = new Material('=', Color.Aquamarine, Color.Chocolate);
            var thirdMaterial = new Material('@', Color.Chocolate, Color.Aquamarine);
            var fourthMaterial = new Material('@', Color.Aquamarine, Color.Coral); 
            var fifthMaterial = new Material('@', Color.Aqua, Color.Chocolate);
            (firstMaterial == secondMaterial).Should().BeFalse();
            (firstMaterial == thirdMaterial).Should().BeFalse();
            (firstMaterial == fourthMaterial).Should().BeFalse();
            (firstMaterial == fifthMaterial).Should().BeFalse();
        }
        
        [Fact]
        public void NotEqualsOperatorWithEqualMaterial_ShouldReturnTrue()
        {
            var firstMaterial = new Material('@', Color.Aquamarine, Color.Chocolate);
            var secondMaterial = new Material('@', Color.Aquamarine, Color.Chocolate);
            (firstMaterial != secondMaterial).Should().BeFalse();
        }
        
        [Fact]
        public void NotEqualsOperatorWithNotEqualMaterials_ShouldReturnFalse()
        {
            var firstMaterial = new Material('@', Color.Aquamarine, Color.Chocolate);
            var secondMaterial = new Material('=', Color.Aquamarine, Color.Chocolate);
            var thirdMaterial = new Material('@', Color.Chocolate, Color.Aquamarine);
            var fourthMaterial = new Material('@', Color.Aquamarine, Color.Coral); 
            var fifthMaterial = new Material('@', Color.Aqua, Color.Chocolate);
            (firstMaterial != secondMaterial).Should().BeTrue();
            (firstMaterial != thirdMaterial).Should().BeTrue();
            (firstMaterial != fourthMaterial).Should().BeTrue();
            (firstMaterial != fifthMaterial).Should().BeTrue();
        }
    }
}