using NUnit.Framework;

namespace Sparky;

[TestFixture]
public class CalculatorNUnitTests
{
    [Test]
    public void AddNumbers_InputTwoInt_GetCorrectAddition()
    {
        //Arrange
        Calculator calculator = new();

        //Act
        int result = calculator.AddNumbers(2, 5);

        //Assert
        Assert.AreEqual(7, result);
    }

    [Test]
    public void AddNumbers_InputTwoInt_GetIncorrectAddition()
    {
        //Arrange
        Calculator calculator = new();

        //Act
        int result = calculator.AddNumbers(2, 5);

        //Assert
        Assert.AreNotEqual(5, result);
    }

    [Test]
    [TestCase(3)]
    [TestCase(7)]
    [TestCase(11)]
    public void IsOddChecker_InputOddNumber_ReturnTrue(int number)
    {
        //Arrange
        Calculator calculator = new();

        //Act
        bool result = calculator.IsOddNumber(number);

        //Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void IsOddChecker_InputEvenNumber_ReturnFalse()
    {
        //Arrange
        Calculator calculator = new();

        //Act
        bool result = calculator.IsOddNumber(2);

        //Assert
        Assert.IsFalse(result);
    }

    [Test]
    [TestCase(10, ExpectedResult = false)]
    [TestCase(11, ExpectedResult = true)]
    public bool IsOddChecker_InputNumber_ReturnTrueIfOdd(int number)
    {
        //Arrange
        Calculator calculator = new();

        //Act
        bool result = calculator.IsOddNumber(number);

        //Assert
        return result;
    }

    [Test]
    [TestCase(5.4, 10.5)] //15.9
    [TestCase(5.43, 10.53)] //15.96
    [TestCase(5.49, 10.59)] //16.08
    public void AddNumbersDouble_InputTwoDouble_GetCorrectAddition(double number1, double number2)
    {
        //Arrange
        Calculator calculator = new();

        //Act
        double result = calculator.AddNumbersDouble(number1,number2);

        //Assert
        Assert.AreEqual(15.9, result,.2); //Resultado pode estar entre 15.7 e 16.1
    }

    [Test]
    public void OddRanger_InputMinAndMaxRange_ReturnsValidOddNumberRange()
    {
        //Arrange
        Calculator calculator = new();
        List<int> expectedOddRange = new() { 5, 7, 9 }; // 5-10

        //Act
        List<int> result = calculator.GetOddRange(5, 10);

        //Assert
        Assert.That(result, Is.EquivalentTo(expectedOddRange));
        Assert.That(result, Does.Contain(7));
        Assert.That(result, Is.Not.Empty);
        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result, Has.No.Member(6));
        Assert.That(result, Is.Ordered);
        Assert.That(result, Is.Unique);
        //Assert.AreEqual(expectedOddRange, result);
        //Assert.Contains(7, result);
    }
}
