using Moq;

namespace Sparky;

public class BankAccountXUnitTests
{
    private BankAccount account;

    public BankAccountXUnitTests()
    {
    }

    //[Fact]
    //public void BankDepositLogFakker_Add100_ReturnTrue()
    //{
    //    //Arrange
    //    BankAccount bankAccount = new(new LogFakker());

    //    //Act
    //    var result = bankAccount.Deposit(100);

    //    //Assert
    //    Assert.True(result);
    //    Assert.Equal(100, bankAccount.GetBalance());
    //}

    [Fact]
    public void BankDeposit_Add100_ReturnTrue()
    {
        //Arrange
        Mock<ILogBook> logMock = new();
        logMock.Setup(x => x.Message(""));
        account = new(logMock.Object);

        //Act
        bool result = account.Deposit(100);

        //Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(200, 100)]
    [InlineData(200, 150)]
    public void BankWithdraw_Withdraw100With200Balance_ReturnsTrue(int balance, int withdraw)
    {
        //Arrange
        Mock<ILogBook> logMock = new();
        logMock.Setup(x => x.LogToDb(It.IsAny<string>())).Returns(true);
        logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<int>(x => x >= 0))).Returns(true);
        account = new(logMock.Object);
        account.Deposit(balance);

        //Act
        bool result = account.Withdraw(withdraw);

        //Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(200, 300)]
    public void BankWithdraw_Withdraw100With300Balance_ReturnsTrue(int balance, int withdraw)
    {
        //Arrange
        Mock<ILogBook> logMock = new();
        logMock.Setup(x => x.LogToDb(It.IsAny<string>())).Returns(true);
        logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<int>(x => x >= 0))).Returns(true);
        logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);
        account = new(logMock.Object);
        account.Deposit(balance);

        //Act
        bool result = account.Withdraw(withdraw);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void BackLogDummy_LogMockString_ReturnsTrue()
    {
        //Arrange
        Mock<ILogBook> logMock = new();
        string desiredOutPut = "hello";
        logMock.Setup(u => u.MessageWithReturnStr(It.IsAny<string>())).Returns((string str) => str.ToLower());

        //Act

        //Assert
        Assert.Equal(desiredOutPut, logMock.Object.MessageWithReturnStr("HELLo"));
    }

    [Fact]
    public void BackLogDummy_LogMockStringOutputStr_ReturnsTrue()
    {
        //Arrange
        Mock<ILogBook> logMock = new();
        string desiredOutPut = "hello";
        logMock.Setup(u => u.LogWithOutputResult(It.IsAny<string>(), out desiredOutPut)).Returns(true);

        //Act
        string result = "";

        //Assert
        Assert.True(logMock.Object.LogWithOutputResult("Ariel", out result));
        Assert.Equal(desiredOutPut, result);
    }

    [Fact]
    public void BackLogDummy_LogRefChecker_ReturnsTrue()
    {
        //Arrange
        Mock<ILogBook> logMock = new();
        Customer customer = new();
        Customer customerNotUsed = new();
        logMock.Setup(u => u.LogWithRwfObj(ref customer)).Returns(true);

        //Act

        //Assert
        Assert.True(logMock.Object.LogWithRwfObj(ref customer));
        Assert.False(logMock.Object.LogWithRwfObj(ref customerNotUsed));
    }

    [Fact]
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
        Assert.Equal(10, logMock.Object.LogSeverity);
        Assert.Equal("Warning", logMock.Object.LogType);

        //callbacks
        string logTemp = "Hello, ";
        logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
            .Returns(true).Callback((string str) => logTemp += str);
        logMock.Object.LogToDb("Ariel");

        Assert.Equal("Hello, Ariel", logTemp);

        //callbacks
        int counter = 5;
        logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
            .Returns(true).Callback(() => counter++);
        logMock.Object.LogToDb("Ariel");

        Assert.Equal(6 ,counter);
    }

    [Fact]
    public void BankLogDummy_VerifyExample()
    {
        //Arrange
        Mock<ILogBook> logMock = new();
        BankAccount bankAccount = new(logMock.Object);

        //Act
        bankAccount.Deposit(100);

        //Assert
        //Verification
        logMock.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(2));
        logMock.Verify(u => u.Message("Test"), Times.AtLeastOnce);
        logMock.VerifySet(u => u.LogSeverity = 101, Times.Once);
        logMock.VerifyGet(u => u.LogSeverity, Times.Once);
    }
}
