namespace Sparky;

public class FiboXUnitTests
{
    private Fibo fibo;
	public FiboXUnitTests()
	{
		fibo = new();
	}

	[Fact]
    public void FiboChecker_Input1_ReturnsFiboSeries()
	{
		//Arrange
		fibo.Range = 1;
		List<int> expectedFiboSeries = new() { 0 };

		//Act
		List<int> result = fibo.GetFiboSeries();

		//Assert
		Assert.NotEmpty(result);
		Assert.Equal(expectedFiboSeries.OrderBy(u => u), result);
		Assert.True(result.SequenceEqual(expectedFiboSeries));
	}

	[Fact]
    public void FiboChecker_Input6_ReturnsFiboSeries()
	{
        //Arrange
        fibo.Range = 6;
        List<int> expectedFiboSeries = new() { 0, 1, 1, 2, 3, 5 };

        //Act
        List<int> result = fibo.GetFiboSeries();

		//Assert
		Assert.Contains(3, result);
		Assert.Equal(6, result.Count);
		Assert.DoesNotContain(4, result);
        Assert.Equal(expectedFiboSeries, result);
    }
}
