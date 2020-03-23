namespace Unity.Tests
{
    using Mathematics;
    using NUnit.Framework;

    public class UtilityTest
    {
        [Test]
        public void ConvertWorldPositionToGridCellIndex()
        {
            // Arrange
            var hGridCellCount = 20;
            var vGridCellCount = 30;
            var hGridCellSize = 1.0f;
            var vGridCellSize = 1.0f;
            var hPosition = 5.7f;
            var vPosition = 12.3f;

            // Act
            var expected = 245;
            var result =
                Utility.GridHelper.GridCellIndex(
                    hGridCellCount, vGridCellCount,
                    hGridCellSize, vGridCellSize,
                    hPosition, vPosition);            

            // Assert
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ConvertWorldPositionToGridCellIndexCombined()
        {
            // Arrange
            var hGridCellCount = 10;
            var vGridCellCount = 10;
            var hGridCellSize = 1.0f;
            var vGridCellSize = 1.0f;
            var hPosition = 5.7f;
            var vPosition = 7.3f;

            // Act
            var expected = 75;
            var result =
                Utility.GridHelper.GridCellIndex(
                    new int2(hGridCellCount, vGridCellCount),
                    new float2(hGridCellSize, vGridCellSize),
                    new float2(hPosition, vPosition));            

            // Assert
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ConvertWorldPositionWitGridCellSizeEnlargedToGridCellIndexCombined()
        {
            // Arrange
            var hGridCellCount = 10;
            var vGridCellCount = 10;
            var hGridCellSize = 2.0f;
            var vGridCellSize = 2.0f;
            var hPosition = 5.7f;
            var vPosition = 7.3f;

            // Act
            var expected = 32;
            var result =
                Utility.GridHelper.GridCellIndex(
                    new int2(hGridCellCount, vGridCellCount),
                    new float2(hGridCellSize, vGridCellSize),
                    new float2(hPosition, vPosition));            

            // Assert
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void ConvertWorldPositionWitGridCellSizeShrinkToGridCellIndexCombined()
        {
            // Arrange
            var hGridCellCount = 10;
            var vGridCellCount = 10;
            var hGridCellSize = 0.5f;
            var vGridCellSize = 0.5f;
            var hPosition = 5.7f;
            var vPosition = 7.3f;

            // Act
            var expected = 151;
            var result =
                Utility.GridHelper.GridCellIndex(
                    new int2(hGridCellCount, vGridCellCount),
                    new float2(hGridCellSize, vGridCellSize),
                    new float2(hPosition, vPosition));            

            // Assert
            Assert.AreEqual(expected, result);
        }
    }    
}
