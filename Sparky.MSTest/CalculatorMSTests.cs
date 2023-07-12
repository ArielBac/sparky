namespace Sparky.MSTest;

[TestClass]
public class CalculatorMSTests
{
    [TestMethod]
    public void AddNumbers_InputTwoInt_GetCorrectAddition()
    {
        //Arrange
        Calculator calculator = new();

        //Act
        int result = calculator.AddNumbers(2, 5);

        //Assert
        Assert.AreEqual(7, result);
    }

    [TestMethod]
    public void AddNumbers_InputTwoInt_GetIncorrectAddition()
    {
        //Arrange
        Calculator calculator = new();

        //Act
        int result = calculator.AddNumbers(2, 5);

        //Assert
        Assert.AreNotEqual(5, result);
    }
}