using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sparky;

public class CalculatorXUnitTests
{
    [Fact]
    public void AddNumbers_InputTwoInt_GetCorrectAddition()
    {
        //Arrange 
        Calculator calculator = new();

        //Act
        int result = calculator.AddNumbers(2, 7);

        //Assert
        Assert.Equal(9, result);
    }

    [Fact]
    public void AddNumbers_InputTwoInt_GetIncorrectAddition()
    {
        //Arrange
        Calculator calculator = new();

        //Act
        int result = calculator.AddNumbers(2, 5);

        //Assert
        Assert.NotEqual(5, result);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(7)]
    public void IsOddChecker_InputOddNumber_ReturnTrue(int number)
    {
        //Arrange
        Calculator calculator = new();

        //Act
        bool isOdd = calculator.IsOddNumber(number);

        //Assert
        Assert.True(isOdd);
    }

    [Fact]
    public void IsOddChecker_InputEvenNumber_ReturnFalse()
    {
        //Arrange
        Calculator calculator = new();

        //Act
        bool isOdd = calculator.IsOddNumber(2);

        //Assert
        Assert.False(isOdd);
    }

    [Theory]
    [InlineData(3, true)]
    [InlineData(1, true)]
    [InlineData(2, false)]
    [InlineData(4, false)]
    public void IsOddChecker_InputNumber_ReturnTrueIfOdd(int number, bool expectedResult)
    {
        //Arrange
        Calculator calculator = new();

        //Act
        bool isOdd = calculator.IsOddNumber(number);

        //Assert
        Assert.Equal(expectedResult, isOdd);
    }

    [Theory]
    [InlineData(5.4, 10.5)] //15.9
    //[InlineData(5.43, 10.53)] //15.96
    //[InlineData(5.49, 10.59)] //16.08
    public void AddNumbersDouble_InputTwoDouble_GetCorrectAddition(double number1, double number2)
    {
        //Arrange
        Calculator calculator = new();

        //Act
        double result = calculator.AddNumbersDouble(number1, number2);

        //Assert
        Assert.Equal(15.9, result, 1);
    }

    [Fact]
    public void OddRanger_InputMinAndMaxRange_ReturnsValidOddNumberRange()
    {
        //Arrange
        Calculator calculator = new();
        List<int> expectedOddRange = new() { 5, 7, 9 }; // 5-10

        //Act
        List<int> result = calculator.GetOddRange(5, 10);

        //Assert
        Assert.Equal(expectedOddRange, result);
        Assert.Contains(7, result);
        Assert.NotEmpty(result);
        Assert.Equal(3, result.Count);
        Assert.DoesNotContain(6, result);
        Assert.Equal(result.OrderBy(u => u), result);
        //Assert.That(result, Is.Unique);
    }
}
