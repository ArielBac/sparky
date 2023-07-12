using NUnit.Framework;

namespace Sparky;

[TestFixture]
public class GradingCalculatorNUnitTests
{
    private GradingCalculator gradingCalculator;
    [SetUp]
    public void setup()
    {
        gradingCalculator = new GradingCalculator();
    }

    [Test]
    public void GradeCalc_InputScore95AndAttendance90_GetAGrade()
    {
        //Arrange
        gradingCalculator.Score = 95;
        gradingCalculator.AttendancePercentage = 90;
        
        //Act
        string result = gradingCalculator.GetGrade();

        //Assert
        Assert.That(result, Is.EqualTo("A"));
        Assert.AreEqual("A", result);
    }

    [Test]
    public void GradeCalc_InputScore85AndAttendance90_GetBGrade()
    {
        //Arrange
        gradingCalculator.Score = 85;
        gradingCalculator.AttendancePercentage = 90;

        //Act
        string result = gradingCalculator.GetGrade();

        //Assert
        Assert.That(result, Is.EqualTo("B"));
        Assert.AreEqual("B", result);
    }

    [Test]
    public void GradeCalc_InputScore65AndAttendance90_GetCGrade()
    {
        //Arrange
        gradingCalculator.Score = 65;
        gradingCalculator.AttendancePercentage = 90;

        //Act
        string result = gradingCalculator.GetGrade();

        //Assert
        Assert.That(result, Is.EqualTo("C"));
        Assert.AreEqual("C", result);
    }

    [Test]
    public void GradeCalc_InputScore95AndAttendance65_GetBGrade()
    {
        //Arrange
        gradingCalculator.Score = 95;
        gradingCalculator.AttendancePercentage = 65;

        //Act
        string result = gradingCalculator.GetGrade();

        //Assert
        Assert.That(result, Is.EqualTo("B"));
        Assert.AreEqual("B", result);
    }

    [Test]
    [TestCase(95, 55)] //<-F
    [TestCase(65, 55)] //<-F
    [TestCase(50, 90)] //<-F
    public void GradeCalc_FailureScenarios_GetFGrade(int score, int attendancePercentage)
    {
        //Arrange
        gradingCalculator.Score = score;
        gradingCalculator.AttendancePercentage = attendancePercentage;

        //Act
        string result = gradingCalculator.GetGrade();

        //Assert
        Assert.That(result, Is.EqualTo("F"));
        Assert.AreEqual("F", result);
    }

    [Test]
    [TestCase(95, 90, ExpectedResult = "A")] //<-F
    [TestCase(85, 90, ExpectedResult = "B")] //<-F
    [TestCase(65, 90, ExpectedResult = "C")] //<-F
    [TestCase(95, 65, ExpectedResult = "B")] //<-F
    [TestCase(95, 55, ExpectedResult = "F")] //<-F
    [TestCase(65, 55, ExpectedResult = "F")] //<-F
    [TestCase(50, 90, ExpectedResult = "F")] //<-F
    public string GradeCalc_AllGradeLogicalScenarios_GetOutPut(int score, int attendancePercentage)
    {
        //Arrange
        gradingCalculator.Score = score;
        gradingCalculator.AttendancePercentage = attendancePercentage;

        //Act
        return gradingCalculator.GetGrade();

        //Assert
    }
}
