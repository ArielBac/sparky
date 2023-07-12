using NUnit.Framework;

namespace Sparky;

[TestFixture]
public class FiboNUnitTests
{
    private Fibo fibo;
    [SetUp]
    public void setup()
    {
        fibo = new Fibo();
    }

    [Test]
    public void FiboChecker_Input1_ReturnsFiboSeries()
    {
        //Arrange
        fibo.Range = 1;
        List<int> expectedFiboSeries = new() { 0 };

        //Act
        List<int> result = fibo.GetFiboSeries();

        //Assert
        Assert.That(result, Is.Not.Empty);
        Assert.That(result, Is.Ordered);
        Assert.That(result, Is.EquivalentTo(expectedFiboSeries));
    }

    [Test]
    public void FiboChecker_Input6_ReturnsFiboSeries()
    {
        //Arrange
        fibo.Range = 6;
        List<int> expectedFiboSeries = new() { 0, 1, 1, 2, 3, 5 };

        //Act
        List<int> result = fibo.GetFiboSeries();

        //Assert
        Assert.That(result, Does.Contain(3));
        Assert.That(result.Count, Is.EqualTo(6));
        Assert.That(result, Has.No.Member(4));
        Assert.That(result, Is.EquivalentTo(expectedFiboSeries));
    }
}
