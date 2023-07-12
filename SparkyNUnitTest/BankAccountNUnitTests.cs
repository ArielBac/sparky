using Moq;
using NUnit.Framework;

namespace Sparky;

[TestFixture]
public class BankAccountNUnitTests
{
    private BankAccount account;
    [SetUp]
    public void setup()
    {
       
    }

    //[Test]
    //public void BankDepositLogFakker_Add100_ReturnTrue()
    //{
    //    //Arrange
    //    BankAccount bankAccount = new(new LogFakker());

    //    //Act
    //    var result = bankAccount.Deposit(100);

    //    //Assert
    //    Assert.IsTrue(result);
    //    Assert.That(bankAccount.GetBalance, Is.EqualTo(100));
    //}

    [Test]
    public void BankDeposit_Add100_ReturnTrue()
    {
        //Arrange
        var logMock = new Mock<ILogBook>();
        logMock.Setup(x => x.Message(""));
        BankAccount bankAccount = new(logMock.Object);

        //Act
        var result = bankAccount.Deposit(100);

        //Assert
        Assert.IsTrue(result);
        Assert.That(bankAccount.GetBalance, Is.EqualTo(100));
    }

    [Test]
    [TestCase(200, 100)]
    [TestCase(200, 150)]
    public void BankWithdraw_Withdraw100With200Balance_ReturnsTrue(int balance, int withdraw)
    {
        //Arrange
        var logMock = new Mock<ILogBook>();
        logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
        logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x => x > 0))).Returns(true);
        BankAccount bankAccount = new(logMock.Object);
        bankAccount.Deposit(balance);

        //Act
        var result = bankAccount.Withdraw(withdraw);

        //Assert
        Assert.IsTrue(result);

    }

    [Test]
    [TestCase(200, 300)]
    public void BankWithdraw_Withdraw300With200Balance_ReturnsFalse(int balance, int withdraw)
    {
        //Arrange
        Mock<ILogBook> logMock = new();
        logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
        logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x => x > 0))).Returns(true);
        //logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x => x <= 0))).Returns(false);
        logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);
        BankAccount bankAccount = new BankAccount(logMock.Object);
        bankAccount.Deposit(balance);

        //Act
        bool result = bankAccount.Withdraw(withdraw);

        //Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void BackLogDummy_LogMockString_ReturnsTrue()
    {
        //Arrange
        Mock<ILogBook> logMock = new();
        string desiredOutPut = "hello";
        logMock.Setup(u => u.MessageWithReturnStr(It.IsAny<string>())).Returns((string str) => str.ToLower());

        //Act

        //Assert
        Assert.That(logMock.Object.MessageWithReturnStr("HELLo"), Is.EqualTo(desiredOutPut));
    }

    [Test]
    public void BackLogDummy_LogMockStringOutputStr_ReturnsTrue()
    {
        //Arrange
        Mock<ILogBook> logMock = new();
        string desiredOutPut = "hello";
        logMock.Setup(u => u.LogWithOutputResult(It.IsAny<string>(), out desiredOutPut)).Returns(true);

        //Act
        string result = "";

        //Assert
        Assert.IsTrue(logMock.Object.LogWithOutputResult("Ariel", out result));
        Assert.That(result, Is.EqualTo(desiredOutPut));
    }

    [Test]
    public void BackLogDummy_LogRefChecker_ReturnsTrue()
    {
        //Arrange
        Mock<ILogBook> logMock = new();
        Customer customer = new();
        Customer customerNotUsed = new();
        logMock.Setup(u => u.LogWithRwfObj(ref customer)).Returns(true);

        //Act
       
        //Assert
        Assert.IsTrue(logMock.Object.LogWithRwfObj(ref customer));
        Assert.IsFalse(logMock.Object.LogWithRwfObj(ref customerNotUsed));
    }

    [Test]
    public void BackLogDummy_SetAndGetLogTypeAndSeverityMock_MockTest()
    {
        //Arrange
        Mock<ILogBook> logMock = new();
        //logMock.SetupAllProperties();
        logMock.Setup(u => u.LogSeverity).Returns(10);
        logMock.Setup(u => u.LogType).Returns("Warning");

        //logMock.Object.LogSeverity = 100;

        //Act

        //Assert
        Assert.That(logMock.Object.LogSeverity, Is.EqualTo(10));
        Assert.That(logMock.Object.LogType, Is.EqualTo("Warning"));

        //callbacks
        string logTemp = "Hello, ";
        logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
            .Returns(true).Callback((string str) => logTemp += str);
        logMock.Object.LogToDb("Ariel");

        Assert.That(logTemp, Is.EqualTo("Hello, Ariel"));

        //callbacks
        int counter = 5;
        logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
            .Returns(true).Callback(() => counter++);
        logMock.Object.LogToDb("Ariel");

        Assert.That(counter, Is.EqualTo(6));
    }

    [Test]
    public void BankLogDummy_VerifyExample()
    {
        //Arrange
        Mock<ILogBook> logMock = new();
        BankAccount bankAccount = new(logMock.Object);
        
        //Act
        bankAccount.Deposit(100);

        //Assert
        //Verification
        logMock.Verify(u => u.Message(It.IsAny<string>()),Times.Exactly(2));
        logMock.Verify(u => u.Message("Test"),Times.AtLeastOnce);
        logMock.VerifySet(u => u.LogSeverity = 101, Times.Once);
        logMock.VerifyGet(u => u.LogSeverity, Times.Once);

    }
}
