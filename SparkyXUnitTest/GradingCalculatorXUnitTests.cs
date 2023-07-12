namespace Sparky;

public class GradingCalculatorXUnitTests
{
	private GradingCalculator gradientCalculator;
	public GradingCalculatorXUnitTests()
	{
		gradientCalculator = new();
	}

	[Fact]
	public void GradeCalc_InputScore95AndAttendance90_GetAGrade()
	{
		//Arrange
		gradientCalculator.Score = 95;
		gradientCalculator.AttendancePercentage = 90;

		//Act
		string result = gradientCalculator.GetGrade();

		//Assert
		Assert.Equal("A", result);
	}

    [Fact]
    public void GradeCalc_InputScore85AndAttendance90_GetBGrade()
    {
        //Arrange
        gradientCalculator.Score = 85;
        gradientCalculator.AttendancePercentage = 90;

        //Act
        string result = gradientCalculator.GetGrade();

        //Assert
        Assert.Equal("B", result);
    }

    [Fact]
    public void GradeCalc_InputScore65AndAttendance90_GetCGrade()
    {
        //Arrange
        gradientCalculator.Score = 65;
        gradientCalculator.AttendancePercentage = 90;

        //Act
        string result = gradientCalculator.GetGrade();

        //Assert
        Assert.Equal("C", result);
    }

    [Fact]
    public void GradeCalc_InputScore95AndAttendance65_GetBGrade()
    {
        //Arrange
        gradientCalculator.Score = 95;
        gradientCalculator.AttendancePercentage = 65;

        //Act
        string result = gradientCalculator.GetGrade();

        //Assert
        Assert.Equal("B", result);
    }

    [Theory]
    [InlineData(95, 55)] //<-F
    [InlineData(65, 55)] //<-F
    [InlineData(50, 90)] //<-F
    public void GradeCalc_FailureScenarios_GetFGrade(int score, int attendancePercentage)
    {
        //Arrange
        gradientCalculator.Score = score;
        gradientCalculator.AttendancePercentage = attendancePercentage;

        //Act
        string result = gradientCalculator.GetGrade();

        //Assert
        Assert.Equal("F", result);
    }

    [Theory]
    [InlineData(95, 90, "A")] //<-F
    [InlineData(85, 90, "B")] //<-F
    [InlineData(65, 90, "C")] //<-F
    [InlineData(95, 65, "B")] //<-F
    [InlineData(95, 55, "F")] //<-F
    [InlineData(65, 55, "F")] //<-F
    [InlineData(50, 90, "F")] //<-F
    public void GradeCalc_AllGradeLogicalScenarios_GetOutPut(int score, int attendancePercentage, string expectedResult)
    {
        //Arrange
        gradientCalculator.Score = score;
        gradientCalculator.AttendancePercentage = attendancePercentage;

        //Act
        string result = gradientCalculator.GetGrade();

        //Assert
        Assert.Equal(expectedResult, result);
    }
}
